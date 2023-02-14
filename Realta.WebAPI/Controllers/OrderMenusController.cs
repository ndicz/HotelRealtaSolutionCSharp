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
            orme_id = r.orme_id,
            orme_order_number   = r.orme_order_number,
            orme_order_date     = r.orme_order_date,
            orme_total_item     = r.orme_total_item,
            orme_total_discount = r.orme_total_discount,
            orme_total_amount   = r.orme_total_amount,
            orme_pay_type       = r.orme_pay_type,
            orme_cardnumber     = r.orme_cardnumber,
            orme_is_paid        = r.orme_is_paid,
            orme_modified_date  = r.orme_modified_date,
            orme_user_id        = r.orme_user_id
            });


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
                orme_id = orderMenus.orme_id,
                orme_order_number = orderMenus.orme_order_number,
                orme_order_date = orderMenus.orme_order_date,
                orme_total_item = orderMenus.orme_total_item,
                orme_total_discount = orderMenus.orme_total_discount,
                orme_total_amount = orderMenus.orme_total_amount,
                orme_pay_type = orderMenus.orme_pay_type,
                orme_cardnumber = orderMenus.orme_cardnumber,
                orme_is_paid = orderMenus.orme_is_paid,
                orme_modified_date = orderMenus.orme_modified_date,
                orme_user_id = orderMenus.orme_user_id
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
                //orme_id = orderMenusDto.orme_id,
                orme_order_number = orderMenusDto.orme_order_number,
                orme_order_date = orderMenusDto.orme_order_date,
                orme_total_item = orderMenusDto.orme_total_item,
                orme_total_discount = orderMenusDto.orme_total_discount,
                orme_total_amount = orderMenusDto.orme_total_amount,
                orme_pay_type = orderMenusDto.orme_pay_type,
                orme_cardnumber = orderMenusDto.orme_cardnumber,
                orme_is_paid = orderMenusDto.orme_is_paid,
                orme_modified_date = orderMenusDto.orme_modified_date,
                orme_user_id = orderMenusDto.orme_user_id
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

                orme_id = orderMenusDto.orme_id,
                orme_order_number = orderMenusDto.orme_order_number,
                orme_order_date = orderMenusDto.orme_order_date,
                orme_total_item = orderMenusDto.orme_total_item,
                orme_total_discount = orderMenusDto.orme_total_discount,
                orme_total_amount = orderMenusDto.orme_total_amount,
                orme_pay_type = orderMenusDto.orme_pay_type,
                orme_cardnumber = orderMenusDto.orme_cardnumber,
                orme_is_paid = orderMenusDto.orme_is_paid,
                orme_modified_date = orderMenusDto.orme_modified_date,
                orme_user_id = orderMenusDto.orme_user_id

            };
            _repositoryManager.OrderMenusRepository.Edit(res);
            return CreatedAtRoute("GetOrderMenusID", new { id = orderMenusDto.orme_id }, new OrderMenusDto
            {

                orme_id = res.orme_id,
                orme_order_number = res.orme_order_number,
                orme_order_date = res.orme_order_date,
                orme_total_item = res.orme_total_item,
                orme_total_discount = res.orme_total_discount,
                orme_total_amount = res.orme_total_amount,
                orme_pay_type = res.orme_pay_type,
                orme_cardnumber = res.orme_cardnumber,
                orme_is_paid = res.orme_is_paid,
                orme_modified_date = res.orme_modified_date,
                orme_user_id = res.orme_user_id


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
