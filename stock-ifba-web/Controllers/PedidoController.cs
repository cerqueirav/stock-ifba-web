using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stock_ifba_web.Models;
using stock_ifba_web.Models.ViewModels;
using System;
using System.Diagnostics;
using System.Text;

namespace stock_ifba_web.Controllers
{
    public class PedidoController : Controller
    {
        #region ATRIBUTOS E CONSTRUTORES
        private readonly IConfiguration _configuration;

        public PedidoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var uri = _configuration.GetSection("ConnectionHostApi").Value + "Order";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var requisicao = await httpClient.GetAsync(uri))
                    {
                        if (requisicao.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var objetoJson = requisicao.Content.ReadAsStringAsync().Result;
                            var objetoModel = JsonConvert.DeserializeObject<ConsultarPedidoViewModel>(objetoJson);
                            return View(objetoModel);
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Não foi possível consultar os pedidos! " + ex);
            }
        }


        public async Task<IActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(SolicitarPedidoViewModel solicitarPedidoViewModel)
        {
            var uri = _configuration.GetSection("ConnectionHostApi").Value + "Order";

            if (ModelState.IsValid)
            {
                try
                {
                    var body = new StringContent(JsonConvert.SerializeObject(solicitarPedidoViewModel.pedidoDto), UnicodeEncoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        using (var requisicao = await httpClient.PostAsync(uri, body))
                        {
                            if (requisicao.IsSuccessStatusCode)
                            {
                                try
                                {
                                    return RedirectToAction("Index");
                                }
                                catch (Exception ex)
                                {
                                    return View(solicitarPedidoViewModel);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View("Não foi possível cadastrar o pedido! " + ex);
                }
            }
            return View(solicitarPedidoViewModel);
        }

        public async Task<IActionResult> Deletar(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction(nameof(Error), new { message = "Não foi possível editar, pedido inválido!" });

            //var obj = await _pedidoService.ListarPorId(id.Value);

            var obj = new Pedido();

            if (obj is null)
                return RedirectToAction(nameof(Error), new { message = "Não foi possível editar, pedido inválido!" });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                //await _pedidoService.Deletar(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Detalhar(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction(nameof(Error), new { message = "Não foi possível editar, pedido inválido!" });

            //var obj = await _pedidoService.ListarPorId(id.Value);

            var obj = new Pedido();

            if (obj is null)
                return RedirectToAction(nameof(Error), new { message = "Não foi possível editar, pedido inválido!" });

            return View(obj);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction(nameof(Error), new { message = "Não foi possível editar, pedido inválido!" });

            //var obj = await _pedidoService.ListarPorId(id.Value);
            var obj = new Pedido();

            if (obj is null)
                return RedirectToAction(nameof(Error), new { message = "Não foi possível editar, pedido inválido!" });

            //var vendedores = await _vendedorService.Listar();
            //PedidoFormViewModel viewModel = new PedidoFormViewModel { Pedido = obj, Vendedores = vendedores };

            var viewModel = new Object();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                //var vendedores = await _vendedorService.Listar();

                //var viewModel = new PedidoFormViewModel { Pedido = pedido, Vendedores = vendedores };

                var viewModel = new Object();
                return View(viewModel);
            }

            if (!id.Equals(pedido.id))
                return RedirectToAction(nameof(Error), new { message = "Id é imcompatível" });

            try
            {
                //await _pedidoService.Atualizar(pedido);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
