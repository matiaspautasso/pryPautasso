using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryPautasso
{
    public partial class frmInventario : Form
    {
        public frmInventario()
        {
            InitializeComponent();
        }
        clsConexion clsConexion = new clsConexion();
        private void Form1_Load(object sender, EventArgs e)
        {
           // clsConexion.MostrarGrilla(dgvMostrar);
            clsConexion.VerificarConexion();
        }

        private void button1_Click(object sender, EventArgs e) //btnInsertar
        {
            //btn insertar este boton no sirve corregir
          
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            string categoria = cboCategoria.Text;
            decimal precio = decimal.Parse(txtPrecio.Text);
            int stock = int.Parse(txtStock.Text);

            clsConexion.insertar(nombre, descripcion, precio, stock, categoria);
            clsConexion.MostrarGrilla(dgvMostrar);
            Limpiar();
        }
        public void Limpiar()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            cboCategoria.Text = "";
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            frmEliminar frmEliminar = new frmEliminar();    
            frmEliminar.ShowDialog();   
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            string categoria = cboCategoria.Text;
            decimal precio = decimal.Parse(txtPrecio.Text);
            int stock = int.Parse(txtStock.Text);
            int id = int.Parse(txtID.Text);
            clsConexion.Actualizar(nombre,descripcion,precio,stock,categoria,id);
            clsConexion.MostrarGrilla(dgvMostrar);
        }
    }
}
