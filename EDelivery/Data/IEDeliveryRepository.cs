using EDelivery.Models;
using EDelivery.ViewModels;
using System.Collections.Generic;

namespace EDelivery.Data
{
    public interface IEDeliveryRepository
    {
        //General
        bool SaveChanges();

        //Customer
        void CreateCustomer(Customer customer);
        Customer GetCustomerById (int customerID);
        List<Customer> GetAllCustomers();
        void DeleteCustomer(Customer customer);
        void EditCustomer(Customer model);

        //DeliveryCompany
        void CreateDeliveryCompany(DeliveryCompany deliveryCompany);
        void CreateDriver(Driver driver);
        DeliveryCompany GetDeliveryCompanyById(int deliveryCompanyId);
        void DeleteDeliveryCompany(DeliveryCompany deliveryCompany);
        Driver GetDriverById(int driverID);
        void EditDriver(Driver model);
        void DeleteDriver(Driver driver);
        void UpdateDeliveryCompany(DeliveryCompany model);

        //Restaurant
        Restaurant GetRestaurantByID(int restaurantID);
        Restaurant GetRestaurantInfo(int restaurantId);
        RestaurantType GetRestaurantTypeByID(int restaurantTypeID);
        List<RestaurantType> GetRestaurantTypes();
        CuisineType GetCuisineTypeById(int cuisineTypeID);
        List<CuisineType> GetAllCuisineTypes();
        List<Restaurant> GetAllRestaurants();
        void CreateNewRestaurant(Restaurant restaurant);
        void EditRestaurant(Restaurant model);
        void EditWorkingHours(WorkingHours model);
        List<WorkingHours> GetListOfWorkingDays(int restaurantId);
        void CreateRestaurantType(RestaurantType restaurantType);
        void CreateCuisineType(CuisineType cuisineType);
        void DeleteRestaurant(Restaurant restaurant);
        void DeleteWorkingHours(WorkingHours workingHours);
        void PostWorkingHours(WorkingHours workingHours);
        List<Restaurant> SearchRestaurant(string restaurantName);
        WorkingHours GetWorkingHoursByID(int workingHoursByID);
        List<Restaurant> GetRestaurantByCuisineId(int cuisineTypeId);
        List<Restaurant> GetRestaurantsByType(int restaurantTypeId);

        //Food
        FoodCategory GetFoodCategoryByID(int foodCategoryID);
        void CreateFood(Food food);
        void EditFood(Food model);
        List<FoodCategory> GetAllFoodCategories();
        void CreateFoodCategory(FoodCategory foodCategory);
        Food GetFoodById(int foodID);
        void DeleteFood(Food food);
        List<Food> GetFoodByRestaurant(int restaurantId);

        //Location
        void CreateLocation(Location location);
        Location GetLocationByID(int locationID);
        void DeleteLocation(Location location);
        void EditLocation(Location model);


        //Login
        string ValidateLogin(string email, string password);


        //Member
        Member GetMemberById(int memberId);
        List<ConfigurationDelivery> MemberTypes();

        //Order
        ConfigurationDelivery[] OrderProcess();
        void CreateOrder(FoodOrder newOrder);
        void DeleteOrder(FoodOrder model);
        FoodOrder GetOrderById(int foodOrderId);
    }
}