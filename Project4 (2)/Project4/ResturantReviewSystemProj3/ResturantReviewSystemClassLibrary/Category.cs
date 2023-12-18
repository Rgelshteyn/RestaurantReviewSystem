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
using System.Web.UI.WebControls;
using Utilities;

namespace ResturantReviewSystemClassLibrary
{
    public class Category
    {

        public int CategoryID {  get; set; }  
        public string CategoryName { get; set; }
        public void PopulateCategoriesDropDownList(DropDownList ddlCategory)
        {
            WebRequest request = WebRequest.Create("http://localhost:4193/RestaurantsApi/Restaurant/GetCategories");
            WebResponse response = request.GetResponse();

            Stream DataStream = response.GetResponseStream();
            StreamReader Reader = new StreamReader(DataStream);
            String data = Reader.ReadToEnd();
            Reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Category> categories = js.Deserialize<List<Category>>(data);

            ddlCategory.DataSource = categories;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
        }

        public void PopulateCategoriesChkBox(CheckBoxList cblCategories)
        {
            WebRequest request = WebRequest.Create("http://localhost:4193/ReviewsApi/Review/GetCategories");
            WebResponse response = request.GetResponse();

            Stream DataStream = response.GetResponseStream();
            StreamReader Reader = new StreamReader(DataStream);
            String data = Reader.ReadToEnd();
            Reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Category> categories = js.Deserialize<List<Category>>(data);

            cblCategories.DataSource = categories;
            cblCategories.DataTextField = "CategoryName";
            cblCategories.DataValueField = "CategoryID";
            cblCategories.DataBind();
        }
    }
}
