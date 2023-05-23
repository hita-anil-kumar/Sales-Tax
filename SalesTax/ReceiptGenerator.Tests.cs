using NUnit.Framework;

namespace SalesTax.Tests
{
    [TestFixture]
    public class ReceiptGeneratorTests
    {
        private IReceiptGenerator? receiptGenerator;

        [SetUp]
        public void Setup()
        {
            ITaxCalculator taxCalculator = new TaxCalculator(); 
            receiptGenerator = new ReceiptGenerator(new ReceiptItemParser(), new ReceiptPrinter(taxCalculator)); 

        }

        [Test]
        public void Test_Input1_ReturnsCorrectReceipt()
        {
            // Arrange
            List<ReceiptItem> items = new List<ReceiptItem>
            {
                new ReceiptItem("book", 12.49m, 1, false, true),
                new ReceiptItem("music CD", 14.99m, 1, false, false),
                new ReceiptItem("chocolate bar", 0.85m, 1, false, true)
            };

            // Act
            string? receipt = receiptGenerator?.GenerateReceipt(items);

            // Assert
            Assert.IsNotNull(receipt);
            string expectedReceipt =
                "Output: 1 book: 12.49\n" +
                "1 music CD: 16.49\n" +
                "1 chocolate bar: 0.85\n" +
                "Sales Taxes: 1.50\n" +
                "Total: 29.83\n";
            Assert.AreEqual(expectedReceipt, receipt);
        }

        [Test]
        public void Test_Input2_ReturnsCorrectReceipt()
        {
            // Arrange
            List<ReceiptItem> items = new List<ReceiptItem>
            {
                new ReceiptItem("imported box of chocolates", 10.00m, 1, true, true),
                new ReceiptItem("imported bottle of perfume", 47.50m, 1, true, false)
            };

            // Act
            string? receipt = receiptGenerator?.GenerateReceipt(items);

            // Assert
            Assert.IsNotNull(receipt);
            string expectedReceipt =
                "Output: 1 imported box of chocolates: 10.50\n" +
                "1 imported bottle of perfume: 54.65\n" +
                "Sales Taxes: 7.65\n" +
                "Total: 65.15\n";
            Assert.AreEqual(expectedReceipt, receipt);
        }

        [Test]
        public void Test_Input3_ReturnsCorrectReceipt()
        {
            // Arrange
            List<ReceiptItem> items = new List<ReceiptItem>
            {
                new ReceiptItem("imported bottle of perfume", 27.99m, 1, true, false),
                new ReceiptItem("bottle of perfume", 18.99m, 1, false, false),
                new ReceiptItem("packet of headache pills", 9.75m, 1, false, true),
                new ReceiptItem("box of imported chocolates", 11.25m, 1, true, true)
            };

            // Act
            string? receipt = receiptGenerator?.GenerateReceipt(items);

            // Assert
            Assert.IsNotNull(receipt);
            string expectedReceipt =
                "Output: 1 imported bottle of perfume: 32.19\n" +
                "1 bottle of perfume: 20.89\n" +
                "1 packet of headache pills: 9.75\n" +
                "1 box of imported chocolates: 11.85\n" +
                "Sales Taxes: 6.70\n" +
                "Total: 74.68\n";
            Assert.AreEqual(expectedReceipt, receipt);
        }
    }
}
