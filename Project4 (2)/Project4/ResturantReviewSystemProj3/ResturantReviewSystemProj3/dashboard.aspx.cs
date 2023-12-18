using ResturantReviewSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResturantReviewSystemProj3
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User loggedInUser = (User)Session["LoggedUser"];

            if (loggedInUser.UserType == "Guest" || loggedInUser.UserType == null)
            {
                btnManageRestaurants.Visible = false;
                btnMakeReservation.Visible = false;
            }
        }

        protected void btnManageRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("restaurant.aspx");
        }

        protected void btnWriteReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("reviews.aspx");
        }

        protected void btnMakeReservation_Click(object sender, EventArgs e)
        {
            Response.Redirect("reservation.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
    }
}