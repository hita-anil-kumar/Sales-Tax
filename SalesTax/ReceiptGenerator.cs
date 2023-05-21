using System.Text;

//namespace SalesTax
//{
//    public class ReceiptGenerator : IReceiptGenerator
//    {
//        public ReceiptItem ParseReceiptItem(string input)
//        {
//            string[] parts = input.Split(" at ");
//            int itemQuantity = int.Parse(parts[0].Split(" ")[0]);
//            string itemName = parts[0].Substring(parts[0].IndexOf(' ') + 1);
//            decimal itemPrice = decimal.Parse(parts[1]);

//            bool isImported = itemName.Contains("imported");
//            bool isExempt = IsItemExempt(itemName);
//            //Console.WriteLine(itemName.Contains("imported"));
//            Console.WriteLine(itemName);
//            Console.WriteLine(isExempt);
//            Console.WriteLine(isImported);
//            return new ReceiptItem(itemName, itemPrice, itemQuantity, isImported, isExempt);
//        }

//        public string GenerateReceipt(List<ReceiptItem> items)
//        {
//            decimal totalAmount = 0m;
//            decimal totalSalesTaxes = 0m;
//            StringBuilder sb = new StringBuilder();

//            foreach (ReceiptItem item in items)
//            {
//                decimal itemTax = CalculateTax(item);
//                decimal itemTotal = item.Price * item.Quantity + itemTax;

//                totalAmount += itemTotal;
//                totalSalesTaxes += itemTax;

//                sb.AppendLine($"{item.Quantity} {item.Name}: {itemTotal:F2}");
//            }

//            sb.AppendLine($"Sales Taxes: {totalSalesTaxes:F2}");
//            sb.AppendLine($"Total: {totalAmount:F2}");

//            return sb.ToString();
//        }

//        private decimal CalculateTax(ReceiptItem item)
//        {
//            decimal taxRate = GetTaxRate(item);
//            decimal itemTax = Math.Ceiling(item.Price * item.Quantity * taxRate * 20) / 20;
//            return itemTax;
//        }

//        private decimal GetTaxRate(ReceiptItem item)
//        {
//            decimal taxRate = 0.1m; // Basic sales tax rate
//                                    //if (item.IsImported)
//                                    //{
//                                    //    taxRate += 0.05m; // Additional tax for imported items
//                                    //}
//            if (item.IsImported && !item.IsExempt)
//            {
//                taxRate += 0.05m; // No tax for exempt items
//            }
//            else if (item.IsImported && item.IsExempt)
//            {
//                taxRate = 0.05m; // No tax for exempt items
//            }
//            else if (item.IsExempt && !item.IsImported)
//            {
//                taxRate = 0m; // No tax for exempt items
//            }
//            return taxRate;
//        }

//        public bool IsItemExempt(string itemName)
//        {
//            // Modify this method as per your exemption rules
//            // For the given inputs, we consider only "book", "food", and "medical" as exempt items

//            string[] exemptKeywords = { "book", "food","chocolate", "medical", "headache", "pills", "pill" };

//            foreach (string keyword in exemptKeywords)
//            {
//                if (itemName.Contains(keyword))
//                {
//                    return true;
//                }

//            }

//            return false;
//        }
//    }

//}

using System.Collections.Generic;

namespace SalesTax
{
    public class ReceiptGenerator : IReceiptGenerator
    {
        private readonly IReceiptItemParser itemParser;
        private readonly IReceiptPrinter receiptPrinter;

        public ReceiptGenerator(IReceiptItemParser itemParser, IReceiptPrinter receiptPrinter)
        {
            this.itemParser = itemParser;
            this.receiptPrinter = receiptPrinter;
        }

        public ReceiptItem ParseReceiptItem(string input)
        {
            return itemParser.ParseReceiptItem(input);
        }

        public string GenerateReceipt(List<ReceiptItem> items)
        {
            return receiptPrinter.GenerateReceipt(items);
        }
    }
}
