using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ClsPrestamo
    {
        public int opc { get; set; }
        public int idPrestamo { get; set; }
        public int idUsuario { get; set; }
        public string idLibro { get; set; }
        public DateTime fechaPrestamo { get; set; }
        public DateTime fechaEntrega { get; set; }
    }
}
