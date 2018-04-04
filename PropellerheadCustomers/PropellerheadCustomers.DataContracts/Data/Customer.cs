// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PropellerheadCustomers.DataContracts
{
    [DataContract]
    public class Customer: DataBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ContactInfo { get; set; }

        [DataMember]
        public CustomerStatus Status { get; set; }

        [DataMember]
        public DateTimeOffset CreatedTimestamp { get; set; }

        [DataMember]
        public IEnumerable<Note> Notes { get; set; }
    }
}