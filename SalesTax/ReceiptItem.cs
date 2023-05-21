namespace SalesTax
{
    public class ReceiptItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsImported { get; set; }
        public bool IsExempt { get; set; }
        public decimal TotalPrice => Quantity * Price;
        public ReceiptItem(string name, decimal price, int quantity, bool isImported, bool isExempt)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            IsImported = isImported;
            IsExempt = isExempt;
        }
    }


}
