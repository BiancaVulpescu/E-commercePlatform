using Application.AIML;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductRecommendationPredictionController : ControllerBase
    {
        private readonly ProductRecommendationPredictionModel productRecommendationPredictionModel;
        public ProductRecommendationPredictionController()
        {
            productRecommendationPredictionModel = new ProductRecommendationPredictionModel();


            var productData = ProductDataParser.ParseUserProductsAsync(cartsId).Result;

            productRecommendationPredictionModel.Train(productData);
        }

        [HttpPost("predict")]

        public ActionResult<float> Predict(ProductData productData)
        {
            return productRecommendationPredictionModel.Predict(productData);
        }

    }
}
