using System.Collections.Generic;

namespace SalesTax
{
    public interface IReceiptGenerator
    {
        ReceiptItem ParseReceiptItem(string input);
        string GenerateReceipt(List<ReceiptItem> items);
    }
}
