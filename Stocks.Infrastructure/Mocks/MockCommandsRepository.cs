using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;

namespace Stocks.Infrastructure.Mocks
{
    public class MockCommandsRepository : IPurchaseOrderRepository
    {
        private readonly List<PurchaseOrder> _purchaseOrderList = new();
        private readonly List<BasketItem> _baskets = new();

        public PurchaseOrder CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            var order = new PurchaseOrder
            {
                Id = _purchaseOrderList.Count + 1,
                ClientFullName = purchaseOrder.ClientFullName,
                CreateAt = DateTime.Now
            };

            _purchaseOrderList.Add(order);

            return order;
        }

        public void AddItemToCommandBasket(BasketItem item)
        {
            _baskets.Add(item);
        }

        public List<PurchaseOrder> GetAllPurchaseOrders()
        {
            return _purchaseOrderList;
        }

        public List<BasketItem> GetPurcharseOrderItems(int orderId)
        {
            return _baskets.Where(basket => basket.PurchaseOrderId == orderId).ToList();
        }

        public void FeedWith(params PurchaseOrder[] commandes)
        {
            _purchaseOrderList.Clear();
            _purchaseOrderList.AddRange(commandes);
        }

        
    }
}
