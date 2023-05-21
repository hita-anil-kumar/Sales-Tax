using System;
using System.Collections.Generic;

//namespace SalesTax
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            List<List<string>> inputLists = new List<List<string>>()
//            {
//            new List<string>()
//            {
//                "1 book at 12.49",
//                "1 music CD at 14.99",
//                "1 chocolate bar at 0.85"
//            },
//            new List<string>()
//            {
//                "1 imported box of chocolates at 10.00",
//                "1 imported bottle of perfume at 47.50"
//            },
//            new List<string>()
//            {
//                "1 imported bottle of perfume at 27.99",
//                "1 bottle of perfume at 18.99",
//                "1 packet of headache pills at 9.75",
//                "1 box of imported chocolates at 11.25"
//            }
//        };

//            ReceiptGenerator receiptGenerator = new ReceiptGenerator();
//            List<List<ReceiptItem>> allItems = new List<List<ReceiptItem>>();

//            foreach (List<string> inputs in inputLists)
//            {
//                List<ReceiptItem> items = new List<ReceiptItem>();

//                foreach (string input in inputs)
//                {
//                    ReceiptItem item = receiptGenerator.ParseReceiptItem(input);
//                    items.Add(item);
//                }

//                allItems.Add(items);
//            }

//            for (int i = 0; i < allItems.Count; i++)
//            {
//                List<ReceiptItem> items = allItems[i];

//                Console.WriteLine("Output " + (i + 1) + ":");
//                string receipt = receiptGenerator.GenerateReceipt(items);
//                Console.WriteLine(receipt);
//                Console.WriteLine();
//            }
//        }
//    }

//}

using System;
using System.Collections.Generic;

namespace SalesTax
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
