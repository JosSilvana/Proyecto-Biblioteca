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
    public partial class Form1 : Form
    {
        ClsPrestamo objPrestamo = null;
        ClsPrestamoMgr objPrestamoMgr = null;
        ClsUsuario objUsuario = null;
        ClsUsuarioMgr objUsuarioMgr = null;
        ClsLibro objLibro = null;
        ClsLibroMgr objLibroMgr = null;
        DataTable Dtt = null;
        DataTable Dtt1 = null;
        DataTable Dtt2 = null;

        int numEjemplaresLibros;
        int numPrestamoDisponible;
        public int idUsuario1;

        public Form1(int Dato)
        {
            InitializeComponent();
            txtIdUsuario.Enabled = false;
            int DatodeForm1 = Dato;
            idUsuario1 = DatodeForm1;
            ConsultarUsuario();
            Listar();
            btnRegistrar.Enabled = false;
            dTPrestamo.Enabled = false;
        }


        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            ConsultarLibro();
            if (numEjemplaresLibros == 0 || numPrestamoDisponible == 0)
            {
                btnRegistrar.Enabled = false;
                MessageBox.Show("No hay ejemplares para prestar o el usuario no disponoe de cupo", "Mensaje");
            }
            else
            {
                btnRegistrar.Enabled = true;
            }
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
           
           if (dTEntrega.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("La fecha de entrega debe ser mayor a la fecha actual", "Mensaje");
            }
            else
            {
                Guardar();
            }
        }
        private void Listar()
        {
            objPrestamo = new ClsPrestamo();
            objPrestamo.opc = 1; //Para ejecutar el select en el Sp
            objPrestamo.idUsuario = idUsuario1;
            objPrestamoMgr = new ClsPrestamoMgr(objPrestamo);

            Dtt = new DataTable();
            Dtt = objPrestamoMgr.Listar();

            if (Dtt.Rows.Count > 0)
                dataGridView1.DataSource = Dtt;

        }

        private void Guardar()
        {
            objPrestamo = new ClsPrestamo();

            objPrestamo.opc = 2; //Para ejecutar el select en el Sp

            objPrestamo.idUsuario = int.Parse(txtIdUsuario.Text);
            objPrestamo.idLibro = txtIdLibro.Text;
            objPrestamo.fechaPrestamo =Convert.ToDateTime(dTPrestamo.Value.Date.ToString("yyy-MM-dd"));
            objPrestamo.fechaEntrega = Convert.ToDateTime(dTEntrega.Value.Date.ToString("yyy-MM-dd"));

            objPrestamoMgr = new ClsPrestamoMgr(objPrestamo);
            try
            {
                objPrestamoMgr.Guardar();
                LimpiarCampos();
                ConsultarUsuario();
                Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            MessageBox.Show("Prestamo de libro registrado correctamente", "Mensaje");
        }

        private void ConsultarUsuario()
        {
            
            objUsuario = new ClsUsuario();    
            objUsuario.idUsuario = idUsuario1; //Para ejecutar el select en el Sp
            objUsuarioMgr = new ClsUsuarioMgr(objUsuario);

            Dtt1 = new DataTable();
            Dtt1 = objUsuarioMgr.consultUsuarioID();

            if (Dtt1.Rows.Count > 0)
            {
                lblId.Text = Dtt1.Rows[0]["idUsuario"].ToString();
                txtIdUsuario.Text = lblId.Text;
                lblNombre.Text = Dtt1.Rows[0]["nombre"].ToString();
                lblApellido.Text = Dtt1.Rows[0]["apellido"].ToString();
                lblTipo.Text = Dtt1.Rows[0]["tipo"].ToString();
                lblNumDisponible.Text = Dtt1.Rows[0]["NumPrestamosDisponibles"].ToString();
                numPrestamoDisponible = int.Parse(lblNumDisponible.Text);
            }       
        }

        private void ConsultarLibro()
        {
            objLibro = new ClsLibro();
            objLibro.idLibro = txtIdLibro.Text; //Para ejecutar el select en el Sp
            objLibroMgr = new ClsLibroMgr(objLibro);
            Dtt2 = new DataTable();
            Dtt2 = objLibroMgr.ListarLibros();

            if (Dtt2.Rows.Count > 0)
            {
                lblAutor.Text = Dtt2.Rows[0]["autor"].ToString();
                lblTitulo.Text = Dtt2.Rows[0]["titulo"].ToString();
                lblNumEjemplares.Text = Dtt2.Rows[0]["numEjemplares"].ToString();
                numEjemplaresLibros = int.Parse(lblNumEjemplares.Text);
            }
            else {
                MessageBox.Show("Ese codigo de libro no existe", "Mensaje");
            }
        }

        private void LimpiarCampos()
        {
            lblAutor.Text = "";
            txtIdLibro.Text = "";
            lblNumEjemplares.Text = "";
            lblTitulo.Text = "";
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            Usuarios usuarios = new Usuarios();                       
            usuarios.Show();
            this.Close();
        }
    }
}