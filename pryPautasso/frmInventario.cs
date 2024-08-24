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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
