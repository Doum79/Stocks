namespace Stocks.Hexagone.UseCases.PurchaseOrder.Commands
{
    public class OrderItem
    {
        public string? ArticleReference { get; set; }
        public int Quantity { get; set; }
        public bool IsTakeaway { get; set; }
    }
}
