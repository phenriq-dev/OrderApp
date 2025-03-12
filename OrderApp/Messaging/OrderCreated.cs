namespace OrderApp.Messaging
{
    public class OrderCreated
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
