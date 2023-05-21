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
