using Microsoft.ML;

namespace Application.AIML
{
    public class ProductPricePredictionModel
    {
        private readonly MLContext mlContext;
        private ITransformer model;
        public ProductPricePredictionModel()
        {
            mlContext = new MLContext();
        }
        public void Train(List<ProductData> productData)
        {
            var dataView = mlContext.Data.LoadFromEnumerable(productData);
            var pipeline = mlContext.Transforms.Text.FeaturizeText("TitleFeaturized", nameof(ProductData.Title))
                .Append(mlContext.Transforms.Text.FeaturizeText("DescriptionFeaturized", nameof(ProductData.Description))
                .Append(mlContext.Transforms.Concatenate("Features", "TitleFeaturized", "DescriptionFeaturized"))
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(ProductData.Price), maximumNumberOfIterations: 100)));

            model = pipeline.Fit(dataView);
        }

        public float Predict(ProductData productData)
        {
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ProductData, ProductDataPrediction>(model);
            var prediction = predictionEngine.Predict(productData);
            return prediction.Price;
        }
    }
}
