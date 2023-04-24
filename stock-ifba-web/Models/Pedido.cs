namespace stock_ifba_web.Models
{
    public class Pedido
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime creationDate { get; set; }
        public double amount { get; set; }
        public int sellerId { get; set; }
        public int clientId { get; set; }
        public int stateId { get; set; }
    }
}
