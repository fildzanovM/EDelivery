using AutoMapper;
using EDelivery.Models;
using EDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.Data
{
    public class EDeliveryProfile
    {
        //Create new Customer 
        public static IMapper CreateCustomer()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDTO>()
                    .ForMember(o => o.FirstName, ex => ex.MapFrom(o => o.CustomerFirstName))
                    .ForMember(o => o.LastName, ex => ex.MapFrom(o => o.CustomerLastName))
                    .ForMember(o => o.CustomerEmail, ex => ex.MapFrom(o => o.Member.MemberEmail))
                    .ForMember(o => o.CustomerPassword, ex => ex.MapFrom(o => o.Member.MemberPassword))
                    .ForMember(o => o.Telephone, ex => ex.MapFrom(o => o.CustomerTelephone))
                    .ForMember(o => o.Location, ex => ex.MapFrom(o => o.Location))
                    .ReverseMap();

                cfg.CreateMap<Location, CustomerLocation>()
                    .ForMember(o => o.City, ex => ex.MapFrom(o => o.CityName))
                    .ForMember(o => o.Address, ex => ex.MapFrom(o => o.AddressName))
                    .ForMember(o => o.Latitude, ex => ex.MapFrom(o => o.Latitude))
                    .ForMember(o => o.Longitude, ex => ex.MapFrom(o => o.Longitude))
                    .ReverseMap();

            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Edit Customer
        public static IMapper EditCustomer()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, EditCustomer>()
                    .ForMember(o => o.FirstName, ex => ex.MapFrom(o => o.CustomerFirstName))
                    .ForMember(o => o.LastName, ex => ex.MapFrom(o => o.CustomerLastName))
                    .ForMember(o => o.Telephone, ex => ex.MapFrom(o => o.CustomerTelephone))
                    .ReverseMap();

            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Customer by ID
        public static IMapper GetCustomerByID()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, GetCustomerbyID>()
                    .ForMember(o => o.CustomerID, ex => ex.MapFrom(o => o.CustomerId))
                    .ForMember(o => o.FirstName, ex => ex.MapFrom(o => o.CustomerFirstName))
                    .ForMember(o => o.LastName, ex => ex.MapFrom(o => o.CustomerLastName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //GetAllCustomers
        public static IMapper GetAllCustomers()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, GetAllCustomers>()
                    .ForMember(o => o.CustomerId, ex => ex.MapFrom(o => o.CustomerId))
                    .ForMember(o => o.Name, ex => ex.MapFrom(o => o.CustomerFirstName + " " + (o.CustomerLastName)))
                    .ForMember(o => o.Telephone, ex => ex.MapFrom(o => o.CustomerTelephone))
                    .ForMember(o => o.Location, ex => ex.MapFrom(o => o.Location));

                cfg.CreateMap<Location, CustomerLocation>()
                    .ForMember(o => o.City, ex => ex.MapFrom(o => o.CityName))
                    .ForMember(o => o.Address, ex => ex.MapFrom(o => o.AddressName))
                    .ForMember(o => o.Latitude, ex => ex.MapFrom(o => o.Latitude))
                    .ForMember(o => o.Longitude, ex => ex.MapFrom(o => o.Longitude));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Create Delivery Company and Drivers for the Company
        public static IMapper CreateDeliveryCompany()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeliveryCompany, DeliveryCompanyDTO>()
                    .ForMember(o => o.DeliveryCompanyID, ex => ex.MapFrom(o => o.DeliveryCompanyId))
                    .ForMember(o => o.CompanyName, ex => ex.MapFrom(o => o.DeliveryCompanyName))
                    .ForMember(o => o.CompanyTelephone, ex => ex.MapFrom(o => o.DeliveryCompanyTelephone))
                    .ForMember(o => o.CompanyEmail, ex => ex.MapFrom(o => o.Member.MemberEmail))
                    .ForMember(o => o.CompanyPassword, ex => ex.MapFrom(o => o.Member.MemberPassword))
                    .ReverseMap();

            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Create Driver For a Company
        public static IMapper CreateDriver()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Driver, CreateDriver>()
                    .ForMember(o => o.FirstName, ex => ex.MapFrom(o => o.DriverFirstName))
                    .ForMember(o => o.LastName, ex => ex.MapFrom(o => o.DriverLastName))
                    .ForMember(o => o.DriverTelephone, ex => ex.MapFrom(o => o.DriverTelephone))
                    .ForMember(o => o.DriverImage, ex => ex.MapFrom(o => o.DriverImage))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Delivery Company by ID
        public static IMapper DeliveryCompanyByID()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeliveryCompany, DeliveryCompanyByID>()
                    .ForMember(o => o.CompanyID, ex => ex.MapFrom(o => o.DeliveryCompanyId))
                    .ForMember(o => o.CompanyName, ex => ex.MapFrom(o => o.DeliveryCompanyName))
                    .ForMember(o => o.CompanyTelephone, ex => ex.MapFrom(o => o.DeliveryCompanyTelephone));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
   
        // Get Restaurant by ID
        public static IMapper GetRestaurantByID()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantByID>()
                    .ForMember(o => o.RestaurantID, ex => ex.MapFrom(o => o.RestaurantId))
                    .ForMember(o => o.RestaurantName, ex => ex.MapFrom(o => o.RestaurantName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Search Restaurant
        public static IMapper SearchRestaurant()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, SearchRestaurant>()
                    .ForMember(o => o.RestaurantID, ex => ex.MapFrom(o => o.RestaurantId))
                    .ForMember(o => o.RestaurantName, ex => ex.MapFrom(o => o.RestaurantName));
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Food Category by ID
        public static IMapper GetFoodCategoryByID()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FoodCategory, FoodCategoryByID>()
                    .ForMember(o => o.CategoryID, ex => ex.MapFrom(o => o.CategoryId))
                    .ForMember(o => o.CategoryName, ex => ex.MapFrom(o => o.CategoryName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Create Food
        public static IMapper CreateFood()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Food, FoodDTO>()
                    .ForMember(o => o.FoodName, ex => ex.MapFrom(o => o.FoodName))
                    .ForMember(o => o.FoodDescription, ex => ex.MapFrom(o => o.FoodDescription))
                    .ForMember(o => o.FoodImage, ex => ex.MapFrom(o => o.FoodImage))
                    .ForMember(o => o.FoodPrice, ex => ex.MapFrom(o => o.FoodPrice))
                    .ForMember(o => o.FoodTypeID, ex => ex.MapFrom(o => o.Category.CategoryId))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Edit Food
        public static IMapper EditFood()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Food, EditFood>()
                    .ForMember(o => o.FoodId, ex => ex.MapFrom(o => o.FoodId))
                    .ForMember(o => o.FoodName, ex => ex.MapFrom(o => o.FoodName))
                    .ForMember(o => o.FoodDescription, ex => ex.MapFrom(o => o.FoodDescription))
                    .ForMember(o => o.FoodPrice, ex => ex.MapFrom(o => o.FoodPrice))
                    .ForMember(o => o.FoodImage, ex => ex.MapFrom(o => o.FoodImage))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Restaurant Type/s
        public static IMapper GetRestaurantType()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RestaurantType, RestaurantTypeDTO>()
                    .ForMember(o => o.RestaurantTypeID, ex => ex.MapFrom(o => o.TypeId))
                    .ForMember(o => o.RestaurantType, ex => ex.MapFrom(o => o.TypeName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //List all Restaurants for a landing page
        public static IMapper GetAllRestaurants()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, GetAllRestaurants>()
                    .ForMember(o => o.RestaurantID, ex => ex.MapFrom(o => o.RestaurantId))
                    .ForMember(o => o.RestaurantName, ex => ex.MapFrom(o => o.RestaurantName))
                    .ForMember(o => o.RestaurantImage, ex => ex.MapFrom(o => o.RestaurantImage))
                    .ForMember(o => o.RestaurantType, ex => ex.MapFrom(o => o.Type.TypeName))
                    .ForMember(o => o.CuisineType, ex => ex.MapFrom(o => o.CuisineType.CuisineTypeName));

            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Cuisine Type by ID
        public static IMapper GetCuisineTypeByID()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CuisineType, CuisineTypeDTO>()
                    .ForMember(o => o.CuisineTypeID, ex => ex.MapFrom(o => o.CuisineTypeId))
                    .ForMember(o => o.CuisineTypeName, ex => ex.MapFrom(o => o.CuisineTypeName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Create Restaurant
        public static IMapper CreateRestaurant()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, CreateRestaurant>()
                    .ForMember(o => o.RestaurantName, ex => ex.MapFrom(o => o.RestaurantName))
                    .ForMember(o => o.RestaurantDescription, ex => ex.MapFrom(o => o.RestaurantDescription))
                    .ForMember(o => o.RestaurantImage, ex => ex.MapFrom(o => o.RestaurantImage))
                    .ForMember(o => o.RestaurantTelephone, ex => ex.MapFrom(o => o.RestaurantTelephone))
                    .ForMember(o => o.RestaurantEmail, ex => ex.MapFrom(o => o.Member.MemberEmail))
                    .ForMember(o => o.RestaurantPassword, ex => ex.MapFrom(o => o.Member.MemberPassword))
                    .ForMember(o => o.RestaurantTypeID, ex => ex.MapFrom(o => o.Type.TypeId))
                    .ForMember(o => o.CuisineTypeID, ex => ex.MapFrom(o => o.CuisineType.CuisineTypeId))
                    .ForMember(o => o.RestaurantLocation, ex => ex.MapFrom(o => o.Location))
                    .ReverseMap();

                cfg.CreateMap<Location, RestaurantLocation>()
                    .ForMember(o => o.City, ex => ex.MapFrom(o => o.CityName))
                    .ForMember(o => o.Address, ex => ex.MapFrom(o => o.AddressName))
                    .ReverseMap();
            });

               IMapper mapper = config.CreateMapper();
               return mapper;
        }

        //Edit Location
        public static IMapper EditLocation()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, EditLocation>()
                    .ForMember(o => o.LocationId, ex => ex.MapFrom(o => o.LocationId))
                    .ForMember(o => o.City, ex => ex.MapFrom(o => o.CityName))
                    .ForMember(o => o.Address, ex => ex.MapFrom(o => o.AddressName))
                    .ForMember(o => o.Latitude, ex => ex.MapFrom(o => o.Latitude))
                    .ForMember(o => o.Longitude, ex => ex.MapFrom(o => o.Longitude))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Edit working hours of the restaurant
        public static IMapper EditWorkingHours()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkingHours, EditWorkingHours>()
                    .ForMember(o => o.WorkingHoursId, ex => ex.MapFrom(o => o.WorkingHoursId))
                    .ForMember(o => o.DayOfWeek, ex => ex.MapFrom(o => PostDayName(o.DayOfWeek)))
                    .ForMember(o => o.TimeOpen, ex => ex.MapFrom(o => o.TimeOpen))
                    .ForMember(o => o.TimeClosed, ex => ex.MapFrom(o => o.TimeClosed))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Post Working Hours
        public static IMapper PostWorkingHours()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkingHours, PostWorkingHours>()
                    .ForMember(o => o.DayOfWeek, ex => ex.MapFrom(o => PostDayName(o.DayOfWeek)))
                    .ForMember(o => o.TimeOpen, ex => ex.MapFrom(o => o.TimeOpen))
                    .ForMember(o => o.TimeClosed, ex => ex.MapFrom(o => o.TimeClosed))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Delete Working Hours
        public static IMapper DeleteWorkingHours()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkingHours, DeleteWorkingHours>()
                    .ForMember(o => o.WorkingHoursId, ex => ex.MapFrom(o => o.WorkingHoursId))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Info about one Restaurant
        public static IMapper RestaurantInfo()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantInfo>()
                    .ForMember(o => o.RestaurantId, ex => ex.MapFrom(o => o.RestaurantId))
                    .ForMember(o => o.RestaurantName, ex => ex.MapFrom(o => o.RestaurantName))
                    .ForMember(o => o.RestaurantDescription, ex => ex.MapFrom(o => o.RestaurantDescription))
                    .ForMember(o => o.RestaurantImage, ex => ex.MapFrom(o => o.RestaurantImage))
                    .ForMember(o => o.RestaurantTelephone, ex => ex.MapFrom(o => o.RestaurantTelephone))
                    .ForMember(o => o.CuisineType, ex => ex.MapFrom(o => o.CuisineType.CuisineTypeName))
                    .ForMember(o => o.RestaurantType, ex => ex.MapFrom(o => o.Type.TypeName))
                    .ForMember(o => o.RestaurantMenu, ex => ex.MapFrom(o => o.Food))
                    .ForMember(o => o.RestaurantLocation, ex => ex.MapFrom(o => o.Location))
                    .ForMember(o => o.RestaurantWorkingHours, ex => ex.MapFrom(o => o.WorkingHours));

                cfg.CreateMap<Food, RestaurantMenuInfo>()
                    .ForMember(o => o.FoodName, ex => ex.MapFrom(o => o.FoodName))
                    .ForMember(o => o.FoodDescription, ex => ex.MapFrom(o => o.FoodDescription))
                    .ForMember(o => o.FoodImage, ex => ex.MapFrom(o => o.FoodImage))
                    .ForMember(o => o.FoodPrice, ex => ex.MapFrom(o => o.FoodPrice))
                    .ForMember(o => o.FoodType, ex => ex.MapFrom(o => o.Category.CategoryName));

                cfg.CreateMap<Location, RestaurantLocationInfo>()
                    .ForMember(o => o.City, ex => ex.MapFrom(o => o.CityName))
                    .ForMember(o => o.Address, ex => ex.MapFrom(o => o.AddressName));

                cfg.CreateMap<WorkingHours, RestaurantWorkingHours>()
                    .ForMember(o => o.DayOfWeek, ex => ex.MapFrom(o => GetDayName(o.DayOfWeek)))
                    .ForMember(o => o.TimeOpen, ex => ex.MapFrom(o => o.TimeOpen))
                    .ForMember(o => o.TimeClosed, ex => ex.MapFrom(o => o.TimeClosed));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Edit Restaurant
        public static IMapper EditRestaurant()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, EditRestaurant>()
                    .ForMember(o => o.RestaurantName, ex => ex.MapFrom(o => o.RestaurantName))
                    .ForMember(o => o.RestaurantDescription, ex => ex.MapFrom(o => o.RestaurantDescription))
                    .ForMember(o => o.RestaurantImage, ex => ex.MapFrom(o => o.RestaurantImage))
                    .ForMember(o => o.RestaurantTelephone, ex => ex.MapFrom(o => o.RestaurantTelephone))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        //Create Cuisine Type
        public static IMapper CreateCuisineType()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CuisineType, CreateCuisineType>()
                    .ForMember(o => o.CuisneType, ex => ex.MapFrom(o => o.CuisineTypeName))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Create Restaurant Type
        public static IMapper CreateRestaurantType()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RestaurantType, CreateRestaurantType>()
                    .ForMember(o => o.RestaurantType, ex => ex.MapFrom(o => o.TypeName))
                    .ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Create Food Category
        public static IMapper CreateFoodCategory()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FoodCategory, CreateFoodCategory>()
                    .ForMember(o => o.FoodCategory, ex => ex.MapFrom(o => o.CategoryName))
                    .ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Create Location
        public static IMapper CreateLocation()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, LocationDTO>()
                    .ForMember(o => o.CityName, ex => ex.MapFrom(o => o.CityName))
                    .ForMember(o => o.AddressName, ex => ex.MapFrom(o => o.AddressName))
                    .ForMember(o => o.Latitude, ex => ex.MapFrom(o => o.Latitude))
                    .ForMember(o => o.Longitude, ex => ex.MapFrom(o => o.Longitude))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //GetFoodById
        public static IMapper GetFoodById()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Food, GetFoodById>()
                    .ForMember(o => o.FoodID, ex => ex.MapFrom(o => o.FoodId))
                    .ForMember(o => o.FoodName, ex => ex.MapFrom(o => o.FoodName))
                    .ForMember(o => o.FoodDescription, ex => ex.MapFrom(o => o.FoodDescription))
                    .ForMember(o => o.FoodImage, ex => ex.MapFrom(o => o.FoodImage))
                    .ForMember(o => o.FoodPrice, ex => ex.MapFrom(o => o.FoodPrice))
                    .ForMember(o => o.FoodTypeID, ex => ex.MapFrom(o => o.Category.CategoryId))
                    .ForMember(o => o.RestaurantID, ex => ex.MapFrom(o => o.Restaurant.RestaurantId)).ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //GetFoodByRestaurantId
        public static IMapper GetFoodRestaurantById()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Food, GetFoodByRestaurantId>()
                    .ForMember(o => o.FoodName, ex => ex.MapFrom(o => o.FoodName))
                    .ForMember(o => o.FoodDescription, ex => ex.MapFrom(o => o.FoodDescription))
                    .ForMember(o => o.FoodPrice, ex => ex.MapFrom(o => o.FoodPrice))
                    .ForMember(o => o.FoodImage, ex => ex.MapFrom(o => o.FoodImage))
                    .ForMember(o => o.FoodCategory, ex => ex.MapFrom(o => o.Category.CategoryName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Edit Delivery Compeny
        public static IMapper EditDeliveryCompany()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeliveryCompany, EditDeliveryCompany>()
                     .ForMember(o => o.CompanyName, ex => ex.MapFrom(o => o.DeliveryCompanyName))
                     .ForMember(o => o.CompanyTelephone, ex => ex.MapFrom(o => o.DeliveryCompanyTelephone))
                     .ReverseMap();


                cfg.CreateMap<Driver, CompanyDriver>()
                    .ForMember(o => o.DriverId, ex => ex.MapFrom(o => o.DriverId))
                    .ForMember(o => o.FirstName, ex => ex.MapFrom(o => o.DriverFirstName))
                    .ForMember(o => o.LastName, ex => ex.MapFrom(o => o.DriverLastName))
                    .ForMember(o => o.DriverImage, ex => ex.MapFrom(o => o.DriverImage))
                    .ForMember(o => o.DriverTelephone, ex => ex.MapFrom(o => o.DriverTelephone))
                    .ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Driver by ID
        public static IMapper GetDriverId()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Driver, GetDriverByID>()
                    .ForMember(o => o.DriverID, ex => ex.MapFrom(o => o.DriverId))
                    .ForMember(o => o.FirstName, ex => ex.MapFrom(o => o.DriverFirstName))
                    .ForMember(o => o.LastName, ex => ex.MapFrom(o => o.DriverLastName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Edit Driver
        public static IMapper EditDriver()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Driver, EditDriver>()
                    .ForMember(o => o.DriverId, ex => ex.MapFrom(o => o.DriverId))
                    .ForMember(o => o.FirstName, ex => ex.MapFrom(o => o.DriverFirstName))
                    .ForMember(o => o.LastName, ex => ex.MapFrom(o => o.DriverLastName))
                    .ForMember(o => o.DriverTelephone, ex => ex.MapFrom(o => o.DriverTelephone))
                    .ForMember(o => o.DriverImage, ex => ex.MapFrom(o => o.DriverImage))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Delete Driver
        public static IMapper DeleteDriver()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Driver, DeleteDriver>()
                    .ForMember(o => o.DriverID, ex => ex.MapFrom(o => o.DriverId))
                    .ForMember(o => o.FirstName, ex => ex.MapFrom(o => o.DriverFirstName))
                    .ForMember(o => o.LastName, ex => ex.MapFrom(o => o.DriverLastName))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Delete Order
        public static IMapper GetOrderId()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FoodOrder, GetOrderId>()
                    .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.OrderId))
                    .ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
       
        //Delete Location
        public static IMapper DeleteLocation()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, DeleteLocaiton>()
                    .ForMember(o => o.LocationId, ex => ex.MapFrom(o => o.LocationId))
                    .ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //GetLocationById
        public static IMapper GetLocationByID()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, GetLocationById>()
                    .ForMember(o => o.LocationID, ex => ex.MapFrom(o => o.LocationId))
                    .ForMember(o => o.CityName, ex => ex.MapFrom(o => o.CityName))
                    .ForMember(o => o.AddressName, ex => ex.MapFrom(o => o.AddressName))
                    .ForMember(o => o.Latitude, ex => ex.MapFrom(o => o.Latitude))
                    .ForMember(o => o.Longitude, ex => ex.MapFrom(o => o.Longitude))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        
        //GetWorkingHoursByID
        public static IMapper GetWorkingHoursById()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkingHours, GetWorkingHoursByID>()
                  .ForMember(o => o.WorkingHoursID, ex => ex.MapFrom(o => o.WorkingHoursId))
                  .ForMember(o => o.DayOfWeek, ex => ex.MapFrom(o => GetDayName(o.DayOfWeek)))
                  .ForMember(o => o.TimeOpen, ex => ex.MapFrom(o => o.TimeOpen))
                  .ForMember(o => o.TimeClosed, ex => ex.MapFrom(o => o.TimeClosed));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //List of Working days
        public static IMapper GetListOfWorkingDays()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkingHours, ListOfWorkingDays>()
                  .ForMember(o => o.DayOfWeek, ex => ex.MapFrom(o => GetDayName(o.DayOfWeek)))
                  .ForMember(o => o.TimeOpen, ex => ex.MapFrom(o => o.TimeOpen))
                  .ForMember(o => o.TimeClosed, ex => ex.MapFrom(o => o.TimeClosed))
                  .ForMember(o => o.RestaurantId, ex => ex.MapFrom(o => o.Restaurant.RestaurantId));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //LoginMapper
        public static IMapper LoginMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, LoginViewModel>()
                   // .ForMember(o => o.RestaurantID, ex => ex.MapFrom(o => o.RestaurantId))
                    .ForMember(o => o.UserName, ex => ex.MapFrom(o => o.RestaurantEmail))
                    .ForMember(o => o.Password, ex => ex.MapFrom(o => o.RestaurantPassword))
                    .ReverseMap();

                cfg.CreateMap<Customer, LoginViewModel>()
                  //  .ForMember(o => o.CustomerID, ex => ex.MapFrom(o => o.CustomerId))
                    .ForMember(o => o.UserName, ex => ex.MapFrom(o => o.CustomerEmail))
                    .ForMember(o => o.Password, ex => ex.MapFrom(o => o.CustomerPassword))
                    .ReverseMap();

                cfg.CreateMap<DeliveryCompany, LoginViewModel>()
                    //.ForMember(o => o.DeliveryCompanyID, ex => ex.MapFrom(o => o.DeliveryCompanyId))
                    .ForMember(o => o.UserName, ex => ex.MapFrom(o => o.DeliveryCompanyEmail))
                    .ForMember(o => o.Password, ex => ex.MapFrom(o => o.DeliveryCompanyPassword))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Member by ID
        public static IMapper GetMemberByID()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Member, GetMemberById>()
                    .ForMember(o => o.MemberID, ex => ex.MapFrom(o => o.MemberId))
                    .ForMember(o => o.MemberEmail, ex => ex.MapFrom(o => o.MemberEmail))
                    .ForMember(o => o.MemberPassword, ex => ex.MapFrom(o => o.MemberPassword))
                    .ForMember(o => o.MemberType, ex => ex.MapFrom(o => MemberType(o.MemberType)));
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Delete Customer Member
        public static IMapper DeleteCustomer()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, DeleteCustomer>()
                    .ForMember(o => o.CustomerId, ex => ex.MapFrom(o => o.CustomerId));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Delete Restaurant
        public static IMapper DeleteRestaurant()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, DeleteRestaurant>()
                    .ForMember(o => o.RestaurantId, ex => ex.MapFrom(o => o.RestaurantId))
                    .ForMember(o => o.RestaurantName, ex => ex.MapFrom(o => o.RestaurantName));
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Delete Delivery Company
        public static IMapper DeleteDeliveryCompany()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeliveryCompany, DeleteDeliveryCompany>()
                    .ForMember(o => o.DeliveryCompanyID, ex => ex.MapFrom(o => o.DeliveryCompanyId))
                    .ForMember(o => o.DeliveryCompanyName, ex => ex.MapFrom(o => o.DeliveryCompanyName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Delete Food
        public static IMapper DeleteFood()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Food, DeleteFood>()
                    .ForMember(o => o.FoodId, ex => ex.MapFrom(o => o.FoodId))
                    .ForMember(o => o.FoodName, ex => ex.MapFrom(o => o.FoodName))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Member Types
        public static IMapper MemberTypeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConfigurationDelivery, MemberTypeDto>()
                    .ForMember(o => o.TypeId, ex => ex.MapFrom(o => o.ConfigurationValue))
                    .ForMember(o => o.MemberType, ex => ex.MapFrom(o => o.ConfigurationName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Order Process
        public static IMapper OrderProcess()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConfigurationDelivery, Order_Process>()
                    .ForMember(o => o.OrderProcessId, ex => ex.MapFrom(o => o.ConfigurationValue))
                    .ForMember(o => o.OrderProcess, ex => ex.MapFrom(o => o.ConfigurationName));
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Login Member
        public static IMapper LoginMember()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Member, LoginMember>()
                    .ForMember(o => o.MemberId, ex => ex.MapFrom(o => o.MemberId))
                    .ForMember(o => o.CustomerId, ex => ex.MapFrom(o => o.Customer.Select(oa => oa.CustomerId)))
                    .ForMember(o => o.RestaurantId, ex => ex.MapFrom(o => o.Restaurant.Select(oa => oa.RestaurantId)))
                    .ForMember(o => o.DeliveryCompanyId, ex => ex.MapFrom(o => o.DeliveryCompany.Select(oa => oa.DeliveryCompanyId)))
                    .ForMember(o => o.MemberType, ex => ex.MapFrom(o => o.MemberType))
                    .ForMember(o => o.MemberEmail, ex => ex.MapFrom(o => o.MemberEmail))
                    .ForMember(o => o.MemberPassword, ex => ex.MapFrom(o => o.MemberPassword))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Create new Order
        public static IMapper CreateOrder()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FoodOrder, CreateOrder>()
                    .ForMember(o => o.OrderDate, ex => ex.MapFrom(o => o.OrderDate))
                    .ReverseMap();

                cfg.CreateMap<OrderItem, OrderItemDTO>()
                    .ForMember(o => o.FoodId, ex => ex.MapFrom(o => o.FoodId))
                    .ForMember(o => o.Quantity, ex => ex.MapFrom(o => o.Quantity))
                    .ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Get Day Name as a string
        public static WeekDays GetDayName(int dayName)
        {
            switch (dayName)
            {
                case 1:
                    return WeekDays.Monday;
                case 2:
                    return WeekDays.Tuesday;
                case 3:
                    return WeekDays.Wednesday;
                case 4:
                    return WeekDays.Thursday;
                case 5:
                    return WeekDays.Friday;
                case 6:
                    return WeekDays.Saturday;
                case 7:
                    return WeekDays.Sunday;
                    
            }

            return 0;
        }

       //post Day Name as a string and sava as an integer 
      public static WeekDays PostDayName(int dayName)
        {
            switch (dayName)
            {
                case 1:
                    return WeekDays.Monday;
                case 2:
                    return WeekDays.Tuesday;
                case 3:
                    return WeekDays.Wednesday;
                case 4:
                    return WeekDays.Thursday;
                case 5:
                    return WeekDays.Friday;
                case 6:
                    return WeekDays.Saturday;
                case 7:
                    return WeekDays.Sunday;
            }

            return 0;
        }

        //Get member type as a string
        public static member_type MemberType(int member)
        {
            switch (member)
            {
                case 8:
                    return member_type.Customer;
                case 9:
                    return member_type.Restaurant;
                case 10:
                    return member_type.DeliveryCompany;
            }

            return 0;
        }

        //Get order status as a string
        public static order_process OrderStatus(int status)
        {
            switch (status)
            {
                case 11:
                    return order_process.OrderConfirmation;
                case 12:
                    return order_process.RestaurantPreparesOrder;
                case 13:
                    return order_process.OrderIsReadyForDelivery;
                case 14:
                    return order_process.OrderIsOnItsWay;
                case 15:
                    return order_process.OrderDelivered;
            }

            return 0;
        }
        
    }  
}
