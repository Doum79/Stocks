using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;
using Stocks.Hexagone.Utils;

namespace Stocks.Hexagone.UseCases.Stocks.Commands
{
    public class DeleteArticleCommandHandler
    {
        private readonly IArticleRepository _articlesRepository;

        public DeleteArticleCommandHandler(IArticleRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public Result<NoData> Handle(DeleteArticleCommand deleteArticleCommand)
        {
            var articleExist = _articlesRepository.IsReferenceExist(deleteArticleCommand.Reference);
            if (!articleExist)
            {
                return ResultFactory.Error<NoData>("Impossible de supprimer l'article: la reference n'existe pas");
            }

            _articlesRepository.DeleteArticle(deleteArticleCommand.Reference);

            return ResultFactory.Success<NoData>("Article supprimé avec succès");
        }
    }

}
