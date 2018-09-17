using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IDAutomation.NetAssembly;
using IPos.Models;
using Microsoft.Ajax.Utilities;

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
                ViewBag.Price = string.Format("{0} đ", productInf?.Sell_Price);
            }

            return View();
        }

        public ActionResult PrintBill(string code)
        {
            using (IPosEntities ctx = new IPosEntities())
            {
                var billInfor = new BillInfor();
                var trans = ctx.Transaction.FirstOrDefault(b => b.code == code);
                if (trans != null)
                {
                    var transactionInfor = from shop in ctx.Shop.Where(b => b.ID == trans.Shop_ID)
                                           from partner in ctx.Partner.Where(b => b.ID == trans.Partner_ID)
                                           from detail in ctx.Transaction_Detail.Where((b => b.Transaction_Code == code))
                                           from productUnit in ctx.Product_Unit.Where((b => b.Product_Code == detail.Product_Code))
                                           from payment in ctx.Transaction_Payment.Where(b => b.Transaction_Code == code)
                                           select new
                                           {
                                               shop,
                                               partner,
                                               detail,
                                               payment,
                                               productUnit
                                           };
                    if (transactionInfor.Any())
                    {
                        var bill = transactionInfor.ToList();
                        var shopInfor = bill.Select(s => s.shop).FirstOrDefault();
                        var partnerInfor = bill.Select(p => p.partner).FirstOrDefault();

                        billInfor = new BillInfor
                        {
                            Transaction = trans,
                            LstTransactionDetail = bill.Select(t => t.detail).ToList(),
                            LstProductUnit = bill.Select(p => p.productUnit).ToList(),
                            TransactionPayment = bill.Select(t => t.payment).ToList(),
                            Shop = shopInfor,
                            Partner = partnerInfor
                            //ShopName = shopInfor?.Name,
                            //ShopPhone = shopInfor?.Phone,
                            //ShopAddress = shopInfor?.Address,
                            //PartnerName = partnerInfor?.Name,
                            //PartnerPhone = partnerInfor?.Phone,
                            //PartnerAddress = partnerInfor?.Address
                        };
                    }
                }
                return View(billInfor);
            }
        }
    }

    public class BillInfor
    {
        public Transaction Transaction { get; set; }
        public IEnumerable<Transaction_Detail> LstTransactionDetail { get; set; }
        public IEnumerable<Product_Unit> LstProductUnit { get; set; }
        //public string ShopName { get; set; }
        //public string ShopPhone { get; set; }
        //public string ShopAddress { get; set; }
        public Shop Shop { get; set; }
        public Partner Partner { get; set; }
        //public string PartnerName { get; set; }
        //public string PartnerPhone { get; set; }
        //public string PartnerAddress { get; set; }
        public IEnumerable<Transaction_Payment> TransactionPayment { get; set; }

    }

    public class TransactionDetail : Transaction_Detail
    {
        public string PriceAfterDiscount
        {
            get => PriceAfterDiscount;
            set => PriceAfterDiscount = ((Price / Quantity) * (100 - Discount) / 100).ToString();
        }
    }
}