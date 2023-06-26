using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;

namespace Stocks.Hexagone.UseCases.TypeArticles.Queries
{
    public class GetTypeArticleByIdQueryHandler
    {
        private readonly ITypeArticleRepository _typeArticleRepository;

        public GetTypeArticleByIdQueryHandler(ITypeArticleRepository typeArticleRepository)
        {
            _typeArticleRepository = typeArticleRepository;
        }

        public Result<TypeArticle> Handle(GetTypeArticleQuery query)
		{
            var typeArticle = _typeArticleRepository.GetTypeArticleById(query.Id);
            if (typeArticle == null)
            {
                return ResultFactory.Error<TypeArticle>("Type article introuvable");
            }

			return ResultFactory.Success(typeArticle);
		}
	}
}

