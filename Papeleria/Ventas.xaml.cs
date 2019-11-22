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
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        private int id, idVenta;
        public Ventas(int idUsuario)
        {
            InitializeComponent();
            this.id = idUsuario;
            mostrarTabla();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            new PrincipalAdmin(id).Show();
            this.Close();
        }

        public void mostrarTabla()
        {
            btnVer.IsEnabled = false;
            String consulta = "select ventaProducto.idVentaProducto, usuarios.Nombre, usuarios.ApellidoP,usuarios.ApellidoM, ventaProducto.total,ventaProducto.FechaHora,ventaProducto.EstadoVenta,usuarios.idUsuario from ventaProducto join usuarios on ventaProducto.idUsuario = usuarios.idUsuario;";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new DGventas
                {
                    idVentaProducto = row["idVentaProducto"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    ApellidoP = row["ApellidoP"].ToString(),
                    ApellidoM = row["ApellidoM"].ToString(),
                    Total = row["total"].ToString(),
                    FechaHora = row["FechaHora"].ToString(),
                    estadoVenta = row["EstadoVenta"].ToString(),
                    idUsuario = row["idUsuario"].ToString()
                };
                dataGridVenta.Items.Add(data);
            }
        }

        private void dataGridBuscador(int opcion, String dato)
        {
            btnVer.IsEnabled = false;
            String prueba = "";
            switch (opcion)
            {
                case 1:
                    prueba = "select ventaProducto.idVentaProducto, usuarios.Nombre, usuarios.ApellidoP,usuarios.ApellidoM, ventaProducto.total,ventaProducto.FechaHora,ventaProducto.EstadoVenta,usuarios.idUsuario from ventaProducto join usuarios on ventaProducto.idUsuario = usuarios.idUsuario where usuario.Nombre = '"+dato+"';";
                    break;
                case 2:
                    prueba = "select ventaProducto.idVentaProducto, usuarios.Nombre, usuarios.ApellidoP,usuarios.ApellidoM, ventaProducto.total,ventaProducto.FechaHora,ventaProducto.EstadoVenta,usuarios.idUsuario from ventaProducto join usuarios on ventaProducto.idUsuario = usuarios.idUsuario where ventaProducto.FechaHora = '"+dato+"' ;";
                    break;
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(prueba, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new DGventas
                {
                    idVentaProducto = row["idVentaProducto"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    ApellidoP = row["ApellidoP"].ToString(),
                    ApellidoM = row["ApellidoM"].ToString(),
                    Total = row["total"].ToString(),
                    FechaHora = row["FechaHora"].ToString(),
                    estadoVenta = row["EstadoVenta"].ToString(),
                    idUsuario = row["idUsuario"].ToString()
                };
                dataGridVenta.Items.Add(data);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dataGridVenta.Items.Clear();
            mostrarTabla();
        }

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            new VentaRealizadaPor(idVenta, id).Show();
            this.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String dato = tbBusqueda.Text;
            int index = cmbBuscarPor.SelectedIndex;
            if (dato != "" && dato != "Ingrese su busqueda" && index != 0)
            {
                dataGridVenta.Items.Clear();
                dataGridBuscador(index, dato);
            }
            else
            {
                if (index == 0)
                {
                    MessageBox.Show("seleccione una opcion de busqueda");
                }
                MessageBox.Show("Ingrese un texto para la busqueda");
            }
        }

        private void dataGridVenta_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    DGventas dato2 = (DGventas)dataGridVenta.SelectedItem;
                    if (dato2 != null)
                    {
                        idVenta = Convert.ToInt16(dato2.idVentaProducto);
                    }
                    break;
                case Key.Up:
                    DGventas dato = (DGventas)dataGridVenta.SelectedItem;
                    if (dato != null)
                    {
                        idVenta = Convert.ToInt16(dato.idVentaProducto);
                    }
                    break;
            }
        }

        private void dataGridVenta_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DGventas dato = (DGventas)dataGridVenta.SelectedItem;

            if (dato != null)
            {
                btnVer.IsEnabled = true;

                idVenta = Convert.ToInt16(dato.idVentaProducto);
            }
        }
    }
}
