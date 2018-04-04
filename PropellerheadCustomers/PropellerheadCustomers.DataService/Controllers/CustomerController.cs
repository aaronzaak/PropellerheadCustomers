// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PropellerheadCustomers.Common;
using PropellerheadCustomers.DataContracts;
using PropellerheadCustomers.DataContracts.Converters;
using PropellerheadCustomers.DataService.Data;

namespace PropellerheadCustomers.DataService.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IDataAccessFactory _dataAccess;

        public CustomerController(IDataAccessFactory dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await this._dataAccess.GetCustomerDataAccess().GetAll();
            var customersListView = customers.GetCustomerListViewData();

            return Responses.GetOkJsonResult(customersListView);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await this._dataAccess.GetCustomerDataAccess().Get(id);
            if (customer == null)
                return new NotFoundResult();

            return Responses.GetOkJsonResult(customer);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            customer.SetId();
            customer.Notes.SetIds();

            var newCustomer = await this._dataAccess.GetCustomerDataAccess().Upsert(customer);
            if (newCustomer == null)
                return Responses.GetServerErrorJsonResult("Failed to add customer");

            return Responses.GetOkJsonResult(newCustomer);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!await this._dataAccess.GetCustomerDataAccess().Delete(id))
                return Responses.GetServerErrorJsonResult("Failed to remove customer");

            return new OkResult();
        }
    }
}