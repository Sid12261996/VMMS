using DataUtilityLayer;
using Microsoft.CSharp.RuntimeBinder;
using VMMAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Globalization;

namespace VMMAPI.Controllers
{
    [SessionTimeoutAttribute]
    [UserAuthenticationFilter]
   
    public class VMMAssignController : Controller
    {
        // GET: VMMAssign

        private string conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        private DataUtility du = new DataUtility();
        private SqlCommand cmdObj;

      

        [HttpGet]
        [ActionName("AssignTaskNew1")]
        public ActionResult AssignTaskNew1()
        {


            return View();
           
        }

       

        public ActionResult LoadCustomer()
        {


            string empty = string.Empty;
            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetCustomerName]";
            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadCity()
        {
            string empty = string.Empty;
            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetCityName]";
            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadAssetType()
        {


            string empty = string.Empty;
            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetAssetType]";
            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);
        }



        public ActionResult LoadTask()
        {


            string empty = string.Empty;
            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetTaskName]";
            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadTaskPhysical()
        {


            string empty = string.Empty;
            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetTaskNameByPhysical]";
            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadVendorCity(string City)
        {


            string empty = string.Empty;
            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetVendorId]";
            this.cmdObj.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar)).Value = City;
            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ShowTransactionDetails(string viewType,UserLogin obj)
        {
            return View();

        }

        public ActionResult ShowTransactionDetailsGet()
        {

            var userId = TempData.Peek("UserID");
            var userType = TempData.Peek("UserType");
            string empty = string.Empty;
            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetTransactionDetails]";
            this.cmdObj.Parameters.Add(new SqlParameter("@transaction", SqlDbType.NVarChar)).Value = "";
            this.cmdObj.Parameters.Add(new SqlParameter("@CusUserName", SqlDbType.NVarChar)).Value = userId;
            this.cmdObj.Parameters.Add(new SqlParameter("@userType", SqlDbType.NVarChar)).Value = userType;
            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [UserAuthenticationFilter]
        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "PortalLogin");

        }




        [HttpGet]
        [UserAuthenticationFilter]
        public ActionResult ViewSummaryReport ()
        {

            return View();

        }


        [HttpGet]
        [UserAuthenticationFilter]
        public ActionResult ViewDashBoardReport()
        {

            return View();

        }

        //test--------------------
        Dictionary<string, Array> Image = new Dictionary<string, Array>();

        [UserAuthenticationFilter]
        public ActionResult ViewDashBoardReport(string StartDate, string EndDate,string CustName,String City,String State,String Status)
        {
            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
            var userId = TempData.Peek("UserID");
            var userType = TempData.Peek("UserType");
            string empty = string.Empty;
            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[Get_Dashboardreport]";
            this.cmdObj.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.NVarChar)).Value = (object)StartDate;
            this.cmdObj.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.NVarChar)).Value = (object)EndDate;
            this.cmdObj.Parameters.Add(new SqlParameter("@CustName", SqlDbType.NVarChar)).Value = (object)CustName;
            this.cmdObj.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar)).Value = (object)City;
            this.cmdObj.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar)).Value = (object)State;
            this.cmdObj.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar)).Value = (object)Status;
            this.cmdObj.Parameters.Add(new SqlParameter("@CusUserName", SqlDbType.NVarChar)).Value = userId;
            this.cmdObj.Parameters.Add(new SqlParameter("@userType", SqlDbType.NVarChar)).Value = userType;

            //Dictionary<string, Array> Image = new Dictionary<string, Array>();
            //Image.Add("",[0]);

            //DataTable dataTable = new DataTable();
            //return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);
            var datatable = this.du.GetDataTableWithProc(this.cmdObj);
            //string[] result = new string[datatable.Columns.Count];
            //DataRow dr = datatable.Rows[0];
            //for (int i = 0; i < dr.ItemArray.Length; i++)
            //{
            //    result[i] = dr[i].ToString();
            //}
            //foreach (string str in result)
            //    Console.WriteLine(str);

            var dict = new List<Dictionary<string, string[]>>();
           
            string myData = datatable.Rows[0][9].ToString();
            if (datatable.Rows.Count > 0)
            {
                
                foreach (DataRow dtRow in datatable.Rows)
                {
                    string[] values = new string[2]; 
                    values[0]= dtRow["BImage"].ToString();
                    values[1]= dtRow["AImage"].ToString();
                    var dummy = new Dictionary<string, string[]>();
                    dummy.Add(dtRow["Description"].ToString(), values);
                    dict.Add(dummy);
                        //var Description=dtRow["Description"].ToString();
                        //var BImage = dtRow["BImage"].ToString();
                        //var AImage = dtRow["AImage"].ToString();
                }
            }
            //object toReturn = new object();
           var toReturn=new toReturnFormat();
              toReturn.Other =returningSerialsedData(datatable);
            toReturn.Description = dict;
            
            return (ActionResult)this.Json(toReturn, JsonRequestBehavior.AllowGet);

        }
        private List<Dictionary<string, object>> returningSerialsedData(DataTable table)
        {

            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (DataColumn column in (InternalDataCollectionBase)table.Columns)
                    dictionary.Add(column.ColumnName, row[column]);
                dictionaryList.Add(dictionary);
            }

            return dictionaryList;
        }
    



    [UserAuthenticationFilter]
        
        public ActionResult ViewSummaryReportView(string StartDate,string EndDate,string CustomerName,string State,string City)
        {
            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
            string empty = string.Empty;
            var userId = TempData.Peek("UserID");
            var userType = TempData.Peek("UserType");
            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetSummaryReport]";
            this.cmdObj.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.NVarChar)).Value = (object)StartDate;
            this.cmdObj.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.NVarChar)).Value = (object)EndDate;
            this.cmdObj.Parameters.Add(new SqlParameter("@CusUserName", SqlDbType.NVarChar)).Value = userId;
            this.cmdObj.Parameters.Add(new SqlParameter("@userType", SqlDbType.NVarChar)).Value = userType;
            this.cmdObj.Parameters.Add(new SqlParameter("@CustomerName", SqlDbType.NVarChar)).Value = CustomerName;
            this.cmdObj.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar)).Value = State;
            this.cmdObj.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar)).Value = City;
            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);
            }

     


        [HttpPost]
        [ActionName("AssignTaskNew")]
        public ActionResult AssignTaskNew_post(FormCollection Coll, VMMOBJ obj)
        {
            bool chk = true;

            //string custName1 = Coll.Get("ddlCustName");
            //if (Coll.Get("ddlCustName") == "" || Coll.Get("ddlCustName") == "-1") { chk = false; ModelState.AddModelError("ddlCustName", "select Customer"); }

            //if (Coll.Get("ddlCity") == "" || Coll.Get("ddlCity") == "-1") { chk = false; ModelState.AddModelError("ddlCity", "Select City"); }
            //if (Coll.Get("ddlVendor") == "" || Coll.Get("ddlVendor") == "-1") { chk = false; ModelState.AddModelError("ddlVendor", "Select Vendor"); }
            if (ModelState.IsValid)
            {

                string custName = Coll.Get("ddlCustName");
                string City = Coll.Get("ddlCity");
                string Vendor = Coll.Get("ddlVendor");
                string ACtType = Coll.Get("ddlActType");
                string AssetsNO = Coll.Get("txtAssestNo");
                string AssetType = Coll.Get("ddlAssestType");

                string s = Coll.Get("submitButton");
                string taskvalue = "";
                if (s == "Submit")
                {

                    int Rowcount = Convert.ToInt32(Request.Form["HiddenRowCount"]);
                    for (int i = 0; i < Rowcount; i++)
                    {
                        var checkBox = Request.Form["chkBox" + i + ""];

                        if (checkBox == "on")
                        {
                            string TaskId = Request.Form["hiddenCName" + i + ""];
                            taskvalue += TaskId + ",";
                        }

                    }
                    if (taskvalue != "")
                    {
                        var input = taskvalue.ToString();
                        var output = input.Substring(0, input.Length - 1);

                        bool flag = new Transaction().Insert_TaskAssignment(City, Vendor, custName, output, ACtType, AssetsNO, AssetType,"","","","","");



                        Session["DataResult"] = flag;
                    }

                    return View();



                }
            }

            return View();
        }





        [HttpPost]
        [ActionName("AssignTaskNew1")]
        public ActionResult AssignTaskNew1_post(FormCollection Coll, VMMOBJ obj)
        {
            bool chk = true;

            //string custName1 = Coll.Get("ddlCustName");
            //if (Coll.Get("ddlCustName") == "" || Coll.Get("ddlCustName") == "-1") { chk = false; ModelState.AddModelError("ddlCustName", "select Customer"); }

            //if (Coll.Get("ddlCity") == "" || Coll.Get("ddlCity") == "-1") { chk = false; ModelState.AddModelError("ddlCity", "Select City"); }
            //if (Coll.Get("ddlVendor") == "" || Coll.Get("ddlVendor") == "-1") { chk = false; ModelState.AddModelError("ddlVendor", "Select Vendor"); }
            if (ModelState.IsValid)
            {
                string custName = Coll.Get("ddlCustName");
                string City = Coll.Get("ddlCity");
                string Vendor = Request.Form["HiddenVendor"].ToString();
                string ACtType = Request.Form["HiddenActType"];//ye kisne chnage kiya hai //Maine kukik iski input typ change ki thi
                string AssetsNO = Coll.Get("txtAssestNo");
                string AssetType = Coll.Get("ddlAssestType");
                string Address = Coll.Get("txtAddress");
                string TaskType = Request.Form["HiddenToggle"];
                string ContactNo = Coll.Get("txtContactNo");
                string SpocName = Coll.Get("txtSpocName");
                string State = Coll.Get("txtState");
                string s = Coll.Get("submitButton");
                string taskvalue = "";
                //=======================
                   

               if (s == "Submit" && (TaskType == "SAE" || TaskType == "SRE" ))
                {

                    int Rowcount = Convert.ToInt32(Request.Form["HiddenRowCount"]);
                    int RowcountP = Convert.ToInt32(Request.Form["HiddenRowCountPhysical"]);

                    for (int i = 0; i < Rowcount; i++)
                    {
                        var checkBox = Request.Form["chkBox" + i + ""];

                        if (checkBox == "on")
                        {
                            string TaskId = Request.Form["hiddenCName" + i + ""];
                            taskvalue += TaskId + ",";
                        }

                    }

                    for (int i = 0; i < RowcountP; i++)
                    {
                        var checkBox = Request.Form["chkBoxP" + i + ""];

                        if (checkBox == "on")
                        {
                            string TaskId = Request.Form["hiddenCNameP" + i + ""];
                            taskvalue += TaskId + ",";
                        }

                    }
                    if (taskvalue != "")
                    {
                        var input = taskvalue.ToString();
                        var output = input.Substring(0, input.Length - 1);

                        bool flag = new Transaction().Insert_TaskAssignment(City, Vendor, custName, output, ACtType, AssetsNO, AssetType,Address,TaskType,ContactNo,SpocName,State);



                        Session["DataResult"] = flag;
                    }

                    return View();



                }
            }

            return View();
        }





        public ActionResult ShowTransactionExpandView(string TRNO)
        {

            ViewBag.Transaction = TRNO;
            return View();

        }

        public static string Base64Decode(string base64EncodedData)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
        }


        public ActionResult ShowTransactionExpandView_get(string TRNO)
        {
            string Transaction = VMMAssignController.Base64Decode(TRNO);

            string empty = string.Empty;

            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetTransactionDetails]";
            this.cmdObj.Parameters.Add(new SqlParameter("@transaction", SqlDbType.NVarChar)).Value = (object)Transaction;

            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);


        }



        public ActionResult ShowPhysialImage(string TRNO)
        {

            string targetPath = Server.MapPath("~/VMMdocs/");

          
            string destFile = System.IO.Path.Combine(targetPath, "/VMMS1001/Assets1/1/shivpic.jpg");

            string Transaction = VMMAssignController.Base64Decode(TRNO);

            string empty = string.Empty;

            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetPhhysicalImages]";
            this.cmdObj.Parameters.Add(new SqlParameter("@transaction", SqlDbType.NVarChar)).Value = (object)Transaction;

            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);


        }


        public ActionResult ShowHealthImage(string TRNO)
        {

            string targetPath = Server.MapPath("~/VMMdocs/");


            string destFile = System.IO.Path.Combine(targetPath, "/VMMS1001/Assets1/1/shivpic.jpg");

            string Transaction = VMMAssignController.Base64Decode(TRNO);

            string empty = string.Empty;

            this.cmdObj = new SqlCommand();
            this.cmdObj.CommandText = "[GetHealthImages]";
            this.cmdObj.Parameters.Add(new SqlParameter("@transaction", SqlDbType.NVarChar)).Value = (object)Transaction;

            DataTable dataTable = new DataTable();
            return (ActionResult)this.Json((object)this.DataTableToJSONWithJavaScriptSerializer(this.du.GetDataTableWithProc(this.cmdObj)), JsonRequestBehavior.AllowGet);


        }



        public ActionResult InsertRemarks(string TRNO, string Activityid, string Remarks, string Status,string AssetsId)
        {
            string Transaction = VMMAssignController.Base64Decode(TRNO);

            string empty = string.Empty;

            bool flag = new Transaction().Insert_Remarks(Transaction, Activityid, Remarks, Status, AssetsId);




            return (ActionResult)this.Json((object)flag, JsonRequestBehavior.AllowGet);

        }



        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (DataColumn column in (InternalDataCollectionBase)table.Columns)
                    dictionary.Add(column.ColumnName, row[column]);
                dictionaryList.Add(dictionary);
            }
            return scriptSerializer.Serialize((object)dictionaryList);
        }
    }

    public class DDLViewModel
    {
        public string SlectedValue { get; set; }
        public List<SelectListItem> CountryList { get; set; }

    }
    ////////////////////////////////////////////////////////////////////////
    public class toReturnFormat {
        public toReturnFormat()
        {
            Description = new List<Dictionary<string, string[]>>();
            Other = new List<Dictionary<string, object>>();
        }
       
        public List<Dictionary<string,string[]>> Description;
        public List<Dictionary<string, object>> Other;


    }
}