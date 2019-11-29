using AutoMapper;
using EDelivery.Data;
using EDelivery.Models;
using EDelivery.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EDelivery.Controllers
{
    [Route("api")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly IEDeliveryRepository _repository;
        private readonly ILogger<LocationController> _logger;
        private readonly IMapper _mapper;

        public LocationController(IEDeliveryRepository repository, ILogger<LocationController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new location.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created new location</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("create/location")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<LocationDTO> CreateLocaation(LocationDTO model)
        {

            try
            {
                IMapper mapper = EDeliveryProfile.CreateLocation();
                var result = mapper.Map<Location>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var memberType = dBContext.Member.First(o => o.MemberId == memberId).MemberType;

                if (memberType.Equals(9))
                {
                    var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;
                    result.RestaurantId = restaurantId;

                }
                else if (memberType.Equals(8))

                {
                    var customerId = dBContext.Customer.First(o => o.MemberId == memberId).CustomerId;
                    result.CustomerId = customerId;
                }
                else
                {
                    return BadRequest();
                }

                _repository.CreateLocation(result);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created new location" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new location:{ex}");
                return BadRequest("Failed to create new location");
            }
       
        }

        /// <summary>
        /// Get Location by ID.
        /// </summary>
        /// <param name="locationID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the location by ID</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("location/{locationID}")]
        public ActionResult<GetLocationById> GetLocationById(int locationID)
        {
            try
            {
                var result = _repository.GetLocationByID(locationID);

                IMapper mapper = EDeliveryProfile.GetLocationByID();

                return mapper.Map<GetLocationById>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the location with the given ID{ex}");
                return BadRequest("Failed to get the location with the given ID");
            }
        }

        /// <summary>
        /// Edit location.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully edited location</response>
        /// <response code="400">If the item is null</response> 
        [HttpPut("edit/location")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<EditLocation> EditLocation(EditLocation model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.EditLocation();
                var editedLocation = mapper.Map<Location>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;
    
                EDeliveryDBContext dBContext = new EDeliveryDBContext();

                var memberType = dBContext.Member.First(o => o.MemberId == memberId).MemberType;
          

                if (memberType.Equals(8))
                {
                    var customerId = dBContext.Customer.First(o => o.MemberId == memberId).CustomerId;
                    editedLocation.CustomerId = customerId;
                }
                else if(memberType.Equals(9))
                {
                    var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;
                    editedLocation.RestaurantId = restaurantId;
                }
                else
                {
                    return BadRequest();
                }

                _repository.EditLocation(editedLocation);
                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Succesfuly edited location" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to edit the location{ex}"); 
            }
            return BadRequest("Failed to edit the location");
        }

        /// <summary>
        /// Delete location.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully deleted location</response>
        /// <response code="400">If the item is null</response> 
        [HttpDelete("delete/location")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<DeleteLocaiton> DeleteLocation([FromBody] DeleteLocaiton model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.DeleteLocation();
                var existingLocation = mapper.Map<Location>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out int id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();

                var memberType = dBContext.Member.First(o => o.MemberId == memberId).MemberType;

                if (memberType.Equals(9))
                {
                    var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;
                    existingLocation.RestaurantId = restaurantId;

                }
                else if (memberType.Equals(8))

                {
                    var customerId = dBContext.Customer.First(o => o.MemberId == memberId).CustomerId;
                    existingLocation.CustomerId = customerId;
                }
                else
                {
                    return BadRequest();
                }
                
                _repository.DeleteLocation(existingLocation);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete the location:{ex}");
            }

            return BadRequest();
        }
    }
}
