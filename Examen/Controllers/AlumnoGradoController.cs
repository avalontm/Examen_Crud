using Examen.Database.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PluginSQL;

namespace Examen.Controllers
{
    public class AlumnoGradoController : Controller
    {
        // GET: AlumnoGradoController
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.Redirect("/account");
            }
            return View();
        }

        // GET: AlumnoGradoController/Details/5
        public ActionResult Details(int id)
        {
            AlumnoGrado model = AlumnoGrado.Find(id);
            return View(model);
        }

        // GET: AlumnoGradoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlumnoGradoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlumnoGrado model)
        {
            try
            {
                if (model.Insert())
                {
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch (Exception ex)
            {
                LOG.WriteLine($"[AlumnoGrado Create] {ex}");
                return View();
            }
        }

        // GET: AlumnoGradoController/Edit/5
        public ActionResult Edit(int id)
        {
            AlumnoGrado model = AlumnoGrado.Find(id);
            return View(model);
        }

        // POST: AlumnoGradoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AlumnoGrado model)
        {
            try
            {
                if (model.Update())
                {
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch (Exception ex)
            {
                LOG.WriteLine($"[EDIT] {ex}");
                return View();
            }
        }

        // GET: AlumnoGradoController/Delete/5
        public ActionResult Delete(int id)
        {
            AlumnoGrado model = AlumnoGrado.Find(id);
            return View(model);
        }

        // POST: AlumnoGradoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AlumnoGrado model)
        {
            try
            {
                if (model.Delete())
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
