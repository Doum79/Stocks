using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;

namespace Stocks.Hexagone.UseCases.Stocks.Queries
{
    public class GetArticleByReferenceQueryHandler
    {

        private readonly IArticleRepository _articlesRepository;

        public GetArticleByReferenceQueryHandler(IArticleRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public Result<Article> Handle(GetArticleByReferenceQuery getByReferenceArticleCommand)
        {
            var articleFound = _articlesRepository.GetArticleByReference(getByReferenceArticleCommand.Reference);
            if (articleFound == null)
            {
                return ResultFactory.Error<Article>("Impossible de Afficher l'article: la reference n'existe pas");
            }

            return ResultFactory.Success(articleFound);
        }
    }
}
