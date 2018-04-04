// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Moq;

using PropellerheadCustomers.Common;
using PropellerheadCustomers.DataContracts;
using PropellerheadCustomers.DataService.Controllers;
using PropellerheadCustomers.Tests;

using Xunit;

namespace PropellerheadCustomers.UnitTests
{
    public class TestWithMocks
    {
        [Fact]
        public async Task DataControllerTest()
        {
            var testCustomerList = new List<Customer> {TestUtils.GetNewRandomCustomer(), TestUtils.GetNewRandomCustomer()};
            var mr = new MockRepository(MockBehavior.Strict);
            var dataAccessFactoryMock = mr.Create<IDataAccessFactory>();
            var dataAccessCustomer = mr.Create<IDataAccess<Customer>>();
            dataAccessFactoryMock.Setup(obj => obj.GetCustomerDataAccess()).Returns(dataAccessCustomer.Object);
            dataAccessCustomer.Setup(obj => obj.GetAll(It.IsAny<Func<Customer, bool>>())).ReturnsAsync(testCustomerList);

            var controller = new CustomerController(dataAccessFactoryMock.Object);

            var result = await controller.GetAll();
            var okResult = result as JsonResult;

            Assert.NotNull(okResult);
            Assert.Equal(okResult.StatusCode, (int) HttpStatusCode.OK);

            var content = okResult.Value as IEnumerable<CustomerListView>;
            Assert.NotNull(content);
            Assert.Equal(testCustomerList.Count, content.Count());
            foreach (var customer in testCustomerList)
            {
                var customerView = content.SingleOrDefault(obj => obj.Id == customer.Id);
                Assert.NotNull(customerView);
                Assert.Equal(customer.Id, customerView.Id);
                Assert.Equal(customer.Name, customerView.Name);
                Assert.Equal(customer.Notes.Count(), customerView.NoteCount);
                Assert.Equal(customer.Status, customerView.Status);
            }
        }
    }
}