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
    public class RestaurantController : Controller
    {
        private readonly IEDeliveryRepository _repository;
        private readonly ILogger<RestaurantController> _logger;
        private readonly IMapper _mapper;

        public RestaurantController(IEDeliveryRepository repository, ILogger<RestaurantController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Restaurant by ID.
        /// </summary>
        /// <param name="restaurantID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the restaurant</response>
        /// <response code="400">If the item is null</response>    
        [HttpGet("restaurant/{restaurantID}")]
        public ActionResult<RestaurantByID> GetRestaurantByID(int restaurantID)
        {
            try
            {
                var result = _repository.GetRestaurantByID(restaurantID);
                IMapper mapper = EDeliveryProfile.GetRestaurantByID();

                return mapper.Map<RestaurantByID>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the Restaurant:{ex}");
                return BadRequest("Failed to get the Restaurant");
            }
        }


        /// <summary>
        /// Get Restaurant type by ID.
        /// </summary>
        /// <param name="restaurantTypeID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the restaurant type</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("restauranttype/{restaurantTypeID}")]
        public ActionResult<RestaurantTypeDTO> GetRestaurantTypeByID(int restaurantTypeID)
        {
            try
            {
                var result = _repository.GetRestaurantTypeByID(restaurantTypeID);
                IMapper mapper = EDeliveryProfile.GetRestaurantType();

                return mapper.Map<RestaurantTypeDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the Restaurant type:{ex}");
                return BadRequest("Failed to get the Restaurant types");
            }
        }

        /// <summary>
        /// Get all Restaurant types.
        /// </summary>
        /// <response code="200">Succesfully returned restaurant types</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("restauranttypes")]
        public ActionResult<List<RestaurantTypeDTO>> GetRestaurantTypes()
        {
            try
            {
                var result = _repository.GetRestaurantTypes();
                IMapper mapper = EDeliveryProfile.GetRestaurantType();                
                return mapper.Map<List<RestaurantTypeDTO>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Restaurant types:{ex}");
                return BadRequest("Failed to get all Restaurant types");
            }
        }

        /// <summary>
        /// Get all Restaurants for the landing page.
        /// </summary>
        /// <response code="200">Succesfully returned restaurant types</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("restaurants")]
        public ActionResult<GetAllRestaurants[]> GetAllRestaurants()
        {
            try
            {
                var result = _repository.GetAllRestaurants();
                IMapper mapper = EDeliveryProfile.GetAllRestaurants();
                return mapper.Map<GetAllRestaurants[]>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Restaurants:{ex}");
                return BadRequest("Failed to get all Restaurants: ");
            }
        }

        /// <summary>
        /// Get all Restaurants by Cuisine Type ID.
        /// </summary>
        /// <param name="cuisineTypeId">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned restaurants by cuisine type Id.</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("restaurants/{cuisineTypeId}")]
        public ActionResult<List<GetAllRestaurants>> GetRestaurantsByCuisineTypeId(int cuisineTypeId)
        {
            try
            {
                var result = _repository.GetRestaurantByCuisineId(cuisineTypeId);
                IMapper mapper = EDeliveryProfile.GetAllRestaurants();
                return mapper.Map<List<GetAllRestaurants>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Restaurants by the given Cuisine Type Id:{ex}");
                return BadRequest("Failed to get Restaurants by the given Cuisine Type Id");
            }
        }

        /// <summary>
        /// Get all Restaurants by Restaurant Type ID.
        /// </summary>
        /// <param name="restaurantTypeId">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned restaurants by restaurant type Id.</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("restaurantByType/{restaurantTypeId}")]
        public ActionResult<List<GetAllRestaurants>> GetRestaurantsByType(int restaurantTypeId)
        {
            try
            {
                var result = _repository.GetRestaurantsByType(restaurantTypeId);
                IMapper mapper = EDeliveryProfile.GetAllRestaurants();
                return mapper.Map<List<GetAllRestaurants>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Restaurants by the given Restaurant Type Id:{ex}");
                return BadRequest("Failed to get the Restaurants by the given Restaurant Type Id");
            }
        }

        /// <summary>
        /// Get Cuisine Type by ID.
        /// </summary>
        /// <param name="cuisineTypeID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned cuisine type</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("cuisinetype/{cuisineTypeID}")]
        public ActionResult<CuisineTypeDTO> GetCuisineTypeById(int cuisineTypeID)
        {
            try
            {
                var result = _repository.GetCuisineTypeById(cuisineTypeID);
                IMapper mapper = EDeliveryProfile.GetCuisineTypeByID();

                return mapper.Map<CuisineTypeDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the cuisine type:{ex}");
                return BadRequest("Failed to get the cuisine type: ");
            }
        }

        /// <summary>
        /// Get all Cuisine Types.
        /// </summary>
        /// <response code="200">Succesfully returned all cuisine types</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("cuisinetypes")]
        public ActionResult<List<CuisineTypeDTO>> GetAllCuisineTypes()
        {
            try
            {
                var result = _repository.GetAllCuisineTypes();
                IMapper mapper = EDeliveryProfile.GetCuisineTypeByID();

                return mapper.Map<List<CuisineTypeDTO>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all cuisine types:{ex}");
                return BadRequest("Failed to get all cuisine types: ");
            }
        }

        /// <summary>
        /// Create restaurant.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created new restaurant</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("create/restaurant")]
        public ActionResult<CreateRestaurant> CreateRestaurant([FromBody] CreateRestaurant model)
        {
            try
            {
                var restaurantType = _repository.GetRestaurantTypeByID(model.RestaurantTypeID);
                if (restaurantType == null) return BadRequest("Restaurant type could not be find");

                var cuisineType = _repository.GetCuisineTypeById(model.CuisineTypeID);
                if (cuisineType == null) return BadRequest("Cuisine type could not be find");

                IMapper mapper = EDeliveryProfile.CreateRestaurant();
                var restaurant = mapper.Map<Restaurant>(model);

                _repository.CreateNewRestaurant(restaurant);
                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created restaurant" });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new restaurant:{ex}");
                return BadRequest("Failed to create new restaurant: ");
            }
        }

        /// <summary>
        /// Create cuisine type.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created new cuisine type</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("create/cuisinetype")]
        public ActionResult<CreateCuisineType> CreateCuisineType(CreateCuisineType model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.CreateCuisineType();
                var cuisineType = mapper.Map<CuisineType>(model);

                _repository.CreateCuisineType(cuisineType);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created new cuisine type" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new cuisine type:{ex}");
                return BadRequest("Failed to create new cuisine type: ");
            }
        }

        /// <summary>
        /// Create restaurant type.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created new restaurant type</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("create/restauranttype")]
        public ActionResult<CreateRestaurantType> CreateRestaurantType(CreateRestaurantType model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.CreateRestaurantType();
                var restaurantType = mapper.Map<RestaurantType>(model);
                
                _repository.CreateRestaurantType(restaurantType);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created new restaurant type" });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new restaurant type:{ex}");
                return BadRequest("Failed to create new restaurant type: ");
            }
        }

        /// <summary>
        /// Get working hours by ID.
        /// </summary>
        /// <param name="workingHoursID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned working hours.</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("workingHours/{workingHoursID}")]
        public ActionResult<GetWorkingHoursByID> GetWorkingHoursByID(int workingHoursID)
        {
            try
            {
                var result = _repository.GetWorkingHoursByID(workingHoursID);
                IMapper mapper = EDeliveryProfile.GetWorkingHoursById();

                return mapper.Map<GetWorkingHoursByID>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get working hours by ID:{ex}");
                return BadRequest("Failed to get working hours by ID");
            }
        }

        /// <summary>
        /// Edit working hours.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully edited working hours.</response>
        /// <response code="400">If the item is null</response> 
        [HttpPut("edit/workingHours")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<EditWorkingHours> EditWorkingHours(EditWorkingHours model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.EditWorkingHours();
                var result = mapper.Map<WorkingHours>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;
                result.RestaurantId = restaurantId;
                
                 _repository.EditWorkingHours(result);
                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Succesfuly edited the working hours!!" });

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to edit working hours:{ex}");
                return BadRequest("Failed to edit working hours");
            }
        }

        /// <summary>
        /// Get one restaurant info by Id.
        /// </summary>
        /// <param name="restaurantId">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returned the restaurant.</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("restaurantInfo{restaurantId}")]
        public ActionResult<RestaurantInfo> GetInfoAboutRestaurant(int restaurantId)
        {
            try
            {
                var result = _repository.GetRestaurantInfo(restaurantId);
                IMapper mapper = EDeliveryProfile.RestaurantInfo();

                return mapper.Map<RestaurantInfo>(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get the restaurant by Id:{ex}");
                return BadRequest("Failed to get the restaurant by Id");
            }
        }

        /// <summary>
        /// Search restaurant by name.
        /// </summary>
        /// <param name="restaurantName">Data to create the houshold from.</param>
        /// <response code="200">Succesfully searched a restaurant</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("search")]
        public ActionResult<List<SearchRestaurant>> SearchRestaurant(string restaurantName)
        {
            try
            {
                var result = _repository.SearchRestaurant(restaurantName);
                if (!result.Any()) return NotFound("Failed to find restaurant with the given name");

                IMapper mapper = EDeliveryProfile.SearchRestaurant();
                return mapper.Map<List<SearchRestaurant>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to find the restaurants with the given name: {ex}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Edit restaurant.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully edited the restaurant</response>
        /// <response code="400">If the item is null</response> 
        [HttpPut("edit/restaurant")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<EditRestaurant> EditRestaurant(EditRestaurant model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.EditRestaurant();
                var newResult = mapper.Map<Restaurant>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;
                newResult.MemberId = memberId;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;
                newResult.RestaurantId = restaurantId;

                var restaurantTypeId = dBContext.Restaurant.First(o => o.RestaurantId == restaurantId).TypeId;
                newResult.TypeId = restaurantTypeId;

                var cuistineTypeId = dBContext.Restaurant.First(o => o.RestaurantId == restaurantId).CuisineTypeId;
                newResult.CuisineTypeId = cuistineTypeId;

                _repository.EditRestaurant(newResult);
                
                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Succesfuly edited the restaurant" });
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to edit the restaurant: {ex}");
            }
            return BadRequest("Failed to edit the restaurant");
        }

        /// <summary>
        /// Delete restaurant.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully deleted restaurant</response>
        /// <response code="400">If the item is null</response> 
        [HttpDelete("delete/restaurant")]
        public ActionResult<DeleteRestaurant> DeleteRestaurant(DeleteRestaurant model)
        {
            try
            {
                var oldRestaurant = _repository.GetRestaurantByID(model.RestaurantId);
                if (oldRestaurant == null) return NotFound("The restaurant with the given Id could not be find");

                _repository.DeleteRestaurant(oldRestaurant);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete the restaurant:{ex}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Post Working Hours.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully posted working hours of the restaurant</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("post/workingHours")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<PostWorkingHours> PostWorkingHours(PostWorkingHours model)
        {
            try
            {                
                IMapper mapper = EDeliveryProfile.PostWorkingHours();
                var newWorkingHours = mapper.Map<WorkingHours>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;

                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;

                newWorkingHours.RestaurantId = restaurantId;

                _repository.PostWorkingHours(newWorkingHours);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Succesfuly created working hours" });


            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new working hours: {ex}");
                return BadRequest("Failed to create new working hours");
            }
        }

        [HttpGet("workynghourslist{restaurantId}")]
        public ActionResult<List<ListOfWorkingDays>> ListOfWorkingDays(int restaurantId)
        {
            try
            {
                var result = _repository.GetListOfWorkingDays(restaurantId);

                IMapper mapper = EDeliveryProfile.GetListOfWorkingDays();

                return mapper.Map<List<ListOfWorkingDays>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get list of the working days: {ex}");
                return BadRequest("Failed to get list of the working days");
            }
        }
        /// <summary>
        /// Delete working hours.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully deleted working hours</response>
        /// <response code="400">If the item is null</response> 
        [HttpDelete("delete/workingHours")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<DeleteWorkingHours> DeleteWorkingHours(DeleteWorkingHours model)
        {
            try
            {
                IMapper mapper = EDeliveryProfile.DeleteWorkingHours();
                var existingWorkingHours = mapper.Map<WorkingHours>(model);

                var userIdClaim = User.FindFirst("MemberId")?.Value;
                var memberId = int.TryParse(userIdClaim, out var id) ? id : 0;
                
                EDeliveryDBContext dBContext = new EDeliveryDBContext();
                var restaurantId = dBContext.Restaurant.First(o => o.MemberId == memberId).RestaurantId;
                existingWorkingHours.RestaurantId = restaurantId;

                _repository.DeleteWorkingHours(existingWorkingHours);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete working hours:{ex}");
                return BadRequest();
            }
        }
    }
}
