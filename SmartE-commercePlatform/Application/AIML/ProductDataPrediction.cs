using Microsoft.ML.Data;

namespace Application.AIML
{
    public class ProductDataPrediction
    {
        [ColumnName("PredictedProduct")]
        public float ProductId { get; set; }
    }
}
