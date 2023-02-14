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
    public class OrmeDetailController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public OrmeDetailController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }


        // GET: api/<OrmeDetailController>
        [HttpGet]
        public IActionResult Get()
        {
            var ormeDetail = _repositoryManager.OrmeDetailRepository.FindAllOrmeDetail().ToList();
            var ormedetailDto = ormeDetail.Select(r => new OrmeDetailDto

            {


                omde_id = r.omde_id,
                orme_price = r.orme_price,
                orme_qty = r.orme_qty,
                orme_subtotal = r.orme_subtotal,
                orme_discount = r.orme_discount,
                omde_orme_id = r.omde_orme_id,
                omde_reme_id = r.omde_reme_id
            });


            return Ok(ormeDetail);
        }

        // GET api/<OrmeDetailController>/5
        [HttpGet("{id}", Name = "GetOrmeDetailID")]
        public IActionResult FindOrmeDetalId(int id)
        {
            var ormeDetail = _repositoryManager.OrmeDetailRepository.FindOrmeDetailById(id);
            if (ormeDetail == null)
            {
                _logger.LogError("Resto object sent from client is null");
                return BadRequest("Resto object is null");
            }

            var ormeDetailDto = new OrmeDetailDto
            {
                omde_id = ormeDetail.omde_id,
                orme_price = ormeDetail.orme_price,
                orme_qty = ormeDetail.orme_qty,
                orme_subtotal = ormeDetail.orme_subtotal,
                orme_discount = ormeDetail.orme_discount,
                omde_orme_id = ormeDetail.omde_orme_id,
                omde_reme_id = ormeDetail.omde_reme_id

            };
            return Ok(ormeDetailDto);
        }

        // POST api/<OrmeDetailController>
        [HttpPost]
        public IActionResult CreateOrmeDetail([FromBody] OrmeDetailDto ormeDetailDto)
        {
            //1. prevent regiondto from null
            if (ormeDetailDto == null)
            {
                _logger.LogError("Regiondto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }

            var ormeDetail = new OrmeDetail()
            {
                orme_price = ormeDetailDto.orme_price,
                orme_qty = ormeDetailDto.orme_qty,
                orme_subtotal = ormeDetailDto.orme_subtotal,
                orme_discount = ormeDetailDto.orme_discount,
                omde_orme_id = ormeDetailDto.omde_orme_id,
                omde_reme_id = ormeDetailDto.omde_reme_id
            };

            _repositoryManager.OrmeDetailRepository.Insert(ormeDetail);

            //forward 

            var res = _repositoryManager.OrmeDetailRepository.FindLastOrmeDetailId().ToList();
            return Ok(res);

        }

        // PUT api/<OrmeDetailController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateOrmeDetail(int id, [FromBody] OrmeDetailDto ormeDetailDto)
        {
            if (ormeDetailDto == null)
            {
                _logger.LogError("RegionDto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }
            var res = new OrmeDetail
            {
                omde_id = id,
                orme_price = ormeDetailDto.orme_price,
                orme_qty = ormeDetailDto.orme_qty,
                orme_subtotal = ormeDetailDto.orme_subtotal,
                orme_discount = ormeDetailDto.orme_discount,
                omde_orme_id = ormeDetailDto.omde_orme_id,
                omde_reme_id = ormeDetailDto.omde_reme_id

            };
            _repositoryManager.OrmeDetailRepository.Edit(res);
            return CreatedAtRoute("GetOrmeDetailID", new { id = ormeDetailDto.omde_id }, new OrmeDetailDto
            {

                omde_id = id,
                orme_price = res.orme_price,
                orme_qty = res.orme_qty,
                orme_subtotal = res.orme_subtotal,
                orme_discount = res.orme_discount,
                omde_orme_id = res.omde_orme_id,
                omde_reme_id = res.omde_reme_id

            });
        }

        // DELETE api/<OrmeDetailController>/5
        [HttpDelete("{id}")]
        public IActionResult RemoveOrmeDetail(int? id)
        {
            if (id == null)
            {
                _logger.LogError($"Resto ID {id} object sent from client is null");
                return BadRequest($"Resto ID {id} object is null");
            }

            // find id first
            var res = _repositoryManager.OrmeDetailRepository.FindOrmeDetailById(id.Value);

            if (res == null)
            {
                _logger.LogError($"Region with id {id} not found");
                return NotFound();
            }


            _repositoryManager.OrmeDetailRepository.Remove(res);
            return Ok("Data has been remove.");
        }
    }
}
