using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Papeleria
{
    /// <summary>
    /// Lógica de interacción para NuevaVenta.xaml
    /// </summary>
    public partial class NuevaVenta : Window
    {
        private int idVenta = new BaseDeDatos().obtenerUltimaVenta(),idUsuario, idProducto,idUnico,ver =0,idProducto2;
        private String cantidad, cantidad2;

        private double total;

        DispatcherTimer timer;
        ToolTip tt;

        public NuevaVenta(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            mostrarTabla();
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3), IsEnabled = true };
            tt = new ToolTip();
        }

        public NuevaVenta(int idUsuario,int idVentaProducto, int modificar)
        {//modificar 0 = no, 1= si, 2 = ver
            // ver 1 = si, 2 = si, 3 = no
            InitializeComponent();
            this.idUsuario = idUsuario;
            this.idVenta = idVentaProducto;
            mostrarProductosAgregado();
            lbTotal.Content = new BaseDeDatos().obtenerTotalVenta(idVenta);
            if (modificar == 0)
            {
                Console.WriteLine("ver  = 1");
                ver = 1;
                tbCantidad.IsEnabled = false;
                tbBusqueda.IsEnabled = false;
                cmbBuscar.IsEnabled = false;
                btnBuscar.IsEnabled = false;
                btnCancelar.Visibility = Visibility.Hidden;
                dataGridUsuarios.IsEnabled = false;
                btnEliminar.Visibility = Visibility.Hidden;
            }
            else if (modificar == 1)
            {
                ver = 3;
                Console.WriteLine("ver = 3");
                mostrarTabla();
            }else if(modificar == 2)
            {
                Console.WriteLine("ver = 2");
                ver = 2;
                tbCantidad.IsEnabled = false;
                tbBusqueda.IsEnabled = false;
                cmbBuscar.IsEnabled = false;
                btnBuscar.IsEnabled = false;
                btnCancelar.Visibility = Visibility.Hidden;
                dataGridUsuarios.IsEnabled = false;
                btnEliminar.Visibility = Visibility.Hidden;
            }
            //
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3), IsEnabled = true };
            tt = new ToolTip();
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            if (tbCantidad.Text == "" || tbCantidad.Text == "Cantidad" ||Convert.ToInt16(tbCantidad.Text) ==0)
            {   
                MessageBox.Show("Ingrese una cantidad valida diferente de 0", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                int nuevaCantidad = Convert.ToInt16(tbCantidad.Text);
                int cantidad = new BaseDeDatos().getsetCantidadProducto(idProducto,nuevaCantidad,0);
                if (cantidad < nuevaCantidad)
                {
                    MessageBox.Show("No se cuenta con esa cantidad de productos en el almacen", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    nuevaCantidad = cantidad-nuevaCantidad;
                    new BaseDeDatos().getsetCantidadProducto(idProducto, nuevaCantidad, 1);
                    new BaseDeDatos().ventaProducto(idProducto, idVenta, Convert.ToInt16(tbCantidad.Text));
                    tbCantidad.Text = "Cantidad";
                    btnAñadir.IsEnabled = false;
                    lbTotal.Content = new BaseDeDatos().obtenerTotalVenta(idVenta);
                    total = Convert.ToDouble(lbTotal.Content);
                    dataGridUsuarios_Copy.Items.Clear();
                    mostrarProductosAgregado();
                    dataGridUsuarios.Items.Clear();
                    mostrarTabla();
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            new BaseDeDatos().añadirProductosdeRegreso(idProducto2, cantidad2);
            new BaseDeDatos().eliminarProductoDeVenta(idUnico);
            lbTotal.Content = new BaseDeDatos().obtenerTotalVenta(idVenta);
            total = Convert.ToDouble(lbTotal.Content);
            dataGridUsuarios.Items.Clear();
            mostrarTabla();
            dataGridUsuarios_Copy.Items.Clear();
            mostrarProductosAgregado();
        }

        private void btnTerminar_Click(object sender, RoutedEventArgs e)
        {
            if (ver == 1)
            {
                new Vender(idUsuario).Show();
                this.Close();

            }
            else if(ver == 3 || ver == 0)
            {
                new BaseDeDatos().registrarVenta(total, idUsuario, idVenta, 1);//El 1 es que modificara una venta que ya existe 0 es para no modificar
                new Vender(idUsuario).Show();
                this.Close();
            }else if(ver == 2)
            {
                this.Close();
            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (ver == 1 || ver == 3)
            {
                new Vender(idUsuario).Show();
                this.Close();
            }
            else if(ver == 0)
            {
                MessageBoxResult result = MessageBox.Show("Se perdera esta venta\n ¿Quieres continuar?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    new BaseDeDatos().eliminaTodalaVenta(idVenta);
                    new Vender(idUsuario).Show();
                    this.Close();
                }
            }
            
        }
       
        public void mostrarTabla()
        {
            String consulta = "select idProducto, nombre, cantidad, precioUnitario,codigo from productos;";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new llenarDGProductosEnVenta
                {
                    idProducto = row["idProducto"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    Cantidad = row["cantidad"].ToString(),
                    PresioUnitario = row["precioUnitario"].ToString(),
                    codigo = row["codigo"].ToString()
                };
                dataGridUsuarios.Items.Add(data);
            }
        }

        public void mostrarProductosAgregado()
        {
            String consulta = "Select idUnico,productos.idProducto, productos.nombre,productoVenta.cantidad,productos.precioUnitario, productos.Codigo from ProductoVenta join productos on ProductoVenta.idProducto = productos.idProducto where idProductoVenta = "+idVenta+";";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new llenarDGProductosEnVenta
                {
                    idUnico = row["idUnico"].ToString(),
                    idProducto = row["idProducto"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    Cantidad = row["cantidad"].ToString(),
                    PresioUnitario = row["precioUnitario"].ToString(),
                    codigo = row["codigo"].ToString()
                };
                dataGridUsuarios_Copy.Items.Add(data);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String dato = tbBusqueda.Text;
            int index = cmbBuscar.SelectedIndex;
            if (dato != "" && dato != "Ingrese su busqueda" && index != 0)
            {
                dataGridUsuarios.Items.Clear();
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

        private void dataGridBuscador(int opcion, String dato)
        {
            String prueba = "";
            switch (opcion)
            {
                case 1:
                    prueba = "select idProducto, nombre, cantidad, precioUnitario,codigo from productos where Nombre ='" + dato + "';";
                    break;
                case 2:
                    prueba = "select idProducto, nombre, cantidad, precioUnitario,codigo from productos where codigo =" + dato + ";";
                    break;
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(prueba, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new llenarDGProductosEnVenta
                {
                    idProducto = row["idProducto"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    Cantidad = row["cantidad"].ToString(),
                    PresioUnitario = row["precioUnitario"].ToString(),
                    codigo = row["codigo"].ToString()
                };
                dataGridUsuarios.Items.Add(data);
            }
        }

        private void tbBusqueda_GotFocus(object sender, RoutedEventArgs e)
        {
            tbBusqueda.Text = "";
        }

        private void dataGridUsuarios_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    llenarDGProductosEnVenta dato2 = (llenarDGProductosEnVenta)dataGridUsuarios.SelectedItem;
                    if (dato2 != null)
                    {
                        idProducto = Convert.ToInt16( dato2.idProducto);
                    }
                    break;
                case Key.Up:
                    LlenadoDataGridProductos dato = (LlenadoDataGridProductos)dataGridUsuarios.SelectedItem;
                    if (dato != null)
                    {
                        idProducto = Convert.ToInt16(dato.idProducto);
                    }
                    break;
            }
        }

        private void tbCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (tbCantidad.Text.Length >= 0)
                {
                    cantidad = tbCantidad.Text;
                }
            }
            else if (Regex.IsMatch(tbCantidad.Text, "^(\\d{1,5})$"))
            {
                cantidad = tbCantidad.Text;
            }
            else if (tbCantidad.Text.Length != 0)
            {
                tt.Content = "Dato no aceptado";
                tbCantidad.ToolTip = tt;
                tt.IsOpen = true;
                timer.Tick += new EventHandler(delegate (object timerSender, EventArgs timerArgs)//Para desaparecer el tooltip en un determinado tiempo
                {
                    tt.IsOpen = false;
                });
                int cursor = tbCantidad.SelectionStart;
                tbCantidad.Text = cantidad;
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbCantidad.SelectionStart = cursor;
                    }
                    else
                    {
                        tbCantidad.SelectionStart = cursor - 1;
                    }

                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dataGridUsuarios.Items.Clear();
            mostrarTabla();
        }

        private void dataGridUsuarios_Copy_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    llenarDGProductosEnVenta dato2 = (llenarDGProductosEnVenta)dataGridUsuarios_Copy.SelectedItem;
                    if (dato2 != null)
                    {
                        idUnico = Convert.ToInt16(dato2.idUnico);
                        idProducto2 = Convert.ToInt16(dato2.idProducto);
                        cantidad2 = dato2.Cantidad;
                    }
                    break;
                case Key.Up:
                    llenarDGProductosEnVenta dato = (llenarDGProductosEnVenta)dataGridUsuarios_Copy.SelectedItem;
                    if (dato != null)
                    {
                        idUnico = Convert.ToInt16(dato.idUnico);
                        idProducto2 = Convert.ToInt16(dato.idProducto);
                        cantidad2 = dato.Cantidad;
                    }
                    break;
            }
        }

        private void dataGridUsuarios_Copy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            llenarDGProductosEnVenta dato = (llenarDGProductosEnVenta)dataGridUsuarios_Copy.SelectedItem;

            if (dato != null)
            {
                btnEliminar.IsEnabled = true;

                idUnico = Convert.ToInt16(dato.idUnico);
                idProducto2 = Convert.ToInt16(dato.idProducto);
                cantidad2 = dato.Cantidad;
            }
        }

        private void dataGridUsuarios_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            llenarDGProductosEnVenta dato = (llenarDGProductosEnVenta)dataGridUsuarios.SelectedItem;

            if (dato != null)
            {
                btnAñadir.IsEnabled = true;

                idProducto = Convert.ToInt16(dato.idProducto);
            }
        }

        private void tbCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            tbCantidad.Text = "";
        }

    }
}
