using Microsoft.AspNetCore.Mvc;
using Realta.Contract;
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


                reme_id = r.reme_id,
                reme_faci_id = r.reme_faci_id,
                reme_name = r.reme_name,
                reme_description = r.reme_description,
                reme_price = r.reme_price,
                reme_status = r.reme_status,
                reme_modified_date = r.reme_modified_date
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
                reme_id = resto.reme_id,
                reme_faci_id = resto.reme_faci_id,
                reme_name = resto.reme_name,
                reme_description = resto.reme_description,
                reme_price = resto.reme_price,
                reme_status = resto.reme_status,
                reme_modified_date = resto.reme_modified_date

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
                reme_faci_id = 2,
                reme_name = restoMenusDto.reme_name,
                reme_description = restoMenusDto.reme_description,
                reme_price = restoMenusDto.reme_price,
                reme_status = restoMenusDto.reme_status,
                reme_modified_date = restoMenusDto.reme_modified_date
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
                reme_id = id,
                reme_name = restoMenusDto.reme_name,
                reme_description = restoMenusDto.reme_description,
                reme_price = restoMenusDto.reme_price,
                reme_status = restoMenusDto.reme_status,
                reme_modified_date = restoMenusDto.reme_modified_date

            };
            _repositoryManager.RestoMenusRepository.Edit(res);
            return CreatedAtRoute("GetRestoMenusID", new { id = restoMenusDto.reme_id }, new RestoMenusDto
            {

                reme_id = id,
                reme_name = res.reme_name,
                reme_description = res.reme_description,
                reme_price = res.reme_price,
                reme_status = res.reme_status,
                reme_modified_date = res.reme_modified_date


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
