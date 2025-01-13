using Application.AIML;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;
using Domain.Repositories;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductRecommendationPredictionController : ControllerBase
    {
        private readonly ProductRecommendationPredictionModel productRecommendationPredictionModel;
        private readonly ProductDataParser productDataParser;
        private readonly IProductRepository productRepository;

        public ProductRecommendationPredictionController(
            ProductRecommendationPredictionModel productRecommendationPredictionModel,
            ProductDataParser productDataParser,
            IProductRepository productRepository)
        {
            this.productRecommendationPredictionModel = productRecommendationPredictionModel;
            this.productDataParser = productDataParser;
            this.productRepository = productRepository;
        }

        [HttpPost("train")]
        public async Task<ActionResult> Train([FromBody] Guid cartsId)
        {
            try
            {
                var productDataList = await productDataParser.ParseUserProductsAsync(cartsId);
                productRecommendationPredictionModel.Train(productDataList);
                return Ok("Training completed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error during training: {ex.Message}");
            }
        }
    }
}
