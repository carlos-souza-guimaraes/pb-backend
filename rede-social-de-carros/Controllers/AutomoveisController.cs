using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rede_social_de_carros.Models;

namespace rede_social_de_carros.Controllers
{
    public class AutomoveisController : Controller
    {
        // GET: HomeController1
        public ActionResult Index()
        {
            Loader.ObterAutomoveis();
            var automoveis = AutomovelService.ObterLista();
            return View(automoveis);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            var automoveis = AutomovelService.ObterLista().Find(p => p.Id == id.ToString());
            return View(automoveis);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Loader.AdicionarAutomovel(new Automovel
                {
                    Id = collection["Id"],
                    UsuarioId = collection["UsuarioId"],
                    NomeAutomovel = collection["NomeAutomovel"],
                    Fabricante = collection["Fabricante"],
                    Modelo = collection["Modelo"],
                    AnoDoAutomovel = int.Parse(collection["AnoDoAutomovel"]),
                    Valor = int.Parse(collection["Valor"]),
                    Original = collection["Original"].Contains("true"),
                    Pecas = new Dictionary<string, string>(),
                    ImageUris = new List<string>() { "" }
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
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

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
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
