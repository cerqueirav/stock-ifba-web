namespace stock_ifba_web.Models.ViewModels
{
    public class ConsultarPedidoViewModel
    {
        public List<Pedido> orders;

        public ConsultarPedidoViewModel()
        {
            orders = new List<Pedido>();
        }
    }
}
