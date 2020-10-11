using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Examen.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace Examen.Controllers
{
    
    public class EmpleadoController : Controller
    {
        const string SessionIdEmpleado = "_idEmpleado";
        EmpleadoClass empleado = new EmpleadoClass();
        AreaClass area = new AreaClass();
        HabilidadesEmpleadoClass habilidades = new HabilidadesEmpleadoClass();
        const int pageSize = 5;
        public IActionResult Index(string _NombreArea,int? page)
        {
            var result = empleado.ListaEmpleadoArea();
            if (!String.IsNullOrEmpty(_NombreArea))
            {
                result = result.Where(empleado => empleado.Area.Contains(_NombreArea));
            }
            int pageNumber = (page ?? 1);
            var listaPaginada = result.ToList().ToPagedList(pageNumber,pageSize);
            return View(listaPaginada);
        }
        public IActionResult AgregarEmpleado()
        {
            var resultjefe = empleado.ListaJefe();
            var resultArea =  area.ListaAreasDrop();
            SelectList listItemsJefe = new SelectList(resultjefe, "IdEmpleado", "NombreCompleto");
            SelectList listItemsArea = new SelectList(resultArea, "IdArea", "Nombre");

            ViewBag.IdJefe = listItemsJefe;
            ViewBag.IdArea = listItemsArea;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AgregarEmpleado(Models.DB.Empleado in_empleado,List<IFormFile> Foto)
        {
            if (!ModelState.IsValid)
            {
                return View(in_empleado);
            }
            byte[] image = null;
            foreach (var item in Foto)
            {
                if (item.Length>0)
                {
                    using (var stream = new MemoryStream())
                    {
                       await item.CopyToAsync(stream);
                       image = stream.ToArray();
                    }
                }
            }
            var result = empleado.AgregarEmplado(in_empleado,image);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(in_empleado);
            }
        }
        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id!=null)
            {
                var result = empleado.DetalleEmpleado(id);
                ViewBag.Habilidades = habilidades.ListaHabilidades(id);
                return View(result);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id != null)
            {
                HttpContext.Session.SetInt32(SessionIdEmpleado, Convert.ToInt32(id));
                var resultjefe = empleado.ListaJefe();
                var resultArea = area.ListaAreasDrop();
                SelectList listItemsJefe = new SelectList(resultjefe, "IdEmpleado", "NombreCompleto");
                SelectList listItemsArea = new SelectList(resultArea, "IdArea", "Nombre");

                ViewBag.IdJefe = listItemsJefe;
                ViewBag.IdArea = listItemsArea;
                var result = empleado.DetalleEmpleado(id);
                ViewBag.Habilidades = habilidades.ListaHabilidades(id);
                return View(result);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async  Task<IActionResult> Editar(Models.DB.Empleado in_empleado, List<IFormFile> Foto)
        {
            if (!ModelState.IsValid)
            {
                return View(in_empleado);
            }
            byte[] image = null;
            foreach (var item in Foto)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        image = stream.ToArray();
                    }
                }
            }
            var result = empleado.Modificar(in_empleado, image);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(in_empleado);
            }
        }
        public IActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                var result = empleado.Eliminar(id);
                if (result)
                {
                    return RedirectToAction("Index");
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
    }
}