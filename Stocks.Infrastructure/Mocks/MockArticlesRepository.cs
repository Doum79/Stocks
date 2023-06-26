using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;

namespace Stocks.Infrastructure.Mocks
{
    public class MockArticleRepository : IArticleRepository
    {
        private static int _internalId = 1;
        private readonly List<Article> _articles = new();

        public void AddArticle(Article article)
        {
            var newArticle = new Article
            {
                Id = _internalId,
                Reference = article.Reference,
                Name = article.Name,
                Price = article.Price,
                Quantity = article.Quantity,
                CanTakeaway = article.CanTakeaway,
                TypeArticleId = article.TypeArticleId
            };

            _articles.Add(newArticle);
            _internalId++;
        }

        public void FeedWith(params Article[] articles)
        {
            _articles.Clear();
            _articles.AddRange(articles);
        }

        public void UpdateArticle(Article article)
        {
            var articleIndex = _articles.FindIndex(s => s.Reference == article.Reference);
            if (articleIndex != -1)
            {
                _articles[articleIndex] = article;
            }
        }

        public int DeleteArticle(string reference)
        {
            return _articles.RemoveAll(r => r.Reference == reference);
        }

        public List<Article> GetArticles()
        {
            return _articles;
        }

        public bool IsReferenceExist(string? reference)
        {
            return _articles.Any((article) => article.Reference == reference);
        }

        public List<Article> GetArticlesByName(string name)
        {
            return _articles.Where(article => article.Name!.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public Article? GetArticleByReference(string? reference)
        {
            return _articles.FirstOrDefault(s => s.Reference == reference);
        }

        public List<Article> GetArticlesByInterval(double priceMin, double priceMax)
        {
            return _articles.Where(x => x.Price >= priceMin && x.Price <= priceMax).ToList();
        }
    }
}
