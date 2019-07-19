using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ClsDatos
    {
        SqlConnection cnnConexion = null;
        SqlCommand cmdComando = null;
        SqlDataAdapter daAdaptador = null;
        DataTable dtRegresa = null;
        string strCadenaConexion = string.Empty;

        //Constructor de la clase
        public ClsDatos()
        {
            strCadenaConexion = @"Data Source=JOSSELYN-R;Initial Catalog=biblioteca;Integrated Security=True";
        }

        public DataTable RetornaTabla(SqlParameter[] parParameter, string nombreSP)

        {
            try
            {
                dtRegresa = new DataTable();
                //Instanciamos el objeto conexion con la cadena de conexion
                cnnConexion = new SqlConnection(strCadenaConexion);

                //Trabajo con el command
                //Instanciamos el objeto comando con el nombreSP  y conexion a utilizar
                cmdComando = new SqlCommand();
                cmdComando.Connection = cnnConexion;
                //Definiendo el tipo de objeto comando como SP
                cmdComando.CommandType = CommandType.StoredProcedure;
                //asignamos el nombre del sp al objeto comando
                cmdComando.CommandText = nombreSP;
                //añadimos el arreglo  parametros en el objeto comando
                cmdComando.Parameters.AddRange(parParameter);

                //Trabajo con el adaptador
                daAdaptador = new SqlDataAdapter(cmdComando);
                daAdaptador.Fill(dtRegresa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnnConexion.Dispose();
                cmdComando.Dispose();
                daAdaptador.Dispose();
            }
            return dtRegresa;
        }

        public void EjecutarSP(SqlParameter[] parParameter, string nombreSP)
        {
            try
            {
                //Instanciamos el objeto conexion con la cadena de conexion
                cnnConexion = new SqlConnection(strCadenaConexion);
                //Instanciamos el objeto comando con el nombreSP  y conexion a utilizar
                cmdComando = new SqlCommand();
                cmdComando.Connection = cnnConexion;
                //Abrimos la conexion
                cnnConexion.Open();
                //Asignamos el tipo comando a ejecutar
                cmdComando.CommandType = CommandType.StoredProcedure;
                //Asignamos el nombre del store procedure
                cmdComando.CommandText = nombreSP;
                //agregamos los parametros a ejecutar
                cmdComando.Parameters.AddRange(parParameter);
                //Ejecutamos el TSQL enn el servidor
                cmdComando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnnConexion.Dispose();
                cmdComando.Dispose();

            }
        }
    }
}