using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPos.Models;

namespace IPos.Controllers
{
    public class ExchangeController : Controller
    {
        // GET: Exchange
        public ActionResult Index()
        {
            return View();
        }

        private const string BillSession = "BillSession";
        #region Bill
        //Add item
        public ActionResult AddItem(string productCode, int quantity)
        {
            var product = GetProductbyCode(productCode);
            if (product == null)
            {
                return null; //TODO
            }


            var bill = Session[BillSession];
            if (bill != null)
            {

            }
            else
            {
                var item = new BillItem()
                {
                    ProductCode = productCode,
                    Quantity = quantity,
                    Price = 0
                };
                
            }
        }

        //Get product by code
        private Products GetProductbyCode(string ProductCode)
        {
            using (var dbContext = new IPosEntities())
            {
                return dbContext.Products.Where(x => x.Code == ProductCode).FirstOrDefault();
            }
        }

        #endregion


    }
    [Serializable]
    public class BillItem
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
