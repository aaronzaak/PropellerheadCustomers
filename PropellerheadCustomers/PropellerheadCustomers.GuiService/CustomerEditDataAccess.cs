// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System.Fabric;
using System.Fabric.Query;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using PropellerheadCustomers.Common;
using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.GuiService
{
    public class CustomerEditDataAccess : IEditDataAccess<Customer>
    {
        private FabricClient _fabricClient;

        private HttpClient _httpClient;

        private StatelessServiceContext _serviceContext;

        public CustomerEditDataAccess(FabricClient fabricClient, HttpClient httpClient, StatelessServiceContext serviceContext)
        {
            this._fabricClient = fabricClient;
            this._httpClient = httpClient;
            this._serviceContext = serviceContext;
        }

        public async Task<Customer> Get(string id)
        {
            var partitions = await this._fabricClient.QueryManager.GetPartitionListAsync(SfProxyMagic.GetDataServiceName(this._serviceContext));

            foreach (Partition partition in partitions)
            {
                var proxyUrl = SfProxyMagic.GetServiceProxy(this._serviceContext, ((Int64RangePartitionInformation) partition.PartitionInformation).LowKey, id);

                using (var response = await this._httpClient.GetAsync(proxyUrl))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK) continue;

                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Customer>(result);
                }
            }

            return null;
        }

        public async Task<Customer> Upsert(Customer item)
        {
            var proxyUrl = SfProxyMagic.GetServiceProxy(this._serviceContext, item.Name);

            using (var response = await this._httpClient.PostAsync(proxyUrl, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var newCustomer = JsonConvert.DeserializeObject<Customer>(responseData);
                    return newCustomer;
                }
            }

            return null;
        }

        public async Task<bool> Delete(string id)
        {
            var partitions = await this._fabricClient.QueryManager.GetPartitionListAsync(SfProxyMagic.GetDataServiceName(this._serviceContext));

            foreach (Partition partition in partitions)
            {
                var proxyUrl = SfProxyMagic.GetServiceProxy(this._serviceContext, ((Int64RangePartitionInformation) partition.PartitionInformation).LowKey, id);

                using (var response = await this._httpClient.DeleteAsync(proxyUrl))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK) return true;
                }
            }

            return false;
        }
    }
}