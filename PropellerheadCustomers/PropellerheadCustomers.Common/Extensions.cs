// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.Common
{
    public static class Extensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static void SetId(this DataBase item)
        {
            if (item.IsNull()) return;

            if (string.IsNullOrWhiteSpace(item.Id))
                item.Id = Guid.NewGuid().ToString("N");
        }

        public static void SetIds(this IEnumerable<DataBase> items)
        {
            if (items.IsNull()) return;

            foreach (var item in items) item.SetId();
        }

        public static bool InvariantIgnoreCaseContains(this string source, string toCheck)
        {
            return source?.IndexOf(toCheck, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}