using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, Descripcion from CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }
            }
            //catch (Exception ex) // Saco el catch porque no estoy haciendo nada con la ex
            //{
            //    throw ex;  // Relanza la excepción, pero puede perder información de la pila de llamadas
            //}
            finally // Cierro la conexión al finalizar el método.
            {
                datos.cerrarConexion();
            }

            return lista;
        }
    }
}