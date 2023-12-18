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
using System.Xml.Linq;
using Utilities;

namespace ResturantReviewSystemClassLibrary
{
    public class Restaurant
    {

        public int ReviewID { get; set; }
        public int CatergoryID { get; set; }
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
        public int RepresentativeID { get; set; }

        public int AddRestaurant(string name, string address, int categoryId, int representativeId, string imageUrl)
        {
            try
            {
                Restaurant restaurant = new Restaurant()
                {
                    RestaurantName = name,
                    Address = address,
                    CatergoryID = categoryId,
                    RepresentativeID = representativeId,
                    ImageUrl = imageUrl
                };

                JavaScriptSerializer js = new JavaScriptSerializer();
                String jsonRest = js.Serialize(restaurant);

                WebRequest request = WebRequest.Create("http://localhost:4193/RestaurantsApi/Restaurant/MakeRestaurant");
                request.Method = "POST";
                request.ContentLength = jsonRest.Length;
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonRest);
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
                //throw ex;
            }
        }
        public DataTable SearchRestaurantsByName(string searchKeyword)
        {
            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "SearchRestaurantsByName";
                objCommand.Parameters.AddWithValue("@SearchKeyword", searchKeyword);

                return objDB.GetDataSetUsingCmdObj(objCommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadRepresentatives(DropDownList ddlRepresentatives)
        {

            WebRequest request = WebRequest.Create("http://localhost:4193/RestaurantsApi/Restaurant/GetReps");
            WebResponse response = request.GetResponse();

            Stream DataStream = response.GetResponseStream();
            StreamReader Reader = new StreamReader(DataStream);
            String data = Reader.ReadToEnd();
            Reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Restaurant> reps = js.Deserialize<List<Restaurant>>(data);

            ddlRepresentatives.DataSource = reps;
            ddlRepresentatives.DataTextField = "RepresentativeName";
            ddlRepresentatives.DataValueField = "RepresentativeID";
            ddlRepresentatives.DataBind();
        }

        public bool DeleteRestaurant(int restaurantID)
        {
            try
            {
                WebRequest request = WebRequest.Create($"http://localhost:4193/RestaurantsApi/Restaurant/DeleteRestaurant/{restaurantID}");
                request.Method = "DELETE";
                request.ContentType = "application/json";

                using (WebResponse response = request.GetResponse())
                {
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    return true;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateRestaurantDetails(int restaurantID, string name, string address, int categoryID, int representativeID, string imageUrl)
        {          
            try
            {
                Restaurant restaurant = new Restaurant()
                {
                    RestaurantID = restaurantID,
                    RestaurantName = name,
                    Address = address,
                    CatergoryID = categoryID,
                    RepresentativeID = representativeID,
                    ImageUrl = imageUrl
                };

                JavaScriptSerializer js = new JavaScriptSerializer();
                String jsonRest = js.Serialize(restaurant);

                WebRequest request = WebRequest.Create("http://localhost:4193/RestaurantsApi/Restaurant/UpdateRestaurant");
                request.Method = "PUT";
                request.ContentLength = jsonRest.Length;
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonRest);
                }

                using (WebResponse response = request.GetResponse())
                {
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                //throw ex;
            }
        }

        public DataTable SearchRestaurants(string selectedCategories)
        {
            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetRestaurantsByCategories";
                objCommand.Parameters.AddWithValue("@Categories", selectedCategories);

                return objDB.GetDataSetUsingCmdObj(objCommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
    }
}
