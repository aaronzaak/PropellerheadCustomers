// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.ServiceFabric.Data;

using PropellerheadCustomers.Common;
using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.DataService.Data
{
    public class DataServiceFactory : IDataAccessFactory
    {
        private IReliableStateManager _stateManager;

        public DataServiceFactory(IReliableStateManager stateManager)
        {
            this._stateManager = stateManager;
        }

        public IDataAccess<Customer> GetCustomerDataAccess()
        {
            return new CustomerDataAccess(this._stateManager);
        }
    }
}