using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nepton.Models
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly Entities db = new Entities();
        public IQueryable<NT_Config> FindAll()
        {
            return db.NT_Config;
        }

        public NT_Config GetConfigById(Guid Id)
        {
            return db.NT_Config.Where(p => p.ConfigID == Id).FirstOrDefault();
        }

        public void AddConfig(NT_Config item)
        {
            db.AddToNT_Config(item);
            db.SaveChanges();
        }

        public bool DeleteConfig(Guid Id)
        {
            var m = db.NT_Config.Where(p => p.ConfigID == Id).FirstOrDefault();
            if (m != null)
            {
                db.DeleteObject(m);
                db.SaveChanges();
                return true;
            }
            else {
                return false;
            }
            
        }




        public void SaveChange()
        {
            db.SaveChanges();
        }
    }
}