﻿namespace Domain.Entities 
{
    public class ShoppingCartItems : CartListItemsBase
    {
        public Guid Cart_Id { get; set; }
        public int Quantity { get; set; }
    }
}