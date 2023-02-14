using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IOrderMenusRepository
    {
        IEnumerable<OrderMenus> FindAllOrderMenus();
        Task<IEnumerable<OrderMenus>> FindAllOrderMenusAsync();
        OrderMenus FindOrderMenusById(int id);
        void Insert(OrderMenus orderMenus);
        void Edit(OrderMenus orderMenus);
        void Remove(OrderMenus orderMenus);
        IEnumerable<OrderMenus> FindLastOrderMenusId();
    }
}
