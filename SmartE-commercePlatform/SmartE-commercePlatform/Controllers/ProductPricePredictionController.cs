using Application.AIML;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductPricePredictionController : ControllerBase
    {
        private readonly ProductPricePredictionModel productPricePredictionModel;
        public ProductPricePredictionController()
        {
            productPricePredictionModel = new ProductPricePredictionModel();
            var sampleData = ProductDataGenerator.GenerateProductData();
            productPricePredictionModel.Train(sampleData);
        }

        [HttpPost("predict")]

        public ActionResult<float> Predict(ProductData productData)
        {
            return productPricePredictionModel.Predict(productData);
        }
    }
}
