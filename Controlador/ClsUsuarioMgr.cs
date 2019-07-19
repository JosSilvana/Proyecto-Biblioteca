using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
namespace Controlador
{
    public class ClsUsuarioMgr
    {
        ClsDatos cnGeneral = null;
        private ClsUsuario objUsuario;
        DataTable tblDatos = null;

        public ClsUsuarioMgr(ClsUsuario objUsuario)
        {
            this.objUsuario = objUsuario;
        }

        //Retorna un usuario
        public DataTable consultUsuarioID()
        {
            tblDatos = new DataTable();
            try
            {
                cnGeneral = new ClsDatos();

                SqlParameter[] parParameter = new SqlParameter[1];

                parParameter[0] = new SqlParameter();
                parParameter[0].ParameterName = "@idUsuario";
                parParameter[0].SqlDbType = SqlDbType.Int;
                parParameter[0].SqlValue = objUsuario.idUsuario;

                tblDatos = cnGeneral.RetornaTabla(parParameter, "sp_consultarUsuarioID");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return tblDatos;
        }

        public DataTable consultUsuarioCedula()
        {
            tblDatos = new DataTable();
            try
            {
                cnGeneral = new ClsDatos();

                SqlParameter[] parParameter = new SqlParameter[1];

                parParameter[0] = new SqlParameter();
                parParameter[0].ParameterName = "@cedula";
                parParameter[0].SqlDbType = SqlDbType.NChar;
                parParameter[0].SqlValue = objUsuario.cedula;

                tblDatos = cnGeneral.RetornaTabla(parParameter, "sp_consultarUsuarioCedula");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return tblDatos;
        }
    }
}

