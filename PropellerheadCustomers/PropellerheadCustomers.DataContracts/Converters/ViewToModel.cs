// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace PropellerheadCustomers.DataContracts.Converters
{
    public static class ViewToModel
    {
        public static CustomerCreateEditModel GetCustomerEditModel(this Customer customer)
        {
            var customerEdit = new CustomerCreateEditModel
            {
                Id = customer.Id,
                ContactInfo = customer.ContactInfo,
                Status = customer.Status,
                Name = customer.Name,
                Notes = customer.Notes != null ? string.Join(Environment.NewLine, customer.Notes.Select(note => note.Text)) : string.Empty
            };
            return customerEdit;
        }

        public static Customer GetCustomer(this CustomerCreateEditModel customerEdit)
        {
            var customer = new Customer
            {
                Id = customerEdit.Id,
                ContactInfo = customerEdit.ContactInfo,
                Name = customerEdit.Name,
                Status = customerEdit.Status,
                Notes = customerEdit.Notes?.Split(Environment.NewLine).Where(text => !string.IsNullOrWhiteSpace(text)).Select(row => new Note {Text = row}).ToList()
            };
            return customer;
        }

        public static void CopyNewData(this Customer customerOld, CustomerCreateEditModel customerNew)
        {
            customerOld.ContactInfo = customerNew.ContactInfo;
            customerOld.Name = customerNew.Name;
            customerOld.Status = customerNew.Status;
            customerOld.Notes = customerNew.Notes.Split(Environment.NewLine).Where(text => !string.IsNullOrWhiteSpace(text)).Select(row => new Note {Text = row}).ToList();
        }
    }
}