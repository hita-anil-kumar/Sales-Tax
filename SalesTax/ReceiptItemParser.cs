namespace SalesTax
{
    public class ReceiptItemParser : IReceiptItemParser
    {
        public ReceiptItem ParseReceiptItem(string input)
        {
            string[] parts = input.Split(" at ");
            int itemQuantity = int.Parse(parts[0].Split(" ")[0]);
            string itemName = parts[0].Substring(parts[0].IndexOf(' ') + 1);
            decimal itemPrice = decimal.Parse(parts[1]);

            bool isImported = itemName.Contains("imported");
            bool isExempt = IsItemExempt(itemName);

            return new ReceiptItem(itemName, itemPrice, itemQuantity, isImported, isExempt);
        }

        public bool IsItemExempt(string itemName)
        {
            string[] exemptKeywords = { "book", "food", "chocolate", "medical", "headache", "pills", "pill" };

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
