// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Fabric;
using System.Linq;

namespace PropellerheadCustomers.GuiService
{
    public static class SfProxyMagic
    {
        public static string GetServiceProxy(StatelessServiceContext serviceContext, string itemName, string id = null)
        {
            var serviceName = GetDataServiceName(serviceContext);
            var proxyAddress = GetProxyAddress(serviceName);
            var partitionKey = GetPartitionKey(itemName);
            return GetProxyUrl(proxyAddress, partitionKey, id);
        }

        public static string GetServiceProxy(StatelessServiceContext serviceContext, long partitionKey, string id = null)
        {
            var serviceName = GetDataServiceName(serviceContext);
            var proxyAddress = GetProxyAddress(serviceName);
            return GetProxyUrl(proxyAddress, partitionKey, id);
        }

        public static Uri GetDataServiceName(ServiceContext context)
        {
            return new Uri($"{context.CodePackageActivationContext.ApplicationName}/PropellerheadCustomers.DataService");
        }

        private static long GetPartitionKey(string name)
        {
            return Char.ToUpper(name.First()) - 'A';
        }

        private static Uri GetProxyAddress(Uri serviceName)
        {
            return new Uri($"http://localhost:19081{serviceName.AbsolutePath}");
        }

        private static string GetProxyUrl(Uri proxyAddress, long partitionKey, string id)
        {
            string idPart = string.IsNullOrWhiteSpace(id) ? string.Empty : $"/{id}";
            var url = $"{proxyAddress}/api/customer{idPart}?PartitionKey={partitionKey}&PartitionKind=Int64Range";
            return url;
        }
    }
}