// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

namespace PropellerheadCustomers.DataContracts
{
    public class CustomerCreateEditModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ContactInfo { get; set; }

        public CustomerStatus Status { get; set; }

        public string Notes { get; set; }
    }
}