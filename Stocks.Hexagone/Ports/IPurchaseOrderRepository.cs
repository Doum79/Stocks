using Stocks.Hexagone.Domain;

namespace Stocks.Hexagone.Ports
{
    public interface IPurchaseOrderRepository
    {
        void AddItemToCommandBasket(BasketItem item);
        PurchaseOrder CreatePurchaseOrder(PurchaseOrder commande);
        List<PurchaseOrder> GetAllPurchaseOrders();
        List<BasketItem> GetPurcharseOrderItems(int orderId);
    }
}
