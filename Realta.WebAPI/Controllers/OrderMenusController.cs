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
    public class OrderMenusController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public OrderMenusController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }


        // GET: api/<OrderMenusController>
        [HttpGet]
        public IActionResult  Get()
        {
            var orderMenus = _repositoryManager.OrderMenusRepository.FindAllOrderMenus().ToList();
            var orderMenusDto = orderMenus.Select(r => new OrderMenusDto
            {
            OrmeId = r.OrmeId,
            OrmeOrderNumber   = r.OrmeOrderNumber,
            OrmeOrderDate     = r.OrmeOrderDate,
            OrmeTotalItem     = r.OrmeTotalItem,
            OrmeTotalDiscount = r.OrmeTotalDiscount,
            OrmeTotalAmount   = r.OrmeTotalAmount,
            OrmePayType       = r.OrmePayType,
            OrmeCardnumber     = r.ormeCardnumber,
            OrmeIsPaid        = r.OrmeIsPaid,
            OrmeModifiedDate  = r.OrmeModifiedDate,
            OrmeUserId        = r.OrmeUserId
            });


            return Ok(orderMenus);
        }

        //GET NESTED

        [HttpGet("js/{id}", Name = "GetOrderMenusNestJson")]
        public IActionResult FindOrderJson(int id)
        {
            var orderMenus = _repositoryManager.OrderMenusRepository.GetOrmeNestedMenuDetail(id);
            return Ok(orderMenus);
        }



        // GET api/<OrderMenusController>/5
        [HttpGet("{id}", Name = "GetOrderMenusID")]
        public IActionResult FIndOrderMenusId(int id)
        {
            var orderMenus = _repositoryManager.OrderMenusRepository.FindOrderMenusById(id);
            if (orderMenus == null)
            {
                _logger.LogError("Resto object sent from client is null");
                return BadRequest("Resto object is null");
            }

            var resOrderMenus = new OrderMenusDto
            {
                OrmeId = orderMenus.OrmeId,
                OrmeOrderNumber = orderMenus.OrmeOrderNumber,
                OrmeOrderDate = orderMenus.OrmeOrderDate,
                OrmeTotalItem = orderMenus.OrmeTotalItem,
                OrmeTotalDiscount = orderMenus.OrmeTotalDiscount,
                OrmeTotalAmount = orderMenus.OrmeTotalAmount,
                OrmePayType = orderMenus.OrmePayType,
                OrmeCardnumber = orderMenus.ormeCardnumber,
                OrmeIsPaid = orderMenus.OrmeIsPaid,
                OrmeModifiedDate = orderMenus.OrmeModifiedDate,
                OrmeUserId = orderMenus.OrmeUserId
            };


            return Ok(resOrderMenus);
        }

        // POST api/<OrderMenusController>
        [HttpPost]
        public IActionResult CreateOrderMenus([FromBody] OrderMenusDto orderMenusDto)
        {
            if (orderMenusDto == null)
            {
                _logger.LogError("Regiondto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }

            var orderMenus = new OrderMenus()
            {
                OrmeOrderDate = orderMenusDto.OrmeOrderDate,
                OrmeTotalItem = orderMenusDto.OrmeTotalItem,
                OrmeTotalDiscount = orderMenusDto.OrmeTotalDiscount,
                OrmeTotalAmount = orderMenusDto.OrmeTotalAmount,
                OrmePayType = orderMenusDto.OrmePayType,
                ormeCardnumber = orderMenusDto.OrmeCardnumber,
                OrmeIsPaid = orderMenusDto.OrmeIsPaid,
                OrmeModifiedDate = orderMenusDto.OrmeModifiedDate,
                OrmeUserId = orderMenusDto.OrmeUserId
            };

            _repositoryManager.OrderMenusRepository.Insert(orderMenus);

            //forward 

            var res = _repositoryManager.OrderMenusRepository.FindLastOrderMenusId().ToList();
            return Ok(res);
        }

        // PUT api/<OrderMenusController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateOrderMenus(int id, [FromBody] OrderMenusDto orderMenusDto)
        {

            if (orderMenusDto == null)
            {
                _logger.LogError("RegionDto object sent from client is null");
                return BadRequest("RegionDto object is null");
            }
            var res = new OrderMenus
            {

                OrmeId = orderMenusDto.OrmeId,
                OrmeOrderNumber = orderMenusDto.OrmeOrderNumber,
                OrmeOrderDate = orderMenusDto.OrmeOrderDate,
                OrmeTotalItem = orderMenusDto.OrmeTotalItem,
                OrmeTotalDiscount = orderMenusDto.OrmeTotalDiscount,
                OrmeTotalAmount = orderMenusDto.OrmeTotalAmount,
                OrmePayType = orderMenusDto.OrmePayType,
                ormeCardnumber = orderMenusDto.OrmeCardnumber,
                OrmeIsPaid = orderMenusDto.OrmeIsPaid,
                OrmeModifiedDate = orderMenusDto.OrmeModifiedDate,
                OrmeUserId = orderMenusDto.OrmeUserId

            };
            _repositoryManager.OrderMenusRepository.Edit(res);
            return CreatedAtRoute("GetOrderMenusID", new { id = orderMenusDto.OrmeId }, new OrderMenusDto
            {

                OrmeId = res.OrmeId,
                OrmeOrderNumber = res.OrmeOrderNumber,
                OrmeOrderDate = res.OrmeOrderDate,
                OrmeTotalItem = res.OrmeTotalItem,
                OrmeTotalDiscount = res.OrmeTotalDiscount,
                OrmeTotalAmount = res.OrmeTotalAmount,
                OrmePayType = res.OrmePayType,
                OrmeCardnumber = res.ormeCardnumber,
                OrmeIsPaid = res.OrmeIsPaid,
                OrmeModifiedDate = res.OrmeModifiedDate,
                OrmeUserId = res.OrmeUserId


            });
        }

        // DELETE api/<OrderMenusController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderMenus(int? id)
        {
            if (id == null)
            {
                _logger.LogError($"Resto ID {id} object sent from client is null");
                return BadRequest($"Resto ID {id} object is null");
            }

            // find id first
            var res = _repositoryManager.OrderMenusRepository.FindOrderMenusById(id.Value);

            if (res == null)
            {
                _logger.LogError($"Region with id {id} not found");
                return NotFound();
            }


            _repositoryManager.OrderMenusRepository.Remove(res);
            return Ok("Data has been remove.");

        }
    }
}
