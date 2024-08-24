using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;



namespace pryPautasso
{
    internal class clsConexion
    {
        OleDbCommand comando;
        OleDbConnection conexion;
        OleDbDataAdapter dataAdapter;

        string cadena;
        public clsConexion()
        {
            cadena = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\\Escritorio\\pryPautasso\\pryPautasso\\bin\\Debug\\DBAStore.mdb";
        }
        public bool VerificarConexion()
        {
            bool resultado= false;
            conexion=new OleDbConnection(cadena);
            try
            {
                conexion.Open();
                resultado=true;
                MessageBox.Show("conectado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                conexion.Close();
            }
            return resultado;   
        }
        public void insertar(string nombre, string descripcion, 
            string precio, string stock, string categoria)
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                comando=new OleDbCommand();
                comando.Connection = conexion;  
                comando.CommandType = CommandType.Text;
                comando.CommandText = $"INSERT INTO ARTICULOS(Nombre,Descripcion,Precio," +
                    $"Stock,Categoria) VALUES('{nombre}'," +
                    $"'{descripcion}'," +
                    $"'{precio}'" +
                    $"'{stock}'" +
                    $"'{categoria}',)";
                conexion.Open() ;
                comando.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
            MessageBox.Show (ex.Message);   
            }finally 
            {
             conexion.Close() ;
            }
        }

    }
}
