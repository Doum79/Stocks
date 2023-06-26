using Stocks.Hexagone.Domain;
using Stocks.Hexagone.UseCases.Stocks.Queries;
using Stocks.Infrastructure.Mocks;

namespace Stocks.Test.Stocks
{
    public class GetAllArticlesQueryHandlerTest
    {
        private readonly MockArticleRepository _articlesRepository;

        public GetAllArticlesQueryHandlerTest()
        {
            _articlesRepository = new MockArticleRepository();
        }

        [Fact]
        public void Recuperer_Tous_Les_Articles()
        {
            //Arrange
            _articlesRepository.FeedWith(new Article
            {
                Id = 1,
                Reference = "REF001",
                Name = "Orange",
                Price = 5.4,
                Quantity = 100
            });

            var getAllArticlesHandler = new GetAllArticlesQueryHandler(_articlesRepository);

            //Act
            var result = getAllArticlesHandler.Handle();
            var expectedArticles = _articlesRepository.GetArticles();

            //Assert
            Assert.Equal(ResultCode.OK, result.Code);
            Assert.Equivalent(expectedArticles, result.Data);
        }
    }
}
