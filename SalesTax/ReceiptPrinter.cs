using System.Collections.Generic;
using System.Text;

namespace SalesTax
{
    public class ReceiptPrinter : IReceiptPrinter
    {
        public string GenerateReceipt(List<ReceiptItem> items)
        {
            decimal totalAmount = 0m;
            decimal totalSalesTaxes = 0m;
            StringBuilder sb = new StringBuilder();

            foreach (ReceiptItem item in items)
            {
                decimal itemTax = CalculateTax(item);
                decimal itemTotal = item.Price * item.Quantity + itemTax;

                totalAmount += itemTotal;
                totalSalesTaxes += itemTax;

                sb.AppendLine($"{item.Quantity} {item.Name}: {itemTotal:F2}");
            }

            sb.AppendLine($"Sales Taxes: {totalSalesTaxes:F2}");
            sb.AppendLine($"Total: {totalAmount:F2}");

            return "Output:\n" + sb.ToString();

        }

        private decimal CalculateTax(ReceiptItem item)
        {
            decimal taxRate = GetTaxRate(item);
            decimal itemTax = Math.Ceiling(item.Price * item.Quantity * taxRate * 20) / 20;
            return itemTax;
        }

        private decimal GetTaxRate(ReceiptItem item)
        {
            decimal taxRate = 0.1m; // Basic sales tax rate

            if (item.IsImported && !item.IsExempt)
            {
                taxRate += 0.05m; // Additional tax for imported items
            }
            else if (item.IsImported && item.IsExempt)
            {
                taxRate = 0.05m; // No tax for exempt imported items
            }
            else if (item.IsExempt && !item.IsImported)
            {
                taxRate = 0m; // No tax for exempt items
            }

            return taxRate;
        }
    }
}

