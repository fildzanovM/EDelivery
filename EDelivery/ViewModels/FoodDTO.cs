using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.ViewModels
{
    //Create Food 
    public class FoodDTO
    {
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public int FoodTypeID { get; set; }
    }

    //Food Category for Create Food
    public class FoodCategoryByID
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    //Create new Food Category
    public class CreateFoodCategory
    {
        public string FoodCategory { get; set; }
    }

    //Get Food by ID
    public class GetFoodById
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public int FoodTypeID { get; set; }
        public int RestaurantID { get; set; }
    }

    //Edit Food
    public class EditFood
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public int CategoryId { get; set; }
    }

    //Delete Food
    public class DeleteFood
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
    }

    //Get Food By Restaurant Id
    public class GetFoodByRestaurantId
    {
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public string FoodCategory { get; set; }
    }
}
