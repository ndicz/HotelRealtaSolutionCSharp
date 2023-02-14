using Realta.Domain.Base;
using Realta.Domain.Repositories;
using Realta.Persistence.Repositories;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private AdoDbContext _adoContext;
        private IVendorRepository _vendorRepository;
        private IRestoMenusRepository _restoMenusRepository;
        private IOrmeDetailRepository _ormeDetailRepository;
        private IOrderMenusRepository _orderMenusRepository;

        public RepositoryManager()
        {
        }

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
        }

        public IVendorRepository VendorRepository
        {
            get
            {
                if (_vendorRepository == null)
                {
                    _vendorRepository = new VendorRepository(_adoContext);
                }
                return _vendorRepository;
            }
        }

        public IRestoMenusRepository RestoMenusRepository
        {
            get
            {
                if (_restoMenusRepository == null)
                {
                    _restoMenusRepository = new RestoMenusRepository(_adoContext);
                }

                return _restoMenusRepository;
            }
        }

        public IOrmeDetailRepository OrmeDetailRepository
        {

            get
            {
                if (_ormeDetailRepository == null)
                {
                    _ormeDetailRepository = new OrmeDetailRepository(_adoContext);
                }

                return _ormeDetailRepository;
            }
        }

        public IOrderMenusRepository OrderMenusRepository
        {

            get
            {
                if (_orderMenusRepository == null)
                {
                    _orderMenusRepository = new OrdermenusRepository(_adoContext);
                }

                return _orderMenusRepository;
            }
        }
    }
}
