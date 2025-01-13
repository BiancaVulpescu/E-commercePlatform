using Microsoft.ML.Data;

namespace Application.AIML
{
    public class ProductDataPrediction
    {
        [ColumnName("Score")]
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? CategoryName { get; set; }
    }
}
