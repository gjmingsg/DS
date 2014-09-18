using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nepton.Models
{
    interface IContactRepository
    {
        IQueryable<NT_Contact> FindAll();

        NT_Contact GetContactById(Guid Id);

        void AddContact(NT_Contact item);

        bool DeleteContact(Guid Id);

    }
}
