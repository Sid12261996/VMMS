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
    public class GetAssestTypeController : ApiController
    {
        [HttpGet]
        public object GetAssestType()
        {
            string conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlCommand cmdObj;
            DataUtility du = new DataUtility();
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "[GetAssetType]";
            DataTable dtList = new DataTable();
            List<AssestType> AssestTypeList = new List<AssestType>();
            dtList = du.GetDataTableWithProc(cmdObj);
            if (dtList.Rows.Count > 0)
            {
                foreach (DataRow dtrow in dtList.Rows)
                {
                    AssestType obj = new AssestType();

                    obj.AssetsType = dtrow["AssetsType"].ToString();
                    
                    obj.AssestTypeId = dtrow["AssestTypeId"].ToString();



                    AssestTypeList.Add(obj);
                }

                return AssestTypeList;
            }

            else
            {
                var msg = new HttpResponseMessage(HttpStatusCode.NoContent) { ReasonPhrase = "No List Found" };
                return msg;

            }

        }
    }

    public class AssestType
    {
      public string   AssetsType { get; set; }

        public string AssestTypeId { get; set; }
    }
}
