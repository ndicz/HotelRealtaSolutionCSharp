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

        public void InsertPhotoAndMenu(MenuPhotosGroupDto menuPhotosGroupDto, out int RemeId)
        {
            var restoMenus = new RestoMenus
            {

                RemeName = menuPhotosGroupDto.MenuPhotosCreateDto.RemeName,
                RemeDescription = menuPhotosGroupDto.MenuPhotosCreateDto.RemeDescription,
                RemePrice = menuPhotosGroupDto.MenuPhotosCreateDto.RemePrice,
                RemeStatus = menuPhotosGroupDto.MenuPhotosCreateDto.RemeStatus,
                RemeModifiedDate = menuPhotosGroupDto.MenuPhotosCreateDto.RemeModifiedDate,
                RemeType = menuPhotosGroupDto.MenuPhotosCreateDto.RemeType,

            };

            _repositoryManager.RestoMenusRepository.Insert(restoMenus);
            RemeId = _repositoryManager.RestoMenusRepository.GetIdSequence();
            var allPhotos = menuPhotosGroupDto.AllPhotos;

         foreach (var itemPhoto in allPhotos)
            {
                var fileName = _utilityService.UploadSingleFile(itemPhoto);
                var menuPhotos = new MenuPhotos
                {
                    RempPhotoFilename = fileName,
                    RempThumbnailFilename = fileName,
                    RempUrl = itemPhoto.FileName,
                    RempPrimary = true,
                    RempRemeId = RemeId
                };
                _repositoryManager.MenuPhotosRepository.Insert(menuPhotos);
            }
        }
    }
}