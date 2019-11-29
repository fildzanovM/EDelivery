using AutoMapper;
using EDelivery.Data;
using EDelivery.Models;
using EDelivery.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EDelivery.Controllers
{
    [Route("api")]
    public class OrderController : Controller
    {
        private readonly IEDeliveryRepository _repository;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(IEDeliveryRepository repository, ILogger<OrderController> logger,
            IMapper mapper,
            EDeliveryDBContext dBContext)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Order Processes.
        /// </summary>
        /// <response code="200">Succesfully returned the order processes</response>
        /// <response code="400">If the item is null</response>    
        [HttpGet("orderprocess")]
        public ActionResult<Order_Process[]> GetOrderProcess()
        {
            try
            {
                var result = _repository.OrderProcess();
                IMapper mapper = EDeliveryProfile.OrderProcess();
                return mapper.Map<Order_Process[]>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the order processes:{ex}");
                return BadRequest("Failed to get the order processes");
            }
        }

        /// <summary>
        /// Get order by Id.
        /// </summary>
        /// <param name="orderId">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned order with the given Id.</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("order/{orderId}")]
        public ActionResult<GetOrderId> GetOrderById(int orderId)
        {
            try
            {
                var result = _repository.GetOrderById(orderId);

                IMapper mapper = EDeliveryProfile.GetOrderId();
                return mapper.Map<GetOrderId>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get order with the given Id: {ex}");
                return BadRequest("Failed to get the order with the given Id");
            }
        }

        /// <summary>
        /// Delete order.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully deleted order</response>
        /// <response code="400">If the item is null</response> 
        [HttpDelete("delete/order")]
        public ActionResult<GetOrderId> DeleteOrder(GetOrderId model)
        {
            try
            {
                var existingOrder = _repository.GetOrderById(model.OrderId);
                if (existingOrder == null) return NotFound("The order with the given Id could not be find");

                _repository.DeleteOrder(existingOrder);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete the order{ex}");
                return BadRequest("Failed to delete the order");
            }
        }



        /// <summary>
        /// Create new order.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created new order</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("order/orderitem")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<CreateOrder> CreateOrder([FromBody] CreateOrder model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.CreateOrder();
                var newOrder = mapper.Map<FoodOrder>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var userId = int.TryParse(userIdClaim, out var id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();

                var userID = dBContext.Customer.First(o => o.MemberId == userId).CustomerId;

                newOrder.CustomerId = userID;
               _repository.CreateOrder(newOrder);
                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Succesfuly created new order" });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to create new order:{ex}");
                return BadRequest("Failed to create new order");
            }
        }
    }
}
