using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services
{
    internal class MenuPhotosServices : IMenuPhotosServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUtilityService _utilityService;

        public MenuPhotosServices(IRepositoryManager repositoryManager, IUtilityService utilityService)
        {
            _repositoryManager = repositoryManager;
            _utilityService = utilityService;
        }

        public void InsertPhotoAndMenu(MenuPhotosGroupDto menuPhotosGroupDto, out int reme_id)
        {
            var restoMenus = new RestoMenus{
            
            RemeName = menuPhotosGroupDto.MenuPhotosCreateDto.RemeName,
            
            };

            _repositoryManager.RestoMenusRepository.Insert(restoMenus);
            reme_id = _repositoryManager.RestoMenusRepository.GetIdSequence();
        }
    }
}