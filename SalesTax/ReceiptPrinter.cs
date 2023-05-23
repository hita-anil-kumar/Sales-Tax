using System.Text;

namespace SalesTax
{
    public class ReceiptPrinter : IReceiptPrinter
    {
        private readonly ITaxCalculator taxCalculator;

        public ReceiptPrinter(ITaxCalculator taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }

        public string GenerateReceipt(List<ReceiptItem> items)
        {
            decimal totalAmount = 0m;
            decimal totalSalesTaxes = 0m;
            StringBuilder sb = new StringBuilder();

            foreach (ReceiptItem item in items)
            {
                decimal itemTax = taxCalculator.CalculateTax(item);
                decimal itemTotal = item.Price * item.Quantity + itemTax;

                totalAmount += itemTotal;
                totalSalesTaxes += itemTax;

                sb.AppendLine($"{item.Quantity} {item.Name}: {itemTotal:F2}");
            }

            sb.AppendLine($"Sales Taxes: {totalSalesTaxes:F2}");
            sb.AppendLine($"Total: {totalAmount:F2}");

            return "Output:\n" + sb.ToString();
        }
    }
}
