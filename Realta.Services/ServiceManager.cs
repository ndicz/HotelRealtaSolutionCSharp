using Realta.Domain.Base;
using Realta.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUtilityService> _lazyUtilityService;
        private readonly Lazy<IMenuPhotosServices> _lazyMenuPhotosServices;

        public ServiceManager(IRepositoryManager repositoryManager, IUtilityService _lazyUtilityService)
        {

            _lazyMenuPhotosServices = new Lazy<IMenuPhotosServices>(() => new MenuPhotosServices(repositoryManager, _lazyUtilityService));
            
        }
        public IMenuPhotosServices MenuPhotosServices => _lazyMenuPhotosServices.Value;
    }
}
