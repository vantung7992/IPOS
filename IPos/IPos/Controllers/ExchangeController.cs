using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using IPos.Models;

namespace IPos.Controllers
{
    public class ExchangeController : Controller
    {
        private const string BillSession = "BillSession";
        private const string BillCurrent = "BillCurrent";
        // GET: Exchange
        public ActionResult Index()
        {

            var billSession = Session[BillSession];
            var listBill = new List<Bill>();
            if (billSession != null)
            {
                listBill = (List<Bill>)billSession;
            }
            else
            {
                //Create new a bill and add to list bill

                var bill = new Bill
                {
                    BillNumber = 1,
                };
                listBill.Add(bill);
                Session[BillSession] = listBill;
                Session[BillCurrent] = bill.BillNumber;
            }
            ViewBag.BillCurrent = Session[BillCurrent];
            return View(listBill);
        }

        #region Build Bill
        //add Bill
        public ActionResult AddBill()
        {
            var billSession = Session[BillSession];
            var bill = new Bill();
            if (billSession != null)
            {
                var listBill = (List<Bill>)Session[BillSession];
                var maxBillNumber = listBill.Max(x => x.BillNumber);
                bill.BillNumber = maxBillNumber + 1;
                listBill.Add(bill);
            }
            else
            {
                //Create new a bill and add to list bill
                bill.BillNumber = 1;
                var listBill = new List<Bill>();
                listBill.Add(bill);
                Session[BillSession] = listBill;
            }
            Session[BillCurrent] = bill.BillNumber;
            return RedirectToAction("Index");
        }

        //delete Bill
        public ActionResult DeleteBill(int billNumber)
        {
            var billSession = Session[BillSession];
            if (billSession != null)
            {
                var listBill = (List<Bill>)Session[BillSession];
                listBill.RemoveAll(x => x.BillNumber == billNumber);

                if (listBill.Count == 0)
                {
                    var bill = new Bill
                    {
                        BillNumber = 1,
                    };
                    listBill.Add(bill);
                }
                Session[BillSession] = listBill;
                Session[BillCurrent] = listBill.Min(x => x.BillNumber);
            }
            return RedirectToAction("Index");
        }

        //set bill current 
        public ActionResult SetBillCurrent(int billNumber)
        {
            Session[BillCurrent] = billNumber;
            return RedirectToAction("Index");
        }

        //add bill item to list
        public ActionResult AddBillItem(string productCode, int quantity, int billNumber)
        {
            var billSession = Session[BillSession];
            if (billSession != null)
            {
                var listBill = (List<Bill>)billSession;
                var listBillItem = listBill.Single(x => x.BillNumber == billNumber).BillItems;
                if (listBillItem.Exists(x => x.Product_Unit.Product_Code == productCode))
                {
                    foreach (var billItem in listBillItem)
                    {
                        if (billItem.Product_Unit.Product_Code == productCode)
                        {
                            billItem.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var billItem = GetBillItemByProductCode(productCode);
                    billItem.Quantity = quantity;
                    listBillItem.Add(billItem);
                }
                Session[BillSession] = listBill;
            }
            else
            {
                //TODO: error
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        //update Bill item
        public JsonResult UpdateBillItem(string billItemModel)
        {
            var jsonBill = new JavaScriptSerializer().Deserialize<List<BillItem>>(billItemModel);
            var sessionBill = (List<BillItem>)Session[BillSession];
            foreach (var item in sessionBill)
            {
                var jsonItem = jsonBill.SingleOrDefault(x => x.Product_Unit.Product_Code == item.Product_Unit.Product_Code);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                    break;
                }
            }
            Session[BillSession] = sessionBill;

            return Json(new { success = true });
        }

        //delete Bill item
        public JsonResult DeleteBillItem(string productCode)
        {
            var sessionBill = (List<BillItem>)Session[BillSession];
            sessionBill.RemoveAll(x => x.Product_Unit.Product_Code == productCode);
            return Json(new { success = true });
        }

        //delete All item
        public JsonResult DeleteAllBillItem()
        {
            Session[BillSession] = null;
            return Json(new { success = true });
        }

        //Get Bill Item by product code
        private BillItem GetBillItemByProductCode(string productCode)
        {
            using (var dbContext = new IPosEntities())
            {

                return (from pu in dbContext.Product_Unit.Where(x => x.Product_Code == productCode)
                        from p in dbContext.Products.Where(x => x.ID == pu.Product_ID)
                        select new BillItem
                        {
                            Product = p,
                            Product_Unit = pu
                        }).FirstOrDefault();
            }
        }

        [HttpGet]
        public ActionResult BillItem_Partial(int billNumber)
        {
            var billSession = Session[BillSession];
            var listBillItem = new List<BillItem>();
            if (billSession != null)
            {
                var listBill = new List<Bill>();
                listBill = (List<Bill>)billSession;
                listBillItem = listBill.SingleOrDefault(x => x.BillNumber == billNumber).BillItems;
            }
            ViewBag.BillCurrent = Session[BillCurrent];
            // return PartialView(listBillItem);
            return Json(new { success = true, html = RenderRazorViewToString("~/Views/Exchange/BillItem_Partial.cshtml", listBillItem) }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Extent
        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                    viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                    ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
    }
    public class Bill
    {
        public int BillNumber { get; set; }
        public List<BillItem> BillItems = new List<BillItem>();
        public decimal? Discount { get; set; }
    }

    [Serializable]
    public class BillItem
    {
        public Products Product { get; set; }
        public Product_Unit Product_Unit { get; set; }
        public int Quantity { get; set; }
    }

}
