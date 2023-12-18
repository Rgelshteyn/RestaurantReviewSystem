using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Utilities;

namespace ResturantReviewSystemClassLibrary
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; } 
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public int FoodQualityRating { get; set; }
        public int ServiceRating { get; set; }
        public int AtmosphereRating { get; set; }
        public int PriceLevelRating { get; set; }
        public string Comments { get; set; }
        public string Address { get; set; }
        public string RepresentativeName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }

        

        public void LoadRestaurants(DropDownList ddlRestaurants)
        {
            WebRequest request = WebRequest.Create("http://localhost:4193/ReviewsApi/Review/GetRestaurants");
            WebResponse response = request.GetResponse();

            Stream DataStream = response.GetResponseStream();
            StreamReader Reader = new StreamReader(DataStream);
            String data = Reader.ReadToEnd();
            Reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Review> reviews = js.Deserialize<List<Review>>(data);

            ddlRestaurants.DataSource = reviews;
            ddlRestaurants.DataTextField = "name";
            ddlRestaurants.DataValueField = "RestaurantID";
            ddlRestaurants.DataBind();
        }


        public int AddReview(string restaurantID, string comments, int foodQualityRating, int serviceRating, int atmosphereRating, int priceLevelRating)
        {
            try
            {
                Review review = new Review()
                {
                    RestaurantID = int.Parse(restaurantID),
                    Comments = comments,
                    FoodQualityRating = foodQualityRating,
                    ServiceRating = serviceRating,
                    AtmosphereRating = atmosphereRating,
                    PriceLevelRating = priceLevelRating
                };

                JavaScriptSerializer js = new JavaScriptSerializer();
                String jsonReviewer = js.Serialize(review);

                WebRequest request = WebRequest.Create("http://localhost:4193/ReviewsApi/Review/MakeReview");
                request.Method = "POST";
                request.ContentLength = jsonReviewer.Length;
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonReviewer);
                }

                using (WebResponse response = request.GetResponse())
                {
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    return 1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

        }


        public int UpdateReviews(int reviewId, int foodQuality, int service, int atmosphere, int price, string comment)
        {
            try
            {
                Review review = new Review()
                {
                    ReviewID = reviewId,
                    FoodQualityRating = foodQuality,
                    ServiceRating = service,
                    AtmosphereRating = atmosphere,
                    PriceLevelRating = price,
                    Comments = comment,
                };
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonReviewer = js.Serialize(review);

                WebRequest request = WebRequest.Create("http://localhost:4193/ReviewsApi/Review/UpdateReview");
                request.Method = "PUT"; 
                request.ContentLength = jsonReviewer.Length;
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonReviewer);
                }

                using (WebResponse response = request.GetResponse())
                {
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    return 1;
                }


            }
            catch (Exception ex)
            {
                return -1; 
            }
            
        }

        public DataTable SearchReviewsByCategory(string selectedCategories)
        {
            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetReviewsByCategories";
                objCommand.Parameters.AddWithValue("@Categories", selectedCategories);

                return objDB.GetDataSetUsingCmdObj(objCommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SearchReviewsByName(string searchKeyword)
        {
            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "SearchReviewsByName";
                objCommand.Parameters.AddWithValue("@SearchKeyword", searchKeyword);

                return objDB.GetDataSetUsingCmdObj(objCommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int DeleteReview(int reviewId)
        {
            try
            {
                WebRequest request = WebRequest.Create($"http://localhost:4193/ReviewsApi/Review/DeleteReview/{reviewId}");
                request.Method = "DELETE";
                request.ContentType = "application/json";

                using (WebResponse response = request.GetResponse())
                {
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    return 1;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
