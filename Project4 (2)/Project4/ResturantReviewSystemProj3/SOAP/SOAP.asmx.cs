using ResturantReviewSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SOAP
{
    /// <summary>
    /// Summary description for SOAP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SOAP : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable SearchRestaurantsByName(string searchKeyword)
        {
            try
            {
                Restaurant restaurant = new Restaurant();
                return restaurant.SearchRestaurantsByName(searchKeyword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public DataTable SearchReviewsByName(string searchKeyword)
        {
            try
            {
                Review review = new Review();
                return review.SearchReviewsByName(searchKeyword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
