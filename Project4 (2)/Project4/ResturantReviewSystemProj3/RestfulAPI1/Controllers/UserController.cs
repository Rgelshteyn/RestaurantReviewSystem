using Microsoft.AspNetCore.Mvc;
using ResturantReviewSystemClassLibrary;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.AccessControl;
using Utilities;

namespace RestfulAPI1.Controllers
{
    
    [Route("UsersApi/[controller]")]
    public class UserController : Controller
    {
        [HttpPost("AuthenticateUser")]
        public IActionResult AuthenticateUser([FromBody] User users)
        {
            DBConnect db = new DBConnect();
            SqlCommand obj = new SqlCommand();
            obj.CommandType = CommandType.StoredProcedure;
            obj.CommandText = "AuthenticateUserLogin";
            obj.Parameters.AddWithValue("@username", users.Username);
            obj.Parameters.AddWithValue("@password", users.Password);

            DataSet myDS = db.GetDataSetUsingCmdObj(obj);

            if (myDS.Tables[0].Rows.Count > 0)
            {
                 users.UserType = myDS.Tables[0].Rows[0]["UserType"].ToString();
                 users.UserID = Convert.ToInt32(myDS.Tables[0].Rows[0]["UserID"]);
                 users.Username = myDS.Tables[0].Rows[0]["Username"].ToString();
                
                return Ok(users);
            }
            else
            {
                return Unauthorized();
            }
           

        }

        [HttpPost("CreateNewUser")]
        public IActionResult CreateNewUser([FromBody] User user)
        {
            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "NewUser"
                };
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@Username", user.Username);
                objCommand.Parameters.AddWithValue("@Password", user.Password);
                objCommand.Parameters.AddWithValue("@UserType", user.UserType);

                int userId = objDB.DoUpdateUsingCmdObj(objCommand);

                if (user.UserType == "Restaurant Representative" && user.RepresentativeName != null && user.PhoneNumber != null && user.Email != null)
                {
                    SqlCommand repCommand = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "NewRepresentative"
                    };
                    repCommand.Parameters.Clear();
                    repCommand.Parameters.AddWithValue("@RepresentativeName", user.RepresentativeName);
                    repCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    repCommand.Parameters.AddWithValue("@Email", user.Email);

                    objDB.DoUpdateUsingCmdObj(repCommand);
                }

                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

    }

       
    
}
