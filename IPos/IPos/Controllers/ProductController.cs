using IPos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IPos.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateNewProduct(string name, int category_id, decimal original_price,
                                           int quantity, int quota_min, int quota_max,
                                           string[] barcode, string[] unit_name, int[] quantitiesPerUnit, decimal[] sell_price, string[] img)
        {
            using (IPosEntities ctx = new IPosEntities())
            {
                try
                {
                    int default_bar_code = GetMaxProductCodeNumber();
                    string base_product_code = barcode[0].Length > 0 ? barcode[0] : string.Format("P{0}", (++default_bar_code).ToString("D" + _product_code_number_length));
                    // Khởi tạo sản phẩm
                    Products _new_product = new Products();
                    _new_product.ID = ctx.Products.Any() ? ctx.Products.Max(b => b.ID) + 1 : 1;
                    _new_product.Name = name;
                    _new_product.Product_Category_ID = category_id;
                    _new_product.Min_Quota = quota_min;
                    _new_product.Max_Quota = quota_max;
                    _new_product.Base_Product_Code = base_product_code;
                    ctx.Products.Add(_new_product);

                    // Khởi tạo unit
                    for (int i = 0; i < barcode.Length; i++)
                    {
                        Product_Unit _new_product_unit = new Product_Unit();
                        _new_product_unit.Product_Code = barcode[i].Length > 0 ? barcode[i] : string.Format("P{0}", (++default_bar_code).ToString("D" + _product_code_number_length));
                        _new_product_unit.Product_ID = _new_product.ID;
                        _new_product_unit.Base_Product_Code = base_product_code;
                        _new_product_unit.Name = unit_name[i];
                        _new_product_unit.Original_Price = original_price * quantitiesPerUnit[i];
                        _new_product_unit.QuantityPerUnit = quantitiesPerUnit[i];
                        _new_product_unit.Sell_Price = sell_price[i];
                        ctx.Product_Unit.Add(_new_product_unit);
                    }

                    // Khởi tạo image
                    long _last_image_id = ctx.Product_Image.Any() ? ctx.Product_Image.Max(b => b.ID) + 1 : 1;
                    for (int i = 0; i < img.Length; i++)
                    {
                        Product_Image _new_image = new Product_Image();
                        _new_image.ID = _last_image_id++;
                        _new_image.Number = i + 1;
                        _new_image.Product_ID = _new_product.ID;
                        _new_image.Contents = img[i];
                        ctx.Product_Image.Add(_new_image);
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

        public static int ProductCurrentQuantity(long product_id)
        {
            using (IPosEntities ctx = new IPosEntities())
            {
                int count = 0;
                var _all_unit = ctx.Product_Unit.Where(b => b.Product_ID == product_id).Select(b => b.Product_Code);
                if (_all_unit.Any())
                {
                    var _all_unit_code = _all_unit.ToList();
                    var _find_transaction = from transaction in ctx.Transaction
                                            from detail in ctx.Transaction_Detail.Where(b => b.Transaction_Code == transaction.code && _all_unit_code.Contains(b.Product_Code))
                                            from product in ctx.Product_Unit.Where(b => b.Product_Code == detail.Product_Code)
                                            select new
                                            {
                                                Type = transaction.Type,
                                                Quantity = detail.Quantity * product.QuantityPerUnit
                                            };
                    string[] _plus_type = { "I", "M", "BR" };
                    string[] _minus_type = { "B", "MR", "D" };
                    var _find_plus_type = _find_transaction.Where(b => _plus_type.Contains(b.Type));
                    var _find_minus_type = _find_transaction.Where(b => _minus_type.Contains(b.Type));
                    count += _find_plus_type.Any() ? (int)_find_plus_type.Sum(b => b.Quantity) : 0;
                    count -= _find_minus_type.Any() ? (int)_find_minus_type.Sum(b => b.Quantity) : 0;
                }

                return count;
            }
        }

        public static List<Dictionary<string, string>> GetCategoryTree()
        {
            List<Dictionary<string, string>> _category_tree = new List<Dictionary<string, string>>();
            using (IPosEntities ctx = new IPosEntities())
            {
                getRescursiveCategories(ctx, _category_tree, 0, 0);
                return _category_tree;
            }
        }

        private static void getRescursiveCategories(IPosEntities ctx, List<Dictionary<string, string>> _tree, long parentId, int level)
        {
            var _find_category = ctx.Product_Categories.Where(b => b.Parent_ID == parentId).OrderBy(b => b.ID);
            if (_find_category.Any())
            {
                foreach (var category in _find_category)
                {
                    Dictionary<string, string> _info = new Dictionary<string, string>();
                    _info.Add("ID", category.ID.ToString());
                    _info.Add("NAME", category.Name);
                    _info.Add("LEVEL", level.ToString());
                    _tree.Add(_info);

                    var _find_childern = ctx.Product_Categories.Where(b => b.Parent_ID == category.ID);
                    if (_find_childern.Any())
                    {
                        getRescursiveCategories(ctx, _tree, category.ID, level + 1);
                    }
                }
            }

        }

        private static int _product_code_number_length = 6; // P123456
        public int GetMaxProductCodeNumber()
        {
            using (IPosEntities ctx = new IPosEntities())
            {
                int _max_value = 0;
                var _find_max_code = ctx.Product_Unit.Where(b => b.Product_Code.StartsWith("P")).OrderByDescending(b => b.Product_Code).Select(b => b.Product_Code);
                if (_find_max_code.Any())
                    _max_value = int.Parse(_find_max_code.First().Replace("P", ""));
                return _max_value;
            }
        }

        public ActionResult ListProduct(/*string searchStr, int categoryId, int page, int pageSize*/)
        {
            string searchStr = "";
            int categoryId = -1;
            int page = 0;
            int pageSize = 10;
            using (IPosEntities ctx = new IPosEntities())
            {
                var _find_product = from product in ctx.Products.Where(b => (searchStr == "" || b.Name.ToLower().Contains(searchStr.ToLower())) && (categoryId == -1 || b.Product_Category_ID == categoryId))
                                    from unit in ctx.Product_Unit.Where(b => b.Product_ID == product.ID && b.Product_Code == b.Base_Product_Code)
                                    select new
                                    {
                                        ID = unit.Product_ID,
                                        CODE = unit.Product_Code,
                                        NAME = product.Name,
                                        SELL_PRICE = unit.Sell_Price,
                                        ORIGINAL_PRICE = unit.Original_Price
                                    };

                int totalItem = _find_product.Count();
                int totalPage = (int)Math.Ceiling((decimal)(totalItem / pageSize));

                _find_product = _find_product.OrderByDescending(b => b.CODE).Skip(page * pageSize).Take(pageSize);
                List<Dictionary<string, string>> _list_product = new List<Dictionary<string, string>>();
                foreach (var product in _find_product)
                {
                    Dictionary<string, string> _info = new Dictionary<string, string>();
                    _info.Add("CODE", product.CODE);
                    _info.Add("NAME", product.NAME);
                    _info.Add("SELL_PRICE", product.SELL_PRICE.GetValueOrDefault(0).ToString("N0"));
                    _info.Add("ORIGINAL_PRICE", product.ORIGINAL_PRICE.GetValueOrDefault(0).ToString("N0"));
                    _info.Add("QUANTITY", ProductCurrentQuantity(product.ID.GetValueOrDefault(0)).ToString("N0"));
                    _list_product.Add(_info);
                }

                var _listModel = new ListModel();
                _listModel.totalItem = totalItem;
                _listModel.totalPage = totalPage;
                _listModel.currentPage = page;
                _listModel.listItem = _list_product;

                return View(_listModel);
            }
        }

        public FileResult ExportProducts()
        {
            using (IPosEntities ctx = new IPosEntities())
            {
                var _all_product = from product in ctx.Products
                                   from unit in ctx.Product_Unit.Where(b => b.Product_ID == product.ID)
                                   from category in ctx.Product_Categories.Where(b => b.ID == product.Product_Category_ID)
                                   select new { product, unit, category };

                List<Dictionary<string, string>> _list_product = new List<Dictionary<string, string>>();
                foreach (var item in _all_product)
                {
                    Dictionary<string, string> _info = new Dictionary<string, string>();
                    _info.Add("Nhóm hàng", item.category.Name);
                    _info.Add("Mã hàng", item.unit.Product_Code);
                    _info.Add("Tên hàng hóa", item.product.Name);
                    _info.Add("Giá bán", item.unit.Sell_Price.GetValueOrDefault(0).ToString("N0"));
                    _info.Add("Giá vốn", item.unit.Original_Price.GetValueOrDefault(0).ToString("N0"));
                    _info.Add("Tồn kho", ProductCurrentQuantity((long)item.product.ID).ToString("N0"));
                    _info.Add("Tồn nhỏ nhất", item.product.Min_Quota.GetValueOrDefault(0).ToString("N0"));
                    _info.Add("Tồn lớn nhất", item.product.Max_Quota.GetValueOrDefault(0).ToString("N0"));
                    _info.Add("Đơn vị tính", item.unit.Name);
                    _info.Add("Quy đổi", item.unit.QuantityPerUnit.GetValueOrDefault(0).ToString("N0"));
                    _info.Add("Mã hàng đơn vị cơ bản", item.unit.Base_Product_Code);
                    _list_product.Add(_info);
                }
                string file_name = string.Format("DanhSachSanPham_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd"));
                return File(Utilities.Utilities.ExportFile(file_name, _list_product), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", file_name);
            }
        }

        public class ListModel
        {
            public int totalItem { get; set; }
            public int totalPage { get; set; }
            public int currentPage { get; set; }
            public List<Dictionary<string, string>> listItem { get; set; }
        }
    }
}