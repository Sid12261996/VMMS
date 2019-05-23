
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
    public class UpdateTaskActivityController : ApiController
    {
        /// <summary>
        /// Upload Document.....
        /// </summary>      
        /// <returns></returns>
        [HttpPost]

        public async Task<object> MediaUpload()
        {
            // Check if the request contains multipart/form-data.
            DataUtility du = new DataUtility();

            DataTable dtList = new DataTable();
            string conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlCommand cmdObj;

            var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
            //access form data
            NameValueCollection formData = provider.FormData;

            IList<HttpContent> files = provider.Files;

            TransactionDetails obj = new TransactionDetails();
            obj.TransactionNo = formData["TransactionNo"].ToString();
            obj.ActivityId = formData["ActivityId"].ToString();
            obj.AssetsId = formData["AssetsId"].ToString();
            obj.actiondate = formData["actiondate"].ToString();
            obj.ActualAssetsId = formData["ActualAssetsId"].ToString();
            obj.DND = formData["DND"].ToString();
            obj.HostName = formData["HostName"].ToString();
            obj.Description = formData["Description"].ToString();
            obj.FaultyDesc = formData["FaultyDesc"].ToString();
            obj.ActualAssetsId = formData["ActualAssetsId"].ToString();
            obj.AssestTypeId = formData["AssestTypeId"].ToString();
            string BImagePath = string.Empty;
            string AImagePath = string.Empty;
            Guid id = Guid.NewGuid();
            var DMId = id;


            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {

                    string directoryName = AppDomain.CurrentDomain.BaseDirectory + "VMMdocs";

                    string dirFullPath = directoryName;
                    bool folderExists = Directory.Exists(directoryName + "\\" + obj.TransactionNo + "\\" + obj.AssestTypeId + "\\" + obj.AssetsId + "\\" + obj.ActivityId);

                    if (!folderExists)
                    {
                        Directory.CreateDirectory(directoryName + "\\" + obj.TransactionNo + "\\" + obj.AssestTypeId + "\\" + obj.AssetsId + "\\" + obj.ActivityId);

                    }


                    HttpContent file1 = files[i];
                    var thisFileName = file1.Headers.ContentDisposition.FileName.Trim('\"');

                    string filename = String.Empty;
                    Stream input = await file1.ReadAsStreamAsync();

                    filename = System.IO.Path.Combine(directoryName + "\\" + obj.TransactionNo + "\\" + obj.AssestTypeId + "\\" + obj.AssetsId + "\\" + obj.ActivityId, thisFileName);
                    if (i == 0)
                    {

                        BImagePath = System.IO.Path.Combine("\\" + obj.TransactionNo + "\\" + obj.AssestTypeId + "\\" + obj.AssetsId + "\\" + obj.ActivityId, thisFileName); ;

                    }
                    else if (i == 1)

                    {
                        AImagePath = filename;


                        AImagePath = System.IO.Path.Combine("\\" + obj.TransactionNo + "\\" + obj.AssestTypeId + "\\" + obj.AssetsId + "\\" + obj.ActivityId, thisFileName); ;



                    }


                    using (Stream file = File.OpenWrite(filename))
                    {
                        input.CopyTo(file);
                        //close file
                        file.Close();


                    }

                }


            }
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "[insertTaskActivityInfo]";

            cmdObj.Parameters
               .Add(new SqlParameter("@TransactionNo", SqlDbType.NVarChar))
               .Value = obj.TransactionNo;


            cmdObj.Parameters
                  .Add(new SqlParameter("@ActivityId", SqlDbType.NVarChar))
                  .Value = obj.ActivityId;




            cmdObj.Parameters
                .Add(new SqlParameter("@AssetsId", SqlDbType.NVarChar))
                .Value = obj.AssetsId;


            cmdObj.Parameters
                .Add(new SqlParameter("@ActualAssetsId", SqlDbType.NVarChar))
                .Value = obj.ActualAssetsId;

            cmdObj.Parameters
              .Add(new SqlParameter("@actiondate", SqlDbType.NVarChar))
              .Value = obj.actiondate;


            cmdObj.Parameters
              .Add(new SqlParameter("@Aimage", SqlDbType.NVarChar))
              .Value = AImagePath;


            cmdObj.Parameters
              .Add(new SqlParameter("@BImage", SqlDbType.NVarChar))
              .Value = BImagePath;

            cmdObj.Parameters
            .Add(new SqlParameter("@DND", SqlDbType.NVarChar))
            .Value = obj.DND;

            cmdObj.Parameters
            .Add(new SqlParameter("@HostName", SqlDbType.NVarChar))
            .Value = obj.HostName;

            cmdObj.Parameters
             .Add(new SqlParameter("@Description", SqlDbType.VarChar))
             .Value = obj.Description;

            cmdObj.Parameters
             .Add(new SqlParameter("@FaultyDesc", SqlDbType.NVarChar))
             .Value = obj.FaultyDesc;

            cmdObj.Parameters
          .Add(new SqlParameter("@AssestTypeId", SqlDbType.NVarChar))
          .Value = obj.AssestTypeId;


            SqlParameter outputParam = cmdObj.Parameters.Add("@ERROR", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;


            //cmdObj = new SqlCommand();
            //cmdObj.CommandText = "[usp_DescriptionMaster]";

            if (du.ExecuteSqlProcedure(cmdObj))
            {
                string Success = outputParam.Value.ToString();
                if (Success == "1")
                {
                    return obj.ActivityId;
                }
                else
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.NoContent) { ReasonPhrase = "Invalid Data" };
                    return msg;
                }
                //  
            }
            else
            {
                var msg = new HttpResponseMessage(HttpStatusCode.NotImplemented) { ReasonPhrase = "Error While insertion" };
                return msg;
            }






        }

    }
}