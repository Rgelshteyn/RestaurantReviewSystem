using ResturantReviewSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace ResturantReviewSystemProj3
{
    public partial class reviews : System.Web.UI.Page
    {
        Review review = new Review();
        Category cat = new Category();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            User loggedInUser = (User)Session["LoggedUser"];
            string usertype = loggedInUser.UserType;

            if (loggedInUser.UserType == "Restaurant Representative")
            {
                btnSubmitReview.Visible = false;
                btnDelete.Visible = false;
                txtDeleteReview.Visible = false;
                lblSearch.Visible = false;
                gvReviews.Columns[gvReviews.Columns.Count - 1].Visible = false;
            }
            else if (loggedInUser.UserType == "Guest" || loggedInUser.UserType == null)
            {
                btnSubmitReview.Visible = false;
                btnDelete.Visible = false;
                txtDeleteReview.Visible = false;
                lblSearch.Visible = false;
                btnRestaurantPage.Visible = false;
                txtComments.Visible = false;
                lblredirect.Visible = false;
                gvReviews.Columns[gvReviews.Columns.Count - 1].Visible = false;
            }

            if (!IsPostBack)
            {
                cat.PopulateCategoriesChkBox(cblCategories);
                review.LoadRestaurants(ddlRestaurants);
                BindReviewsToGridView();
            }


        }

        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlRestaurants.SelectedValue))
            {
                lblMessage.Text = "Please select a restaurant.";
                return;
            }

            if (string.IsNullOrEmpty(txtComments.Text.Trim()))
            {
                lblMessage.Text = "Comments cannot be empty.";
                return;
            }

            string selectedRestaurantID = ddlRestaurants.SelectedValue;
            string comments = txtComments.Text;
            int foodQualityRating = int.Parse(ddlFoodQuality.SelectedValue);
            int serviceRating = int.Parse(ddlServiceRating.SelectedValue);
            int atmosphereRating = int.Parse(ddlAtmosphereRating.SelectedValue);
            int priceLevelRating = int.Parse(ddlPriceLevelRating.SelectedValue);

            int result = review.AddReview(selectedRestaurantID, comments, foodQualityRating, serviceRating, atmosphereRating, priceLevelRating);

            if (result > 0)
            {
                lblMessage.Text = "Restaurant added successfully!";
                BindReviewsToGridView();
            }
            else
            {
                lblMessage.Text = "Failed to add the restaurant";
            }

        }

     



        private void BindReviewsToGridView()
        {
            WebRequest request = WebRequest.Create("http://localhost:4193/ReviewsApi/Review/");
            WebResponse response= request.GetResponse();
            Stream dataSteam = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataSteam);
            string data = reader.ReadToEnd();
            reader.Close(); 
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Review> reviews = js.Deserialize<List<Review>>(data);


            gvReviews.DataSource = reviews;
            gvReviews.DataBind();

            
        }

        protected void gvReviews_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvReviews.EditIndex = e.NewEditIndex;
            BindReviewsToGridView();
        }

        protected void gvReviews_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvReviews.Rows[e.RowIndex];
            int reviewId = Convert.ToInt32(gvReviews.DataKeys[e.RowIndex].Value);
            int foodQuality = int.Parse(((TextBox)row.FindControl("TextBoxFoodQuality")).Text);
            int service = int.Parse(((TextBox)row.FindControl("TextBoxService")).Text);
            int atmosphere = int.Parse(((TextBox)row.FindControl("TextBoxAtmosphere")).Text);
            int price = int.Parse(((TextBox)row.FindControl("TextBoxPrice")).Text);
            string comment = ((TextBox)row.FindControl("TextBoxComment")).Text;

            
            int updated = review.UpdateReviews(reviewId, foodQuality, service, atmosphere, price, comment);
            if (updated > 0)
            {
                lblStatus.Text = "Review updated successfully!";
                gvReviews.EditIndex = -1;
                BindReviewsToGridView();
            }
            else
            {
                lblStatus.Text = "Failed to update review.";
            }
           

            //gvReviews.EditIndex = -1;
            //BindReviewsToGridView();
        }

        protected void gvReviews_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvReviews.EditIndex = -1;
            BindReviewsToGridView();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int reviewID;

            if (string.IsNullOrEmpty(txtDeleteReview.Text))
            {
                lblStatus.Text = "Please enter a Review ID.";
                return;
            }

            if (int.TryParse(txtDeleteReview.Text, out reviewID))
            {
                int deleted = review.DeleteReview(reviewID);

                if (deleted > 0)
                {
                    lblStatus.Text = "Review deleted successfully!";
                    txtDeleteReview.Text = "";
                    BindReviewsToGridView();
                }
                else
                {
                    lblStatus.Text = "Failed to delete the review. Ensure the ID is correct.";
                }
            }
            else
            {
                lblStatus.Text = "Please enter a valid Review ID.";
                txtDeleteReview.Text = "";
            }
        }

        protected void btnRestaurantPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("restaurant.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }

        protected void btnSearchCat_Click(object sender, EventArgs e)
        {
            string selectedCategories = string.Join(",", cblCategories.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value));

            DataTable reviews = review.SearchReviewsByCategory(selectedCategories);

            gvReviews.DataSource = reviews;
            gvReviews.DataBind();
        }
        protected void btnSearchGrid_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    string searchKeyword = txtSearch.Text.Trim();

                    if (!string.IsNullOrEmpty(searchKeyword))
                    {
                        SOAP.SOAP searchService = new SOAP.SOAP();

                        DataTable searchedReviews = searchService.SearchReviewsByName(searchKeyword);

                        gvReviews.DataSource = searchedReviews;
                        gvReviews.DataBind();
                    }
                    else
                    {
                        BindReviewsToGridView();
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "An error occurred during the review search.";
                }
            }
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            BindReviewsToGridView();
        }
    }
}