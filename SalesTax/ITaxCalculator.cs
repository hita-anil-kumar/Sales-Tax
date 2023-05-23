namespace SalesTax
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(ReceiptItem item);
    }
}
