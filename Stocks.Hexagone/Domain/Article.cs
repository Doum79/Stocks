namespace Stocks.Hexagone.Domain
{
    public class Article
    {
        public int Id { get; set; }
        public string? Reference { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int TypeArticleId { get; set; }
        public bool CanTakeaway { get; set; }
    }
}
