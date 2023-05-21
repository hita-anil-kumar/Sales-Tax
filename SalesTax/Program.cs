﻿namespace SalesTax
{
    class Program
    {
        static void Main(string[] args)
        {
            IReceiptItemParser itemParser = new ReceiptItemParser();
            IReceiptPrinter receiptPrinter = new ReceiptPrinter();
            IReceiptGenerator receiptGenerator = new ReceiptGenerator(itemParser, receiptPrinter);

            List<ReceiptItem> items = new List<ReceiptItem>();

            Console.Write("Enter the number of items in the basket: ");
            int itemCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the receipt items (one item per line): ");
            for (int i = 0; i < itemCount; i++)
            {
                string line = Console.ReadLine();
                ReceiptItem item = receiptGenerator.ParseReceiptItem(line);
                items.Add(item);
            }

            string receipt = receiptGenerator.GenerateReceipt(items);
            Console.WriteLine(receipt);
        }
    }
}
