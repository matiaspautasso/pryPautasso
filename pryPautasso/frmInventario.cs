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
            
           // clsConexion.VerificarConexion();
        }

        private void button1_Click(object sender, EventArgs e) //btnInsertar
        {
            //btn insertar
            int ID=int.Parse(txtID.Text);
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            string categoria = txtCategoria.Text;
            decimal precio = decimal.Parse(txtPrecio.Text);
            int stock = int.Parse(txtStock.Text);

            clsConexion.insertar(ID,nombre, descripcion, precio, stock, categoria);
            clsConexion.MostrarGrilla(dgvMostrar);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            clsConexion.Eliminar(nombre,descripcion);
            clsConexion.MostrarGrilla(dgvMostrar);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            string categoria = txtCategoria.Text;
            decimal precio = decimal.Parse(txtPrecio.Text);
            int stock = int.Parse(txtStock.Text);
            int id = int.Parse(txtID.Text);
            clsConexion.Actualizar(nombre,descripcion,precio,stock,categoria,id);
            clsConexion.MostrarGrilla(dgvMostrar);
        }
    }
}
