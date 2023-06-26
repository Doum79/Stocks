using Stocks.Hexagone.Domain;
using Stocks.Hexagone.UseCases.Stocks.Queries;
using Stocks.Infrastructure.Mocks;

namespace Stocks.Test.Stocks
{
    public class GetByReferenceArticleQueryHandlerTest
    {
        private readonly MockArticleRepository _articlesRepository;

        public GetByReferenceArticleQueryHandlerTest()
        {
            _articlesRepository = new MockArticleRepository();
        }

        [Fact]
        public void Quand_La_Reference_Nexiste_Pas_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            _articlesRepository.FeedWith(new Article
            {
                Id = 1,
                Reference = "",
                Name = "Orange",
                Price = 100,
                Quantity = 50
            });
            var getByReferenceArticleCommand = new GetArticleByReferenceQuery { Reference = "REF_Inexistant" };
            var getByReferenceArticleCommandHandler = new GetArticleByReferenceQueryHandler(_articlesRepository);

            //Act
            var result = getByReferenceArticleCommandHandler.Handle(getByReferenceArticleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible de Afficher l'article: la reference n'existe pas", result.Message);
        }

        [Fact]
        public void Quand_Toute_Les_Condition_Sont_Respectes_Devrait_Retourner_Succes()
        {
            //Arrange
            var expectedArticle = new Article
            {
                Id = 1,
                Reference = "REF001",
                Name = "Orange",
                Price = 5.4,
                Quantity = 100
            };
            _articlesRepository.FeedWith(expectedArticle);

            var getByreferencearticleCommand = new GetArticleByReferenceQuery { Reference = "REF001" };
            var getByReferencearticleCommandHandler = new GetArticleByReferenceQueryHandler(_articlesRepository);

            //Act
            var result = getByReferencearticleCommandHandler.Handle(getByreferencearticleCommand);

            //Assert
            Assert.Equal(ResultCode.OK, result.Code);
            Assert.Equivalent(expectedArticle, result.Data);

        }
    }
}
