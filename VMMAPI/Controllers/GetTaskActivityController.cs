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
    public class GetTaskActivityController : ApiController
    {

        [HttpPost]
        public object GetTaskActivity([FromBody]UserAuthParams userAuthParams)
        {
            string conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlCommand cmdObj;
            DataUtility du = new DataUtility();
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "[GetAllTaskTransaction]";


            cmdObj.Parameters
                .Add(new SqlParameter("@UserId", SqlDbType.NVarChar))
                .Value = userAuthParams.authToken;
         
            DataTable dtList = new DataTable();


            List<TransactionDetails> TransactionDetailsList = new List<TransactionDetails>();
            dtList = du.GetDataTableWithProc(cmdObj);
            if (dtList.Rows.Count > 0)
            {
                foreach (DataRow dtrow in dtList.Rows)
                {
                    TransactionDetails obj = new TransactionDetails();

                    obj.TransactionNo = dtrow["TransactionNo"].ToString();
                    obj.AssetsId = dtrow["AssestId"].ToString();
                    obj.ActivityId = dtrow["Activity"].ToString();
                    obj.ActivityName = dtrow["TaskName"].ToString();

                    obj.CustId = dtrow["CustName"].ToString();
                    obj.CustName = dtrow["CustName"].ToString();


                    obj.AssignToId = dtrow["AssignToNew"].ToString();
                    obj.AssignToName = dtrow["Name"].ToString();
                    obj.AssignmentDate= dtrow["AssignDateNew"].ToString();

                    obj.HostName = dtrow["HostName"].ToString();
                    obj.BImage = dtrow["BImage"].ToString();
                    obj.Aimage = dtrow["AImage"].ToString();
                    obj.completedby = dtrow["CompleteBy"].ToString();
                    obj.actiondate = dtrow["ActDate"].ToString();

                    obj.Description = dtrow["Description"].ToString();
                    obj.DND = dtrow["DND"].ToString();
                    obj.FaultyDesc = dtrow["FaultyDesc"].ToString();

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
}
