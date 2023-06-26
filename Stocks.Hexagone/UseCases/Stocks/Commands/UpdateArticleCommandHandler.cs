using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;
using Stocks.Hexagone.Utils;

namespace Stocks.Hexagone.UseCases.Stocks.Commands
{
    public class UpdateArticleCommandHandler
    {
        private readonly IArticleRepository _articlesRepository;

        public UpdateArticleCommandHandler(IArticleRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public Result<NoData> Handle(UpdateArticleCommand updateArticleCommand)
        {
            if (updateArticleCommand.Price <= 0)
            {
                return ResultFactory.Error<NoData>("Impossible de modifier un article avec un prix inferieur ou égale à 0");
            }

            if (string.IsNullOrEmpty(updateArticleCommand.Name))
            {
                return ResultFactory.Error<NoData>("Impossible de modifier un article avec un nom vide");
            }

            var article = _articlesRepository.GetArticleByReference(updateArticleCommand.Reference);
            if(article == null)
            {
                return ResultFactory.Error<NoData>("Impossible de faire la mise à jour");
            }

            article.Name = updateArticleCommand.Name;
            article.Price = updateArticleCommand.Price;
            article.Quantity = updateArticleCommand.Quantity;

            _articlesRepository.UpdateArticle(article);

            return ResultFactory.Success<NoData>("Article modifié avec succès");
        }
    }
}
