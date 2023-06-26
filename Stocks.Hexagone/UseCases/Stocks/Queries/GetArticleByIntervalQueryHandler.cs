using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Hexagone.UseCases.Stocks.Queries
{
    public class GetArticleByIntervalQueryHandler
    {


        private readonly IArticleRepository _articlesRepository;
        public GetArticleByIntervalQueryHandler(IArticleRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public Result<List<Article>> Handle(GetArticleByIntervalQuery intervalArticleCommand)
        {
            if (intervalArticleCommand.PriceMin < 0 || intervalArticleCommand.PriceMax <0)
            {
                return ResultFactory.Error<List<Article>>("Impossible d'afficher un article avec un prix inferieur ou égale à 0");
            }

            var minPrice = Math.Min(intervalArticleCommand.PriceMin, intervalArticleCommand.PriceMax);
            var maxPrice = Math.Max(intervalArticleCommand.PriceMin, intervalArticleCommand.PriceMax);

            var articles = _articlesRepository.GetArticlesByInterval(minPrice, maxPrice);

            return ResultFactory.Success(articles);
        }
    }
}
