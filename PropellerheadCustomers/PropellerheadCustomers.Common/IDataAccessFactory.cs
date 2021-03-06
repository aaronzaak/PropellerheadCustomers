﻿// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.Common
{
    public interface IDataAccessFactory
    {
        IDataAccess<Customer> GetCustomerDataAccess();
    }
}