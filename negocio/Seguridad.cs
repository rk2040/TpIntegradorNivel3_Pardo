using dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

            // return usuario != null && usuario.Id != 0;  // opción mas corta
        }

        public static bool esAdmin(object user)
        {
            //Usuario usuario = user != null ? (Usuario)user : null;

            Usuario usuario = user as Usuario;

            return usuario != null ? usuario.Admin : false;
            //return usuario != null && usuario.Admin; // mas corto
        }

        // SOBRECARGA 1- El metodo original que ya tenía (solo maneja el string)
        public static string manejoError(Exception error)
        {
            return error.Message;
        }

        // SOBRECARGA 2- El metodo nuevo (guarda en archivo y además devuelve el string)
        public static string manejoError(string nombreMetodo, Exception error)
        {
            // Arma el mensaje para el log
            string mensajeLog = $"[{DateTime.Now}] ERROR EN {nombreMetodo}: {error.Message}\n";

            // Traduce la ruta virtual con "~" a una ruta física real de Windows
            string rutaCarpetaErrores = HttpContext.Current.Server.MapPath("~/Errores/");

            // Si la carpeta "Errores" no existe en la raíz de Web Forms, se crea sola
            if (!Directory.Exists(rutaCarpetaErrores))
            {
                Directory.CreateDirectory(rutaCarpetaErrores);
            }

            // Combina la carpeta con el nombre del archivo de texto de forma segura
            string rutaArchivoCompleta = Path.Combine(rutaCarpetaErrores, "errores_log.txt");

            // Escribe el error
            File.AppendAllText(rutaArchivoCompleta, mensajeLog);

            // Reutiliza el método de arriba para devolver el mensaje así no duplico codigo
            return manejoError(error);
        }
    }
}