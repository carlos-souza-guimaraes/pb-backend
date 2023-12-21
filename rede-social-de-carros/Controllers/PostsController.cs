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
    public class PostsController : Controller
    {
        private PostService _postService;
        private UsuarioService _usuarioService;

        public PostsController(PostService postService, UsuarioService usuarioService) : base()
        {
            _postService = postService;
            _usuarioService = usuarioService;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {

            var currentUser = User.Identity.Name;
            var posts = _postService.ObterTodos()
                .Result
                .Where(p => p.Usuario.UserName == currentUser);
            return View(posts);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _postService.ObterTodos() == null)
            {
                return NotFound();
            }

            var post = await _postService.ObterPorId((int)id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_usuarioService.ObterTodos().Result.Where(u => u.Email == User.Identity.Name), "Id", "Id");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,DataPublicacao,Curtidas,ImagemUri,Titulo,Conteudo")] Post post)
        {
            post.Usuario = await _usuarioService.ObterPorId(post.UsuarioId);
            post.StampTime();
            if (ModelState.IsValid)
            {
                _postService.Adicionar(post);
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_usuarioService.ObterTodos().Result.Where(u => u.Email == User.Identity.Name), "Id", "Id", post.UsuarioId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _postService.ObterTodos() == null)
            {
                return NotFound();
            }

            var post = await _postService.ObterPorId((int)id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_usuarioService.ObterTodos().Result.Where(u => u.Email == User.Identity.Name), "Id", "Id", post.UsuarioId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,DataPublicacao,Curtidas,ImagemUri,Titulo,Conteudo")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _postService.Editar(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["UsuarioId"] = new SelectList(_usuarioService.ObterTodos().Result.Where(u => u.Email == User.Identity.Name), "Id", "Id", post.UsuarioId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _postService.ObterTodos() == null)
            {
                return NotFound();
            }
            var post = await _postService.ObterPorId((int)id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_postService.ObterTodos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var post = await _postService.ObterPorId((int)id);
            if (post != null)
            {
                await _postService.Deletar(post);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            var posts = _postService.ObterTodos().Result;
            return posts.Any(e => e.Id == id);
        }
    }
}
