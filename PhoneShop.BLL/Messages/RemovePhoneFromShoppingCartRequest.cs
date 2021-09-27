namespace PhoneShop.BLL.Messages
{
    public class RemovePhoneFromShoppingCartRequest
    {
        public int CustomerId { get; set; }
        public int PhoneId { get; set; }
    }
}