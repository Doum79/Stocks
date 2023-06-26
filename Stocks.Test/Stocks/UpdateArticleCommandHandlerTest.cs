using Stocks.Hexagone.Domain;
using Stocks.Hexagone.UseCases.Stocks.Commands;
using Stocks.Infrastructure.Mocks;

namespace Stocks.Test.Stocks
{
    public class UpdateArticleCommandHandlerTest
    {
        private readonly MockArticleRepository _articlesRepository;

        public UpdateArticleCommandHandlerTest()
        {
            _articlesRepository = new MockArticleRepository();
        }

        [Fact]
        public void Quand_Prix_Est_Inferieur_Ou_Egal_Zero_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            var updateArticleCommand = new UpdateArticleCommand { Price = 0 };
            var updateArticleCommandHandler = new UpdateArticleCommandHandler(_articlesRepository);

            //Act

            var result = updateArticleCommandHandler.Handle(updateArticleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible de modifier un article avec un prix inferieur ou égale à 0", result.Message);
        }

        [Fact]
        public void Quand_Nom_Est_Inexistant_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            var updateArticleCommand = new UpdateArticleCommand { Price = 20, Name = "" };
            var updateArticleCommandHandler = new UpdateArticleCommandHandler(_articlesRepository);

            //Act
            var result = updateArticleCommandHandler.Handle(updateArticleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible de modifier un article avec un nom vide", result.Message);
        }

        [Fact]
        public void Quand_La_Reference_Nexiste_Pas_Devrait_Retourner_Une_Erreur()
        {
            //Arrange 
            var updateArticleCommand = new UpdateArticleCommand { Reference = "REF_INVALIDE", Price = 0 };
            var updateArticleCommandHandler = new UpdateArticleCommandHandler(_articlesRepository);

            //Act
            var result = updateArticleCommandHandler.Handle(updateArticleCommand);

            //Assert
            Assert.Equal(ResultCode.BadRequest, result.Code);
            Assert.Equal("Impossible de modifier un article avec un prix inferieur ou égale à 0", result.Message);
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
            var updateArticleCommand = new UpdateArticleCommand { 
                Price = 20, 
                Name = "Pomme", 
                Quantity = 67,
                Reference = "REF001" 
            };
            var updateArticleCommandHandler = new UpdateArticleCommandHandler(_articlesRepository);

            //Act
            var result = updateArticleCommandHandler.Handle(updateArticleCommand);
            var article = _articlesRepository.GetArticleByReference(updateArticleCommand.Reference);

            //Assert
            Assert.Equal(ResultCode.OK, result.Code);
            Assert.Equal("Article modifié avec succès", result.Message);

            var articleExpected = new Article
            {
                Id = 1,
                Reference = "REF001",
                Price = 20,
                Name = "Pomme",
                Quantity = 67,
            };

            Assert.Equivalent(articleExpected, article);
        }
    }
}
