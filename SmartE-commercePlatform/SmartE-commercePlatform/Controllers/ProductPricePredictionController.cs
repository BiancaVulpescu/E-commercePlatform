using Application.AIML;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

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
            var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var filePath = Path.Combine(parentDirectory, "product_data.csv");

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException($"The dataset file was not found at {filePath}");
            }

            var productData = ProductDataParser.ParseCsv(filePath);

            productPricePredictionModel.Train(productData);
        }

        [HttpPost("predict")]

        public ActionResult<float> Predict(ProductData productData)
        {
            return productPricePredictionModel.Predict(productData);
        }
        
    }
}
