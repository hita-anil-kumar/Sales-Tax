using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesTax
{
    //public class ReceiptGenerator
    //{
    //    private readonly decimal basicSalesTaxRate = 0.1m;
    //    private readonly decimal importDutyRate = 0.05m;
    //    private readonly decimal roundingFactor = 0.05m;

    //    private readonly Dictionary<string, bool> exemptItems = new Dictionary<string, bool>()
    //    {
    //        { "book", true },
    //        { "books", true },
    //        { "food", true },
    //        { "medical", true },
    //        { "headache", true },
    //        { "pill", true },
    //        { "pills", true }
    //    };


    //    public void GenerateReceipt(List<ReceiptItem> items)
    //    {
    //        decimal salesTax = 0;
    //        decimal totalAmount = 0;

    //        Console.WriteLine("Output:");
    //        var itemQuantities = new Dictionary<string, int>();
    //        foreach (var item in items)
    //        {
    //            decimal itemPrice = item.Price;
    //            decimal taxAmount = CalculateTaxAmount(item);

    //            salesTax += taxAmount;
    //            totalAmount += itemPrice + taxAmount;

    //            if (!itemQuantities.ContainsKey(item.Name))
    //            {
    //                itemQuantities[item.Name] = 1;
    //            }
    //            else
    //            {
    //                itemQuantities[item.Name]++;
    //            }
    //            //Console.WriteLine(itemQuantities[item.Name]);
    //           //Console.WriteLine(GetFormattedItemName(item));
    //            //Console.WriteLine(item.Price);
    //            Console.WriteLine($"{itemQuantities[item.Name]} {GetFormattedItemName(item)}: {itemPrice:F2}");
    //        }

    //        Console.WriteLine($"Sales Taxes: {salesTax:F2}");
    //        Console.WriteLine($"Total: {totalAmount:F2}");
    //        Console.WriteLine();
    //    }


    //    private string GetFormattedItemName(ReceiptItem item)
    //    {
    //        string itemName = item.Name;

    //        // Check if the item is imported and adjust the name accordingly
    //        if (item.IsImported)
    //        {
    //            itemName = itemName.Replace("imported", "").Trim();
    //            itemName = "imported " + itemName;
    //        }

    //        // Adjust the name if it contains the word "chocolate" to match the expected format
    //        //if (itemName.Contains("chocolate"))
    //        //{
    //        //    itemName = itemName.Replace("chocolate", "chocolates");
    //        //}

    //        return itemName;
    //    }


    //    private decimal CalculateTaxAmount(ReceiptItem item)
    //    {
    //        decimal taxAmount = 0;

    //        if (!item.IsExempt)
    //            taxAmount += Math.Ceiling(item.Price * basicSalesTaxRate / roundingFactor) * roundingFactor;

    //        if (item.IsImported)
    //            taxAmount += Math.Ceiling(item.Price * importDutyRate / roundingFactor) * roundingFactor;

    //        return taxAmount;
    //    }

    //    private static ReceiptItem ParseInput(string input)
    //    {
    //        ReceiptGenerator receiptGenerator = new ReceiptGenerator(); // Create an instance of ReceiptGenerator
    //        return receiptGenerator.ParseReceiptItem(input); // Call the ParseReceiptItem method using the instance
    //    }


    //    public ReceiptItem ParseReceiptItem(string input)
    //    {
    //        string[] parts = input.Split(" at ");
    //        string itemName = parts[0].Substring(parts[0].IndexOf(' ') + 1);
    //        decimal itemPrice = decimal.Parse(parts[1]);

    //        bool isImported = itemName.Contains("imported");
    //        bool isExempt = IsItemExempt(itemName);

    //        return new ReceiptItem(itemName, itemPrice, isImported, isExempt);
    //    }

    //    private bool IsItemExempt(string itemName)
    //    {
    //        return exemptItems.Keys.Any(itemName.Contains);
    //    }
    //}

    public class ReceiptGenerator
    {
        public ReceiptItem ParseReceiptItem(string input)
        {
            string[] parts = input.Split(" at ");
            int itemQuantity = int.Parse(parts[0].Split(" ")[0]);
            string itemName = parts[0].Substring(parts[0].IndexOf(' ') + 1);
            decimal itemPrice = decimal.Parse(parts[1]);

            bool isImported = itemName.Contains("imported");
            bool isExempt = IsItemExempt(itemName);
            //Console.WriteLine(itemName.Contains("imported"));
            Console.WriteLine(itemName);
            Console.WriteLine(isExempt);
            Console.WriteLine(isImported);
            return new ReceiptItem(itemName, itemPrice, itemQuantity, isImported, isExempt);
        }

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

            return sb.ToString();
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
                                    //if (item.IsImported)
                                    //{
                                    //    taxRate += 0.05m; // Additional tax for imported items
                                    //}
            if (item.IsImported && !item.IsExempt)
            {
                taxRate += 0.05m; // No tax for exempt items
            }
            else if (item.IsImported && item.IsExempt)
            {
                taxRate = 0.05m; // No tax for exempt items
            }
            else if (item.IsExempt && !item.IsImported)
            {
                taxRate = 0m; // No tax for exempt items
            }
            return taxRate;
        }

        private bool IsItemExempt(string itemName)
        {
            // Modify this method as per your exemption rules
            // For the given inputs, we consider only "book", "food", and "medical" as exempt items

            string[] exemptKeywords = { "book", "food","chocolate", "medical", "headache", "pills", "pill" };

            foreach (string keyword in exemptKeywords)
            {
                if (itemName.Contains(keyword))
                {
                    return true;
                }
                
            }

            return false;
        }
    }

}
