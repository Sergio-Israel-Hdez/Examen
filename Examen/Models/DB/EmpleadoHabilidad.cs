using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen.Models.DB
{
    public partial class EmpleadoHabilidad
    {
        public int IdHabilidad { get; set; }
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        public string NombreHabilidad { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
    }
}
