using Microsoft.AspNetCore.Mvc;
using Realta.Contract;
using Realta.Domain.Base;
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrderMenusController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderMenusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
