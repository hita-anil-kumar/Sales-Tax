namespace SalesTax
{
    public interface IReceiptItemParser
    {
        ReceiptItem ParseReceiptItem(string input);
    }
}
