using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMMAPI.Repository;
using System.Data;
using VMMAPI.Models;

namespace VMMAPI.Controllers
{
    public class ImageDescriptionController : Controller
    {
        DataRepository dataRepository = new DataRepository();
        DataSet ds = new DataSet();
        TransactionDetails td = new TransactionDetails();


        public ActionResult ImageGET()
        {
            ds = dataRepository.GetImage(td);
            ViewBag.TransactionDetails = ds.Tables[0];
            return View();
        }
    }
}