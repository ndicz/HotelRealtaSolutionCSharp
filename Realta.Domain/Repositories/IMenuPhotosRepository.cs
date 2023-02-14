using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IMenuPhotosRepository
    {
        IEnumerable<MenuPhotos> FindAllMenuPhotos();
        Task<IEnumerable<MenuPhotos>> FindAllMenuPhotosAsync();
        MenuPhotos FindMenuPhotosById(int id);
        void Insert(MenuPhotos menuPhotos);
        void Edit(MenuPhotos menuPhotos);
        void Remove(MenuPhotos menuPhotos);
        IEnumerable<MenuPhotos> FindLastMenuPhotosId();
    }
}
