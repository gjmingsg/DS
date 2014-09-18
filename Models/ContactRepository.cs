using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nepton.Models
{
    public class ContactRepository : IContactRepository
    {
        private readonly Entities db = new Entities();
        public IQueryable<NT_Contact> FindAll()
        {
            return db.NT_Contact;
        }

        public NT_Contact GetContactById(Guid Id)
        {
            return db.NT_Contact.Where(p=>p.ContactID == Id).FirstOrDefault();
        }

        public void AddContact(NT_Contact item)
        {
            item.ContactID = Guid.NewGuid();
            item.CreateTime = DateTime.Now;
            item.Status = "draft";
            db.AddToNT_Contact(item);
            db.SaveChanges();
        }

        public bool DeleteContact(Guid Id)
        {
            var item = db.NT_Contact.Where(p => p.ContactID == Id).FirstOrDefault();
            if (item != null)
            {
                db.DeleteObject(item);
                db.SaveChanges();
                return true;
            }
            else {
                return false;
            }
        }
    }
}