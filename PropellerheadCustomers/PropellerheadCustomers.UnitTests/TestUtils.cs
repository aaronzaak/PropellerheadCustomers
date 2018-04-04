// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.Tests
{
    public static class TestUtils
    {
        public static string RandomText()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static Customer GetNewRandomCustomer()
        {
            return new Customer
            {
                Name = RandomText(),
                ContactInfo = RandomText(),
                Id = RandomText(),
                Notes = new List<Note>
                {
                    new Note {Text = RandomText(), Id = RandomText()},
                    new Note {Text = RandomText(), Id = RandomText()}
                }
            };
        }
    }
}