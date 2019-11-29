using AutoMapper;
using EDelivery.Data;
using EDelivery.Models;
using EDelivery.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class FoodController : Controller
    {
        private readonly IEDeliveryRepository _repository;
        private readonly ILogger<FoodController> _logger;
        private readonly IMapper _mapper;
       

        public FoodController(IEDeliveryRepository repository, ILogger<FoodController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Food Category by ID.
        /// </summary>
        /// <param name="foodCategoryID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the food category</response>
        /// <response code="400">If the item is null</response>  
        [HttpGet("foodcategory/{foodCategoryID}")]
        public ActionResult<FoodCategoryByID> GetFoodCategoryByID(int foodCategoryID)
        {
            try
            {
                var result = _repository.GetFoodCategoryByID(foodCategoryID);

                IMapper mapper = EDeliveryProfile.GetFoodCategoryByID();

                return mapper.Map<FoodCategoryByID>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the Food category:{ex}");
                return BadRequest("Failed to get the Food category");
            }
        }

        /// <summary>
        /// Create food.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created new food</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost("create/food")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<FoodDTO> CreateFood(FoodDTO model)
        {
            try
            {
                var foodCategory = _repository.GetFoodCategoryByID(model.FoodTypeID);
                if (foodCategory == null) return BadRequest("Food Category could not be find");

                IMapper mapper = EDeliveryProfile.CreateFood();
                var newFood = mapper.Map<Food>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();

                var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;
                newFood.RestaurantId = restaurantId;
                
                _repository.CreateFood(newFood);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created food" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new food:{ex}");
                return BadRequest("Failed to create new food");
            }
        }

        /// <summary>
        /// Get food by ID.
        /// </summary>
        /// <param name="foodID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the food.</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("food/foodID")]
        public ActionResult<GetFoodById> GetFoodById(int foodID)
        {
            try
            {
                var result = _repository.GetFoodById(foodID);
                IMapper mapper = EDeliveryProfile.GetFoodById();

                return mapper.Map<GetFoodById>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to return the food:{ex}");
                return BadRequest("Failed to return the food");
            }
        }

        /// <summary>
        /// Get food by restaurant ID.
        /// </summary>
        /// <param name="restaurantId">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the food.</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("foodByRestaurant/{restaurantId}")]
        public ActionResult<List<GetFoodByRestaurantId>> GetFoodByRestaurantId(int restaurantId)
        {
            try
            {
                var result = _repository.GetFoodByRestaurant(restaurantId);
                IMapper mapper = EDeliveryProfile.GetFoodRestaurantById();

                return mapper.Map<List<GetFoodByRestaurantId>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get food by restaurant Id: {ex}");
                return BadRequest("Failed to get food by restaurant Id:");
            }
        }

        /// <summary>
        /// Get all food categories.
        /// </summary>
        /// <response code="200">Succesfully returned all food categories</response>
        /// <response code="400">If the item is null</response>  
        [HttpGet("foodcategories")]
        public ActionResult<List<FoodCategoryByID>> GetAllFoodCategories()
        {
            try
            {
                var result = _repository.GetAllFoodCategories();
                IMapper mapper = EDeliveryProfile.GetFoodCategoryByID();

                return mapper.Map<List<FoodCategoryByID>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all food categories:{ex}");
                return BadRequest("Failed to get all food categories");
            }
        }

        /// <summary>
        /// Create new food category.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully create new food category</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost("create/foodcategory")]
        public ActionResult<CreateFoodCategory> CreateFoodCategory(CreateFoodCategory model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.CreateFoodCategory();
                var result = mapper.Map<FoodCategory>(model);
                _repository.CreateFoodCategory(result);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created new food category" });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new food category:{ex}");
                return BadRequest("Failed to create new food category");
            }
        }

        /// <summary>
        /// Edit food.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully edited food</response>
        /// <response code="400">If the item is null</response> 
        [HttpPut("edit/food")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<EditFood> EditFoodCategory(EditFood model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.EditFood();
                var editedFood = mapper.Map<Food>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out int id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;
                editedFood.RestaurantId = restaurantId;

                var categoryId = dBContext.Food.First(o => o.FoodId == model.FoodId).CategoryId;
                editedFood.CategoryId = categoryId;
   
                _repository.EditFood(editedFood);
      
                    return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Succesfuly edited food" });                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to edit the food:{ex}");
                
            }
            return BadRequest("Failed to edit the food");
        }

        /// <summary>
        /// Delete food.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully deleted food</response>
        /// <response code="400">If the item is null</response> 
        [HttpDelete("delete/food")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<DeleteFood> DeleteFood([FromBody] DeleteFood model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.DeleteFood();
                var existingFood = mapper.Map<Food>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out int id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();

                var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;
                existingFood.RestaurantId = restaurantId;
                
                _repository.DeleteFood(existingFood);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete the food{ex}");
            }
            return BadRequest();
        }
    }
}
