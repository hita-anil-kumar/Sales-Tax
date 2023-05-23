namespace SalesTax
{
    public class TaxCalculator : ITaxCalculator
    {
        public decimal CalculateTax(ReceiptItem item)
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
                taxRate += 0.05m; // Additional tax for imported and unexempted items 
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
