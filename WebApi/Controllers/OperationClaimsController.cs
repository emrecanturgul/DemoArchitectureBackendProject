﻿using Business.Repositories.OperationClaimRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;
        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }
        [HttpPost("add")]
        public IActionResult Add(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Add(operationClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result);
        }
            [HttpPost("update")]
            public IActionResult Update(OperationClaim operationClaim)
            {
                var result = _operationClaimService.Update(operationClaim);
                if (result.Success)
                {
                    return Ok(result.Message);
                }
                return BadRequest(result);
            }
        [HttpPost("delete")] 
        public IActionResult Delete(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Delete(operationClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyId")]
        public IActionResult GetById(int id)
        {
            var result = _operationClaimService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            var result = _operationClaimService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}