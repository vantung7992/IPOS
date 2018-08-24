using IPos.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace IPos.Controllers
{
    public class ProductController : Controller
    {
        public JsonResult CreateNewProduct(string name, int category_id, decimal original_price,
                                           int quantity, int quota_min, int quota_max,
                                           string[] barcode, string[] unit_name, int[] quantitiesPerUnit, decimal[] sell_price)
        {
            using (IPosEntities ctx = new IPosEntities())
            {
                try
                {
                    // Khởi tạo sản phẩm
                    Products _new_product = new Products();
                    _new_product.ID = ctx.Products.Any() ? ctx.Products.Max(b => b.ID) + 1 : 1;
                    _new_product.Name = name;
                    _new_product.Product_Category_ID = category_id;
                    _new_product.Min_Quota = quota_min;
                    _new_product.Max_Quota = quota_max;
                    _new_product.Base_Product_Code = barcode[0];
                    ctx.Products.Add(_new_product);

                    // Khởi tạo unit
                    for (int i = 0; i < barcode.Length; i++)
                    {
                        Product_Unit _new_product_unit = new Product_Unit();
                        _new_product_unit.Product_Code = barcode[i];
                        _new_product_unit.Product_ID = _new_product.ID;
                        _new_product_unit.Base_Product_Code = barcode[0];
                        _new_product_unit.Name = unit_name[i];
                        _new_product_unit.Original_Price = original_price * quantitiesPerUnit[i];
                        _new_product_unit.QuantityPerUnit = quantitiesPerUnit[i];
                        _new_product_unit.Sell_Price = sell_price[i];
                        ctx.Product_Unit.Add(_new_product_unit);
                    }

                    // Khởi tạo giao dịch "Initiation"
                    Transaction _new_transaction = new Transaction();
                    _new_transaction.code = Utilities.Utilities.CreateNewTransactionCode(Utilities.Utilities.TransactionType.Initiation());
                    _new_transaction.Type = Utilities.Utilities.TransactionType.Initiation();
                    _new_transaction.Created_Time = DateTime.Now;
                    _new_transaction.Created_User = ""; // MANHDC - TODO
                    _new_transaction.Shop_ID = 0; // MANHDC - TODO
                    _new_transaction.Discount = 0;
                    _new_transaction.Note = "Khởi tạo sản phẩm";
                    ctx.Transaction.Add(_new_transaction);

                    // Khởi tạo chi tiết giao dịch (số lượng, giá tiền)
                    Transaction_Detail _new_transaction_detail = new Transaction_Detail();
                    _new_transaction_detail.Transaction_Code = _new_transaction.code;
                    _new_transaction_detail.Product_Code = _new_product.Base_Product_Code;
                    _new_transaction_detail.Quantity = quantity;
                    _new_transaction_detail.Price = original_price;
                    _new_transaction_detail.Total = quantity * original_price;
                    ctx.Transaction_Detail.Add(_new_transaction_detail);

                    // Khởi tạo thanh toán tiền mặt 0 đồng
                    Transaction_Payment _new_transaction_payment = new Transaction_Payment();
                    _new_transaction_payment.ID = ctx.Transaction_Payment.Any() ? ctx.Transaction_Payment.Max(b => b.ID) + 1 : 1;
                    _new_transaction_payment.Transaction_Code = _new_transaction.code;
                    _new_transaction_payment.Type = "M";
                    _new_transaction_payment.Amount = 0;
                    ctx.Transaction_Payment.Add(_new_transaction_payment);

                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        msg = ex.Message;
                    }
                    return Json("ERR:" + msg, JsonRequestBehavior.AllowGet);
                }
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}