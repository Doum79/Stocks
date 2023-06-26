using Stocks.Hexagone.Domain;
using Stocks.Hexagone.UseCases.Stocks.Commands;
using Stocks.Infrastructure.Mocks;

namespace Stocks.Test.Stocks
{
    public class AddUserHandlerTest
    {
        private readonly MockArticleRepository _mockArticlesRepository;
        private readonly MockTypeArticleRepository _mockTypeArticleRepository;

        public AddUserHandlerTest()
        {
            _mockArticlesRepository = new MockArticleRepository();
            _mockTypeArticleRepository = new MockTypeArticleRepository();
        }

        [Fact]
        public void Quand_Prix_Est_Inferieur_Ou_Egal_Zero_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            var articleCommand = new AddArticleCommand { Price = 0 };
            var addArticleCommandHandler = new AddArticleCommandHandler(_mockArticlesRepository, _mockTypeArticleRepository);

            //Act
            var result = addArticleCommandHandler.Handle(articleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible d'ajouter un article avec un prix inferieur ou égale à 0", result.Message);
        }

        [Fact]
        public void Quand_Nom_Est_Inexistant_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            var articleCommand = new AddArticleCommand { Price = 20, Name = "" };
            var addArticleCommandHandler = new AddArticleCommandHandler(_mockArticlesRepository, _mockTypeArticleRepository);

            //Act
            var result = addArticleCommandHandler.Handle(articleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible d'ajouter un article avec un nom vide", result.Message);
        }

        [Fact]
        public void Quand_TypeArticle_Introuvable_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            _mockArticlesRepository.FeedWith(new Article { Reference = "REF001" });
            var articleCommand = new AddArticleCommand
            {
                Price = 20,
                Name = "Pomme",
                Reference = "REF001",
                TypeArticleId = 1
            };
            var addArticleCommandHandler = new AddArticleCommandHandler(_mockArticlesRepository, _mockTypeArticleRepository);

            //Act
            var result = addArticleCommandHandler.Handle(articleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible d'ajouter l'article: type article introuvable", result.Message);
        }

        [Fact]
        public void Quand_On_Marque_Un_Article_Non_Alimentaire_A_Emporter_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            _mockArticlesRepository.FeedWith(new Article { Reference = "REF001" });
            _mockTypeArticleRepository.FeedWith(
                new TypeArticle { Id = 1, Label = "Non alimentaire", IsFoodProduct = false },
                new TypeArticle { Id = 2, Label = "Alimentaire", IsFoodProduct = true }
            );

            var addArticleCommandHandler = new AddArticleCommandHandler(_mockArticlesRepository, _mockTypeArticleRepository);

            var articleCommand = new AddArticleCommand
            {
                Price = 20,
                Name = "Pomme",
                Reference = "REF001",
                TypeArticleId = 1,
                CanTakeaway = true
            };

            //Act
            var result = addArticleCommandHandler.Handle(articleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible d'emporter un article non alimentaire", result.Message);
        }

        [Fact]
        public void Quand_La_Reference_Exist_Devrait_Retourner_Une_Erreur()
        {
            //Arrange
            _mockTypeArticleRepository.FeedWith(
                new TypeArticle { Id = 1, Label = "Non alimentaire", IsFoodProduct = false },
                new TypeArticle { Id = 2, Label = "Alimentaire", IsFoodProduct = true }
            );
            _mockArticlesRepository.FeedWith(new Article { Reference = "REF001" });
            var addArticleCommandHandler = new AddArticleCommandHandler(_mockArticlesRepository, _mockTypeArticleRepository);

            var articleCommand = new AddArticleCommand
            {
                Price = 20,
                Name = "Pomme",
                Reference = "REF001",
                TypeArticleId = 1
            };

            //Act
            var result = addArticleCommandHandler.Handle(articleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible d'ajouter l'article: la reference existe dejà", result.Message);
        }

        [Fact]
        public void Quand_Toute_Les_Condition_Sont_Respectes_Devrait_Retourner_Succes()
        {
            //Arrange
            _mockTypeArticleRepository.FeedWith(
                new TypeArticle { Id = 1, Label = "Non alimentaire", IsFoodProduct = false },
                new TypeArticle { Id = 2, Label = "Alimentaire", IsFoodProduct = true }
            );

            var addArticleCommandHandler = new AddArticleCommandHandler(_mockArticlesRepository, _mockTypeArticleRepository);

            var articleCommand = new AddArticleCommand {
                Price = 20,
                Name = "Pomme",
                Reference = "REF001",
                TypeArticleId = 1
            };

            //Act
            var result = addArticleCommandHandler.Handle(articleCommand);
            var articles = _mockArticlesRepository.GetArticles();

            //Assert
            Assert.Equal(ResultCode.OK, result.Code);
            Assert.Equal("Article ajouté avec succes", result.Message);
            Assert.Single(articles);
        }
    }
}
