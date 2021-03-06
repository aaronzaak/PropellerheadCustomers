﻿// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.Common
{
    public interface IServiceDataAccessFactory
    {
        IViewDataAccess<CustomerListView> GetCustomerViewDataAccess();

        IEditDataAccess<Customer> GetCustomerDataAccess();
    }
}