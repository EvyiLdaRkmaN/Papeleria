using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Papeleria
{
    /// <summary>
    /// Lógica de interacción para VerInventario.xaml
    /// </summary>
    public partial class VerInventario : Window
    {
        public VerInventario()
        {
            InitializeComponent();
            mostrarTabla();
        }


        public void mostrarTabla()
        {
            /*String consulta = "select idProducto,nombre,cantidad ,precioUnitario ,descripcion,idProvedor,Codigo from productos;";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new LlenadoDataGridProductos
                {
                    idProducto = row["idProducto"].ToString(),
                    Nombre = row["nombre"].ToString(),
                    Cantidad = row["cantidad"].ToString(),
                    PresioUnitario = row["precioUnitario"].ToString(),
                    Descripcion = row["descripcion"].ToString(),
                    idProveedor = row["idProvedor"].ToString(),
                    codigo = row["Codigo"].ToString(),
                };
                dataGridUsuarios.Items.Add(data);
            }*/
        }

        private void dataGridBuscador(int opcion, String dato)
        {
            /*String prueba = "";
            switch (opcion)
            {
                case 1:
                    prueba = "select idProducto,nombre,cantidad ,precioUnitario ,descripcion,idProvedor,Codigo from productos where nombre ='" + dato + "';";
                    break;
                case 2:
                    prueba = "select idProducto,nombre,cantidad ,precioUnitario ,descripcion,idProvedor,Codigo from productos where Codigo ='" + dato + "';";
                    break;
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(prueba, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new LlenadoDataGridProductos
                {
                    idProducto = row["idProducto"].ToString(),
                    Nombre = row["nombre"].ToString(),
                    Cantidad = row["cantidad"].ToString(),
                    PresioUnitario = row["precioUnitario"].ToString(),
                    Descripcion = row["descripcion"].ToString(),
                    idProveedor = row["idProvedor"].ToString(),
                    codigo = row["Codigo"].ToString(),
                };
                dataGridUsuarios.Items.Add(data);
            }*/
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String dato = tbBusqueda.Text;
            int index = cmbBuscarPor.SelectedIndex;
            if (dato != "" && dato != "Ingrese su busqueda" && index != 0)
            {
                dataGridUsuarios.Items.Clear();
                dataGridBuscador(index, dato);
                tbBusqueda.Text = "Ingrese su busqueda";
                cmbBuscarPor.SelectedIndex = 0;
            }
            else if (index == 0)
            {
                MessageBox.Show("seleccione una opcion de busqueda");
            }
            else
            {
                MessageBox.Show("Ingrese un texto para la busqueda");
            }
        }

        private void tbBusqueda_GotFocus(object sender, RoutedEventArgs e)
        {
            tbBusqueda.Text = "";
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dataGridUsuarios.Items.Clear();
            mostrarTabla();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
