using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VMMAPI.Controllers
{
    public class AssestController : Controller
    {
       // SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        // GET: Assest
        //public void UpdateIE()
        //{
        //    SqlCommand cmd = new SqlCommand("usp_IEUpdate", connection);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Id", ie.Id);
        //    cmd.Parameters.AddWithValue("@Question", ie.Question);
        //    cmd.Parameters.AddWithValue("@Answer", ie.Answer);
        //    connection.Open();
        //    int i = cmd.ExecuteNonQuery();
        //    connection.Close();
        //}
    }
}