using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba_Tecnica.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Fecha_Nacimiento { get; set; }
        public string Confirmar_Clave { get; set; }

    }
}