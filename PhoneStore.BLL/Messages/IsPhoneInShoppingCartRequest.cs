namespace PhoneStore.BLL.Messages
{
    public class IsPhoneInShoppingCartRequest
    {
        public int CustomerId { get; set; }
        public int PhoneId { get; set; }
    }
}