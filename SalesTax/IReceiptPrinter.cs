using System.Collections.Generic;

namespace SalesTax
{
    public interface IReceiptPrinter
    {
        string GenerateReceipt(List<ReceiptItem> items);
    }
}
