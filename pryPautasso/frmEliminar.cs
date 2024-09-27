using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pryPautasso
{
    public partial class frmEliminar : Form
    {
        public frmEliminar()
        {
            InitializeComponent();
        }
        clsConexion clsConexion = new clsConexion();    

        private void frmEliminar_Load(object sender, EventArgs e)
        {
            LvMostrar.View = View.Details;
            LvMostrar.Columns.Add("IdProducto", 100);
            LvMostrar.Columns.Add("Nombre", 150);
            LvMostrar.Columns.Add("Descripcion", 200);
            LvMostrar.Columns.Add("Precio", 100);
            LvMostrar.Columns.Add("Stock", 80);
            LvMostrar.Columns.Add("Categoria", 120);
            clsConexion.MostrarListView(LvMostrar);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            clsConexion.EliminarRegLv(LvMostrar);
            clsConexion.MostrarListView(LvMostrar);    

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
