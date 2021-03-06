﻿// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PropellerheadCustomers.Common;
using PropellerheadCustomers.DataContracts;
using PropellerheadCustomers.DataContracts.Converters;

namespace PropellerheadCustomers.GuiService.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly IServiceDataAccessFactory _dataAccess;

        public CustomerController(IServiceDataAccessFactory dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task<ViewResult> Index(string sortOrder, string searchString)
        {
            this.ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            this.ViewData["NoteCountSortParm"] = sortOrder == "NoteCount" ? "NoteCount_desc" : "NoteCount";
            this.ViewData["CurrentFilter"] = searchString;

            var data = await this._dataAccess.GetCustomerViewDataAccess().GetAll();

            if (!string.IsNullOrEmpty(searchString)) data = data.Where(customer => customer.Name.InvariantIgnoreCaseContains(searchString));

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(obj => obj.Name);
                    break;
                case "NoteCount_desc":
                    data = data.OrderByDescending(obj => obj.NoteCount);
                    break;
                case "NoteCount":
                    data = data.OrderBy(obj => obj.NoteCount);
                    break;
                default:
                    data = data.OrderBy(obj => obj.Name);
                    break;
            }

            var filteredData = data.ToList();

            return this.View(filteredData);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return this.View(new CustomerCreateEditModel());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CustomerCreateEditModel newCustomer)
        {
            var customer = newCustomer.GetCustomer();
            customer.CreatedTimestamp = DateTimeOffset.UtcNow;

            var da = this._dataAccess.GetCustomerDataAccess();
            await da.Upsert(customer);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return this.NotFound();
            var da = this._dataAccess.GetCustomerDataAccess();
            var customer = await da.Get(id).ConfigureAwait(false);
            var editCustomer = customer.GetCustomerEditModel();

            return this.View(editCustomer);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] CustomerCreateEditModel editedCustomer)
        {
            var da = this._dataAccess.GetCustomerDataAccess();
            var customer = await da.Get(editedCustomer.Id).ConfigureAwait(false);
            customer.CopyNewData(editedCustomer);
            await da.Upsert(customer);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return this.NotFound();
            var da = this._dataAccess.GetCustomerDataAccess();
            var customer = await da.Get(id).ConfigureAwait(false);
            return this.View(customer);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var da = this._dataAccess.GetCustomerDataAccess();
            await da.Delete(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}