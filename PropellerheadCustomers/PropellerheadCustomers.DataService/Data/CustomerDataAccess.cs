// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.ServiceFabric.Data;

using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.DataService.Data
{
    public class CustomerDataAccess : BaseDataAccess<Customer>
    {
        public CustomerDataAccess(IReliableStateManager stateManager) : base(stateManager, "CustomerDataDictionary")
        {
        }
    }
}