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
    public class ClsPrestamoMgr
    {
        ClsDatos cnGeneral = null;
        private ClsPrestamo objPrestamo;
        DataTable tblDatos = null;

        public ClsPrestamoMgr(ClsPrestamo objPrestamo)
        {
            this.objPrestamo = objPrestamo;
        }

        //Retorna la tabla con datos
        public DataTable Listar()
        {
            tblDatos = new DataTable();
            try
            {
                cnGeneral = new ClsDatos();
                SqlParameter[] parParameter = new SqlParameter[2];

                parParameter[0] = new SqlParameter();
                parParameter[0].ParameterName = "@opc";
                parParameter[0].SqlDbType = SqlDbType.Int;
                parParameter[0].SqlValue = objPrestamo.opc;

                parParameter[1] = new SqlParameter();
                parParameter[1].ParameterName = "@idUsuario";
                parParameter[1].SqlDbType = SqlDbType.Int;
                parParameter[1].SqlValue = objPrestamo.idUsuario;

                tblDatos = cnGeneral.RetornaTabla(parParameter,"sp_consultarPrestamo");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return tblDatos;
        }

        public void Guardar()
        {
            try
            {
                cnGeneral = new ClsDatos();

                SqlParameter[] parParameter = new SqlParameter[5];

                parParameter[0] = new SqlParameter();
                parParameter[0].ParameterName = "@opc";
                parParameter[0].SqlDbType = SqlDbType.Int;
                parParameter[0].SqlValue = objPrestamo.opc;

                parParameter[1] = new SqlParameter();
                parParameter[1].ParameterName = "@idUsuario";
                parParameter[1].SqlDbType = SqlDbType.Int;
                parParameter[1].SqlValue = objPrestamo.idUsuario;

                parParameter[2] = new SqlParameter();
                parParameter[2].ParameterName = "@idLibro";
                parParameter[2].SqlDbType = SqlDbType.Char;
                parParameter[2].Size = 5;
                parParameter[2].SqlValue = objPrestamo.idLibro;

                parParameter[3] = new SqlParameter();
                parParameter[3].ParameterName = "@fechaPrestamo";
                parParameter[3].SqlDbType = SqlDbType.DateTime;
                parParameter[3].SqlValue = objPrestamo.fechaPrestamo;

                parParameter[4] = new SqlParameter();
                parParameter[4].ParameterName = "@fechaEntrega";
                parParameter[4].SqlDbType = SqlDbType.DateTime;
                parParameter[4].SqlValue = objPrestamo.fechaEntrega;
                cnGeneral.EjecutarSP(parParameter, "sp_consultarPrestamo");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}


