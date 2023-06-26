using Stocks.Hexagone.Domain;

namespace Stocks.Hexagone.Ports
{
    public interface ITypeArticleRepository
	{
		TypeArticle? GetTypeArticleById(int id);
	}
}

