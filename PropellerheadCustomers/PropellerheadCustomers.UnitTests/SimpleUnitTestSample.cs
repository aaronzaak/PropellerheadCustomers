// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using PropellerheadCustomers.DataContracts;
using PropellerheadCustomers.DataContracts.Converters;
using PropellerheadCustomers.Tests;

using Xunit;

namespace PropellerheadCustomers.UnitTests
{
    public class SimpleUnitTestSample
    {
        [Fact]
        public void ViewToModelTest()
        {
            var customer = TestUtils.GetNewRandomCustomer();

            var customerEditModel = customer.GetCustomerEditModel();

            Assert.Equal(customer.Name, customerEditModel.Name);
            Assert.Equal(customer.ContactInfo, customerEditModel.ContactInfo);
            Assert.Equal(customer.Status, customerEditModel.Status);
            foreach (var note in customer.Notes) Assert.Contains(note.Text, customerEditModel.Notes);
        }
    }
}