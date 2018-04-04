// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;

using PropellerheadCustomers.Common;
using PropellerheadCustomers.DataContracts;

namespace PropellerheadCustomers.DataService.Data
{
    public class BaseDataAccess<T> : IDataAccess<T> where T : DataBase
    {
        protected readonly IReliableStateManager _stateManager;

        private readonly string _stateName;

        protected BaseDataAccess(IReliableStateManager stateManager, string stateName)
        {
            this._stateManager = stateManager;
            this._stateName = stateName;
        }

        public async Task<T> Upsert(T item)
        {
            if (item.IsNull())
                throw new EmptyDataException(nameof(T));
            var state = await this.GetState();
            T newValue;
            using (var tx = this._stateManager.CreateTransaction())
            {
                newValue = await state.AddOrUpdateAsync(tx, item.Id, item, (id, oldValue) => item);
                await tx.CommitAsync();
            }

            return newValue;
        }

        public async Task<bool> UpsertBatch(IEnumerable<T> items)
        {
            if (items.IsNull() || items.Any(obj => obj.IsNull() || string.IsNullOrWhiteSpace(obj.Id)))
                throw new EmptyDataException(nameof(T));

            var state = await this.GetState();
            using (var tx = this._stateManager.CreateTransaction())
            {
                foreach (var item in items)
                {
                    var newValue = await state.AddOrUpdateAsync(tx, item.Id, item, (id, oldValue) => item);
                    if (newValue.IsNull())
                    {
                        tx.Abort();
                        return false;
                    }
                }

                await tx.CommitAsync();
            }

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var state = await this.GetState();
            using (var tx = this._stateManager.CreateTransaction())
            {
                if (await state.ContainsKeyAsync(tx, id))
                {
                    await state.TryRemoveAsync(tx, id);
                    await tx.CommitAsync();
                    return true;
                }

                return false;
            }
        }

        public async Task<bool> Delete(Func<T, bool> predicate)
        {
            if (predicate.IsNull())
                throw new UnableToExecuteQueryException(nameof(predicate), nameof(this.Delete));

            var ct = new CancellationToken();
            var state = await this.GetState();
            using (var tx = this._stateManager.CreateTransaction())
            {
                var stateEnum = await state.CreateEnumerableAsync(tx);
                var customers = stateEnum.GetAsyncEnumerator();
                while (await customers.MoveNextAsync(ct))
                    if (predicate(customers.Current.Value))
                        await state.TryRemoveAsync(tx, customers.Current.Value.Id);

                await tx.CommitAsync();

                return true;
            }
        }

        public async Task<T> Get(string id)
        {
            var state = await this.GetState();
            using (var tx = this._stateManager.CreateTransaction())
            {
                var dataItem = await state.TryGetValueAsync(tx, id);
                if (dataItem.HasValue) return dataItem.Value;

                return null;
            }
        }

        public async Task<IEnumerable<T>> GetAll(Func<T, bool> predicate)
        {
            var ct = new CancellationToken();
            var state = await this.GetState();
            using (var tx = this._stateManager.CreateTransaction())
            {
                var stateEnum = await state.CreateEnumerableAsync(tx);
                var customers = stateEnum.GetAsyncEnumerator();
                var result = new List<T>();
                while (await customers.MoveNextAsync(ct))
                    if (predicate == null || predicate(customers.Current.Value))
                        result.Add(customers.Current.Value);

                return result;
            }
        }

        public async Task<int> Count(Func<T, bool> predicate = null)
        {
            var ct = new CancellationToken();
            var state = await this.GetState();
            using (var tx = this._stateManager.CreateTransaction())
            {
                var stateEnum = await state.CreateEnumerableAsync(tx);
                var customers = stateEnum.GetAsyncEnumerator();
                var result = 0;
                while (await customers.MoveNextAsync(ct))
                    if (predicate != null && predicate(customers.Current.Value))
                        result++;

                return result;
            }
        }

        protected async Task<IReliableDictionary2<string, T>> GetState()
        {
            return await this._stateManager.GetOrAddAsync<IReliableDictionary2<string, T>>(this._stateName);
        }
    }
}