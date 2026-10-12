using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void setearProcedimiento(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        public void ejecutarLectura()
        {   // Sin try catch porque entran en el try catch del metodo que lo llama
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        public void ejecutarAccion()
        {   // Sin try catch porque entran en el try catch del metodo que lo llama
            comando.Connection = conexion;
            conexion.Open();
            comando.ExecuteNonQuery();
        }

        public int ejecutarAccionScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return int.Parse(comando.ExecuteScalar().ToString());
            }
            //catch (Exception ex) // Saco el catch porque no estoy haciendo nada con la ex
            //{
            //    throw ex;  // Relanza la excepción, pero puede perder información de la pila de llamadas
            //}
            finally
            {  // Acá si cierro la conexión porque no tengo un lector que se vaya a leer en el metodo que lo llame
                conexion.Close(); 
            }
        }

        public void setearParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void cerrarConexion()
        {
            if(lector != null)
                lector.Close();

            conexion.Close();
        }
    }
}