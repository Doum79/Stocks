using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;

namespace Stocks.Hexagone.UseCases.Stocks.Queries
{
    public class GetAllArticlesQueryHandler
    {
        private readonly IArticleRepository _articlesRepository;

        public GetAllArticlesQueryHandler(IArticleRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public Result<List<Article>> Handle()
        {
            var articles = _articlesRepository.GetArticles();

            return ResultFactory.Success(articles);
        }
    }
}
