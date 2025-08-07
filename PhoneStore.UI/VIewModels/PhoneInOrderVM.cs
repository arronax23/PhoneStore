namespace PhoneStore.UI.VIewModels
{
    public class PhoneInOrderVM
    {
        public int PhoneId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public PhoneColorVM Color { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
    }
}
