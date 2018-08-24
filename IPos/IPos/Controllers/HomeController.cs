using IPos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPos.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult CreateNewProduct(string barcode, string name, decimal category_id, decimal original_price, decimal sell price,)
        {
            using (IPosEntities ctx = new IPosEntities())
            {

            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}