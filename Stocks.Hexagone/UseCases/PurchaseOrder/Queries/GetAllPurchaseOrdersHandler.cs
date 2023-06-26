using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;

namespace Stocks.Hexagone.UseCases.PurchaseOrder.Queries
{
    public class GetAllPurchaseOrdersHandler
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public GetAllPurchaseOrdersHandler(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public Result<List<PurchaseOrderWithDetails>> Handle()
        {
            var orders = _purchaseOrderRepository.GetAllPurchaseOrders();

            var result = orders.Select(order =>
            {
                var basketItems = _purchaseOrderRepository.GetPurcharseOrderItems(order.Id);
                var purchaseOrderWithDetails = new PurchaseOrderWithDetails
                {
                    Order = order,
                    Basket = basketItems
                };

                return purchaseOrderWithDetails;
            }).ToList();

            return ResultFactory.Success(result);
        }
    }
}
