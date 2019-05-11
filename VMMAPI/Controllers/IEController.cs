using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMMAPI.Models;
using VMMAPI.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;

namespace VMMAPI.Controllers
{
    public class IEController : Controller
    {
        DataRepository dataRepository = new DataRepository();
        DataSet ds = new DataSet();

        [HttpGet]
        public ActionResult IEGET()
        {
            ds = dataRepository.GetIE();
            ViewBag.IE = ds.Tables[0];
            return View();
        }
        [HttpGet]
        public ActionResult IEGETAPI()
        {
            ds = dataRepository.GetIE();
            ViewBag.IE = ds.Tables[0];
            var data = DataTableToJSONWithJavaScriptSerializer(ds.Tables[0]);
            return (ActionResult)this.Json(data, JsonRequestBehavior.AllowGet);           
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
        [HttpGet]
        public ActionResult AddIE()
        {
            ds = dataRepository.GetIE();
            ViewBag.IE = ds.Tables[0];
            return View();
        }
        [HttpPost]
        public ActionResult AddIE(IE fc)
        {
            var res= dataRepository.InsertIE(fc);
            TempData["Message"] = "Record has been insertesd Successfully with Id: " +res ;
            return RedirectToAction("AddIE", "IE");
        }

        [HttpGet]
        public ActionResult UpdateIE(int Id)
        {
            ds = dataRepository.GetIEById(Id);
            ViewBag.IE = ds.Tables[0];
            return View();
         }
        //[ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateIE(FormCollection fc)
        {
            IE ie = new IE();
            ie.Id = Convert.ToInt32(fc["Id"]);
            ie.Question = fc["Question"];
            ie.Answer = fc["Answer"];
            dataRepository.UpdateIE(ie);
            TempData["Message"] = "Record has been updated Successfully with Id: "+ie.Id;
            return RedirectToAction("AddIE", "IE");
            
        }
    }
}
