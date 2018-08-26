using ClosedXML.Excel;
using IPos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPos.Utilities
{
    public class Utilities
    {
        public class TransactionType
        {
            private static string initiation = "I";
            private static string bill = "B";
            private static string bill_return = "BR";
            private static string merchandise = "M";
            private static string merchandise_return = "MR";
            private static string destroy = "D";
            private static string payment = "P";
            private static string receipt = "R";

            public static string Initiation() { return initiation; }
            public static string Bill() { return bill; }
            public static string BillReturn() { return bill_return; }
            public static string Merchandise() { return merchandise; }
            public static string MerchandiseReturn() { return merchandise_return; }
            public static string Destroy() { return destroy; }
            public static string Payment() { return payment; }
            public static string Receipt() { return receipt; }
        }

        private static int _transaction_code_number_length = 6; // B123456
        public static string CreateNewTransactionCode(string code)
        {
            using (IPosEntities ctx = new IPosEntities())
            {
                var _find_max_code = ctx.Transaction.Where(b => b.code.StartsWith(code)).OrderByDescending(b => b.code).Select(b => b.code);
                int _max_value = 1;
                if (_find_max_code.Any())
                    _max_value = int.Parse(_find_max_code.First().Replace(code, "")) + 1;
                return string.Format("{0}{1}", code, _max_value.ToString("D" + _transaction_code_number_length));
            }
        }

        public static string ExportFile(string fileName, List<Dictionary<string, string>> listItem)
        {
            var workbook = new XLWorkbook();
            workbook.AddWorksheet("DanhSach");
            var ws = workbook.Worksheet("DanhSach");

            string file_path = HttpContext.Current.Server.MapPath("~/Export/" + fileName);

            int row = 1, column = 1;
            foreach (string key in listItem[0].Keys)
            {
                ws.Cell(row, column).Value = key;
                column++;
            }
            row++;
            foreach (var item in listItem)
            {
                column = 1;
                foreach (string key in listItem[0].Keys)
                {
                    ws.Cell(row, column).Value = item[key];
                    column++;
                }
                row++;
            }

            workbook.SaveAs(file_path);
            return file_path;
        }
    }
}