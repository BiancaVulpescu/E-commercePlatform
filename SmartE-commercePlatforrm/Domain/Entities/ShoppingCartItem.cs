﻿namespace Domain.Entities 
{
    public class ShoppingCartItem : CartListItemBase
    {
        public Guid Cart_Id { get; set; }
        public int Quantity { get; set; }
    }
}
