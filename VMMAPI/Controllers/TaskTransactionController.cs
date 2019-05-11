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
    public class TaskTransactionController : ApiController
    {

        [HttpPost]
        public object TaskTransaction([FromBody]UserAuthParams userAuthParams)
        {
            string conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlCommand cmdObj;
            DataUtility du = new DataUtility();
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "[GetTaskTrandaction]";


            cmdObj.Parameters
                .Add(new SqlParameter("@UserId", SqlDbType.NVarChar))
                .Value = userAuthParams.authToken;

            DataTable dtList = new DataTable();


            List<TaskTransactionClass> TransactionDetailsList = new List<TaskTransactionClass>();
            dtList = du.GetDataTableWithProc(cmdObj);
            if (dtList.Rows.Count > 0)
            {
                foreach (DataRow dtrow in dtList.Rows)
                {
                    TaskTransactionClass obj = new TaskTransactionClass();
                    obj.ActivityId = dtrow["TaskId"].ToString();
                    obj.TransactionNo = dtrow["TransactionNo"].ToString();
                   
                   
                    obj.ActivityName = dtrow["TaskName"].ToString();
                    
                    obj.CustName = dtrow["CustName"].ToString();
                    obj.AssignToName = dtrow["VendorName"].ToString();
                    obj.AssestType = dtrow["assestType"].ToString();
                    obj.TaskType = dtrow["TaskType"].ToString();
                    obj.ContactNo = dtrow["ContactNo"].ToString();
                    obj.SpocName = dtrow["SpocName"].ToString();
                    obj.State = dtrow["State"].ToString();



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


        public class TaskTransactionClass
        {
            public string ActivityId { set; get; }
            public string TransactionNo { set; get; }


            public string ActivityName { set; get; }

            public string CustName { set; get; }
            public string AssignToName { set; get; }

            public string AssestType { set; get; }

            public string TaskType { set; get; }
            public string ContactNo { set; get; }
            public string SpocName { set; get; }
            public string State { set; get; }
        }
    }
}
