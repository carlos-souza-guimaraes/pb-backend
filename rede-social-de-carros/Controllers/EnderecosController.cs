using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using rede_social_de_carros.Data;
using rede_social_de_carros.Models;
using rede_social_de_carros.Services;

namespace rede_social_de_carros.Controllers
{
    [Authorize]
    public class EnderecosController : Controller
    {
        public readonly EnderecoService _enderecoService;
        public readonly UsuarioService _usuarioService;
        private readonly IHttpClientFactory _httpClientFactory;

        public EnderecosController(EnderecoService enderecoService, IHttpClientFactory httpClientFactory, UsuarioService usuarioService)
        {
            _enderecoService = enderecoService;
            _httpClientFactory = httpClientFactory;
            _usuarioService = usuarioService;
        }

        // GET: Enderecos
        public async Task<IActionResult> Index()
        {

            var usuario = _usuarioService.ObterTodos().Result.Find(u => u.Email == User.Identity.Name);
            var address = _enderecoService.ObterTodos()
                .Result
                .Where(a => a.Id == usuario.EnderecoId);
            return View(address);
        }

        // GET: Enderecos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _enderecoService.ObterTodos() == null)
            {
                return NotFound();
            }

            var endereco = await _enderecoService.ObterPorId((int)id);

            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // GET: Enderecos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enderecos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cep,Logradouro,Bairro,Localidade")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                await _enderecoService.Adicionar(endereco);

                var currentUser = User.Identity.Name;
                var user = _usuarioService.ObterTodos().Result.Find(u => u.Email == currentUser);

                if (user != null)
                {
                    user.EnderecoId = endereco.Id;
                    user.Endereco = endereco;
                    await _usuarioService.Editar(user);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        // GET: Enderecos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _enderecoService.ObterTodos() == null)
            {
                return NotFound();
            }

            var endereco = await _enderecoService.ObterPorId((int)id);

            if (endereco == null)
            {
                return NotFound();
            }
            return View(endereco);
        }

        // POST: Enderecos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cep,Logradouro,Bairro,Localidade")] Endereco endereco)
        {
            if (id != endereco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _enderecoService.Editar(endereco);

                    var currentUser = User.Identity.Name;
                    var user = _usuarioService.ObterTodos().Result.Find(u => u.Email == currentUser);

                    if (user != null)
                    {
                        user.EnderecoId = endereco.Id;
                        user.Endereco = endereco;
                        await _usuarioService.Editar(user);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoExists(endereco.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        // GET: Enderecos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _enderecoService.ObterTodos() == null)
            {
                return NotFound();
            }

            var endereco = await _enderecoService.ObterPorId((int)id);

            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Enderecos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _enderecoService.ObterTodos() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Endereco'  is null.");
            }
            var endereco = await _enderecoService.ObterPorId(id);
            if (endereco != null)
            {
                await _enderecoService.Deletar(endereco);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoExists(int id)
        {
            var enderecos = _enderecoService.ObterTodos().Result;
            return enderecos.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult GetAddressDetails(string cep)
        {
            if (cep.Length == 8)
            {
                var httpClient = _httpClientFactory.CreateClient("ViaCep");
                var apiEndpoint = $"ws/{cep}/json";

                var response = httpClient.GetAsync(apiEndpoint).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var addressDetails = JsonConvert.DeserializeObject<Endereco>(content);

                    return Json(addressDetails);
                }
            }

            return Json(null);
        }
    }
}
