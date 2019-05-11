using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.IO;
using System.Web.Configuration;
using System.Web;
using VMMAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Globalization;
using DataUtilityLayer;

namespace VMMAPI.Controllers
{
    public class UserLoginController : ApiController
    {

        [HttpPost]
        public object UserLogin([FromBody]UserAuthParams userAuthParams)
        {
            string conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlCommand cmdObj;
            DataUtility du = new DataUtility();
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "[LoginMaster]";


            cmdObj.Parameters
                .Add(new SqlParameter("@UserId", SqlDbType.NVarChar))
                .Value = userAuthParams.authToken;


            cmdObj.Parameters
               .Add(new SqlParameter("@Password", SqlDbType.NVarChar))
               .Value = userAuthParams.Password;
            DataTable dtList = new DataTable();


            List<UserDetails> UserDetailsList = new List<UserDetails>();
            dtList = du.GetDataTableWithProc(cmdObj);

            if (dtList.Rows.Count > 0)
            {
                
                UserDetails obj = new UserDetails();
                obj.UserId = dtList.Rows[0][0].ToString();
                
                UserDetailsList.Add(obj);
                return UserDetailsList;
            }

            else
            {
                var msg = new HttpResponseMessage(HttpStatusCode.NoContent) { ReasonPhrase = "No List Found" };
                return msg;

            }

        }
    }
}
