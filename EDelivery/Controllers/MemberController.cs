
using AutoMapper;
using EDelivery.Data;
using EDelivery.Models;
using EDelivery.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.Controllers
{
    [Route("api")]
    [ApiController]
    public class MemberController : Controller
    {
        private readonly IEDeliveryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<MemberController> _logger;

        public MemberController(IEDeliveryRepository repository, IMapper mapper, ILogger<MemberController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Get one member by Id.
        /// </summary>
        /// <param name="memberID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the member.</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("member/{memberID}")]
        public ActionResult<GetMemberById> GetMemberById(int memberID)
        {
            try
            {
                var result = _repository.GetMemberById(memberID);
                IMapper mapper = EDeliveryProfile.GetMemberByID();
                return mapper.Map<GetMemberById>(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get member by the given ID:{ex}");
                return BadRequest("Failed to get member by the given iD");
            }
        }


        /// <summary>
        /// Get list of member types.
        /// </summary>
        /// <response code="200">Succesfully returned the member types.</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("member/memberTypes")]
        public ActionResult<List<MemberTypeDto>> GetMemberTypes()
        {
            try
            {
                var result = _repository.MemberTypes();

                IMapper mapper = EDeliveryProfile.MemberTypeMapper();
                return mapper.Map<List<MemberTypeDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get all member types: {ex}");
                return BadRequest("Failed to get all member types");
            }
        }
    }

}
