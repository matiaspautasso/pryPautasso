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
            cadena = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\matia\OneDrive\Escritorio\facultad\2024\4to sem\lab 3\tp1Lab3\pryPautasso\pryPautasso\Datos\BdPr1.accdb";
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
                comando.CommandText = "INSERT INTO Hoja 1  (Nombre, Descripción, Precio, Stock, Categoría) " +
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
                comando.CommandText = "SELECT * FROM Hoja 1";
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
                comando = new OleDbCommand("SELECT * FROM Hoja 1", conexion);

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
                conexion.Close();
            }
        }
        public void EliminarRegLv(ListView listView)
        {
            // Verificar que haya un ítem seleccionado
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un registro para eliminar.");
                return;
            }

            // Obtener el IdProducto del registro seleccionado (primer subitem del ListViewItem)
            string idProducto = listView.SelectedItems[0].Text;

            OleDbConnection conexion = null;
            OleDbCommand comando = null;

            try
            {
                // Confirmar antes de eliminar
                DialogResult confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este registro?",
                                                             "Confirmar eliminación",
                                                             MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Inicializar la conexión
                    conexion = new OleDbConnection(cadena);
                    conexion.Open(); // Abrir la conexión

                    // Crear el comando para eliminar el registro
                    string consultaEliminar = "DELETE FROM Hoja 1 WHERE Id = @IdProducto";
                    comando = new OleDbCommand(consultaEliminar, conexion);

                    // Asignar el parámetro
                    comando.Parameters.AddWithValue("@IdProducto", idProducto);

                    // Ejecutar la consulta de eliminación
                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Registro eliminado correctamente.");

                        // Actualizar el ListView llamando nuevamente a MostrarListView
                        MostrarListView(listView);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el registro.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error si ocurre un problema
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Asegurarse de cerrar y liberar recursos
               
                    comando.Dispose();
                

                
                    conexion.Close();
                
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
                comando.CommandText = "DELETE FROM Hoja 1 WHERE Id = @ID";
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
                comando.CommandText = $"UPDATE Hoja 1 SET Nombre='{nombre}', " +
                    $"Descripción='{descripcion}'," +
                    $" Precio={precio}, " +
                    $"Stock={stock}, " +
                    $"Categoría='{categoria}' " +
                    $"WHERE Id = {id}";

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
