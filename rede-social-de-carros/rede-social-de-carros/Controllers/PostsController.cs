using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rede_social_de_carros.Models;

namespace rede_social_de_carros.Controllers
{
    public class PostsController : Controller
    {
        // GET: PostsController
        public ActionResult Index()
        {
            Loader.ObterPosts();
            var posts = PostService.ObterLista();
            return View(posts);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            var post = PostService.ObterLista().Find(p => p.Id == id.ToString());
            return View(post);
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Loader.AdicionarPost(new Post
                {
                    Id = collection["Id"],
                    UsuarioId = collection["UsuarioId"],
                    DataPublicacao = collection["DataPublicacao"],
                    Curtidas = int.Parse(collection["Curtidas"]),
                    ImagemUri = collection["ImagemUri"],
                    Titulo = collection["Titulo"],
                    Conteudo = collection["Conteudo"]
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostsController/Edit/5
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

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostsController/Delete/5
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
