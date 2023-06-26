using Stocks.Hexagone.Domain;
using Stocks.Hexagone.Ports;

namespace Stocks.Infrastructure.Mocks
{
    public class MockTypeArticleRepository : ITypeArticleRepository
    {
        private readonly List<TypeArticle> _typeArticles;

        public MockTypeArticleRepository()
        {
            _typeArticles = new List<TypeArticle>();

            FeedWith(
                new TypeArticle { Id = 1, Label = "Non alimentaire", IsFoodProduct = false },
                new TypeArticle { Id = 2, Label = "Alimentaire", IsFoodProduct = true }
            );
        }

        public TypeArticle? GetTypeArticleById(int id)
        {
            return _typeArticles.FirstOrDefault(typeArticle => typeArticle.Id == id);
        }

        public void FeedWith(params TypeArticle[] typeArticles)
        {
            _typeArticles.Clear();
            _typeArticles.AddRange(typeArticles);
        }
    }
}

