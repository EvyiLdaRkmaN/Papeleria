using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Lógica de interacción para Vender.xaml
    /// </summary>
    public partial class Vender : Window
    {
        private int idUsuario, idVenta, modificar = 0;
        public Vender(int id)
        {
            InitializeComponent();
            this.idUsuario = id;
            mostrarTabla();
            String puesto = new BaseDeDatos().getPuesto(idUsuario);
            if (puesto == "Vendedor")
            {
                btnVolver.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSalir.Visibility = Visibility.Hidden;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            //int numeroVenta = new BaseDeDatos().NuevaVenta(idUsuario);
            new BaseDeDatos().registrarVenta(0,idUsuario,0,0);
            new NuevaVenta(idUsuario).Show();
            this.Close();
        }

        public void mostrarTabla()
        {
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            String consulta = "select idVentaProducto, usuarios.Nombre,usuarios.ApellidoP,usuarios.ApellidoM, total,FechaHora, ventaProducto.idUsuario from ventaProducto join usuarios on ventaProducto.idUsuario = usuarios.idUsuario where usuarios.idUsuario = "+idUsuario+ " and EstadoVenta != 'Pagado';";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new DGVender
                {
                    idVentaProducto = row["idVentaProducto"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    ApellidoP = row["ApellidoP"].ToString(),
                    ApellidoM = row["ApellidoM"].ToString(),
                    Total = row["total"].ToString(),
                    FechaHora = row["FechaHora"].ToString(),
                    idUsuario = row["idUsuario"].ToString()
                };
                dataGridVentas.Items.Add(data);
            }
        }

        private void dataGridBuscador(int opcion, String dato)
        {
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnVer.IsEnabled = false;
            String prueba = "";
            switch (opcion)
            {
                case 1:
                    prueba = "select idVentaProducto, usuarios.Nombre,usuarios.ApellidoP,usuarios.ApellidoM, total,FechaHora, ventaProducto.idUsuario from ventaProducto join usuarios on ventaProducto.idUsuario = usuarios.idUsuario where Nombre ='" + dato + "' and EstadoVenta != 'Pagado';";
                    break;
                case 2:
                    prueba = "select idVentaProducto, usuarios.Nombre,usuarios.ApellidoP,usuarios.ApellidoM, total,FechaHora, ventaProducto.idUsuario from ventaProducto join usuarios on ventaProducto.idUsuario = usuarios.idUsuario where idVentaProducto =" + dato + " and EstadoVenta != 'Pagado';";
                    break;
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(prueba, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new DGVender
                {
                    idVentaProducto = row["idVentaProducto"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    ApellidoP = row["ApellidoP"].ToString(),
                    ApellidoM = row["ApellidoM"].ToString(),
                    Total = row["total"].ToString(),
                    FechaHora = row["FechaHora"].ToString(),
                    idUsuario = row["idUsuario"].ToString()
                };
                dataGridVentas.Items.Add(data);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            new NuevaVenta(idUsuario, idVenta, 1).Show();
            this.Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boton = MessageBox.Show("¿Realmente desea eliminar esta venta?", "Alerta", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (boton == MessageBoxResult.OK)
            {
                new BaseDeDatos().eliminarVenta(idVenta);
                dataGridVentas.Items.Clear();
                mostrarTabla();
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            new PrincipalAdmin(idUsuario).Show();
            this.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String dato = tbBusqueda.Text;
            int index = cmbBuscarPor.SelectedIndex;
            if (dato != "" && dato != "Ingrese su busqueda" && index != 0)
            {
                dataGridVentas.Items.Clear();
                dataGridBuscador(index, dato);
                cmbBuscarPor.SelectedIndex = 0;
                tbBusqueda.Text = "Ingrese su busqueda";
            }
            else if(dato == "")
            {
                MessageBox.Show("Ingrese un texto para la busqueda");
            }
            else if (index == 0)
            {
                MessageBox.Show("seleccione una opcion de busqueda");
            }
        }

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            new NuevaVenta(idUsuario,idVenta,modificar).Show();
            this.Close();
        }

        private void tbBusqueda_GotFocus(object sender, RoutedEventArgs e)
        {
            tbBusqueda.Text = "";
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void dataGridVentas_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    DGVender dato2 = (DGVender)dataGridVentas.SelectedItem;
                    if (dato2 != null)
                    {
                        idVenta = Convert.ToInt16(dato2.idVentaProducto);
                    }
                    break;
                case Key.Up:
                    DGVender dato = (DGVender)dataGridVentas.SelectedItem;
                    if (dato != null)
                    {
                        idVenta = Convert.ToInt16(dato.idVentaProducto);
                    }
                    break;
            }

        }

        private void dataGridVentas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DGVender dato = (DGVender)dataGridVentas.SelectedItem;

            if (dato != null)
            {
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnVer.IsEnabled = true;

                idVenta = Convert.ToInt16(dato.idVentaProducto);
            }
        }
    }
}
