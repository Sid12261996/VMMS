using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMMAPI.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VMMAPI.Repository;

namespace VMMAPI.Controllers
{
    public class PortalLoginController : Controller
    {
        DataRepository des = new DataRepository();
        // GET: PortalLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login_Post(UserLogin obj)
        {
            long UserType = 1;
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (obj.UserId == "Admin" && obj.Password == "12345")
                {
                    TempData["UserType"] = UserType;

                    Session["UserID"] = Guid.NewGuid();
                    return RedirectToAction("AssignTaskNew1", "VMMAssign");

                }
                else
                {

                    UserType = 2;
                    TempData["UserType"] = UserType;
                    TempData["UserID"] = obj.UserId;
                    Session["UserID"] = Guid.NewGuid();
                    var rt = Guid.NewGuid();
                    int cnt = this.des.GetUserAuthenticate(obj.UserId, obj.Password);
                    if (cnt == 1)
                    {
                        return RedirectToAction("ShowTransactionDetails", "VMMAssign");
                    }
                    else
                    {
                        ViewBag.Message = "Please Login with correct User and Passwod";
                        return View();
                    }
                    // return View();
                }


                //else
                //{
                //    return View();
                //}

            }
        }
    }
}