using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IOrmeDetailRepository
    {
        IEnumerable<OrmeDetail> FindAllOrmeDetail();
        Task<IEnumerable<OrmeDetail>> FindAllOrmeDetailAsync();
        OrmeDetail FindOrmeDetailById(int id);
        void Insert(OrmeDetail ormeDetail);
        void Edit(OrmeDetail ormeDetail);
        void Remove(OrmeDetail ormeDetail);
        IEnumerable<OrmeDetail> FindLastOrmeDetailId();
    }
}
