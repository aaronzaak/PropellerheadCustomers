// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropellerheadCustomers.Common
{
    public interface IDataAccess<T>
    {
        Task<T> Upsert(T item);

        Task<bool> UpsertBatch(IEnumerable<T> item);

        Task<bool> Delete(string id);

        Task<bool> Delete(Func<T, bool> predicate = null);

        Task<T> Get(string id);

        Task<IEnumerable<T>> GetAll(Func<T, bool> predicate = null);

        Task<int> Count(Func<T, bool> predicate = null);
    }
}