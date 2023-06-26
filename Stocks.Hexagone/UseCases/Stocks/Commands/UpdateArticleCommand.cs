namespace Stocks.Hexagone.UseCases.Stocks.Commands
{
    public class UpdateArticleCommand
    {
        public string Reference { get; set; } = default!;
        public double Price { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
    }
}
