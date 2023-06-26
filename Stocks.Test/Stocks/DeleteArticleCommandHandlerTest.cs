using Stocks.Hexagone.Domain;
using Stocks.Hexagone.UseCases.Stocks.Commands;
using Stocks.Infrastructure.Mocks;

namespace Stocks.Test.Stocks
{
    public class DeleteArticleCommandHandlerTest
    {

        private readonly MockArticleRepository _articlesRepository;

        public DeleteArticleCommandHandlerTest()
        {
            _articlesRepository = new MockArticleRepository();
        }

        [Fact]
        public void Quand_La_Reference_Nexiste_Pas_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            var deleteArticleCommand = new DeleteArticleCommand { Reference = "REF_Inexistant"};
            var deleteArticleCommandHandler = new DeleteArticleCommandHandler(_articlesRepository);

            //Act
            var result = deleteArticleCommandHandler.Handle(deleteArticleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible de supprimer l'article: la reference n'existe pas", result.Message);
        }

        [Fact]
        public void Quand_Toute_Les_Condition_Sont_Respectes_Devrait_Retourner_Succes()
        {
            //Arrange 
             _articlesRepository.FeedWith(new Article { 
                Id = 1,
                Reference = "REF001", 
                Name = "Orange",
                Price = 5.4, 
                Quantity = 100
            });
            var deletearticleCommand = new DeleteArticleCommand { Reference = "REF001" };
            var deleteArticleCommandHandler = new DeleteArticleCommandHandler(_articlesRepository);

            //Act
            var result = deleteArticleCommandHandler.Handle(deletearticleCommand);
            var article = _articlesRepository.GetArticleByReference(deletearticleCommand.Reference);

            //Assert
            Assert.Equal(ResultCode.OK, result.Code);
            Assert.Equal("Article supprimé avec succès", result.Message);
        }
    }
}
