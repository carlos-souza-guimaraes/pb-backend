using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rede_social_de_carros.Models;

namespace rede_social_de_carros.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: UsuariosController
        public ActionResult Index()
        {
            Loader.ObterUsuarios();
            var users = UsuarioService.ObterLista();
            return View(users);
        }

        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            var users = UsuarioService.ObterLista().Find(p => p.Id == id.ToString());
            return View(users);
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Loader.AdicionarUsuario(new Usuario
                {
                    Id = collection["Id"],
                    NomeUsuario = collection["NomeUsuario"],
                    SenhaUsuario = collection["SenhaUsuario"],
                    Email = collection["Email"],
                    PostList = new List<Post>(),
                    Automoveis = new List<Automovel>()
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
