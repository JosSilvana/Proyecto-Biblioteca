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
    public class ClsLibroMgr
    {
        ClsDatos cnGeneral = null;
        private ClsLibro objLibro;
        DataTable tblDatos = null;

        public ClsLibroMgr(ClsLibro objLibro)
        {
            this.objLibro = objLibro;
        }
        public DataTable ListarLibros()
        {
            tblDatos = new DataTable();
            try
            {
                cnGeneral = new ClsDatos();
                SqlParameter[] parParameter = new SqlParameter[1];
                parParameter[0] = new SqlParameter();
                parParameter[0].ParameterName = "@idLibro";
                parParameter[0].SqlDbType = SqlDbType.Char;
                parParameter[0].Size = 5;
                parParameter[0].SqlValue = objLibro.idLibro;

                tblDatos = cnGeneral.RetornaTabla(parParameter, "sp_consultarLibroID");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return tblDatos;
        }
    }
}
