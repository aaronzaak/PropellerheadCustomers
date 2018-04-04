﻿// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace PropellerheadCustomers.DataContracts
{
    [DataContract]
    public class DataBase
    {
        [DataMember]
        public string Id { get; set; }
    }
}