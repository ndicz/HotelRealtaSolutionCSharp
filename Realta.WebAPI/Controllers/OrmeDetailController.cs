﻿using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
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


                OmdeId = r.OmdeId,
                OrmePrice = r.OrmePrice,
                OrmeQty = r.OrmeQty,
                OrmeSubtotal = r.OrmeSubtotal,
                OrmeDiscount = r.OrmeDiscount,
                OmdeOrmeId = r.OmdeOrmeId,
                OmdeRemeId = r.OmdeRemeId
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
                OmdeId = ormeDetail.OmdeId,
                OrmePrice = ormeDetail.OrmePrice,
                OrmeQty = ormeDetail.OrmeQty,
                OrmeSubtotal = ormeDetail.OrmeSubtotal,
                OrmeDiscount = ormeDetail.OrmeDiscount,
                OmdeOrmeId = ormeDetail.OmdeOrmeId,
                OmdeRemeId = ormeDetail.OmdeRemeId

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
                OrmePrice = ormeDetailDto.OrmePrice,
                OrmeQty = ormeDetailDto.OrmeQty,
                OrmeSubtotal = ormeDetailDto.OrmeSubtotal,
                OrmeDiscount = ormeDetailDto.OrmeDiscount,
                OmdeOrmeId = ormeDetailDto.OmdeOrmeId,
                OmdeRemeId = ormeDetailDto.OmdeRemeId
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
                OmdeId = id,
                OrmePrice = ormeDetailDto.OrmePrice,
                OrmeQty = ormeDetailDto.OrmeQty,
                OrmeSubtotal = ormeDetailDto.OrmeSubtotal,
                OrmeDiscount = ormeDetailDto.OrmeDiscount,
                OmdeOrmeId = ormeDetailDto.OmdeOrmeId,
                OmdeRemeId = ormeDetailDto.OmdeRemeId

            };
            _repositoryManager.OrmeDetailRepository.Edit(res);
            return CreatedAtRoute("GetOrmeDetailID", new { id = ormeDetailDto.OmdeId }, new OrmeDetailDto
            {

                OmdeId = id,
                OrmePrice = res.OrmePrice,
                OrmeQty = res.OrmeQty,
                OrmeSubtotal = res.OrmeSubtotal,
                OrmeDiscount = res.OrmeDiscount,
                OmdeOrmeId = res.OmdeOrmeId,
                OmdeRemeId = res.OmdeRemeId

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
