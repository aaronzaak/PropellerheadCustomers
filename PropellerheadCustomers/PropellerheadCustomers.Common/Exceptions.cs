// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace PropellerheadCustomers.Common
{
    public class EmptyDataException : Exception
    {
        public EmptyDataException(string className) : base(message: $"{className} passed as null to DataLayer")
        {
        }
    }

    public class UnableToExecuteQueryException : Exception
    {
        public UnableToExecuteQueryException(string missingValues, string method) : base(message: $"{missingValues} passed as null to {method} in DataLayer")
        {
        }
    }
}