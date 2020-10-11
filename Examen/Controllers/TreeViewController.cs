using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Examen.Controllers
{
    public class TreeViewController : Controller
    {
        public IActionResult Index()
        {
            var result = GetDatosTreeView();
            return View(result);
        }
        public IEnumerable<Models.TreeViewModel> GetDatosTreeView()
        {
            var db = new Models.DB.ExamenContext();
            var result = from em in db.Empleado
                         where em.IdJefe == null
                         select new Models.TreeViewModel
                         {
                             IdEmpleado = em.IdEmpleado,
                             Nombre = em.NombreCompleto,
                             Correo = em.Correo,
                             Asignado = (from e in db.Empleado where e.IdJefe == em.IdEmpleado select e)
                         };
            return result;
        }
    }
}
