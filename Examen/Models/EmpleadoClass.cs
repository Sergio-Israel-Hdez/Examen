using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen.Models.DB;

namespace Examen.Models
{
    public class EmpleadoClass
    {
        public IQueryable<Empleado> ListaEmpleados()
        {
            using (var db = new ExamenContext())
            {
                var result = db.Empleado.ToList();
                return result.AsQueryable();
            }
        }
        public Empleado DetalleEmpleado(int? id)
        {
            using (var db = new ExamenContext())
            {
                Empleado empleado = db.Empleado.Find(id);
                return empleado;
            }
        }
        public IQueryable<EmpleadoModel> ListaEmpleadoArea()
        { 
            var db = new ExamenContext();
            var result = (from emple in db.Empleado
                          join area in db.Area on emple.IdArea equals area.IdArea
                          select new EmpleadoModel
                          {
                              IdEmpleado = emple.IdEmpleado,
                              Nombre = emple.NombreCompleto,
                              Correo = emple.Correo,
                              Area = area.Nombre,
                              Foto = emple.Foto
                          });
            return result;
        }
        public bool EliminarEmpleado(int id)
        {
            using (var db = new ExamenContext())
            {
                Empleado old_empleado = db.Empleado.Find(id);
                if (old_empleado != null)
                {
                    db.Empleado.Remove(old_empleado);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool AgregarEmplado (Models.DB.Empleado in_empleado,byte[] image)
        {
            if (in_empleado!=null)
            {
                using (var db = new ExamenContext())
                {
                    Empleado new_empleado = new Empleado();
                    new_empleado.NombreCompleto = in_empleado.NombreCompleto;
                    new_empleado.Cedula = in_empleado.Cedula;
                    new_empleado.Correo = in_empleado.Correo;
                    new_empleado.FechaNacimiento = in_empleado.FechaNacimiento;
                    new_empleado.FechaIngreso = in_empleado.FechaIngreso;
                    new_empleado.IdJefe = in_empleado.IdJefe;
                    new_empleado.IdArea = in_empleado.IdArea;
                    new_empleado.Foto = image;

                    db.Empleado.Add(new_empleado);
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public IQueryable<dynamic> ListaJefe()
        {
            var db = new ExamenContext();
            var result = from emple in db.Empleado where emple.IdJefe == null select new { emple.IdEmpleado,emple.NombreCompleto };
            return result;
        }
        public bool Modificar(Models.DB.Empleado in_empleado, byte[] image)
        {
            if (in_empleado != null)
            {
                using (var db = new ExamenContext())
                {
                    Empleado new_empleado = db.Empleado.Find(in_empleado.IdEmpleado);
                    new_empleado.NombreCompleto = in_empleado.NombreCompleto;
                    new_empleado.Cedula = in_empleado.Cedula;
                    new_empleado.Correo = in_empleado.Correo;
                    new_empleado.FechaNacimiento = in_empleado.FechaNacimiento;
                    new_empleado.FechaIngreso = in_empleado.FechaIngreso;
                    new_empleado.IdJefe = in_empleado.IdJefe;
                    new_empleado.IdArea = in_empleado.IdArea;
                    new_empleado.Foto = image;
                  
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool Eliminar(int? id)
        {
            try
            {
                using (var db = new ExamenContext())
                {
                    Empleado old_empleado = db.Empleado.Find(id);
                    if (old_empleado != null)
                    {
                        db.Empleado.Remove(old_empleado);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
