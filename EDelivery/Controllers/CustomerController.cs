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
    public class CustomerController : Controller
    {
        private readonly IEDeliveryRepository _repository;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;

        public CustomerController(IEDeliveryRepository repository, ILogger<CustomerController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Post Customer.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created customer</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("create/customer")]
        public ActionResult<CustomerDTO> CreateCustomer([FromBody] CustomerDTO model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.CreateCustomer();
                var customer = mapper.Map<Customer>(model);

                _repository.CreateCustomer(customer);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created new customer" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new Customer:{ex}");
                return BadRequest("Failed to create new Customer");
            }
        }

        /// <summary>
        /// Get All Customers -- sho ima
        /// </summary>
        /// <response code="200">Succesfully returned all customers</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("customers")]
        public ActionResult<List<GetAllCustomers>> GetAllCustomers()
        {
            try
            {
                var result = _repository.GetAllCustomers();

                IMapper mapper = EDeliveryProfile.GetAllCustomers();
                return mapper.Map<List<GetAllCustomers>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to return all customers:{ex}");
                return BadRequest("Failed to return all customers.");
            }
        }

        /// <summary>
        /// Get Customer by ID.
        /// </summary>
        /// <param name="customerID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created customer</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("customer/{customerID}")]
        public ActionResult<GetCustomerbyID> GetCustomerById(int customerID)
        {
            try
            {
                var result = _repository.GetCustomerById(customerID);
                IMapper mapper = EDeliveryProfile.GetCustomerByID();

                return mapper.Map<GetCustomerbyID>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the Customer by ID:{ex}");
                return BadRequest("Failed to get the Customer by ID");
            }
        }

        /// <summary>
        /// Edit customer.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully edited customer</response>
        /// <response code="400">If the item is null</response> 
        [HttpPut("edit/customer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<EditCustomer> EditCustomer(EditCustomer model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.EditCustomer();
                var newResult = mapper.Map<Customer>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;

                newResult.MemberId = memberId;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var customerId = dBContext.Customer.First(o => o.MemberId == memberId).CustomerId;
                newResult.CustomerId = customerId;

                var locationId = dBContext.Location.First(o => o.CustomerId == customerId).LocationId;
                locationId = newResult.Location.Select(o => o.LocationId).FirstOrDefault();
                
                _repository.EditCustomer(newResult);
        
             return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Succesfuly edited customer" });
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to edit the customer:{ex}");
            }
            return BadRequest("Failed to edit the customer.");
        }

        /// <summary>
        /// Delete customer.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully deleted customer</response>
        /// <response code="400">If the item is null</response> 
        [HttpDelete("delete/customer")]
        public ActionResult<DeleteCustomer> DeleteCustomer(DeleteCustomer model)
        {
            try
            {
                var oldCustomer = _repository.GetCustomerById(model.CustomerId);
                if (oldCustomer == null) return NotFound("The customer with the given id could not be find");

               _repository.DeleteCustomer(oldCustomer);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to delete the customer:{ex}");
            }
            return BadRequest("Failed to delte the customer");
        }
    }
}
