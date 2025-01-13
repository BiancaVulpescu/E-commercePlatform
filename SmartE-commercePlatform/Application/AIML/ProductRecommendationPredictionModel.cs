using Application.AIML;
using Microsoft.ML;
using System.Collections.Generic;

public class ProductRecommendationPredictionModel
{
    private MLContext mlContext;
    private ITransformer model;

    public ProductRecommendationPredictionModel()
    {
        mlContext = new MLContext(seed: 1); // Initialize MLContext
    }

    public void Train(List<ProductData> productData)
    {
        var dataView = mlContext.Data.LoadFromEnumerable(productData);

        var pipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: nameof(ProductData.CategoryId))
            .Append(mlContext.Transforms.Concatenate("Features", nameof(ProductData.CategoryId)))
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: "PredictedCategoryId", inputColumnName: "Label"));

        model = pipeline.Fit(dataView);
    }

}
