using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuPhotosController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public MenuPhotosController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }


        // GET: api/<MenuPhotosController>
        [HttpGet]
        public IActionResult Get()
        {
            var menuPhotos = _repositoryManager.MenuPhotosRepository.FindAllMenuPhotos().ToList();
            var menuPhotosDto = menuPhotos.Select(r => new MenuPhotosDto
            {

            RempId = r.RempId,
            RempThumbnailFilename = r.RempThumbnailFilename,
            RempPhotoFilename = r.RempPhotoFilename,
            RempPrimary = r.RempPrimary,
            RempUrl = r.RempUrl,  
            RempRemeId = r.RempRemeId
           
            });


            return Ok(menuPhotos);
        }

        // GET api/<MenuPhotosController>/5
        [HttpGet("{id}", Name = "GetMenuPhotosID")]
        public IActionResult FindMenuPhotosId(int id)
        {
            var photos = _repositoryManager.MenuPhotosRepository.FindMenuPhotosById(id);
            if (photos == null)
            {
                _logger.LogError("Resto object sent from client is null");
                return BadRequest("Resto object is null");
            }
            var menuPhotsDto = new MenuPhotosDto
            {
                RempId = photos.RempId,
                RempThumbnailFilename = photos.RempThumbnailFilename,
                RempPhotoFilename = photos.RempPhotoFilename,
                RempPrimary = photos.RempPrimary,
                RempUrl = photos.RempUrl,
                RempRemeId = photos.RempRemeId

            };
            return Ok(menuPhotsDto);
        }

        // POST api/<MenuPhotosController>
        [HttpPost]
        public IActionResult CreateMenuPhotos([FromBody] MenuPhotosDto menuPhotosDto)
        {
            if (menuPhotosDto == null)
            {
                _logger.LogError("Regiondto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }
            var menuPhotos = new MenuPhotos()
            {
                RempId = menuPhotosDto.RempId,
                RempThumbnailFilename = menuPhotosDto.RempThumbnailFilename,
                RempPhotoFilename = menuPhotosDto.RempPhotoFilename,
                RempPrimary = menuPhotosDto.RempPrimary,
                RempUrl = menuPhotosDto.RempUrl,
                RempRemeId = menuPhotosDto.RempRemeId
            };

            _repositoryManager.MenuPhotosRepository.Insert(menuPhotos);

            //forward 

            var res = _repositoryManager.MenuPhotosRepository.FindLastMenuPhotosId().ToList();
            return Ok(res);

        }

        // PUT api/<MenuPhotosController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateMenuPhotos(int id, [FromBody] MenuPhotosDto menuPhotosDto)
        {
            if (menuPhotosDto == null)
            {
                _logger.LogError("RegionDto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }

            var res = new MenuPhotos
            {
                RempId = id,
                RempThumbnailFilename = menuPhotosDto.RempThumbnailFilename,
                RempPhotoFilename = menuPhotosDto.RempPhotoFilename,
                RempPrimary = menuPhotosDto.RempPrimary,
                RempUrl = menuPhotosDto.RempUrl,
                RempRemeId = menuPhotosDto.RempRemeId

            };
            _repositoryManager.MenuPhotosRepository.Edit(res);
            return CreatedAtRoute("GetMenuPhotosID", new { id = menuPhotosDto.RempId }, new MenuPhotosDto
            {

                RempId = id,
                RempThumbnailFilename = res.RempThumbnailFilename,
                RempPhotoFilename = res.RempPhotoFilename,
                RempPrimary = res.RempPrimary,
                RempUrl = res.RempUrl,
                RempRemeId = res.RempRemeId


            });
        }

        // DELETE api/<MenuPhotosController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMenuPhotos(int? id)
        {
            if (id == null)
            {
                _logger.LogError($"Resto ID {id} object sent from client is null");
                return BadRequest($"Resto ID {id} object is null");
            }

            // find id first
            var res = _repositoryManager.MenuPhotosRepository.FindMenuPhotosById(id.Value);

            if (res == null)
            {
                _logger.LogError($"Region with id {id} not found");
                return NotFound();
            }


            _repositoryManager.MenuPhotosRepository.Remove(res);
            return Ok("Data has been remove.");
        }
    }
}
