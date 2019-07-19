using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ClsUsuario
    {
        public int opc { get; set; }
        public int idUsuario { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string direccion { get; set; }
        public string tipo { get; set; }
        public int numeroPrestamosDisponibles { get; set; }
    }
}
