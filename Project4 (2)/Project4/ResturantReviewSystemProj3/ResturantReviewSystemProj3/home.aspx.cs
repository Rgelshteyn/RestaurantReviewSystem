using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using System.Data;
using System.Data.SqlClient;
using ResturantReviewSystemClassLibrary;

namespace ResturantReviewSystemProj3
{
    public partial class home : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButtonClicked(object sender, EventArgs e)
        {
            string username_entered = txtUsername.Text.Trim();
            string password_entered = txtPassword.Text.Trim();


            //User user = new User("",username_entered,password_entered);
            User user = new User();
            bool isAuthenticated = user.AuthenticateUser(username_entered, password_entered);
            

            if (isAuthenticated)
            {
                Session["LoggedUser"] = user;
                Response.Redirect("dashboard.aspx");
            }
            else
            {
                lblErrorMessage.Text = "Invalid username or password.";
            }
        }

        protected void btnCreateAcount_Click(object sender, EventArgs e)
        {
            Response.Redirect("createAccount.aspx");
        }

        protected void btnGuest_Click(object sender, EventArgs e)
        {
            User guestUser = new User("Guest"); 
            guestUser.UserType = "Guest";
            Session["LoggedUser"] = guestUser;
            Response.Redirect("dashboard.aspx");
            
        }
    }
}