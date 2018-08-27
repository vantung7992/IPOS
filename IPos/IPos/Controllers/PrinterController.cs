using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IDAutomation.NetAssembly;
using IPos.Models;

namespace IPos.Controllers
{
    public class PrinterController : Controller
    {
        private static readonly FontEncoder FontEncoder = new FontEncoder();

        // GET: Printer
        public ActionResult PrintBarcode()
        {
                //"<html><head><meta charset='utf-8' /><meta name='viewport' content='width=device-width, initial-scale=1.0'><style>@font-face {font-family: 'code128';src: url('../../Styles/fonts/code128.ttf') format('truetype');}p {margin: 0px;}</style></head><body style='padding: 0; margin: 0;'><table style='text-align: center'><tr><td style='width: 130px; height: 95px;'><p style='font-size: 13px; margin-top: 5px;'><b>Mỳ ommi bò hầm abc</b></p><p style='font-family: 'code128'; font-size: 25px;'>"+PrinterEncodeString("P000001")+"</p><p style='font-size: 10px;'>P000001</p><p style='font-size: 13px;'>24.000 đ</p></td><td style='width: 2px'></td><td style='width: 130px; height: 95px;'><p style='font-size: 13px; margin-top: 5px;'><b>Mỳ ommi bò hầm</b></p><p style='font-family: code128; font-size: 25px;'>"+PrinterEncodeString("P000001")+"</p><p style='font-size: 10px;'>P000001</p><p style='font-size: 13px;'>24.000 đ</p></td></tr></table></body></html>"
            return View();
        }

        public ActionResult PrintProduct(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                return View();
            }
            //productCode = productCode.ToUpper();
            using (IPosEntities ctx = new IPosEntities())
            {
                var productInf = ctx.Product_Unit.FirstOrDefault(p => p.Product_Code.ToUpper() == productCode);
                ViewBag.Name = productInf?.Name;
                ViewBag.Barcode = FontEncoder.Code128a(productCode);
                ViewBag.Product_code = productCode;
                ViewBag.Price = string.Format("{0} đ",productInf?.Sell_Price);
            }

            return View();
        }

        public ActionResult PrintBill(IPos.Controllers.Bill bill)
        {
            var billItem = bill.BillItems;
            //THiếu thông tin nào trong class BIll của anh chua có thì e cứ để một trường const sau này sửa  chứ đừng thêm vafp BIll của anh vôi ko thì conflic 



            //using (IPosEntities ctx = new IPosEntities())
            //{
                
            //    var billInf = from transaction in ctx.Transaction.Where(b => b.code == code)
            //        from detail in ctx.Transaction_Detail.Where((b => b.Transaction_Code == code))
            //        from payment in ctx.Transaction_Payment.Where(b => b.Transaction_Code == code)
            //        select new
            //        {
            //           transaction,
            //            detail,
            //            payment
            //        };
            //    if (billInf.Any())
            //    {
            //        BillModel bill = new BillModel()
            //        {
            //            Transaction = 
            //        };
            //        List<Transaction_Detail>  lstTransactionDetails = new List<Transaction_Detail>(billInf.Select(b=>b.detail));

            //        var objTransaction = billInf.FirstOrDefault()?.transaction;
            //        var objPayment = billInf.FirstOrDefault()?.payment;
            //    }
                
                
            //}
            return View();
        }
    }
}