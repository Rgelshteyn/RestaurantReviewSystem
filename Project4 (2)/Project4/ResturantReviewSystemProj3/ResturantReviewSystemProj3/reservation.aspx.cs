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
    public partial class reservation : System.Web.UI.Page
    {
        Review review = new Review();
        Reservation reservations = new Reservation();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            User loggedInUser = (User)Session["LoggedUser"];
            string usertype = loggedInUser.UserType;

            if (loggedInUser.UserType == "Restaurant Representative")
            {
                btnSubmitReservation.Visible = false;
            
            }
            else if (loggedInUser.UserType == "Reviewer")
            {
                txtReservationID.Visible = false;
                btnDelete.Visible = false;
                gvReservations.Columns[gvReservations.Columns.Count - 1].Visible = false;
            }

            if (!IsPostBack)
            {
                review.LoadRestaurants(ddlReservationRestaurant);
                BindReservationsToGridView();
            }

        }

        private void BindReservationsToGridView()
        {
            User loggedInUser = (User)Session["LoggedUser"];
            int userId = loggedInUser.UserID;
            string userType = loggedInUser.UserType;

            string url = $"http://localhost:4193/ReservationsApi/Reservation/GetReservations?userId={userId}&userType={userType}";
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Reservation> reservations = js.Deserialize<List<Reservation>>(data);

            gvReservations.DataSource = reservations;
            gvReservations.DataBind();
        }


        protected void btnSubmitReservation_Click(object sender, EventArgs e)
        {
            User loggedInUser = (User)Session["LoggedUser"];
            int restaurantID = int.Parse(ddlReservationRestaurant.SelectedValue);
            int userID = loggedInUser.UserID;

            DateTime selectedDate = calReservationDate.SelectedDate;
            TimeSpan selectedTime;
            if (!TimeSpan.TryParse(ddlReservationTime.SelectedItem.Text, out selectedTime))
            {
                lblReservationStatus.Text = "Invalid time format selected. Please select a valid time.";
                return;
            }
            int isSuccess = reservations.MakeReservation(restaurantID, userID, selectedDate, selectedTime);

            if (isSuccess > 0)
            {
                lblReservationStatus.Text = "Reservation made successfully!";
                BindReservationsToGridView();
            }
            else
            {
                lblReservationStatus.Text = "Error making reservation. Please try again.";
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int reservationID;
            if (int.TryParse(txtReservationID.Text, out reservationID))
            {
                bool success = reservations.DeleteReservation(reservationID);
                if (success)
                {
                    lblDeleteStatus.Text = "Reservation successfully deleted.";
                    BindReservationsToGridView();
                }
                else
                {
                    lblDeleteStatus.Text = "Error deleting reservation. Please check the ID and try again.";
                }
            }
            else
            {
                lblDeleteStatus.Text = "Invalid Reservation ID. Please enter a valid number.";
            }
        }
        protected void gvReservations_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvReservations.EditIndex = e.NewEditIndex;
            BindReservationsToGridView();
        }

        protected void gvReservations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvReservations.Rows[e.RowIndex];
            int reservationID = Convert.ToInt32(gvReservations.DataKeys[e.RowIndex].Value);


            Calendar cal = (Calendar)row.FindControl("calEditReservationDate");
            DropDownList ddl = (DropDownList)row.FindControl("ddlEditReservationTime");

            DateTime date = cal.SelectedDate;
            TimeSpan time = TimeSpan.Parse(ddl.SelectedValue);

            bool isSuccess = reservations.UpdateReservation(reservationID, date, time);

            if (isSuccess)
            {
                lblDeleteStatus.Text = "Reservation updated successfully!";
                gvReservations.EditIndex = -1;
                BindReservationsToGridView();
            }
            else
            {
                lblDeleteStatus.Text = "Error updating reservation. Please try again.";
            }

            //gvReservations.EditIndex = -1;
            //BindReservationsToGridView();
        }

        protected void gvReservations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvReservations.EditIndex = -1;
            BindReservationsToGridView();
        }

        protected DateTime ConvertToDate(object dateObj)
        {
            if (dateObj != null && DateTime.TryParse(dateObj.ToString(), out DateTime parsedDate))
            {
                return parsedDate;
            }
            return DateTime.MinValue; 
        }


        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }
    }
}