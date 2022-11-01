using Examen.Database.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PluginSQL;

namespace Examen.Controllers
{
    public class AlumnosController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.Redirect("/account");
            }
            return View();
        }

        // GET: AlumnosController/Details/5
        public ActionResult Details(int id)
        {
            Alumno model = Alumno.Find(id);
            return View(model);
        }

        // GET: AlumnosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlumnosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Alumno model)
        {
            try
            {
                if (model.Insert())
                {
                    return RedirectToAction(nameof(Index));
                }
             
                return View();
            }
            catch(Exception ex)
            {
                LOG.WriteLine($"[Alumno Create] {ex}");
                return View();
            }
        }

        // GET: AlumnosController/Edit/5
        public ActionResult Edit(int id)
        {
            Alumno model = Alumno.Find(id);
            return View(model);
        }

        // POST: AlumnosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Alumno model)
        {
            try
            {
                if (model.Update())
                {
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch(Exception ex)
            {
                LOG.WriteLine($"[EDIT] {ex}");
                return View();
            }
        }

        // GET: AlumnosController/Delete/5
        public ActionResult Delete(int id)
        {
            Alumno model = Alumno.Find(id);
            return View(model);
        }

        // POST: AlumnosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Alumno model)
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
