using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.UI.Models;

namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var anuncios = await ObterTodosAnuncios();
            return View(anuncios);
        }

        public async Task<ActionResult> create()
        {
            var marcas = await ObterTodasMarcas();
            ViewBag.Marcas = new SelectList(marcas.Select(a => new { a.Id, a.Nome }).AsEnumerable(), "Id", "Nome");
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> create(AnuncioViewModel anuncio)
        {   
            if (TempData.ContainsKey("nomeMarca"))
                anuncio.Marca = TempData["nomeMarca"].ToString();

            var statusCode = await EnviarAnuncio(anuncio);
            if (statusCode.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var anuncio = await ObterAnuncioPorCodigo(id);
            return View(anuncio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnuncioViewModel anuncio)
        {
            await AtualizarAnuncio(anuncio);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var anuncio = await ObterAnuncioPorCodigo(id);
            return View(anuncio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await RemoverAnuncio(id);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IEnumerable<MarcaViewModel>> ObterTodasMarcas()
        {
            IEnumerable<MarcaViewModel> marcas = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/v1/");

                var responseTask = await client.GetAsync("anuncios/marca");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IList<MarcaViewModel>>();
                    marcas = readTask.Result;
                }
                else
                {
                    marcas = Enumerable.Empty<MarcaViewModel>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }

                return marcas;
            }
        }

        public async Task<IEnumerable<ModeloViewModel>> ObterModelo(int marcaId, string descricao)
        {
            IEnumerable<ModeloViewModel> modelos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/v1/");

                var responseTask = await client.GetAsync($"anuncios/modelo/{marcaId}");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IList<ModeloViewModel>>();
                    modelos = readTask.Result;
                }
                else
                {
                    modelos = Enumerable.Empty<ModeloViewModel>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
            }

            TempData["nomeMarca"] = descricao;
            return modelos;
        }

        public async Task<IEnumerable<VersaoViewModel>> ObterVersao(int modeloId)
        {
            IEnumerable<VersaoViewModel> versoes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/v1/");

                var responseTask = await client.GetAsync($"anuncios/versao/{modeloId}");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IList<VersaoViewModel>>();
                    versoes = readTask.Result;
                }
                else
                {
                    versoes = Enumerable.Empty<VersaoViewModel>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
            }

            return versoes;
        }

        private async Task<IEnumerable<AnuncioViewModel>> ObterTodosAnuncios()
        {
            IEnumerable<AnuncioViewModel> anuncios = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/v1/");

                var responseTask = await client.GetAsync("anuncios");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IList<AnuncioViewModel>>();
                    anuncios = readTask.Result;
                }
                else
                {
                    anuncios = Enumerable.Empty<AnuncioViewModel>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }

                return anuncios;
            }
        }

        private async Task<HttpResponseMessage> EnviarAnuncio(AnuncioViewModel anuncio)
        {
            using (var client = new HttpClient())
            {
                var endpoint = "https://localhost:5001/api/v1/anuncios";

                var json = JsonConvert.SerializeObject(anuncio);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                return await client.PostAsync(endpoint, payload);
            }
        }

        private async Task<HttpResponseMessage> AtualizarAnuncio(AnuncioViewModel anuncio)
        {
            var model = new AnuncioAtualizarViewModel();
            model.set(anuncio.Id, anuncio.Ano, anuncio.Quilometragem, anuncio.Observacao);
           

            using (var client = new HttpClient())
            {
                var endpoint = "https://localhost:5001/api/v1/anuncios";

                var json = JsonConvert.SerializeObject(model);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                return await client.PutAsync(endpoint, payload);
            }
        }

        private async Task<AnuncioViewModel> ObterAnuncioPorCodigo(int id)
        {
            AnuncioViewModel anuncios = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/v1/");

                var responseTask = await client.GetAsync($"anuncios/{id}");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<AnuncioViewModel>();
                    anuncios = readTask.Result;
                }
                else
                {
                    anuncios = null;
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }

                return anuncios;
            }
        }

        private async Task<HttpResponseMessage> RemoverAnuncio(int id)
        {   
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/v1/");
                var response =  await client.DeleteAsync($"anuncios/{id}");
                return response;
            }
        }
             
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
