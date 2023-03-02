using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestoMenusController : ControllerBase
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public RestoMenusController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }



        // GET: api/<RestoMenusController>
        [HttpGet]
        public IActionResult Get()
        {
            var restoMenus = _repositoryManager.RestoMenusRepository.FindAllRestoMenus().ToList();
            var restoMenusDto = restoMenus.Select(r => new RestoMenusDto
            {


                RemeId = r.RemeId,
                RemeFaciId = r.RemeFaciId,
                RemeName = r.RemeName,
                RemeDescription = r.RemeDescription,
                RemePrice = r.RemePrice,
                RemeStatus = r.RemeStatus,
                RemeModifiedDate = r.RemeModifiedDate,
                RemeType = r.RemeType
            });


            return Ok(restoMenus);

        }

        // GET api/<RestoMenusController>/5
        [HttpGet("{id}", Name = "GetRestoMenusID")]
        public IActionResult FindRestoMenusId(int id)
        {
            var resto = _repositoryManager.RestoMenusRepository.FindRestoById(id);
            if (resto == null)
            {
                _logger.LogError("Resto object sent from client is null");
                return BadRequest("Resto object is null");
            }

            var restoMenusDto = new RestoMenusDto
            {
                RemeId = resto.RemeId,
                RemeFaciId = resto.RemeFaciId,
                RemeName = resto.RemeName,
                RemeDescription = resto.RemeDescription,
                RemePrice = resto.RemePrice,
                RemeStatus = resto.RemeStatus,
                RemeModifiedDate = resto.RemeModifiedDate,
                RemeType = resto.RemeType

            };
            return Ok(restoMenusDto);
        }

        // POST api/<RestoMenusController> //
        [HttpPost]
        public IActionResult CreateRestoMenus([FromBody] RestoMenusDto restoMenusDto)
        {
            //1. prevent regiondto from null
            if (restoMenusDto == null)
            {
                _logger.LogError("Regiondto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }

            var restoMenus = new RestoMenus()
            {
                RemeFaciId = 2,
                RemeName = restoMenusDto.RemeName,
                RemeDescription = restoMenusDto.RemeDescription,
                RemePrice = restoMenusDto.RemePrice,
                RemeStatus = restoMenusDto.RemeStatus,
                RemeModifiedDate = restoMenusDto.RemeModifiedDate,
                RemeType = restoMenusDto.RemeType
            };

            _repositoryManager.RestoMenusRepository.Insert(restoMenus);

            //forward 

            var res = _repositoryManager.RestoMenusRepository.FindLastMenusId().ToList();
            return Ok (res);

        }

        // PUT api/<RestoMenusController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateRestoMenus(int id, [FromBody] RestoMenusDto restoMenusDto)
        {

            if (restoMenusDto == null)
            {
                _logger.LogError("RegionDto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }
            var res = new RestoMenus
            {
                RemeId = id,
                RemeName = restoMenusDto.RemeName,
                RemeDescription = restoMenusDto.RemeDescription,
                RemePrice = restoMenusDto.RemePrice,
                RemeStatus = restoMenusDto.RemeStatus,
                RemeModifiedDate = restoMenusDto.RemeModifiedDate

            };
            _repositoryManager.RestoMenusRepository.Edit(res);
            return CreatedAtRoute("GetRestoMenusID", new { id = restoMenusDto.RemeId }, new RestoMenusDto
            {

                RemeId = id,
                RemeName = res.RemeName,
                RemeDescription = res.RemeDescription,
                RemePrice = res.RemePrice,
                RemeStatus = res.RemeStatus,
                RemeModifiedDate = res.RemeModifiedDate


            });

        }


        // DELETE api/<RestoMenusController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRestoMenus(int? id)
        {
            if (id == null)
            {
                _logger.LogError($"Resto ID {id} object sent from client is null");
                return BadRequest($"Resto ID {id} object is null");
            }

            // find id first
            var res = _repositoryManager.RestoMenusRepository.FindRestoById(id.Value);

            if (res == null)
            {
                _logger.LogError($"Region with id {id} not found");
                return NotFound();
            }


            _repositoryManager.RestoMenusRepository.Remove(res);
            return Ok("Data has been remove.");

        }
    }
}
