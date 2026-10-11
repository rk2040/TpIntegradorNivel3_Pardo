using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            //Usuario usuario = user != null ? (Usuario)user : null;  // No comprueba si el obj es de Tipo Usuario

            Usuario usuario = user as Usuario; // Si el obj no es de tipo Usuario, devuelve null

            if (usuario != null && usuario.Id != 0)
                return true;
            else
                return false;
        }

        public static bool esAdmin(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            return usuario != null ? usuario.Admin : false;
        }

        public static string manejoError(object error)
        {
            return error.ToString();
        }

    }
}