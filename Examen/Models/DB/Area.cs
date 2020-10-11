using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Examen.Models.DB
{
    
    public partial class Area
    {
        public Area()
        {
            Empleado = new HashSet<Empleado>();
        }

        public int IdArea { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
