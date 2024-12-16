namespace Application.AIML
{
    public static class ProductDataGenerator
    {
        public static List<ProductData> GenerateProductData()
        {
            return new List<ProductData>
            {
                new ProductData
                {
                    Title = "Product 1",
                    Description = "Description 1",
                    Price = 49.9f
                },
                new ProductData
                {
                    Title = "Product 2",
                    Description = "Description 2",
                    Price = 149.9f
                },
                new ProductData
                {
                    Title = "Product 3",
                    Description = "Description 3",
                    Price = 349.9f
                },
                new ProductData
                {
                    Title = "Product 4",
                    Description = "Description 4",
                    Price = 399.9f
                },
                new ProductData
                {
                    Title = "Product 5",
                    Description = "Description 5",
                    Price = 449.9f
                }
            };
        }
    }
}
 