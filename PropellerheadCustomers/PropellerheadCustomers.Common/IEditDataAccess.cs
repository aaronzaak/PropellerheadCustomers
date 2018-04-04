// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace PropellerheadCustomers.Common
{
    public interface IEditDataAccess<T>
    {
        Task<T> Get(string id);

        Task<T> Upsert(T item);

        Task<bool> Delete(string id);
    }
}