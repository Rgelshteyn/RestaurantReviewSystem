using ResturantReviewSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResturantReviewSystemProj3
{
    public partial class createAccount : System.Web.UI.Page
    {
        User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string userType = ddlUserType.SelectedValue;

            if (string.IsNullOrEmpty(username) || username.Length < 5)
            {
                lblError.Text = "Username should be at least 5 characters long.";
                return;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                lblError.Text = "Password should be at least 8 characters long.";
                return;
            }

            if (userType == "Reviewer")
            {
                user.CreateNewUser(username, password, userType);
                lblSuccess.Text = "Reviewer account created successfully!";
            }
            else if (userType == "Restaurant Representative")
            {
                string name = txtRepresentativeName.Text;
                string phone = txtPhoneNumber.Text;
                string email = txtEmail.Text;

                if (string.IsNullOrEmpty(name) || name.Length < 3)
                {
                    lblError.Text = "Representative name should be at least 3 characters long.";
                    return;
                }

                if (string.IsNullOrEmpty(phone) || phone.Length < 10)
                {
                    lblError.Text = "Please enter a valid phone number.";
                    return;
                }

                if (string.IsNullOrEmpty(email) || email.Length < 7)
                {
                    lblError.Text = "Please enter a valid email address.";
                    return;
                }
                user.CreateNewUser(username, password, userType, name, phone, email);
                lblSuccess.Text = "Representative account created successfully!";
            }

        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserType.SelectedValue == "Restaurant Representative")
                pnlRepresentativeDetails.Visible = true;
            else
                pnlRepresentativeDetails.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
    }
    
}