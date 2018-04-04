// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Query;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using PropellerheadCustomers.Common;
using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.GuiService
{
    public class CustomerViewDataAccess : IViewDataAccess<CustomerListView>
    {
        private readonly FabricClient _fabricClient;

        private readonly HttpClient _httpClient;

        private readonly StatelessServiceContext _serviceContext;

        public CustomerViewDataAccess(FabricClient fabricClient, HttpClient httpClient, StatelessServiceContext serviceContext)
        {
            this._fabricClient = fabricClient;
            this._httpClient = httpClient;
            this._serviceContext = serviceContext;
        }

        public async Task<IEnumerable<CustomerListView>> GetAll(Func<CustomerListView, bool> whereFunc = null)
        {
            var partitions = await this._fabricClient.QueryManager.GetPartitionListAsync(SfProxyMagic.GetDataServiceName(this._serviceContext));
            var result = new List<CustomerListView>();

            foreach (Partition partition in partitions)
            {
                var proxyUrl = SfProxyMagic.GetServiceProxy(this._serviceContext, ((Int64RangePartitionInformation) partition.PartitionInformation).LowKey);

                using (var response = await this._httpClient.GetAsync(proxyUrl))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK) continue;

                    result.AddRange(JsonConvert.DeserializeObject<List<CustomerListView>>(await response.Content.ReadAsStringAsync()));
                }
            }

            return result;
        }
    }
}