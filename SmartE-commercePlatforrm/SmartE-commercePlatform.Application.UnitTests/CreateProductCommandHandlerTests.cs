using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Repositories;
using System.Runtime.CompilerServices;

namespace SmartE_commercePlatform.Application.UnitTests
{
    public class CreateProductCommandHandlerTests
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public CreateProductCommandHandlerTests()
        {
            productRepository = Substitute.For<IProductRepository>();
        }
        [Fact]
        
    }
}