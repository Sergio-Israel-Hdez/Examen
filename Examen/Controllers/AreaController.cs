using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Examen.Models;
using X.PagedList;

namespace Examen.Controllers
{
    public class AreaController : Controller
    {
        AreaClass area = new AreaClass();
        const int pageSize = 5;
        public IActionResult Index(int? page)
        {
            var result = area.ListaAreas();
            int pageNumber = (page ?? 1);
            var listaPaginada = result.ToList().ToPagedList(pageNumber, pageSize);
            return View(listaPaginada);
        }
        public IActionResult AgregarArea()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AgregarArea(Models.DB.Area _area)
        {
            if (ModelState.IsValid)
            {
                var result = area.AgregarArea(_area);
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
                return View(_area);
            }
        }
        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id!=null)
            {
                var result = area.DetalleArea(id);
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
            if (id!=null)
            {
                var result = area.DetalleArea(id);
                return View(result);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Editar(Models.DB.Area in_area)
        {
            if (!ModelState.IsValid)
            {
                return View(in_area);
            }
            var result = area.ModificarArea(in_area);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(in_area);
            }
        }
        [HttpGet]
        public IActionResult Eliminar(int? id)
        {
            if (id!=null)
            {
                var result = area.Eliminar(id);
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
