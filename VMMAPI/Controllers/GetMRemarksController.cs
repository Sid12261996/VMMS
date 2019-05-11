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
    public class GetMRemarksController : ApiController
    {
        [HttpPost]
        public object GetMRemarks([FromBody]UserAuthParams userAuthParams)
        {
            string conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlCommand cmdObj;
            DataUtility du = new DataUtility();
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "[GetManagerRemarks]";


            cmdObj.Parameters
                .Add(new SqlParameter("@UserId", SqlDbType.NVarChar))
                .Value = userAuthParams.authToken;

            DataTable dtList = new DataTable();


            List<TransactionDetailsM> TransactionDetailsList = new List<TransactionDetailsM>();
            dtList = du.GetDataTableWithProc(cmdObj);
            if (dtList.Rows.Count > 0)
            {
                foreach (DataRow dtrow in dtList.Rows)
                {
                    TransactionDetailsM obj = new TransactionDetailsM();

                    obj.TransactionNo = dtrow["TransactionNo"].ToString();
                    obj.AssetsId = dtrow["AssestId"].ToString();
                    obj.ActivityId = dtrow["Activity"].ToString();
                   
                    obj.Status= dtrow["Status"].ToString();
                    obj.Remarks = dtrow["ManagerRemarks"].ToString();



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
    }


    public class TransactionDetailsM
    {
        public string TransactionNo { set; get; }
    
        public string AssetsId { set; get; }
        public string ActivityId { set; get; }
      
        public string Status { set; get; }
        public string Remarks { set; get; }
    }
}
