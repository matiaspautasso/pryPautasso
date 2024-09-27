using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Drawing;



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
            cadena = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=./DBAStore.accdb";
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
                conexion = new OleDbConnection(cadena);
                comando=new OleDbCommand();
                comando.Connection = conexion;  
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO ARTICULOS (Nombre, Descripcion, Precio, Stock, Categoria) " +
                             "VALUES (@nombre, @descripcion, @precio, @stock, @categoria)";
            
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@precio", precio);
                comando.Parameters.AddWithValue("@stock", stock);
                comando.Parameters.AddWithValue("@categoria", categoria);
            //recordar !!! ===>  ID: Es el nombre del parámetro en la consulta SQL.
            //ID: Es el valor que se asignará al parámetro @ID en la consulta.
            //        Esta variable contiene el valor que se quiere insertar en la columna IDproducto de la tabla.
                conexion.Open();
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
        public void MostrarListView(ListView listView)
        {
            OleDbConnection conexion = null;
            OleDbCommand comando = null;
            OleDbDataAdapter dataAdapter = null;

            try
            {
                // Inicializar la conexión
                conexion = new OleDbConnection(cadena);
                conexion.Open(); // Abrir la conexión

                // Crear el comando para la consulta SQL
                comando = new OleDbCommand("SELECT * FROM ARTICULOS", conexion);

                // Crear un DataTable para almacenar los datos
                DataTable TABLA = new DataTable();

                // Usar un OleDbDataAdapter para llenar el DataTable
                dataAdapter = new OleDbDataAdapter(comando);
                dataAdapter.Fill(TABLA);

                // Limpiar el ListView antes de cargar nuevos datos
                listView.Items.Clear();

                // Iterar sobre cada fila del DataTable y agregarla al ListView
                foreach (DataRow fila in TABLA.Rows)
                {
                    // Crear un ListViewItem con la primera columna (IdProducto)
                    ListViewItem item = new ListViewItem(fila["IdProducto"].ToString());

                    // Agregar el resto de las columnas como SubItems
                    item.SubItems.Add(fila["Nombre"].ToString());
                    item.SubItems.Add(fila["Descripcion"].ToString());
                    item.SubItems.Add(fila["Precio"].ToString());
                    item.SubItems.Add(fila["Stock"].ToString());
                    item.SubItems.Add(fila["Categoria"].ToString());

                    // Agregar el item al ListView
                    listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error si algo sale mal
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Asegurarse de cerrar y liberar recursos

                // Cerrar el adaptador si es necesario (aunque no requiere explícitamente un cierre)
                if (dataAdapter != null)
                {
                    dataAdapter.Dispose();
                }

                // Cerrar el comando si es necesario
                if (comando != null)
                {
                    comando.Dispose();
                }

                // Cerrar la conexión
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }
        public void Eliminar(int ID)    // 
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM ARTICULOS WHERE IDproducto = @ID";
                comando.Parameters.AddWithValue("@ID", ID);
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
