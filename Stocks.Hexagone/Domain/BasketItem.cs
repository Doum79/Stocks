namespace Stocks.Hexagone.Domain
{
    public class BasketItem
    {
        public int PurchaseOrderId { get; set; }
        public string? ArticleReference { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Rate { get; set; }
        public bool IsTakeaway { get; set; }
    }
}
