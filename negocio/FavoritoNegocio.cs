using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class FavoritoNegocio
    {
        public List<Articulo> listar(int id = 0)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select\r\nA.id,\r\nA.Codigo,\r\nA.Nombre,\r\nA.Descripcion,\r\nM.Descripcion as Marca,\r\nC.Descripcion as Categoria,\r\nA.IdMarca,\r\nA.IdCategoria,\r\nA.ImagenUrl,\r\nA.Precio\r\nfrom ARTICULOS A\r\ninner join MARCAS M on A.IdMarca = M.Id\r\ninner join CATEGORIAS C on A.IdCategoria = C.id\r\ninner join FAVORITOS F on A.Id = F.IdArticulo\r\nwhere F.IdUser = @id;");

                datos.setearParametros("@id", id);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.MarcaTipo = new Marca();
                    aux.MarcaTipo.Id = (int)datos.Lector["IdMarca"];
                    aux.MarcaTipo.Descripcion = (string)datos.Lector["Marca"];
                    aux.CategoriaTipo = new Categoria();
                    aux.CategoriaTipo.Id = (int)datos.Lector["IdCategoria"];
                    aux.CategoriaTipo.Descripcion = (string)datos.Lector["Categoria"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }
                return lista;
            }
            finally
            {
               datos.cerrarConexion();
            }
        }

        public void agregarFavorito(int idUsuario, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                List<Articulo> listaAux = new List<Articulo>();
                listaAux = listar(idUsuario);

                if (!esFavorito(idUsuario, idArticulo))
                {
                    datos.setearConsulta("Insert into FAVORITOS (IdUser, IdArticulo) values (@idUsuario, @idArticulo)");
                    datos.setearParametros("@idUsuario", idUsuario);
                    datos.setearParametros("@idArticulo", idArticulo);
                    datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }           
        }

        public bool esFavorito(int idUsuario, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> listaAux = new List<Articulo>();
            listaAux = listar(idUsuario);

            return listaAux.Any(x => x.Id == idArticulo); // Si no hay coincidencia devuelve false
        }

        public void eliminarFavorito(int idUsuario, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM FAVORITOS WHERE idUser = @idUsuario AND idArticulo = @idArticulo");
                datos.setearParametros("@idUsuario", idUsuario);
                datos.setearParametros("@idArticulo", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
