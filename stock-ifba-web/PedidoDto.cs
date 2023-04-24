namespace stock_ifba_web
{
    public class PedidoDto
    {
        public string Description { get; set; }
        public double Amount { get; set; }
        public int ClientId { get; set; }
        public int SellerId { get; set; }
    }
}
