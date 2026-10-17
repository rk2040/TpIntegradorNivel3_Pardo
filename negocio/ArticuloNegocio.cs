using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dominio;

namespace negocio
{
    public class ArticuloNegocio
    { 
        //private AccesoDatos datos = new AccesoDatos();  // Inicializo datos en cada metodo que lo necesite y lo cierro en el mismo metodo.

  
        public List<Articulo> listarConSP(int id = 0) // = 0 para que sea un parametro "opcional". cuando no agregue parametro tomara 0 
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storedListar");
                datos.setearParametros("@id", id); // cambie el SP en la BD y ahora necesito un id, para poder usar solo listarConSP y asi poder borrar el metodo listar() ya que se repetian practicamente
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
            //catch (Exception ex) // Saco el catch porque no estoy haciendo nada con la ex
            //{
            //    throw ex;  // Relanza la excepción, pero puede perder información de la pila de llamadas
            //}
            finally // Cierro la conexión al finalizar el método.
            {
                datos.cerrarConexion();  
            }
        }

        public void agregarConSP(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedAltaArticulo");
                datos.setearParametros("@codigo", nuevo.Codigo);
                datos.setearParametros("@nombre", nuevo.Nombre);
                datos.setearParametros("@descripcion", nuevo.Descripcion);
                datos.setearParametros("@idMarca", nuevo.MarcaTipo.Id);
                datos.setearParametros("@idCategoria", nuevo.CategoriaTipo.Id);
                datos.setearParametros("@imagenUrl", nuevo.ImagenUrl);
                datos.setearParametros("@precio", nuevo.Precio);
                datos.ejecutarAccion();
            }
            //catch (Exception ex) // Saco el catch porque no estoy haciendo nada con la ex
            //{ 
            //    throw ex;  // Relanza la excepción, pero puede perder información de la pila de llamadas
            //}
            finally // Cierro la conexión acá y no en AccesoDatos, porque necesito que siga abierta para que continue leyendo mientras se este ejecuntando este metodo.
            {
                datos.cerrarConexion();
            }
        }


        public void modificarConSP(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarArticulo");
                //datos.setearConsulta("Update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @imagenUrl, Precio = @precio where id = @id");
                datos.setearParametros("@id", nuevo.Id);
                datos.setearParametros("@codigo", nuevo.Codigo);
                datos.setearParametros("@nombre", nuevo.Nombre);
                datos.setearParametros("@descripcion", nuevo.Descripcion);
                datos.setearParametros("@idMarca", nuevo.MarcaTipo.Id);
                datos.setearParametros("@idCategoria", nuevo.CategoriaTipo.Id);
                datos.setearParametros("@imagenUrl", nuevo.ImagenUrl);
                datos.setearParametros("@precio", nuevo.Precio);
                datos.ejecutarAccion();
            }
            //catch (Exception ex) // Saco el catch porque no estoy haciendo nada con la ex
            //{
            //    throw ex;  // Relanza la excepción, pero puede perder información de la pila de llamadas
            //}
            finally // Cierro la conexión al finalizar el método.
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            //catch (Exception ex) // Saco el catch porque no estoy haciendo nada con la ex
            //{
            //    throw ex;  // Relanza la excepción, pero puede perder información de la pila de llamadas
            //}
            finally // Cierro la conexión al finalizar el método.
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.ImagenUrl, A.Precio, A.Id, A.IdMarca, A.IdCategoria from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id AND A.IdCategoria = C.Id AND \r\n";

                decimal precio = 0; // Para validar que el valor ingresado sea valido

                if (campo == "Precio" && !decimal.TryParse(filtro, out precio))
                    throw new Exception("El precio ingresado no es valido");

                switch (campo)
                {
                    case "Codigo":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += "A.Codigo like @filtro";
                                datos.setearParametros("@filtro", filtro + "%");
                                break;
                            case "Termina con":
                                consulta += "A.Codigo like @filtro";
                                datos.setearParametros("@filtro", "%" + filtro);
                                break;
                            case "Contiene":
                                consulta += "A.Codigo like @filtro";
                                datos.setearParametros("@filtro", "%" + filtro + "%");
                                break;
                            default:
                                break;
                        }
                        break;

                    case "Nombre":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += "A.Nombre like @filtro";
                                datos.setearParametros("@filtro", filtro + "%");
                                break;
                            case "Termina con":
                                consulta += "A.Nombre like @filtro";
                                datos.setearParametros("@filtro", "%" + filtro);
                                break;
                            case "Contiene":
                                consulta += "A.Nombre like @filtro";
                                datos.setearParametros("@filtro", "%" + filtro + "%");
                                break;
                            default:
                                break;
                        }
                        break;
                    
                    case "Marca":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += "M.Descripcion like @filtro";
                                datos.setearParametros("@filtro", filtro + "%");
                                break;
                            case "Termina con":
                                consulta += "M.Descripcion like @filtro";
                                datos.setearParametros("@filtro", "%" + filtro);
                                break;
                            case "Contiene":
                                consulta += "M.Descripcion like @filtro";
                                datos.setearParametros("@filtro", "%" + filtro + "%");
                                break;
                            default:
                                break;
                        }
                        break;

                    case "Categoria":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += "C.Descripcion like @filtro";
                                datos.setearParametros("@filtro", filtro + "%");
                                break;
                            case "Termina con":
                                consulta += "C.Descripcion like @filtro";
                                datos.setearParametros("@filtro", "%" + filtro);
                                break;
                            case "Contiene":
                                consulta += "C.Descripcion like @filtro";
                                datos.setearParametros("@filtro", "%" + filtro + "%");
                                break;
                            default:
                                break;
                        }
                        break;

                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "A.Precio > @filtro";
                                datos.setearParametros("@filtro", precio);
                                break;
                            case "Menor a":
                                consulta += "A.Precio < @filtro";
                                datos.setearParametros("@filtro", precio);
                                break;
                            case "Igual a":
                                consulta += "A.Precio = @filtro";
                                datos.setearParametros("@filtro", precio);
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }

                datos.setearConsulta(consulta);
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
            //catch (Exception ex) // Saco el catch porque no estoy haciendo nada con la ex
            //{
            //    throw ex;  // Relanza la excepción, pero puede perder información de la pila de llamadas
            //}
            finally // Cierro la conexión al finalizar el método.
            {
                datos.cerrarConexion();
            }
        }
    }
}