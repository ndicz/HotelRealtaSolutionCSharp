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

            RempId = r.remp_id,
            RempThumbnailFilename = r.remp_thumbnail_filename,
            RempPhotoFilename = r.remp_photo_filename,
            remp_primary = r.remp_primary,
            remp_url = r.remp_url,  
            remp_reme_id = r.remp_reme_id
           
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
                RempId = photos.remp_id,
                RempThumbnailFilename = photos.remp_thumbnail_filename,
                RempPhotoFilename = photos.remp_photo_filename,
                remp_primary = photos.remp_primary,
                remp_url = photos.remp_url,
                remp_reme_id = photos.remp_reme_id

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
                remp_id = menuPhotosDto.RempId,
                remp_thumbnail_filename = menuPhotosDto.RempThumbnailFilename,
                remp_photo_filename = menuPhotosDto.RempPhotoFilename,
                remp_primary = menuPhotosDto.remp_primary,
                remp_url = menuPhotosDto.remp_url,
                remp_reme_id = menuPhotosDto.remp_reme_id
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
                remp_id = id,
                remp_thumbnail_filename = menuPhotosDto.RempThumbnailFilename,
                remp_photo_filename = menuPhotosDto.RempPhotoFilename,
                remp_primary = menuPhotosDto.remp_primary,
                remp_url = menuPhotosDto.remp_url,
                remp_reme_id = menuPhotosDto.remp_reme_id

            };
            _repositoryManager.MenuPhotosRepository.Edit(res);
            return CreatedAtRoute("GetMenuPhotosID", new { id = menuPhotosDto.RempId }, new MenuPhotosDto
            {

                RempId = id,
                RempThumbnailFilename = res.remp_thumbnail_filename,
                RempPhotoFilename = res.remp_photo_filename,
                remp_primary = res.remp_primary,
                remp_url = res.remp_url,
                remp_reme_id = res.remp_reme_id


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
