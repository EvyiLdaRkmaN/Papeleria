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
    /// Lógica de interacción para Caja.xaml
    /// </summary>
    public partial class Caja : Window
    {
        private int idUsuario, idVenta;
        private double total;
        public Caja(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            mostrarTabla();
            mostrarPagados();
            int corte = new BaseDeDatos().validarSalidaCorte(idUsuario);
            if (corte == 1)
            {
                btnCorte.IsEnabled = false;
            }
            String puesto = new BaseDeDatos().getPuesto(idUsuario);
            if (puesto == "Cajero")
            {
                btnVolver.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSalir.Visibility = Visibility.Hidden;
            }

        }


        public void mostrarTabla()
        {
            btnPagar.IsEnabled = false;
            String consulta = "select idVentaProducto, usuarios.Nombre,usuarios.ApellidoP,usuarios.ApellidoM, total,FechaHora, ventaProducto.idUsuario from ventaProducto join usuarios on ventaProducto.idUsuario = usuarios.idUsuario where EstadoVenta = 'Pendiente';";
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
                    Total = row["total"].ToString(),
                    FechaHora = row["FechaHora"].ToString()
                };
                dataGridVentas.Items.Add(data);
            }
        }

        public void mostrarPagados()
        {
            String consulta = "select idVenta, pagoTotal,fechaHora from pagoVenta where idUsuario = " + idUsuario+" and corte = 0;";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new DGVender
                {
                    idVentaProducto = row["idVenta"].ToString(),
                    Total = row["pagoTotal"].ToString(),
                    FechaHora = row["fechaHora"].ToString()
                };
                dataGridPagados.Items.Add(data);
            }
        }

        public void mostrarTablaBusqueda(String dato)
        {
            btnPagar.IsEnabled = false;
            String consulta = "select idVentaProducto, usuarios.Nombre,usuarios.ApellidoP,usuarios.ApellidoM, total,FechaHora, ventaProducto.idUsuario from ventaProducto join usuarios on ventaProducto.idUsuario = usuarios.idUsuario where idVentaProducto = "+dato+";";
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
                    Total = row["total"].ToString(),
                    FechaHora = row["FechaHora"].ToString()
                };
                dataGridVentas.Items.Add(data);
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            int salida = new BaseDeDatos().validarSalidaCorte(idUsuario);
            if (salida == 1)
            {
                new PrincipalAdmin(idUsuario).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No puedes salir hasta realizar tu corte de caja ", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbBusqueda.Text = "Ingrese su busqueda";
            dataGridVentas.Items.Clear();
            mostrarTabla();
        }

        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            new CobrarVenta(idVenta,idUsuario,total).Show();
            this.Close();
        }
        
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String dato = tbBusqueda.Text;
            int index = cmbBuscarPor.SelectedIndex;
            if (dato != "" && dato != "Ingrese su busqueda" && index != 0)
            {
                dataGridVentas.Items.Clear();
                mostrarTablaBusqueda(tbBusqueda.Text);
                cmbBuscarPor.SelectedIndex = 0;
                tbBusqueda.Text = "Ingrese su busqueda";
            }
            else if (dato == "" || dato == "Ingrese su busqueda")
            {
                MessageBox.Show("Ingrese un texto para la busqueda");
            }
            else if (index == 0)
            {
                MessageBox.Show("seleccione una opcion de busqueda");
            }
            
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
                        total = Convert.ToDouble( dato2.Total);
                    }
                    break;
                case Key.Up:
                    DGVender dato = (DGVender)dataGridVentas.SelectedItem;
                    if (dato != null)
                    {
                        idVenta = Convert.ToInt16(dato.idVentaProducto);
                        total = Convert.ToDouble(dato.Total);
                    }
                    break;
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            int salida = new BaseDeDatos().validarSalidaCorte(idUsuario);
            if (salida == 1)
            {
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No puedes salir hasta realizar tu corte de caja ", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void btnCorte_Click(object sender, RoutedEventArgs e)
        {
            double total = new BaseDeDatos().getTotalCobrado(idUsuario);
            MessageBox.Show("total vendido = "+total, "Mensaje", MessageBoxButton.OK, MessageBoxImage.Hand);
            new BaseDeDatos().realizarCorte(idUsuario);
            dataGridPagados.Items.Clear();
            mostrarPagados();
        }

        private void tbBusqueda_GotFocus(object sender, RoutedEventArgs e)
        {
            tbBusqueda.Text = "";
        }

        private void dataGridVentas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DGVender dato = (DGVender)dataGridVentas.SelectedItem;

            if (dato != null)
            {
                btnPagar.IsEnabled = true;

                idVenta = Convert.ToInt16(dato.idVentaProducto);
                total = total = Convert.ToDouble(dato.Total);
            }
        }
    }
}
