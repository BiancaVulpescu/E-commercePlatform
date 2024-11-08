using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.Commands
{
    public class CreateShoppingCartItemCommand : CartListItemsBaseDto, IRequest<Result<Guid>>
    {
        public Guid Cart_Id { get; set; }
        public int Quantity { get; set; }
    }

}
