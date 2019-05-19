using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VectorAutoregression.Models;

namespace VectorAutoregression.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dm = new DataModel(InputModels.BMI.ToList(), InputModels.Chol.ToList());
            return View(dm);
        }
    }
}