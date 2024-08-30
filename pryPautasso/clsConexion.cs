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
            cadena = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Alumno\\Source\\Repos\\pryPautasso\\pryPautasso\\carpeta1\\DBAStore.accdb";
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
            decimal precio, int stock, string categoria)

        {
            try
            {
                // Crear la conexión
                conexion = new OleDbConnection(cadena);

                // Crear el comando
                comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;

                // Construir la consulta SQL concatenando las variables directamente
                comando.CommandText = $"INSERT INTO ARTICULOS (Nombre, Descripcion, Precio, Stock, Categoria) " +
                                      $"VALUES ('{nombre}', '{descripcion}', {precio}, {stock}, '{categoria}')";

                // Abrir la conexión y ejecutar el comando
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                // Manejar excepciones
                Console.WriteLine("Error: " + ex.Message);
            }
            finally 
            {
             conexion.Close() ;
            }
        }

        public void MostrarGrilla(DataGridView grilla)
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM ARTICULOS";
                DataTable TABLA = new DataTable();
                dataAdapter = new OleDbDataAdapter(comando);
                dataAdapter.Fill(TABLA);
                grilla.DataSource = TABLA;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }
        public void Eliminar(string NOMBRE, string descripcion) // 
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = $"DELETE FROM ARTICULOS WHERE Nombre = '{NOMBRE}' " +
                    $"AND Descripcion = '{descripcion}'";
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        public void Actualizar(string nombre, string descripcion, decimal precio, int stock, string categoria, int id)
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = $"UPDATE ARTICULOS SET Nombre='{nombre}', " +
                    $"Descripcion='{descripcion}'," +
                    $" Precio={precio}, " +
                    $"Stock={stock}, " +
                    $"Categoria='{categoria}' " +
                    $"WHERE IDproducto = {id}";

                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }


    }
}
