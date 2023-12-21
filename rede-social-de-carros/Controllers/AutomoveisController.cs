using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rede_social_de_carros.Data;
using rede_social_de_carros.Models;

namespace rede_social_de_carros.Controllers
{
    [Authorize]
    public class AutomoveisController : Controller
    {
        private AutomovelService _automovelService;
        private UsuarioService _usuarioService;

        public AutomoveisController(AutomovelService automovelService, UsuarioService usuarioService) : base()
        {
            _automovelService = automovelService;
            _usuarioService = usuarioService;
        }

        // GET: Automoveis
        public async Task<IActionResult> Index()
        {
            var currentUser = User.Identity.Name;
            var automoveis = _automovelService.ObterTodos()
                .Result
                .Where(p => p.Usuario.UserName == currentUser);
            return View(automoveis);
        }

        // GET: Automoveis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _automovelService.ObterTodos() == null)
            {
                return NotFound();
            }

            var automovel = await _automovelService.ObterPorId((int)id);

            if (automovel == null)
            {
                return NotFound();
            }

            return View(automovel);
        }

        // GET: Automoveis/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_usuarioService.ObterTodos().Result.Where(u => u.Email == User.Identity.Name), "Id", "Id");
            return View();
        }

        // POST: Automoveis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,NomeAutomovel,Fabricante,Modelo,AnoDoAutomovel,Valor,Original,ImageUri")] Automovel automovel)
        {
            if (ModelState.IsValid)
            {
                _automovelService.Adicionar(automovel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_usuarioService.ObterTodos().Result.Where(u => u.Email == User.Identity.Name), "Id", "Id");
            return View(automovel);
        }

        // GET: Automoveis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _automovelService.ObterTodos() == null)
            {
                return NotFound();
            }

            var automovel = await _automovelService.ObterPorId((int)id);

            if (automovel == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_usuarioService.ObterTodos().Result.Where(u => u.Email == User.Identity.Name), "Id", "Id");
            return View(automovel);
        }

        // POST: Automoveis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,NomeAutomovel,Fabricante,Modelo,AnoDoAutomovel,Valor,Original,ImageUri")] Automovel automovel)
        {
            if (id != automovel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _automovelService.Editar(automovel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutomovelExists(automovel.Id))
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
            ViewData["UsuarioId"] = new SelectList(_usuarioService.ObterTodos().Result.Where(u => u.Email == User.Identity.Name), "Id", "Id");
            return View(automovel);
        }

        // GET: Automoveis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _automovelService.ObterTodos() == null)
            {
                return NotFound();
            }

            var automovel = await _automovelService.ObterPorId((int)id);
            if (automovel == null)
            {
                return NotFound();
            }

            return View(automovel);
        }

        // POST: Automoveis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_automovelService.ObterTodos() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Automoveis'  is null.");
            }
            var automovel = await _automovelService.ObterPorId((int)id);
            if (automovel != null)
            {
                await _automovelService.Deletar(automovel);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AutomovelExists(int id)
        {
            var automoveis = _automovelService.ObterTodos().Result;
            return (automoveis.Any(e => e.Id == id));
        }
    }
}
