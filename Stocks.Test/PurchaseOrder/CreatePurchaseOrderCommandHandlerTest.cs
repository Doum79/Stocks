using Stocks.Hexagone.Domain;
using Stocks.Hexagone.UseCases.PurchaseOrder.Commands;
using Stocks.Infrastructure.Mocks;

namespace Stocks.Hexagone.UseCases.Commands
{
    public class CreatePurchaseOrderCommandHandlerTest
    {
        private readonly MockCommandsRepository _commandeRepository;
        private readonly MockArticleRepository _mockArticlesRepository;
        private readonly MockTypeArticleRepository _mockTypeArticleRepository;

        public CreatePurchaseOrderCommandHandlerTest()
        {
            _commandeRepository = new MockCommandsRepository();
            _mockArticlesRepository = new MockArticleRepository();
            _mockTypeArticleRepository = new MockTypeArticleRepository();
        }

        [Fact]
        public void Quand_Le_Panier_D_Article_Est_Vide__Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            var purchaseOrder = new CreatePurchaseOrderCommand { OrderItems = new List<OrderItem>() };
            var purchaseOrderHandler = new CreatePurchaseOrderCommandHandler(_commandeRepository, _mockArticlesRepository, _mockTypeArticleRepository);

            //Act
            var result = purchaseOrderHandler.Handle(purchaseOrder);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Votre panier est vide", result.Message);
        }

        [Fact]
        public void Quand_Le_Nom_Du_Client_Est_Null_Ou_Vide_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            var purchaseOrder = new CreatePurchaseOrderCommand
            {
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem { ArticleReference = "REF_012", Quantity = 4, IsTakeaway = false },
                },
                ClientFullName = ""
            };

            var purchaseOrderHandler = new CreatePurchaseOrderCommandHandler(_commandeRepository, _mockArticlesRepository, _mockTypeArticleRepository);

            //Act

            var result = purchaseOrderHandler.Handle(purchaseOrder);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Le nom du client n'existe pas", result.Message);
        }

        [Fact]
        public void Quand_Au_Moins_Une_Reference_Est_Introuvable_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            var purchaseOrderHandler = new CreatePurchaseOrderCommandHandler(_commandeRepository, _mockArticlesRepository, _mockTypeArticleRepository);

            _mockArticlesRepository.FeedWith(
                new Article { Id = 1, Reference = "REF_012", Name = "Orange", Price = 2.3, Quantity = 10 },
                new Article { Id = 2, Reference = "REF_013", Name = "Poire", Price = 5.4, Quantity = 10 }
            );

            var purchaseOrder = new CreatePurchaseOrderCommand
            {
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem { ArticleReference = "REF_012", Quantity = 4, IsTakeaway = false },
                    new OrderItem { ArticleReference = "REF_Error", Quantity = 2, IsTakeaway = false }
                },
                ClientFullName = "Aziz"
            };


            //Act
            var result = purchaseOrderHandler.Handle(purchaseOrder);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Une erreur s'est produite lors de la création de la commande", result.Message);
        }

        [Fact]
        public void Quand_Commande_Contient_Un_Article_Non_Emportable_Marque_Comme_Emporte_Devrait_Retourner_Une_Erreur()
        {
            //Arrange
            _mockTypeArticleRepository.FeedWith(
                new TypeArticle { Id = 1, Label = "Non alimentaire", IsFoodProduct = false },
                new TypeArticle { Id = 2, Label = "Alimentaire", IsFoodProduct = true }
            );
            _mockArticlesRepository.FeedWith(
                new Article { Id = 1, Reference = "REF_004", Name = "Chaussure 1", Price = 39.99, Quantity = 100, TypeArticleId = 1, CanTakeaway = false },
                new Article { Id = 2, Reference = "REF_012", Name = "Orange", Price = 2.3, Quantity = 10, TypeArticleId = 2, CanTakeaway = false },
                new Article { Id = 3, Reference = "REF_013", Name = "Poire", Price = 5.4, Quantity = 10, TypeArticleId = 2, CanTakeaway = true }
            );

            var purchaseOrderHandler = new CreatePurchaseOrderCommandHandler(_commandeRepository, _mockArticlesRepository, _mockTypeArticleRepository);

            var purchaseOrder = new CreatePurchaseOrderCommand
            {
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem { ArticleReference = "REF_013", Quantity = 4, IsTakeaway = true },
                    new OrderItem { ArticleReference = "REF_004", Quantity = 2, IsTakeaway = true }, // Error
                    new OrderItem { ArticleReference = "REF_012", Quantity = 1, IsTakeaway = true }, // Error
                },
                ClientFullName = "Aziz"
            };

            //Act
            var result = purchaseOrderHandler.Handle(purchaseOrder);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Une erreur s'est produite lors de la création de la commande", result.Message);
        }

        [Fact]
        public void Quand_Toute_Les_Condition_Sont_Respectes_Devrait_Retourner_Succes()
        {
            //Arrange
            _mockTypeArticleRepository.FeedWith(
                new TypeArticle { Id = 1, Label = "Non alimentaire", IsFoodProduct = false },
                new TypeArticle { Id = 2, Label = "Alimentaire", IsFoodProduct = true }
            );
            _mockArticlesRepository.FeedWith(
                new Article { Id = 1, Reference = "REF_004", Name = "Chaussure 1", Price = 39.99, Quantity = 100, TypeArticleId = 1, CanTakeaway = false },
                new Article { Id = 2, Reference = "REF_012", Name = "Orange", Price = 2.3, Quantity = 10, TypeArticleId = 2, CanTakeaway = false },
                new Article { Id = 3, Reference = "REF_013", Name = "Poire", Price = 5.4, Quantity = 10, TypeArticleId = 2, CanTakeaway = true }
            );

            var purchaseOrderHandler = new CreatePurchaseOrderCommandHandler(_commandeRepository, _mockArticlesRepository, _mockTypeArticleRepository);

            var purchaseOrder = new CreatePurchaseOrderCommand
            {
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem { ArticleReference = "REF_013", Quantity = 4, IsTakeaway = true },
                    new OrderItem { ArticleReference = "REF_004", Quantity = 2 },
                },
                ClientFullName = "Aziz"
            };

            //Act
            var result = purchaseOrderHandler.Handle(purchaseOrder);

            //Assert
            Assert.Equal(ResultCode.OK, result.Code);
            Assert.Equal("Commande ajoutée avec succes", result.Message);
        }
    }
}
