using ContactsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactsApi.Repository {
    public class ContactsRespoitory : IContactsRepository
    {
        private static List<Contact> _contacts = new List<Contact>();
        public void Add(Contact item)
        {
            item.Id = Guid.NewGuid();
            _contacts.Add(item);
        }

        public Contact Find(Guid id)
        {
            return GetByKey(id);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contacts;
        }

        public void Remove(Guid id)
        {
            var remove = GetByKey(id);
            if(remove != null)
                _contacts.Remove(remove);
        }

        public void Update(Contact item)
        {
            var id = item.Id.GetValueOrDefault();
            if(id == Guid.Empty)
                return;

            var updateItem = GetByKey(id);
            if(updateItem == null)
                return;
            
            updateItem.FirstName = item.FirstName;
            updateItem.LastName = item.LastName;
            updateItem.IsFamilyMember = item.IsFamilyMember;
            updateItem.Company = item.Company;
            updateItem.Email = item.Email;
            updateItem.JobTitle = item.JobTitle;
            updateItem.MobilePhone = item.MobilePhone;
            updateItem.DateOfBirth = item.DateOfBirth;
            updateItem.AnniversaryDate = item.AnniversaryDate;
        }

        private Contact GetByKey(Guid key)
        {
            return _contacts.SingleOrDefault(c => c.Id == key);
        }
    }
}