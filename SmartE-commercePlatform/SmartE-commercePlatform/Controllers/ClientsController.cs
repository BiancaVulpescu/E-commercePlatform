using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator mediator;
        public ClientsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
