using Stocks.Hexagone.Domain;

namespace Stocks.Hexagone.Ports
{
    public interface IArticleRepository
    {
        bool IsReferenceExist(string? reference);
        void AddArticle(Article article);
        List<Article> GetArticles();
        List<Article> GetArticlesByName(string name);

        List<Article> GetArticlesByInterval(double priceMin, double priceMax);
        int DeleteArticle(string reference);
        Article? GetArticleByReference(string? reference);
        void UpdateArticle(Article article);
    }
}
