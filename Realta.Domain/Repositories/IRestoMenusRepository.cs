using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IRestoMenusRepository
    {
        IEnumerable<RestoMenus> FindAllRestoMenus();
        Task<IEnumerable<RestoMenus>> FindAllRestoAsync();
        RestoMenus FindRestoById(int id);
        void Insert(RestoMenus restoMenus);
        void Edit(RestoMenus restoMenus);   
        void Remove(RestoMenus restoMenus);
        IEnumerable<RestoMenus> FindLastMenusId();
    }
}
