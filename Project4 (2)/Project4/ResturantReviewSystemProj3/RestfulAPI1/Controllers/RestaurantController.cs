using Microsoft.AspNetCore.Mvc;
using ResturantReviewSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Xml.Linq;
using Utilities;

namespace RestfulAPI1.Controllers
{
    [Route("RestaurantsApi/[controller]")]
    public class RestaurantController : Controller
    {
        [HttpGet]
        [Route("GetRestaurants")]
        public List<Restaurant> GetAllRestaurants()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllRestaurants";

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            List<Restaurant> restaurants = new List<Restaurant>();

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                Restaurant rest = new Restaurant
                {
                    RestaurantID = int.Parse(record["RestaurantID"].ToString()),
                    Name = record["name"].ToString(),
                    Address = record["address"].ToString(),
                    CategoryName = record["CategoryName"].ToString(),
                    RepresentativeName = record["RepresentativeName"].ToString(),
                    PhoneNumber = record["PhoneNumber"].ToString(),
                    Email = record["Email"].ToString(),
                    ImageUrl = record["ImageUrl"].ToString()
                };
                restaurants.Add(rest);
            }

            return restaurants;
        }

        [HttpGet]
        [Route("GetReps")]
        public List<Restaurant> GetAllReps()
        {
            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetRepresentatives";

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            List<Restaurant> reps = new List<Restaurant>();

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                Restaurant rep = new Restaurant
                {
                    RepresentativeID = int.Parse(record["RepresentativeID"].ToString()),
                    RepresentativeName = record["RepresentativeName"].ToString(),
                };
                reps.Add(rep);
            }

            return reps;
        }
        [HttpGet]
        [Route("GetCategories")]
        public List<Category> GetAllCategories()
        {
            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetCategories";

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            List<Category> categories = new List<Category>();

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                Category category = new Category
                {
                    CategoryID = int.Parse(record["CategoryID"].ToString()),
                    CategoryName = record["CategoryName"].ToString()
                };
                categories.Add(category);
            }

            return categories;
        }

        [HttpDelete("DeleteRestaurant/{restaurantID}")]
        public IActionResult DeleteRestaurant(int restaurantID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteRestaurant";
            objCommand.Parameters.AddWithValue("@RestaurantID", restaurantID);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return Ok("Restaurant deleted successfully");
            }
            else
            {
                return StatusCode(400, "Failed to delete restaurant");
            }

        }

        [HttpPost("MakeRestaurant")]
        public IActionResult MakeRestaurantPost([FromBody] Restaurant restaurant)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddRestaurant";

            objCommand.Parameters.AddWithValue("@Name", restaurant.RestaurantName);
            objCommand.Parameters.AddWithValue("@Address", restaurant.Address);
            objCommand.Parameters.AddWithValue("@CategoryID", restaurant.CatergoryID);
            objCommand.Parameters.AddWithValue("@RepresentativeID", restaurant.RepresentativeID);
            objCommand.Parameters.AddWithValue("@ImageUrl", restaurant.ImageUrl);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return Ok("Restaurant successfully created");
            }
            else
            {
                return StatusCode(400, "Failed to create the Restaurant");
            }
        }

        [HttpPut("UpdateRestaurant")]
        public IActionResult UpdatePutRest([FromBody] Restaurant restaurant)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateRestaurants";

            objCommand.Parameters.AddWithValue("@RestaurantID", restaurant.RestaurantID);
            objCommand.Parameters.AddWithValue("@name", restaurant.RestaurantName);
            objCommand.Parameters.AddWithValue("@address", restaurant.Address);
            objCommand.Parameters.AddWithValue("@CategoryID", restaurant.CatergoryID);
            objCommand.Parameters.AddWithValue("@RepresentativeID", restaurant.RepresentativeID);
            objCommand.Parameters.AddWithValue("@ImageUrl", restaurant.ImageUrl);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return Ok("Restaurant Updated");
            }
            else
            {
                return StatusCode(400, "Falled to update restaurant");
            }
        }





    }

}
