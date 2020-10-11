using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen.Models.DB
{
    public partial class Empleado
    {
        public Empleado()
        {
            EmpleadoHabilidad = new HashSet<EmpleadoHabilidad>();
            InverseIdJefeNavigation = new HashSet<Empleado>();
        }

        public int IdEmpleado { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Correo { get; set; }
        [Required]
        public DateTime? FechaNacimiento { get; set; }
        [Required]
        public DateTime FechaIngreso { get; set; }
        public int? IdJefe { get; set; }
        [Required]
        public int IdArea { get; set; }

        public byte[] Foto { get; set; }

        public virtual Area IdAreaNavigation { get; set; }
        public virtual Empleado IdJefeNavigation { get; set; }
        public virtual ICollection<EmpleadoHabilidad> EmpleadoHabilidad { get; set; }
        public virtual ICollection<Empleado> InverseIdJefeNavigation { get; set; }
    }
}
