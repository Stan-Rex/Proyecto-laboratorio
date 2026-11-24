using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class clsAutenticacion
    {
        public string usuario = "Stalin";
        public string clave = "123456789";
        public bool Autenticacion(string usuarioIngresado, string claveIngresada)
        {
            if (usuarioIngresado == usuario && claveIngresada == clave)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
