// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System.Fabric;
using System.Net.Http;

using PropellerheadCustomers.Common;
using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.GuiService
{
    public class ServiceDataAccessFactory : IServiceDataAccessFactory
    {
        private readonly FabricClient _fabricClient;

        private readonly StatelessServiceContext _serviceContext;

        public ServiceDataAccessFactory(FabricClient fabricClient, StatelessServiceContext serviceContext)
        {
            this._fabricClient = fabricClient;
            this._serviceContext = serviceContext;
        }

        public IViewDataAccess<CustomerListView> GetCustomerViewDataAccess()
        {
            return new CustomerViewDataAccess(this._fabricClient, new HttpClient(), this._serviceContext);
        }

        public IEditDataAccess<Customer> GetCustomerDataAccess()
        {
            return new CustomerEditDataAccess(this._fabricClient, new HttpClient(), this._serviceContext);
        }
    }
}