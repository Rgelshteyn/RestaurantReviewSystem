using ResturantReviewSystemClassLibrary;
using SOAP;
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
    public partial class restuarant : System.Web.UI.Page
    {
        Restaurant restaurant = new Restaurant();
        Category category = new Category();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            User loggedInUser = (User)Session["LoggedUser"];
            string usertype = loggedInUser.UserType;

            if (loggedInUser.UserType == "Reviewer")
            {
                txtDeleteRestaurantID.Visible = false;
                btnDeleteRestaurant.Visible = false;
                gvRestaurants.Columns[gvRestaurants.Columns.Count - 1].Visible = false;
            }
         

            if (!IsPostBack)
            {
                category.PopulateCategoriesDropDownList(ddlCategory);
                restaurant.LoadRepresentatives(ddlRepresentatives);
                category.PopulateCategoriesChkBox(cblCategories);
                BindRestaurantsToGridView();

            }

        }

        private void BindRestaurantsToGridView()
        {
            WebRequest request = WebRequest.Create("http://localhost:4193/RestaurantsApi/Restaurant/GetRestaurants");
            WebResponse response = request.GetResponse();
            Stream dataSteam = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataSteam);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Restaurant> restaurants = js.Deserialize<List<Restaurant>>(data);


            gvRestaurants.DataSource = restaurants;
            gvRestaurants.DataBind();
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtRestaurantName.Text;
            string address = txtAddress.Text;
            int catid = int.Parse(ddlCategory.SelectedValue);
            int repid = int.Parse(ddlRepresentatives.SelectedValue);
            string imageurl = txtImageUrl.Text;

            int result = restaurant.AddRestaurant(name, address, catid, repid, imageurl);

            if (result > 0)
            {
                lblMessage.Text = "Restaurant added successfully!";
                BindRestaurantsToGridView();
            }
            else
            {
                lblMessage.Text = "Failed to add the restaurant";
            }
        }
        

        protected void gvRestaurants_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRestaurants.EditIndex = e.NewEditIndex;
            BindRestaurantsToGridView();

        }
        protected void gvRestaurants_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int rowIndex = e.RowIndex;

            string restaurantID = gvRestaurants.DataKeys[rowIndex]["RestaurantID"].ToString();
            string name = ((TextBox)gvRestaurants.Rows[rowIndex].FindControl("TextBoxName")).Text;
            string address = ((TextBox)gvRestaurants.Rows[rowIndex].FindControl("txtAddressEdit")).Text.Trim();
            int categoryID = int.Parse(((DropDownList)gvRestaurants.Rows[rowIndex].FindControl("ddlCategoryEdit")).SelectedValue);
            int representativeID = int.Parse(((DropDownList)gvRestaurants.Rows[rowIndex].FindControl("ddlRepresentativesEdit")).SelectedValue);
            string imageUrl = ((TextBox)gvRestaurants.Rows[rowIndex].FindControl("TextBoxImageUrl")).Text.Trim();

            bool updated = restaurant.UpdateRestaurantDetails(int.Parse(restaurantID), name, address, categoryID, representativeID, imageUrl);

            if (updated)
            {
                lblStatus.Text = "Restaurant details updated successfully!";
                gvRestaurants.EditIndex = -1;
                BindRestaurantsToGridView();
            }
            else
            {
                lblStatus.Text = "Failed to update restaurant details.";
            }
        }

        protected void gvRestaurants_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRestaurants.EditIndex = -1;
            BindRestaurantsToGridView();
        }

        protected void btnDeleteRestaurant_Click(object sender, EventArgs e)
        {
            int restaurantID;

            if (string.IsNullOrEmpty(txtDeleteRestaurantID.Text))
            {
                lblStatus.Text = "Please enter a Restaurant ID.";
                return;
            }

            if (int.TryParse(txtDeleteRestaurantID.Text, out restaurantID))
            {
                bool deleted = restaurant.DeleteRestaurant(restaurantID);

                if (deleted)
                {
                    lblStatus.Text = "Restaurant deleted successfully!";
                    txtDeleteRestaurantID.Text = "";
                    BindRestaurantsToGridView();
                }
                else
                {
                    lblStatus.Text = "Failed to delete the restaurant. Ensure the ID is correct.";
                    txtDeleteRestaurantID.Text = "";
                }
            }
            else
            {
                lblStatus.Text = "Please enter a valid Restaurant ID.";
                txtDeleteRestaurantID.Text = "";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedCategories = string.Join(",", cblCategories.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value));

            DataTable restaurants = restaurant.SearchRestaurants(selectedCategories);

            gvRestaurants.DataSource = restaurants;
            gvRestaurants.DataBind();
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }

        protected void gvRestaurants_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlCategories = (DropDownList)e.Row.FindControl("ddlCategoryEdit"); 

                    if (ddlCategories != null)
                    {
                        category.PopulateCategoriesDropDownList(ddlCategories);
                       
                    }

                    DropDownList ddlRepresentatives = (DropDownList)e.Row.FindControl("ddlRepresentativesEdit");
                    if (ddlRepresentatives != null)
                    {
                        restaurant.LoadRepresentatives(ddlRepresentatives);

                    }
                }
            }
        }
        protected void btnSearchGrid_Click(object sender, EventArgs e)
        {
            try
            {
                string searchKeyword = txtSearch.Text.Trim();

                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    SOAP.SOAP soapService = new SOAP.SOAP();

                    DataTable searchedRestaurants = soapService.SearchRestaurantsByName(searchKeyword);

                    gvRestaurants.DataSource = searchedRestaurants;
                    gvRestaurants.DataBind();
                }
                else
                {
                    BindRestaurantsToGridView();
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "An error occurred during the search.";
                throw ex;
            }
        }

        protected void btnResetGV_Click(object sender, EventArgs e)
        {
            BindRestaurantsToGridView();
        }
    }
}