using Stocks.Hexagone.Domain;
using Stocks.Hexagone.UseCases.Stocks.Queries;
using Stocks.Infrastructure.Mocks;

namespace Stocks.Test.Stocks
{
    public class GetByNameArticleQueryHandlerTest
    {
        private readonly MockArticleRepository _articlesRepository;

        public GetByNameArticleQueryHandlerTest()
        {
            _articlesRepository = new MockArticleRepository();
        }

        [Fact]
        public void Quand_Nom_Est_Inexistant_Devrait_Retourner_Une_Erreur()
        {

            //Arrange 
            var getByNameArticleCommand = new GetArticleByNameQuery { Name = "" };
            var getByNameArticleCommandHandler = new GetArticleByNameQueryHandler(_articlesRepository);

            //Act
            var result = getByNameArticleCommandHandler.Handle(getByNameArticleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible d'afficher un article avec un nom vide", result.Message);
        }


        [Fact]
        public void Quand_Toute_Les_Condition_Sont_Respectes_Devrait_Retourner_Succes()
        {
            //Arrange 
            _articlesRepository.FeedWith(
                new Article { Id = 1, Reference = "REF001", Name = "Orange", Price = 2.3, Quantity = 10 },
                new Article { Id = 2, Reference = "REF002", Name = "Poire", Price = 5.4, Quantity = 10 },
                new Article { Id = 3, Reference = "REF003", Name = "Banane", Price = 0.4, Quantity = 10 },
                new Article { Id = 4, Reference = "REF004", Name = "Kiwi", Price = 7, Quantity = 10 }
                );

            var getByNameArticleCommand = new GetArticleByNameQuery { Name = "AN" };
            var getByNameArticleCommandHandler = new GetArticleByNameQueryHandler(_articlesRepository);

            //Act
            var result = getByNameArticleCommandHandler.Handle(getByNameArticleCommand);

            var expectedArticles = new List<Article>
            {
                new Article { Id = 1, Reference = "REF001", Name = "Orange", Price = 2.3, Quantity = 10 },
                new Article { Id = 3, Reference = "REF003", Name = "Banane", Price = 0.4, Quantity = 10 }
            };

            //Assert
            Assert.Equal(ResultCode.OK, result.Code);
            Assert.Equivalent(expectedArticles, result.Data);
        }
    }
}
