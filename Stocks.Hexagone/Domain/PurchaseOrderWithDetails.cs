namespace Stocks.Hexagone.Domain
{
    public class PurchaseOrderWithDetails
    {
        public PurchaseOrder Order { get; set; } = default!;
        public List<BasketItem> Basket { get; set; } = new List<BasketItem>();
    }
}
