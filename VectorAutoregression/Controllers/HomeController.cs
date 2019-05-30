using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VectorAutoregression.Models;
using VectorAutoregression.Services;

namespace VectorAutoregression.Controllers
{
    public class HomeController : Controller
    {
        static VarDataModel MainModel;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if(file==null)
            {
                MainModel = new VarDataModel(InputModels.BMI.ToList(), InputModels.Chol.ToList());
            } else
            {
                MainModel = InputConverter.ConvertToDM(file);
            }
            return RedirectToAction("Var");
        }
        
        public ActionResult Var()
        {
             return View(MainModel);
        }
    }
}