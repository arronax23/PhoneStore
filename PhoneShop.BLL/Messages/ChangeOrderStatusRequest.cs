namespace PhoneShop.BLL.Messages
{
    public class ChangeOrderStatusRequest
    {
        public int OrderId { get; set; }
        public string NewStatus { get; set; }
    }
}