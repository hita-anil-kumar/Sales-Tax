namespace SalesTax
{
    class Program
    {
        static void Main(string[] args)
        {

            IReceiptItemParser itemParser = new ReceiptItemParser();
            ITaxCalculator taxCalculator = new TaxCalculator(); 
            IReceiptPrinter receiptPrinter = new ReceiptPrinter(taxCalculator); 
            IReceiptGenerator receiptGenerator = new ReceiptGenerator(itemParser, receiptPrinter);



            List<ReceiptItem> items = new List<ReceiptItem>();

            Console.Write("Enter the number of items in the basket: ");
            string? itemCountInput = Console.ReadLine();

            int itemCount = int.Parse(itemCountInput!);

            Console.WriteLine("Enter the receipt items (one item per line): ");
            for (int i = 0; i < itemCount; i++)
            {
                string? line = Console.ReadLine();
                ReceiptItem item = receiptGenerator.ParseReceiptItem(line!);
                items.Add(item);
            }

            string receipt = receiptGenerator.GenerateReceipt(items);
            Console.WriteLine(receipt);
        }
    }
}

