using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ResturantReviewSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using Utilities;

namespace RestfulAPI.Controllers
{
    [ApiController]
    [Route("ReviewsApi/[controller]")]
    public class ReviewController : ControllerBase
    {

        [HttpGet]
        public List<Review> Get()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllReviews";

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            List<Review> list = new List<Review>();

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                Review review = new Review
                {
                    ReviewID = int.Parse(record["ReviewID"].ToString()),
                    RestaurantName = record["RestaurantName"].ToString(),
                    ImageUrl = record["ImageUrl"].ToString(),
                    CategoryName = record["CategoryName"].ToString(),
                    FoodQualityRating = int.Parse(record["FoodQualityRating"].ToString()),
                    ServiceRating = int.Parse(record["ServiceRating"].ToString()),
                    AtmosphereRating = int.Parse(record["AtmosphereRating"].ToString()),
                    PriceLevelRating = int.Parse(record["PriceLevelRating"].ToString()),
                    Comments = record["Comments"].ToString()
                };

                list.Add(review);
            }

            return list;
        }

        [HttpGet]
        [Route("GetRestaurants")]
        public List<Review> GetAllRestaurants()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllRestaurants";

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            List<Review> restaurants = new List<Review>();

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                Review review = new Review
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
                restaurants.Add(review);
            }

            return restaurants;
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

        [HttpPost("MakeReview")]
        public IActionResult MakeReviewPost([FromBody] Review review)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "NewReview";
            objCommand.Parameters.AddWithValue("@RestaurantID", review.RestaurantID);
            objCommand.Parameters.AddWithValue("@FoodQualityRating", review.FoodQualityRating);
            objCommand.Parameters.AddWithValue("@ServiceRating", review.ServiceRating);
            objCommand.Parameters.AddWithValue("@AtmosphereRating", review.AtmosphereRating);
            objCommand.Parameters.AddWithValue("@PriceLevelRating", review.PriceLevelRating);
            objCommand.Parameters.AddWithValue("@Comments", review.Comments);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return Ok("Review successfully created");
            }
            else
            {
                return StatusCode(400, "Failed to create the Review");
            }
        }



        [HttpPut("UpdateReview")]
        public IActionResult UpdatePut([FromBody] Review review)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateReview";
            objCommand.Parameters.AddWithValue("@ReviewID", review.ReviewID);
            objCommand.Parameters.AddWithValue("@FoodQualityRating", review.FoodQualityRating);
            objCommand.Parameters.AddWithValue("@ServiceRating", review.ServiceRating);
            objCommand.Parameters.AddWithValue("@AtmosphereRating", review.AtmosphereRating);
            objCommand.Parameters.AddWithValue("@PriceLevelRating", review.PriceLevelRating);
            objCommand.Parameters.AddWithValue("@Comments", review.Comments);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return Ok("Review Updated");
            }
            else
            {
                return StatusCode(400, "Falled to update review");
            }
        }

        [HttpDelete("DeleteReview/{reviewId}")]
        public IActionResult DeleteReview(int reviewId)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteReviewById";
            objCommand.Parameters.AddWithValue("@ReviewID", reviewId);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return Ok("Review deleted successfully");
            }
            else
            {
                return StatusCode(400, "Failed to delete review");
            }

        }

    }
}
