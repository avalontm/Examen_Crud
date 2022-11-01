using Examen.Database.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PluginSQL;

namespace Examen.Controllers
{
    public class GradosController : Controller
    {
        // GET: GradosController
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.Redirect("/account");
            }
            return View();
        }

        // GET: GradosController/Details/5
        public ActionResult Details(int id)
        {
            Grado model = Grado.Find(id);
            return View(model);
        }

        // GET: GradosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GradosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grado model)
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
                LOG.WriteLine($"[Alumno Create] {ex}");
                return View();
            }
        }

        // GET: GradosController/Edit/5
        public ActionResult Edit(int id)
        {
            Grado model = Grado.Find(id);
            return View(model);
        }

        // POST: GradosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Grado model)
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

        // GET: GradosController/Delete/5
        public ActionResult Delete(int id)
        {
            Grado model = Grado.Find(id);
            return View(model);
        }

        // POST: GradosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Grado model)
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
