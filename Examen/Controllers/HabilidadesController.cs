using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Examen.Controllers
{
    public class HabilidadesController : Controller
    {
        const string SessionIdEmpleado = "_idEmpleado";
        Models.HabilidadesEmpleadoClass _habilidad = new Models.HabilidadesEmpleadoClass();
        [HttpGet]
        public IActionResult AgregarHabilidad(int id)
        {
            ViewBag.emplado = id;
            HttpContext.Session.SetInt32(SessionIdEmpleado, id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarHabilidad(Models.DB.EmpleadoHabilidad habilidad)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }


            var result = _habilidad.AgregarHabilidad(habilidad);
            if (result)
            {
                int idempleado =Convert.ToInt32(HttpContext.Session.GetInt32(SessionIdEmpleado));
                return  RedirectToAction("Detalle","Empleado", new { id = idempleado });

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public IActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                var result = _habilidad.EliminarHabilidad(id);
                if (result)
                {
                    int idempleado = Convert.ToInt32(HttpContext.Session.GetInt32(SessionIdEmpleado));
                    return RedirectToAction("Detalle", "Empleado", new { id = idempleado });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public JsonResult SugerenciasHabilidad()
        {
            var result = _habilidad.HabilidadesSugerencias();
            return Json(result);
        }
    }
}
