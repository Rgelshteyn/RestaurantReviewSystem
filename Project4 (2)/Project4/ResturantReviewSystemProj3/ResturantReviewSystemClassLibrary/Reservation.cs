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
using Utilities;

namespace ResturantReviewSystemClassLibrary
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int RestaurantID { get; set; }
        public int UserID { get; set; }
        public string ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        public string RestaurantName { get; set; } 
        public string ImageUrl { get; set; }

        public int MakeReservation(int restaurantID, int userID, DateTime reservationDate, TimeSpan reservationTime)
        {
            try
            {
                Reservation reserve = new Reservation()
                {
                    RestaurantID = restaurantID,
                    UserID = userID,
                    ReservationDate = reservationDate.ToString("yyyy-MM-dd"),
                    ReservationTime = reservationTime.ToString(@"hh\:mm"), 
                };
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonReservation = js.Serialize(reserve);

                WebRequest request = WebRequest.Create("http://localhost:4193/ReservationsApi/Reservation/MakeReservations");
                request.Method = "POST";
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonReservation);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseData = reader.ReadToEnd();
                        if (responseData.Contains("successfully")) 
                        {
                            return 1;
                        }
                        else
                        {
                            return -2; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                return -1;
            }
        }


        public bool DeleteReservation(int reservationID)
        {
            try
            {
                WebRequest request = WebRequest.Create($"http://localhost:4193/ReservationsApi/Reservation/DeleteReservation/{reservationID}");
                request.Method = "DELETE";
                request.ContentType = "application/json";

                WebResponse response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseData = reader.ReadToEnd();
                    reader.Close(); 
                    response.Close();
                    return responseData.Contains("successfully");
                }


            }
            catch (Exception ex)
            {
                throw ex;
               
            }
        }

        public bool UpdateReservation(int reservationID, DateTime updatedDate, TimeSpan updatedTime)
        {
            try
            {
                Reservation reservation = new Reservation()
                {
                    ReservationID = reservationID,
                    ReservationDate = updatedDate.ToString("yyyy-MM-dd"),
                    ReservationTime = updatedTime.ToString(@"hh\:mm"),
                };

                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonReservation = js.Serialize(reservation);

                WebRequest request = WebRequest.Create("http://localhost:4193/ReservationsApi/Reservation/UpdateReservation");
                request.Method = "PUT";
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonReservation);
                    writer.Flush();
                    writer.Close();
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseData = reader.ReadToEnd();
                        return responseData.Contains("successfully");
                    }
                }
            }
            catch (Exception ex)
            {
              
                throw ex; 
            }
        }


    }
}
