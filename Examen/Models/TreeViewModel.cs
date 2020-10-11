using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.Models
{
    public class TreeViewModel
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public IEnumerable<Models.DB.Empleado> Asignado { get; set; }
    }
}
