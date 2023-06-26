using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;

namespace Stocks.Hexagone.UseCases.Stocks.Queries
{
    public class GetArticleByNameQueryHandler
    {
        private readonly IArticleRepository _articlesRepository;

        public GetArticleByNameQueryHandler(IArticleRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public Result<List<Article>> Handle(GetArticleByNameQuery getByNameArticleQuery)
        {
            if (string.IsNullOrEmpty(getByNameArticleQuery.Name))
            {
                return ResultFactory.Error<List<Article>>("Impossible d'afficher un article avec un nom vide");
            }

            var articles = _articlesRepository.GetArticlesByName(getByNameArticleQuery.Name);

            return ResultFactory.Success(articles);
        }
    }
}
