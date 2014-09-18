using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nepton.Models
{
    public interface IConfigRepository
    {
        IQueryable<NT_Config> FindAll();

        NT_Config GetConfigById(Guid Id);

        void AddConfig(NT_Config item);

        bool DeleteConfig(Guid Id);

        void SaveChange();
    }
}
