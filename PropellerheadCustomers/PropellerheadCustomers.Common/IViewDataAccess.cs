// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropellerheadCustomers.Common
{
    public interface IViewDataAccess<T>
    {
        Task<IEnumerable<T>> GetAll(Func<T, bool> whereFunc = null);
    }
}