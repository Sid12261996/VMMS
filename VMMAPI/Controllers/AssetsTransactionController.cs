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
    public class AssetsTransactionController : ApiController
    {
        [HttpPost]
        public object AssetsTransaction([FromBody]UserAuthParams userAuthParams)
        {
            string conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlCommand cmdObj;
            DataUtility du = new DataUtility();
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "[GetAssetsTransaction]";


            cmdObj.Parameters
                .Add(new SqlParameter("@UserId", SqlDbType.NVarChar))
                .Value = userAuthParams.authToken;

            DataTable dtList = new DataTable();


            List<AssetsTransactionClass> TransactionDetailsList = new List<AssetsTransactionClass>();
            dtList = du.GetDataTableWithProc(cmdObj);
            if (dtList.Rows.Count > 0)
            {
                foreach (DataRow dtrow in dtList.Rows)
                {
                    AssetsTransactionClass obj = new AssetsTransactionClass();

                    obj.TransactionNo = dtrow["TransactionNo"].ToString();
                    obj.AssetsId = dtrow["AssestId"].ToString();
                    obj.CustomerId= dtrow["customerId"].ToString();
                    obj.VendorId = dtrow["vendorId"].ToString();
                    obj.Location = dtrow["Location"].ToString();
                    obj.AssestType= dtrow["assestType"].ToString();
                    obj.Address = dtrow["Address"].ToString();


                    TransactionDetailsList.Add(obj);
                }

                return TransactionDetailsList;
            }

            else
            {
                var msg = new HttpResponseMessage(HttpStatusCode.NoContent) { ReasonPhrase = "No List Found" };
                return msg;

            }

        }


        public class AssetsTransactionClass
        {
            public string TransactionNo { set; get; }
            public string AssetsId { set; get; }

            public string CustomerId { set; get; }
            public string VendorId { set; get; }
            public string Location { set; get; }

            public string AssestType { set; get; }

            public string Address { set; get; }
        }
    }
}
