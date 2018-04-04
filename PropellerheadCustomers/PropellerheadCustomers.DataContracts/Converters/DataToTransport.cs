// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace PropellerheadCustomers.DataContracts.Converters
{
    public static class DataToTransport
    {
        public static IEnumerable<CustomerListView> GetCustomerListViewData(this IEnumerable<Customer> customers)
        {
            return customers.Select(customer => new CustomerListView
            {
                Id = customer.Id,
                Name = customer.Name,
                CreatedTimestamp = customer.CreatedTimestamp,
                Status = customer.Status,
                NoteCount = customer.Notes?.Count() ?? 0
            }).ToList();
        }
    }
}