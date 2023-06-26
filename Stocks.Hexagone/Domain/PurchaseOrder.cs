namespace Stocks.Hexagone.Domain
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string? ClientFullName { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
