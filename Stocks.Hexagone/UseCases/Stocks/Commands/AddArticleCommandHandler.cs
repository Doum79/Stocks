using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;
using Stocks.Hexagone.Utils;

namespace Stocks.Hexagone.UseCases.Stocks.Commands
{
    public class AddArticleCommandHandler
    {
        private readonly IArticleRepository _articlesRepository;
        private readonly ITypeArticleRepository _typeArticleRepository;

        public AddArticleCommandHandler(IArticleRepository articlesRepository, ITypeArticleRepository typeArticleRepository)
        {
            _articlesRepository = articlesRepository;
            _typeArticleRepository = typeArticleRepository;
        }

        public Result<NoData> Handle(AddArticleCommand command)
        {
            if (command.Price <= 0)
            {
                return ResultFactory.Error<NoData>("Impossible d'ajouter un article avec un prix inferieur ou égale à 0");  
            }

            if (string.IsNullOrEmpty(command.Name))
            {
                return ResultFactory.Error<NoData>("Impossible d'ajouter un article avec un nom vide");
            }

            var typeArticle = _typeArticleRepository.GetTypeArticleById(command.TypeArticleId);
            if(typeArticle == null)
            {
                return ResultFactory.Error<NoData>("Impossible d'ajouter l'article: type article introuvable");
            }

            if(CanTakeawaitNonFoodArticle(typeArticle, command))
            {
                return ResultFactory.Error<NoData>("Impossible d'emporter un article non alimentaire");
            }

            if (_articlesRepository.IsReferenceExist(command.Reference))
            {
                return ResultFactory.Error<NoData>("Impossible d'ajouter l'article: la reference existe dejà");
            }

            _articlesRepository.AddArticle(new Article
            {
                Name = command.Name,
                Price = command.Price,
                Quantity = command.Quantity,
                Reference = command.Reference,
                TypeArticleId = command.TypeArticleId,
                CanTakeaway = command.CanTakeaway,
            });

            return ResultFactory.Success<NoData>("Article ajouté avec succes");
        }

        private static bool CanTakeawaitNonFoodArticle(TypeArticle typeArticle, AddArticleCommand command)
        {
            return !typeArticle.IsFoodProduct && command.CanTakeaway;
        }
    }
}
