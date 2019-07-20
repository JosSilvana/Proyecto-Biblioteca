using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controlador;

namespace Biblioteca
{
    
    public partial class Usuarios : Form
    {
        

        ClsUsuario objUsuario = null;
        ClsUsuarioMgr objUsuarioMgr = null;
        DataTable Dtt = null;
        int disp;
        public Usuarios()
        {
            InitializeComponent();
            BtnPrestarLibro.Enabled = false; 
        }

        private void BtnBuscarUsuario_Click(object sender, EventArgs e)
        {
            ConsultarUsuario();
        }

        private void ConsultarUsuario()
        {
            objUsuario = new ClsUsuario();
            objUsuario.cedula = txtCedula.Text; //Para ejecutar el select en el Sp
            objUsuarioMgr = new ClsUsuarioMgr(objUsuario);

            Dtt = new DataTable();
            Dtt = objUsuarioMgr.consultUsuarioCedula();

            if (Dtt.Rows.Count > 0)
            {
                txtId.Text = Dtt.Rows[0]["idUsuario"].ToString();
                txtNombres.Text = Dtt.Rows[0]["nombre"].ToString();
                txtApellidos.Text = Dtt.Rows[0]["apellido"].ToString();
                txtTipo.Text = Dtt.Rows[0]["tipo"].ToString();
                txtDisponibilidad.Text = Dtt.Rows[0]["NumPrestamosDisponibles"].ToString();
                txtFechaNac.Text = (Dtt.Rows[0]["fecNacimiento"].ToString());
                BtnPrestarLibro.Enabled = true;
                disp = int.Parse(txtDisponibilidad.Text);

            }
            else {
              
                    MessageBox.Show("Usuario no existente", "Mensaje");
                }
            if (disp == 0)
            {
                MessageBox.Show("No tiene disponibilidad para llevarse un libro", "Mensaje");
                BtnPrestarLibro.Enabled = false;
            }

        }

        private void BtnPrestarLibro_Click(object sender, EventArgs e)
        {
            Form1 formulario = new Form1(int.Parse(txtId.Text)); //te pide un parametro                          
            formulario.Show();
            this.Close();
        }
    }
}
