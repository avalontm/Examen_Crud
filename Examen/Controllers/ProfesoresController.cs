using Examen.Database.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PluginSQL;

namespace Examen.Controllers
{

    public class ProfesoresController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.Redirect("/account");
            }
            return View();
        }

        // GET: ProfesoresController/Details/5
        public ActionResult Details(int id)
        {
            Profesor model = Profesor.Find(id);
            return View(model);
        }

        // GET: ProfesoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfesoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Profesor model)
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
                LOG.WriteLine($"[Profesor Create] {ex}");
                return View();
            }
        }

        // GET: ProfesoresController/Edit/5
        public ActionResult Edit(int id)
        {
            Profesor model = Profesor.Find(id);
            return View(model);
        }

        // POST: ProfesoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Profesor model)
        {
            try
            {
                if (model.Update())
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

        // GET: ProfesoresController/Delete/5
        public ActionResult Delete(int id)
        {
            Profesor model = Profesor.Find(id);
            return View(model);
        }

        // POST: ProfesoresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Profesor model)
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
