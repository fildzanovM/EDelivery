using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.ViewModels
{
    //GetRestaurantByID
    public class RestaurantByID
    {
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }

    }

    //Search Restaurant
    public class SearchRestaurant
    {
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
    }

    //List All Restaurants For Landing Page
    public class GetAllRestaurants
    {
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantImage { get; set; }
        public string RestaurantType { get; set; }
        public string CuisineType { get; set; }

    }
    //Restaurant Location
    public class RestaurantLocation
    {
        public string City { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class RestaurantWorkingHours
    {
        public string DayOfWeek { get; set; }
        public TimeSpan TimeOpen { get; set; }
        public TimeSpan TimeClosed { get; set; }
    }
    //Restaurant type by id and all restaurant types
    public class RestaurantTypeDTO
    {
        public int RestaurantTypeID { get; set; }
        public string RestaurantType { get; set; }
    }

    public class CuisineTypeDTO
    {
        public int CuisineTypeID { get; set; }
        public string CuisineTypeName { get; set; }
    }

    //Create new Restaurant
    public class CreateRestaurant
    {
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantImage { get; set; }
        public string RestaurantTelephone { get; set; }
        public string RestaurantEmail { get; set; }
        public string RestaurantPassword { get; set; }
        public int RestaurantTypeID { get; set; }
        public int CuisineTypeID { get; set; }
        public ICollection<RestaurantLocation> RestaurantLocation { get; set; }

    }

    //Info about one restaurant
    public class RestaurantInfo
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantImage { get; set; }
        public string RestaurantTelephone { get; set; }
        public string RestaurantType { get; set; }
        public string CuisineType { get; set; }
        public ICollection<RestaurantMenuInfo> RestaurantMenu { get; set; }
        public ICollection<RestaurantLocationInfo> RestaurantLocation { get; set; }
        public ICollection<RestaurantWorkingHours> RestaurantWorkingHours { get; set; }
    }

    //Info about Restaurant Location
    public class RestaurantLocationInfo
    {
        public string City { get; set; }
        public string Address { get; set; }
    }

    //Restaurant Information for login
    public class RestaurantLogin
    {
        public int RestaurantID { get; set; }
        public string RestaurantEmail { get; set; }
        public string RestaurantPassword { get; set; }
    }

    //Restaurant menu INFO
    public class RestaurantMenuInfo
    {
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public string FoodType { get; set; }
    }
    
    //For create new Restaurant
    public class RestaurantMenu
    {
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public int FoodTypeID { get; set; }
    }

    //For Edit Restaurant
    public class EditRestaurantMenu
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public int CategoryID { get; set; }
        public int RestaurantId { get; set; }
    }

    //Create Restaurant Type
    public class CreateRestaurantType
    {
        public string RestaurantType { get; set; }
    }

    //Create Cuisine Type
    public class CreateCuisineType
    {
        public string CuisneType { get; set; }
    }

    //Delete Restaurant
    public class DeleteRestaurant
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
    }

    //Edit Restaurant
    public class EditRestaurant
    {
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantImage { get; set; }
        public string RestaurantTelephone { get; set; }
    }

    public class EditRestaurantLocation
    {
        public int LocationId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RestaurantId { get; set; }
    }

    //Get Working Hours by ID
    public class GetWorkingHoursByID
    {
        public int WorkingHoursID { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan TimeOpen { get; set; }
        public TimeSpan TimeClosed { get; set; }
    }

    //List of Working Hours by Id
    public class ListOfWorkingDays
    {
        public string DayOfWeek { get; set; }
        public TimeSpan TimeOpen { get; set; }
        public TimeSpan TimeClosed { get; set; }
        public int RestaurantId { get; set; }
    }
    //Edit Working Hours
    public class EditWorkingHours
    {
        public int WorkingHoursId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan TimeOpen { get; set; }
        public TimeSpan TimeClosed { get; set; }
    }

    //Delete Working Hours
    public class DeleteWorkingHours
    {
        public int WorkingHoursId { get; set; }
    }

    //Post Working Hours
    public class PostWorkingHours
    {
        public WeekDays DayOfWeek { get; set; }
        public TimeSpan TimeOpen { get; set; }
        public TimeSpan TimeClosed { get; set; }
    }
}
