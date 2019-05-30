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
        static PreProcessDataModel MainModel;

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
                MainModel = new PreProcessDataModel(InputModels.BMI.ToList(), InputModels.Chol.ToList());
            } else
            {
                //if(System.IO.Path.GetExtension(file.FileName).Substring(1)=="txt")
                MainModel = InputConverter.ConvertToDM(file);
            }
            return RedirectToAction("PreProcessing");
        }

        public ActionResult PreProcessing()
        {    
            return View(MainModel);
        }

        public ActionResult Var()
        {
             var dm = new VarDataModel(MainModel.Delta1X1.Select(x=>x.Y.Value).ToList(), MainModel.Delta1X2.Select(x => x.Y.Value).ToList());
             return View(dm);
        }
    }
}