using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen.Models.DB;

namespace Examen.Models
{
    public class AreaClass
    {
        public IQueryable<Area> ListaAreas()
        {
            using (var db = new ExamenContext())
            {
                var result = db.Area.ToList();
                return result.AsQueryable();
            }
        }
        public bool AgregarArea(Models.DB.Area in_area)
        {
            if (in_area!=null)
            {
                using (var db = new ExamenContext())
                {
                    db.Area.Add(in_area);
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public Area DetalleArea(int? id)
        {
            if (id!=null)
            {
                using (var db = new ExamenContext())
                {
                    Area area = db.Area.Find(id);
                    return area;
                }
            }
            else
            {
                return null;
            }
        }
        public bool ModificarArea(Area new_area)
        {
            if (new_area!=null)
            {
                using (var db = new ExamenContext())
                {
                    Area old_are = db.Area.Find(new_area.IdArea);
                    if (old_are!=null)
                    {
                        old_are.Nombre = new_area.Nombre;
                        old_are.Descripcion = new_area.Descripcion;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        public bool Eliminar(int? idarea)
        {
            if (idarea!=null)
            {
                using (var db = new ExamenContext())
                {
                    Area old_are = db.Area.Find(idarea);
                    if (old_are != null)
                    {
                        db.Area.Remove(old_are);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        public IQueryable<dynamic> ListaAreasDrop()
        {
            var db = new ExamenContext();
            var result = from area in db.Area select new { area.IdArea, area.Nombre };
            return result;
        }
        
    }
}
