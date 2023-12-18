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
    public class User
    {
        public string UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }
        public string RepresentativeName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }



        public User(string userType, string username, string password, int userid)
        {
            UserType = userType;
            Username = username;
            Password = password;
            UserID = userid;
        }
        public User(string userType)
        {
            UserType = userType;
        }

        public User()
        {

        }

        public bool AuthenticateUser(string username, string password)
        {
            try {

                User user = new User()
                {
                    Username = username,
                    Password = password
                };

                JavaScriptSerializer js = new JavaScriptSerializer();
                String jsonUser = js.Serialize(user);

                WebRequest request = WebRequest.Create("http://localhost:4193/UsersApi/User/AuthenticateUser");
                request.Method = "POST";
                request.ContentLength = jsonUser.Length;
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonUser);
                }

                using (WebResponse response = request.GetResponse())
                {
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    User responseUser = js.Deserialize<User>(data);


                    this.UserType = responseUser.UserType;
                    this.UserID = responseUser.UserID;
                    this.Username = responseUser.Username;

                    return true; 
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int CreateNewUser(string username, string password, string userType, string representativeName = null, string phoneNumber = null, string email = null)
        {
            try
            {
                User user =  new User()
                {
                    Username = username,
                    Password = password,
                    UserType = userType,
                    RepresentativeName = representativeName,
                    PhoneNumber = phoneNumber,
                    Email = email
                };

                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonUser = js.Serialize(user);

                WebRequest request = WebRequest.Create("http://localhost:4193/UsersApi/User/CreateNewUser");
                request.Method = "POST";
                request.ContentLength = jsonUser.Length;
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonUser);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream theDataStream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(theDataStream))
                    {
                        string data = reader.ReadToEnd();

                        int userId = int.Parse(data);
                        return userId; 
                    }
                }
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }



    }
}
