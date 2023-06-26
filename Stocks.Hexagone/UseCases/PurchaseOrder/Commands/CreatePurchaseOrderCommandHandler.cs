using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;
using Stocks.Hexagone.Utils;

namespace Stocks.Hexagone.UseCases.PurchaseOrder.Commands
{
    public class CreatePurchaseOrderCommandHandler
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ITypeArticleRepository _typeArticleRepository;

        public CreatePurchaseOrderCommandHandler(
            IPurchaseOrderRepository commandRepository,
            IArticleRepository articleRepository,
            ITypeArticleRepository typeArticleRepository)
        {
            _purchaseOrderRepository = commandRepository;
            _articleRepository = articleRepository;
            _typeArticleRepository = typeArticleRepository;
        }

        public Result<NoData> Handle(CreatePurchaseOrderCommand command)
        {
            if (command.OrderItems == null || !command.OrderItems.Any())
            {
                return ResultFactory.Error<NoData>("Votre panier est vide");
            }

            if (string.IsNullOrEmpty(command.ClientFullName))
            {
                return ResultFactory.Error<NoData>("Le nom du client n'existe pas");
            }

            var basket = GetBasket(command.OrderItems);
            if (basket.IsError())
            {
                return ResultFactory.Error<NoData>(basket.Message);
            }

            // Si base de données réel, création une transaction pour cette operation
            var purchaseOrder = new Domain.PurchaseOrder { ClientFullName = command.ClientFullName };
            var order = _purchaseOrderRepository.CreatePurchaseOrder(purchaseOrder);
            foreach (var item in basket.Data!)
            {
                item.PurchaseOrderId = order.Id;
                _purchaseOrderRepository.AddItemToCommandBasket(item);
            }

            return ResultFactory.Success<NoData>("Commande ajoutée avec succes");
        }

        private Result<List<BasketItem>> GetBasket(List<OrderItem> orderItems)
        {
            var basket = new List<BasketItem>();
            foreach (var item in orderItems)
            {
                var article = _articleRepository.GetArticleByReference(item.ArticleReference);
                if (article == null)
                {
                    return ResultFactory.Error<List<BasketItem>>("Une erreur s'est produite lors de la création de la commande");
                }
                
                if (IsNonFoodProductAndAnormalyMarkedAsTakeaway(article, item))
                {
                    return ResultFactory.Error<List<BasketItem>>("Une erreur s'est produite lors de la création de la commande");
                }

                basket.Add(ToBasket(item, article));
            }

            return ResultFactory.Success(basket);
        }

        private bool IsNonFoodProductAndAnormalyMarkedAsTakeaway(Article article, OrderItem basketItem)
        {
            var typeArticle = _typeArticleRepository.GetTypeArticleById(article.TypeArticleId);

            return typeArticle == null ||
                (!typeArticle.IsFoodProduct && basketItem.IsTakeaway) ||
                (typeArticle.IsFoodProduct && !article.CanTakeaway && basketItem.IsTakeaway);
        }

        private static double GetRate(OrderItem item)
        {
            return item.IsTakeaway ? Rates.FOOD_RATE : Rates.NON_FOOD_RATE;
        }

        private static BasketItem ToBasket(OrderItem item, Article article)
        {
            var rate = GetRate(item);
            return new BasketItem
            {
                ArticleReference = article.Reference,
                Name = article.Name,
                Price = article.Price,
                Quantity = item.Quantity,
                IsTakeaway = item.IsTakeaway,
                Rate = rate
            };
        }
    }
}
