using AutoMapper;
using EDelivery.Models;
using EDelivery.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace EDelivery.Data
{
    public class EDeliveryRepository : IEDeliveryRepository
    {
        private readonly EDeliveryDBContext _dbContext;
        private readonly ILogger<EDeliveryRepository> _logger;

        public EDeliveryRepository(EDeliveryDBContext dbContext, ILogger<EDeliveryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        //Save Changes 
        public bool SaveChanges()
        {
            _logger.LogInformation($"Attempting to save the changes in the context");
            return (_dbContext.SaveChanges() > 0);
        }

        //Create Customer 
        public void CreateCustomer(Customer customer)
        {
            _logger.LogInformation($"Creating new customer");
            
            _dbContext.Customer.Add(customer);

            if (customer.Member.MemberEmail.Any() && customer.Member.MemberPassword.Any())
            {
                MemberDTO memberDTO = new MemberDTO()
                {
                    CustomerEmail = customer.Member.MemberEmail,
                    CustomerPassword = customer.Member.MemberPassword
                };
                _dbContext.SaveChanges();
            }

            if (customer.Location.Any())
            {
                foreach (var location in customer.Location)
                {
                    CustomerLocation customerLocation = new CustomerLocation
                    {
                        City = location.CityName,
                        Address = location.AddressName
                    };

                    _dbContext.SaveChanges();
                }
            }
        }

        //Get Customer by ID
        public Customer GetCustomerById (int customerID)
        {
            _logger.LogInformation($"Getting customer by ID");
            var result = _dbContext.Customer
                .Include(o => o.Location)
                .Where(o => o.CustomerId == customerID).FirstOrDefault();

            return result;
        }

        //Get All Customers
        public List<Customer> GetAllCustomers()
        {
            _logger.LogInformation($"Getting all customers");
            var result = _dbContext.Customer
                .Include(o => o.Location).ToList();

            return result;
        }

        //Delete Customer
        public void DeleteCustomer(Customer customer)
        {
            _logger.LogInformation("Deleting customer");

            if (_dbContext.Location.Any())
            {
                _dbContext.Location.RemoveRange(customer.Location);
                _dbContext.SaveChanges();
            }

            if (_dbContext.FoodOrder.Any())
            {
                foreach(var ec in _dbContext.FoodOrder.Where(o => o.CustomerId == customer.CustomerId)) 
                {
                    foreach(var ce in _dbContext.OrderItem.Where(o => o.Order.CustomerId == customer.CustomerId))
                    {
                        _dbContext.OrderItem.RemoveRange(ce);
                    }
                    _dbContext.OrderItem.RemoveRange(ec.OrderItem);  
                }           
                _dbContext.FoodOrder.RemoveRange(customer.FoodOrder);
                _dbContext.SaveChanges();
            }

            var memberId = customer.MemberId;
           
            foreach (var ec in _dbContext.Member.Where(o => o.MemberId == memberId))
            {
                _dbContext.Member.RemoveRange(ec);
            }

            _dbContext.Customer.Remove(customer);
            _dbContext.SaveChanges();

        }

        //Delete Orders
        public void DeleteOrder(FoodOrder model)
        {
            _logger.LogInformation("Deleting order");

            if (_dbContext.OrderItem.Any())
            {
                _dbContext.OrderItem.RemoveRange(model.OrderItem);
                _dbContext.SaveChanges();
            }

            _dbContext.FoodOrder.Remove(model);
            _dbContext.SaveChanges();
        }

        //Create DeliveryCompany
        public void CreateDeliveryCompany(DeliveryCompany deliveryCompany)
        {
            _logger.LogInformation($"Creating new delivery company");
            _dbContext.DeliveryCompany.Add(deliveryCompany);

            if (deliveryCompany.Member.MemberEmail.Any() && deliveryCompany.Member.MemberPassword.Any())
            {
                DeliveryCompanyMember memberDTO = new DeliveryCompanyMember()
                {
                    DeliveryCompanyEmail = deliveryCompany.Member.MemberEmail,
                    DeliveryCompanyPassword = deliveryCompany.Member.MemberPassword
                };
                _dbContext.SaveChanges();
            }

        }

        //Get Delivery Company by ID
        public DeliveryCompany GetDeliveryCompanyById(int deliveryCompanyId)
        {
            _logger.LogInformation($"Getting Delivery Company by ID");

            var deliveryCompany = _dbContext.DeliveryCompany
                .Include(o => o.Driver)
                .Where(o => o.DeliveryCompanyId == deliveryCompanyId);

            return deliveryCompany.FirstOrDefault();
        }
        
        //Edit Delivery Company and Driver
        public void UpdateDeliveryCompany(DeliveryCompany model)
        {
            _logger.LogInformation("Editing information about the delivery company");
            var oldDeliveryCompany = _dbContext.DeliveryCompany
                .Where(o => o.DeliveryCompanyId == model.DeliveryCompanyId)
                .FirstOrDefault();

            if (oldDeliveryCompany != null)
            {
                _dbContext.Entry(oldDeliveryCompany).CurrentValues.SetValues(model);                
            }
            _dbContext.SaveChanges();
        }

        //Delete Delivery Company 
        public void DeleteDeliveryCompany(DeliveryCompany deliveryCompany)
        {
            _logger.LogInformation($"Deleting delivery company");
            if (deliveryCompany.Driver.Any())
            {
                _dbContext.Driver.RemoveRange(deliveryCompany.Driver);
                _dbContext.SaveChanges();
            }
        
            var memberID = deliveryCompany.MemberId;
            foreach(var company in _dbContext.Member.Where(o => o.MemberId == memberID))
            {
                _dbContext.Member.RemoveRange(company);
            }

            _dbContext.DeliveryCompany.Remove(deliveryCompany);
            _dbContext.SaveChanges();
        }

        //Create new Driver 
        public void CreateDriver(Driver driver)
        {
            _logger.LogInformation($"Creating new driver");
            _dbContext.Driver.Add(driver);
            _dbContext.SaveChanges();
        }

        //Get Driver by ID
        public Driver GetDriverById(int driverID)
        {
            _logger.LogInformation($"Getting driver by ID");
            var result = _dbContext.Driver
                .Where(o => o.DriverId == driverID).FirstOrDefault();

            return result;
        }

        //Delete Driver
        public void DeleteDriver(Driver driver)
        {
            _logger.LogInformation("Deleting driver");
            _dbContext.Driver.Remove(driver);
            _dbContext.SaveChanges();
        }

        //Get Restaurant by ID
        public Restaurant GetRestaurantByID(int restaurantID)
        {
            _logger.LogInformation($"Getting Restaurant by ID");

            var restaurant = _dbContext.Restaurant
                .Include(o => o.CuisineType)
                .Include(o => o.Type)
                .Where(o => o.RestaurantId == restaurantID);

            return restaurant.FirstOrDefault();
        }

        //Search Restaurants
        public List<Restaurant> SearchRestaurant(string restaurantName)
        {
            _logger.LogInformation("Searching restaurant");
            List<Restaurant> restaurants = _dbContext.Restaurant
                .Where(o => o.RestaurantName.Contains(restaurantName))
                .OrderBy(o => o.RestaurantName).ToList();

            return restaurants;
        }
        //Get Food Category by ID
        public FoodCategory GetFoodCategoryByID(int foodCategoryID)
        {
            _logger.LogInformation($"Getting Food Category by ID");

            var foodCategory = _dbContext.FoodCategory
                .Where(o => o.CategoryId == foodCategoryID);

            return foodCategory.FirstOrDefault();
        }

        //Create Food
        public void CreateFood(Food food)
        {
            _logger.LogInformation($"Creating new food");
            _dbContext.Food.Add(food);
            _dbContext.SaveChanges();
        }

        //Edit Food
        public void EditFood(Food model)
        {
            _logger.LogInformation($"Editing food");
            var existingFood = _dbContext.Food
                .Where(o => o.FoodId == model.FoodId)
                .Include(o => o.Category)
                .FirstOrDefault();

            if(existingFood != null)
            {
                _dbContext.Entry(existingFood).CurrentValues.SetValues(model);             
            }
            _dbContext.SaveChanges();
        }

    
        //Delete Food
        public void DeleteFood(Food food)
        {
            _logger.LogInformation($"Deliting food");
            _dbContext.Food.Remove(food);
            _dbContext.SaveChanges();
        } 
        //Get food by ID
        public Food GetFoodById(int foodID)
        {
            _logger.LogInformation($"Getting food by ID");
            var food = _dbContext.Food.Include(o => o.Category)
                .Include(o => o.Restaurant)
                .Where(o => o.FoodId == foodID).FirstOrDefault();

            return food;           
        }

        //Get All Food Categories
        public List<FoodCategory> GetAllFoodCategories()
        {
            _logger.LogInformation($"Getting all food categories");

            var foodCategory = _dbContext.FoodCategory.ToList();
            return foodCategory;
        }

        //Get Restaurant Type by ID
        public RestaurantType GetRestaurantTypeByID(int restaurantTypeID)
        {
            _logger.LogInformation($"Getting restaurant type by ID");

            var restaurantType = _dbContext.RestaurantType
                .Where(o => o.TypeId == restaurantTypeID);

            return restaurantType.FirstOrDefault();
        }

        //Get All restaurant types
        public List<RestaurantType> GetRestaurantTypes()
        {
            _logger.LogInformation($"Getting all restaurant types");
            var restaurantTypes = _dbContext.RestaurantType.ToList();

            return restaurantTypes;               
        }

        //Get All Restaurants for Landing Page
        public List<Restaurant> GetAllRestaurants()
        {

            _logger.LogInformation($"Getting all restaurants");
           
           var result = _dbContext.Restaurant
                .Include(o => o.CuisineType)
                .Include(o => o.Type)
                .Include(o => o.Location)
                .ToList();

            return result;
        }

        //Get Cuisine Type by ID
        public CuisineType GetCuisineTypeById(int cuisineTypeID)
        {
            _logger.LogInformation($"Getting cuisine type by ID");

            var result = _dbContext.CuisineType
                .Where(o => o.CuisineTypeId == cuisineTypeID).FirstOrDefault();

            return result;
        }

        //Get FoodOrder by Id
        public FoodOrder GetOrderById(int foodOrderId)
        {
            _logger.LogInformation("Getting food order by Id");
            var result = _dbContext.FoodOrder
                .Where(o => o.OrderId == foodOrderId).FirstOrDefault();
            return result;
        }

        //Login
        public string ValidateLogin(string email, string password)
        {
            _logger.LogInformation("Loging into EDelivery");
            Member member = _dbContext.Member.Where(o => o.MemberEmail == email && o.MemberPassword == password).FirstOrDefault();
            if(member != null)
            {
                return member.MemberId.ToString();
            }
            return null;
        }

        //Get all Cuisine Types
        public List<CuisineType> GetAllCuisineTypes()
        {
            _logger.LogInformation($"Getting all cuisine types");
            var result = _dbContext.CuisineType.ToList();

            return result;
        }

        //Get Restaurants by Cuisine Type
        public List<Restaurant> GetRestaurantByCuisineId(int cuisineTypeId)
        {
            _logger.LogInformation("Getting restaurant by cuisine type");

            var result = _dbContext.Restaurant
                .Include(o => o.CuisineType)
                .Include(o => o.Type)
                .Include(o => o.Location)
                .Where(o => o.CuisineTypeId == cuisineTypeId).ToList();

            return result;
        }

        //Get Restaurants by Type
        public List<Restaurant> GetRestaurantsByType(int restaurantTypeId)
        {
            _logger.LogInformation("Getting all restaurants by restaurant type");

            var result = _dbContext.Restaurant
                .Include(o => o.CuisineType)
                .Include(o => o.Type)
                .Include(o => o.Location)
                .Where(o => o.TypeId == restaurantTypeId).ToList();

            return result;
        }

        //Get Info about one Restaurant
        public Restaurant GetRestaurantInfo(int restaurantId)
        {
            _logger.LogInformation("Getting information about one restaurant");

            var result = _dbContext.Restaurant
                .Where(o => o.RestaurantId == restaurantId)
                .Include(o => o.Type)
                .Include(o => o.CuisineType)
                .Include(o => o.Food)
                .ThenInclude(o => o.Category)
                .Include(o => o.Location)
                .Include(o => o.WorkingHours)
                .FirstOrDefault();

            return result;
        }

        //Get food by Restaurant Id
        public List<Food> GetFoodByRestaurant(int restaurantId)
        {
            _logger.LogInformation("Getting food by restaurant Id");

            var result = _dbContext.Food
                .Where(o => o.Restaurant.RestaurantId == restaurantId)
                .Include(o => o.Category)
                .OrderBy(o => o.Category.CategoryName).ToList();

            return result;
        }

        //Create new Restaurant
        public void CreateNewRestaurant(Restaurant restaurant)
        {
            _logger.LogInformation($"Creating new Restaurant");
            _dbContext.Restaurant.Add(restaurant);



            if (restaurant.Member.MemberEmail.Any() && restaurant.Member.MemberPassword.Any())
            {
                RestaurantMember memberDTO = new RestaurantMember()
                {
                    RestaurantEmail = restaurant.Member.MemberEmail,
                    RestaurantPassword = restaurant.Member.MemberPassword
                };
                _dbContext.SaveChanges();
            }

            if (restaurant.Location.Any())
            {
                foreach(var location in restaurant.Location)
                {
                    RestaurantLocation restaurantLocation = new RestaurantLocation
                    {
                        City = location.CityName,
                        Address = location.AddressName
                    };
                    _dbContext.SaveChanges();
                }
            }
        }

        //Edit Restaurant and menu
        public void EditRestaurant(Restaurant model)
        {
            _logger.LogInformation("Editing information about restaurant");
            var oldRestaurant = _dbContext.Restaurant
                .Where(o => o.RestaurantId == model.RestaurantId)
                .Include(o => o.CuisineType)
                .Include(o => o.Type)
                .Include(o => o.Member)
                .Include(o => o.Food)
                .Include(o => o.Location)
                .FirstOrDefault();

            if(oldRestaurant != null)
            {
                _dbContext.Entry(oldRestaurant).CurrentValues.SetValues(model);

                foreach(var restaurantMenu in model.Food)
                {
                    var existingFood = _dbContext.Food
                        .Include(o => o.Category)
                        .Where(o => o.FoodId == restaurantMenu.FoodId)
                        .FirstOrDefault();

                    if(existingFood != null)
                    {
                        _dbContext.Entry(existingFood).CurrentValues.SetValues(restaurantMenu);
                    }
                }

                foreach(var restaurantLocation in model.Location)
                {
                    var existingLocation = _dbContext.Location
                        .Where(o => o.LocationId == restaurantLocation.LocationId)
                        .FirstOrDefault();

                    if(existingLocation != null)
                    {
                        _dbContext.Entry(existingLocation).CurrentValues.SetValues(restaurantLocation);
                    }
                }
            }

            _dbContext.SaveChanges();
        }

        //Edit Customer
        public void EditCustomer(Customer model)
        {
            _logger.LogInformation("Editing customer!");

            var existingCustomer = _dbContext.Customer                
                .Where(o => o.CustomerId == model.CustomerId)
                .Include(o => o.Location)
                .FirstOrDefault();

            if(existingCustomer != null)
            {
                _dbContext.Entry(existingCustomer).CurrentValues.SetValues(model);

                foreach(var customerLocation in model.Location)
                {
                    var existingLocation = _dbContext.Location
                        .Where(o => o.LocationId == customerLocation.LocationId)
                        .FirstOrDefault();

                    if(existingLocation != null)
                    {
                        _dbContext.Entry(existingLocation).CurrentValues.SetValues(customerLocation);
                    }
                }
            }
            _dbContext.SaveChanges();
        }




        //GetWorkingHoursByID
        public WorkingHours GetWorkingHoursByID(int workingHoursByID)
        {

            _logger.LogInformation("Get working hours by ID");
            var result = _dbContext.WorkingHours
                .Where(o => o.WorkingHoursId == workingHoursByID).FirstOrDefault();

            return result;
        }



        //Get ListOfWorkingHours
        public List<WorkingHours> GetListOfWorkingDays(int restaurantId)
        {
            _logger.LogInformation("List of working days");
            var result = _dbContext.WorkingHours
                .Include(o => o.Restaurant)
                .Where(o => o.RestaurantId == restaurantId)
                .ToList();
            
            return result;
        }
        //Post Working Hours 
        public void PostWorkingHours(WorkingHours workingHours)
        {
            _logger.LogInformation("Post working creation for a restaurant");
            _dbContext.WorkingHours.Add(workingHours);
            _dbContext.SaveChanges();
        }

        //Edit Working Hours
        public void EditWorkingHours(WorkingHours model)
        {
            _logger.LogInformation("Editing working hours of the restaurant");

            var existingWorkingHours = _dbContext.WorkingHours
                .Where(o => o.WorkingHoursId == model.WorkingHoursId)
                .Include(o => o.Restaurant)
                .FirstOrDefault();

            if(existingWorkingHours != null)
            {
                _dbContext.Entry(existingWorkingHours).CurrentValues.SetValues(model);
            }

            _dbContext.SaveChanges();
        }


        //Delete Restaurant
        public void DeleteRestaurant(Restaurant restaurant)
        {
            _logger.LogInformation("Deleting restaurant");
            if (restaurant.Food.Any())
            {
                _dbContext.Food.RemoveRange(restaurant.Food);
                _dbContext.SaveChanges();
            }
            if (restaurant.WorkingHours.Any())
            {
                _dbContext.WorkingHours.RemoveRange(restaurant.WorkingHours);
                _dbContext.SaveChanges();
            }
            if (restaurant.Location.Any())
            {
                _dbContext.Location.RemoveRange(restaurant.Location);
                _dbContext.SaveChanges();
            }

            var memberId = restaurant.MemberId;
            foreach(var member in _dbContext.Member.Where(o => o.MemberId == memberId))
            {
                _dbContext.Member.RemoveRange();
            }

            _dbContext.Restaurant.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        //Create new Cuisine Type
        public void CreateCuisineType(CuisineType cuisineType)
        {
            _logger.LogInformation($"Creating new cuisine type");
            _dbContext.CuisineType.Add(cuisineType);
            _dbContext.SaveChanges();
        }

        //Create new Restaurant Type
        public void CreateRestaurantType(RestaurantType restaurantType)
        {
            _logger.LogInformation($"Creating new restaurant type");
            _dbContext.RestaurantType.Add(restaurantType);
            _dbContext.SaveChanges();
        }

        //Create new FoodCategory
        public void CreateFoodCategory(FoodCategory foodCategory)
        {
            _logger.LogInformation($"Creating new food category");
            _dbContext.FoodCategory.Add(foodCategory);
            _dbContext.SaveChanges();
        }

        //Create new Location
        public void CreateLocation(Location location)
        {
            _logger.LogInformation($"Creating new location");
            _dbContext.Location.Add(location);
            _dbContext.SaveChanges();
        }

        //Edit Location
        public void EditLocation(Location model)
        {
            _logger.LogInformation("Editing Location");
            var existingLocation = _dbContext.Location
                .Where(o => o.LocationId == model.LocationId)
                .FirstOrDefault();

            if (existingLocation != null)
            {
                _dbContext.Entry(existingLocation).CurrentValues.SetValues(model);
            }
            _dbContext.SaveChanges();
        }
        
        //Edit Driver
        public void EditDriver(Driver model)
        {
            _logger.LogInformation("Editing information about the driver");
            var existingDriver = _dbContext.Driver
                .Where(o => o.DriverId == model.DriverId)
                .FirstOrDefault();

            if(existingDriver != null)
            {
                _dbContext.Entry(existingDriver).CurrentValues.SetValues(model);
            }
            _dbContext.SaveChanges();
        }

        //Get Location by ID
        public Location GetLocationByID(int locationID)
        {
            _logger.LogInformation("Getting location by ID");
            var result = _dbContext.Location
                .Where(o => o.LocationId == locationID).FirstOrDefault();

            return result;
        }

        //Delete Location
        public void DeleteLocation(Location location)
        {
            _logger.LogInformation("Deleting the location");
            _dbContext.Location.Remove(location);
            _dbContext.SaveChanges();
        }

        //Delete Working Hours
        public void DeleteWorkingHours(WorkingHours workingHours)
        {
            _logger.LogInformation("Deleting working hours");
            _dbContext.WorkingHours.Remove(workingHours);
            _dbContext.SaveChanges();
        }

        //Get Member By Id
        public Member GetMemberById(int memberId)
        {
            _logger.LogInformation("Getting member by ID");
            var result = _dbContext.Member
                .Where(o => o.MemberId == memberId).FirstOrDefault();

            return result;
        }

        //Get All Member Types
        public List<ConfigurationDelivery> MemberTypes()
        {
            _logger.LogInformation("Getting all member types");
            var result = _dbContext.ConfigurationDelivery
                .Where(o => o.ProgrameCode == "member_type").ToList();

            return result;
        }

        //Get Order Process
        public ConfigurationDelivery[] OrderProcess()
        {
            _logger.LogInformation("Getting all order processes");
            var result = _dbContext.ConfigurationDelivery
                   .Where(o => o.ProgrameCode == "order_process").ToArray();

            return result;
        }

        //Create Order
        public void CreateOrder(FoodOrder newOrder)
        {
            _logger.LogInformation("Creating  order");

            DateTime dateTime = DateTime.Now;
            newOrder.OrderDate = dateTime;

            _dbContext.FoodOrder.Add(newOrder);

            foreach (var item in newOrder.OrderItem)
            {
               item.Food = _dbContext.Food.Find(item.FoodId);
              
            }
            _dbContext.SaveChanges();
        }


    }
}
