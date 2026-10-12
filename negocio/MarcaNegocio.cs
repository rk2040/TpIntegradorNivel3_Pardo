using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, Descripcion from MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }
            }
            //catch (Exception ex) // Saco el catch porque no estoy haciendo nada con la ex
            //{
            //    throw ex;  // Relanza la excepción, pero puede perder información de la pila de llamadas
            //}
            finally // Cierro la conexión acá y no en AccesoDatos ejecutarLectura(), porque necesito que siga abierta para que continue leyendo mientras se este ejecuntando este metodo.
            {
                datos.cerrarConexion();
            }

            return lista;
        }
    }
}