using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.Models
{
    public class HabilidadesEmpleadoClass
    {
        public IQueryable<Models.DB.EmpleadoHabilidad> ListaHabilidades(int? idempleado)
        {
            var db = new Models.DB.ExamenContext();
            var result = (from ha in db.EmpleadoHabilidad where ha.IdEmpleado == idempleado select ha);
            return result;
        }
        public bool AgregarHabilidad(Models.DB.EmpleadoHabilidad habilidad)
        {
            if (habilidad!=null)
            {
                using (var db = new Models.DB.ExamenContext())
                {
                    db.EmpleadoHabilidad.Add(habilidad);
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        public bool EliminarHabilidad(int? idhabilidad)
        {
            if (idhabilidad!=null)
            {
                using (var db = new Models.DB.ExamenContext())
                {
                    Models.DB.EmpleadoHabilidad empleHabilidad = db.EmpleadoHabilidad.Find(idhabilidad);
                    if (empleHabilidad!=null)
                    {
                        db.EmpleadoHabilidad.Remove(empleHabilidad);
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
        public IQueryable<string> HabilidadesSugerencias()
        {
            var db = new Models.DB.ExamenContext();
            var result = db.EmpleadoHabilidad.Select(ha => ha.NombreHabilidad).Distinct();

            return result;
        }
    }
}
