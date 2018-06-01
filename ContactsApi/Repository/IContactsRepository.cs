using System;
using ContactsApi.Models;
using System.Collections.Generic;

namespace ContactsApi.Repository {
    public interface IContactsRepository {
        void Add(Contact item);
        IEnumerable<Contact> GetAll();
        Contact Find(Guid id);
        void Remove(Guid id);
        void Update(Contact item);
    }
}