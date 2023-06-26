namespace Stocks.Hexagone.UseCases.PurchaseOrder.Commands
{
    public class CreatePurchaseOrderCommand
    {
        public List<OrderItem>? OrderItems { get; set; }
        public string? ClientFullName { get; set; }
    }
}
