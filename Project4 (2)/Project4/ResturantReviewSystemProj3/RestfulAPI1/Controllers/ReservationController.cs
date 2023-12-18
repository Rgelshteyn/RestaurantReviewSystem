using Microsoft.AspNetCore.Mvc;
using ResturantReviewSystemClassLibrary;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Utilities;
using System;

namespace RestfulAPI1.Controllers
{
    [ApiController]
    [Route("ReservationsApi/[controller]")]
    public class ReservationController : ControllerBase
    {

        [HttpPost("MakeReservations")]
        public IActionResult Post([FromBody] Reservation reservation)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "NewReservation";

            objCommand.Parameters.AddWithValue("@RestaurantID", reservation.RestaurantID);
            objCommand.Parameters.AddWithValue("@UserID", reservation.UserID);
            objCommand.Parameters.AddWithValue("@ReservationDate", DateTime.Parse(reservation.ReservationDate));
            objCommand.Parameters.AddWithValue("@ReservationTime", TimeSpan.Parse(reservation.ReservationTime));

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            if (result > 0)
            {
                return Ok("Reservation successfully created");
            }
            else
            {
                return StatusCode(400, "Failed to create the reservation");
            }
        }

        [HttpDelete("DeleteReservation/{reservationID}")]
        public IActionResult DeleteReservations(int reservationID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType= CommandType.StoredProcedure;
            objCommand.CommandText= "DeleteReservation";

            objCommand.Parameters.AddWithValue("@ReservationID", reservationID);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);
            if (result > 0)
            {
                return Ok("Reservation deleted successfully");
            }
            else
            {
                return StatusCode(400, "Failed to delete the reservation");
            }


        }

        [HttpGet("GetReservations")]
        public List<Reservation> GetReservations(int userId, string userType)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;

            if (userType == "Reviewer")
            {
                objCommand.CommandText = "GetReviewerReservations";
                objCommand.Parameters.AddWithValue("@UserID", userId);
            }
            else if (userType == "Restaurant Representative")
            {
                objCommand.CommandText = "GetAllReservations";
            }

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            List<Reservation> list = new List<Reservation>();

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                Reservation reservation = new Reservation
                {
                    ReservationID = int.Parse(record["ReservationID"].ToString()),
                    RestaurantID = int.Parse(record["RestaurantID"].ToString()),
                    ReservationDate = record["ReservationDate"].ToString(),
                    ReservationTime = record["ReservationTime"].ToString(),
                    RestaurantName = record["RestaurantName"].ToString(),
                    ImageUrl = record["ImageUrl"].ToString()
                };

                list.Add(reservation);
            }

            return list;
        }

        [HttpPut("UpdateReservation")]
        public IActionResult UpdateReservation([FromBody] Reservation reservation)
        {

             DBConnect objDB = new DBConnect();
             SqlCommand objCommand = new SqlCommand();

             objCommand.CommandType = CommandType.StoredProcedure;
             objCommand.CommandText = "UpdateReservation";

             objCommand.Parameters.AddWithValue("@ReservationID", reservation.ReservationID);
             objCommand.Parameters.AddWithValue("@UpdatedDate", reservation.ReservationDate);
             objCommand.Parameters.AddWithValue("@UpdatedTime", reservation.ReservationTime);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

             if (result > 0)
             {

                return Ok("Reservation updated successfully");
             }
             else
             {
                return StatusCode(400, "Failed to update the reservation");
             }
            
        }



    }
}
