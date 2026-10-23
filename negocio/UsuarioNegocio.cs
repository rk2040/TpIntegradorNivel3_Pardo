using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public int agregarUsuario(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedAgregarUsuario");
                datos.setearParametros("@email", nuevo.Email);
                datos.setearParametros("@pass", nuevo.Pass);
                datos.setearParametros("@nombre", nuevo.Nombre);
                datos.setearParametros("@apellido", nuevo.Apellido);
                datos.setearParametros("@imagenPerfil", nuevo.ImagenPerfil);
                // Los usuarios nuevos se registran como no administradores por defecto. Por eso no agrego @admin

                return datos.ejecutarAccionScalar();
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

        public bool Login(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select Id, email, pass, nombre, apellido, urlImagenPerfil, admin from USERS where email = @email And pass = @pass");
                datos.setearParametros("@email", usuario.Email);
                datos.setearParametros("@pass", usuario.Pass);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"]; // id incremental desde BD 
                    if (!(datos.Lector["nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["apellido"];
                    if (!(datos.Lector["urlImagenPerfil"] is DBNull))    
                        usuario.ImagenPerfil = (string)datos.Lector["urlImagenPerfil"];
                    usuario.Admin = (bool)datos.Lector["admin"];
                    return true;
                }
                return false;
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

        public void actualizar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update USERS set Nombre = @nombre, Apellido = @apellido, urlImagenPerfil = @imagen Where Id = @id");
                datos.setearParametros("@id", usuario.Id);
                datos.setearParametros("@nombre", usuario.Nombre);
                datos.setearParametros("@apellido", usuario.Apellido);
                datos.setearParametros("@imagen", (object)usuario.ImagenPerfil ?? DBNull.Value);
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

        public List<Usuario> listarUsuarios(){
            AccesoDatos datos =new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();

            try
            {
                datos.setearConsulta("SELECT id, email, nombre, apellido, admin FROM USERS  ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Email = (string)datos.Lector["email"];
                    aux.Nombre = datos.Lector["nombre"] is DBNull ? "" : (string)datos.Lector["nombre"];
                    aux.Apellido = datos.Lector["apellido"] is DBNull ? "" : (string)datos.Lector["apellido"];
                    aux.Admin = (bool)datos.Lector["admin"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void cambiarPermisosAdmin(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Esto invierte el valor de admin (si es 1 pasa a 0, si es 0 pasa a 1)
                datos.setearConsulta("UPDATE USERS SET admin = CASE WHEN admin = 1 THEN 0 ELSE 1 END WHERE Id = @Id AND Id != 1"); // AND Id != 1 , para proteger al Admin original de posibles "Errores" y que quede sin Admin
                datos.setearParametros("@Id", idUsuario);
                datos.ejecutarAccion();
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

    }
}
