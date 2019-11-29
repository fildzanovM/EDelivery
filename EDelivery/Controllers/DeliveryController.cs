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
    public class DeliveryController : Controller
    {
        private readonly IEDeliveryRepository _repository;
        private readonly ILogger<DeliveryController> _logger;
        private readonly IMapper _mapper;

        public DeliveryController(IEDeliveryRepository repository, ILogger<DeliveryController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Post Delivery Company.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created Delivery Company</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("create/deliverycompany")]
        public ActionResult<DeliveryCompanyDTO> CreateDeliveryCompany([FromBody] DeliveryCompanyDTO model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.CreateDeliveryCompany();

                var deliveryCompany = mapper.Map<DeliveryCompany>(model);

                _repository.CreateDeliveryCompany(deliveryCompany);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created delivery company" });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new Delivery Company:{ex}");
                return BadRequest("Failed to create new Delivery Company");
            }
        }

        /// <summary>
        /// Get DeliveryCompany by ID.
        /// </summary>
        /// <param name="deliveryCompanyID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the Delivery Company</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("deliverycompany/{deliveryCompanyID}")]
        public ActionResult<DeliveryCompanyByID>GetDeliveryCompanyByID(int deliveryCompanyID)
        {
            try
            {
                var result = _repository.GetDeliveryCompanyById(deliveryCompanyID);
                IMapper mapper = EDeliveryProfile.DeliveryCompanyByID();

                return mapper.Map<DeliveryCompanyByID>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the Delivery Company:{ex}");
                return BadRequest("Failed to get the Delivery Company");
            }
        }

        /// <summary>
        /// Post Driver.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created driver</response>
        /// <response code="400">If the item is null</response>    
        [HttpPost("create/driver")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<CreateDriver> CreateDriver([FromBody] CreateDriver model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.CreateDriver();
                var driver = mapper.Map<Driver>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();

                var deliveryCompanyId = dBContext.DeliveryCompany.First(o => o.MemberId == memberId).DeliveryCompanyId;
                driver.DeliveryCompanyId = deliveryCompanyId;

                _repository.CreateDriver(driver);
                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created driver" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new Driver:{ex}");
                return BadRequest("Failed to create new Driver");
            }
        }

        /// <summary>
        /// Edit delivery company.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully edited delivery company</response>
        /// <response code="400">If the item is null</response>  
        [HttpPut("edit/deliveryCompany")]
        public ActionResult<EditDeliveryCompany> EditDeliveryCompany(EditDeliveryCompany model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.EditDeliveryCompany();
                var newResult = mapper.Map<DeliveryCompany>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;
                newResult.MemberId = memberId;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var deliveryCompanyId = dBContext.DeliveryCompany.First(o => o.MemberId == memberId).DeliveryCompanyId;
                newResult.DeliveryCompanyId = deliveryCompanyId;

                _repository.UpdateDeliveryCompany(newResult);
                
                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Succesfuly edited delivery company" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to edit the Delivery company:{ex}");
                
            }
            return BadRequest("Failed to edit the Delivery company");
        }

        /// <summary>
        /// Delete Delivery Company.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully deleted delivery company</response>
        /// <response code="400">If the item is null</response>  
        [HttpDelete("delete/deliveryCompany")]
        public ActionResult<DeleteDeliveryCompany> DeleteDeliveryCompany([FromBody] DeleteDeliveryCompany model)
        {
            try
            {
                var oldDeliveryCompany = _repository.GetDeliveryCompanyById(model.DeliveryCompanyID);
                if (oldDeliveryCompany == null) return NotFound("Delivery company with the given ID could not be find");

                _repository.DeleteDeliveryCompany(oldDeliveryCompany);
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete the delivery company: {ex}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Edit driver.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully edited driver</response>
        /// <response code="400">If the item is null</response>  
        [HttpPut("edit/driver")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<EditDriver> EditDriver(EditDriver model)
        {
            try
            {     
                IMapper mapper = EDeliveryProfile.EditDriver();
                var result = mapper.Map<Driver>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var deliveryCompanyId = dBContext.DeliveryCompany.First(o => o.MemberId == memberId).DeliveryCompanyId;
                result.DeliveryCompanyId = deliveryCompanyId;

                _repository.EditDriver(result);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Succesfuly edited driver" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to edit the driver{ex}");
            }

            return BadRequest("Failed to edit the driver");
        }

        /// <summary>
        /// Delete Driver.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully deleted driver</response>
        /// <response code="400">If the item is null</response>  
        [HttpDelete("delete/driver")]
        public ActionResult<DeleteDriver> DeleteDriver([FromBody]DeleteDriver model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.DeleteDriver();
                var oldDriver = mapper.Map<Driver>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var deliveryCompanyId = dBContext.DeliveryCompany.First(o => o.MemberId == memberId).DeliveryCompanyId;
                oldDriver.DeliveryCompanyId = deliveryCompanyId;

                _repository.DeleteDriver(oldDriver);
                return Ok()
;            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete the driver{ex}");
            }

            return BadRequest();
        }

        /// <summary>
        /// Get driver by ID.
        /// </summary>
        /// <param name="driverID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the driver by ID</response>
        /// <response code="400">If the item is null</response>  
        [HttpGet("driver/{driverID}")]
        public ActionResult<GetDriverByID> GetDriverByID(int driverID)
        {
            try
            {
                var result = _repository.GetDriverById(driverID);

                IMapper mapper = EDeliveryProfile.GetDriverId();
                return mapper.Map<GetDriverByID>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the Driver:{ex}");
                return BadRequest("Failed to get the Driver");
            }
        }


    }
}
