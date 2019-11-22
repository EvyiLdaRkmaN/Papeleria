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
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        private int id, proveedor,idUsuario;
        private string nombre = "", canitdad = "", preciUnitario = "", descripcion = "", codigo = "";
        public Productos(int idUsuario)
        {
            InitializeComponent();
            mostrarTabla();
            this.idUsuario = idUsuario;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dataGridUsuarios.Items.Clear();
            mostrarTabla();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            new NuevoProductoWindow1(idUsuario).Show();
            this.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String dato = tbBusqueda.Text;
            int index = cmbBuscarPor.SelectedIndex;
            if (dato != "" && dato != "Ingrese su busqueda" && index != 0)
            {
                dataGridUsuarios.Items.Clear();
                dataGridBuscador(index, dato);
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

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            new PrincipalAdmin(idUsuario).Show();
            this.Close();
            //MessageBoxResult boton = MessageBox.Show("¿Realmente desea eliminar este producto?", "Alerta", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
        }

        private void dataGridUsuarios_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    LlenadoDataGridProductos dato2 = (LlenadoDataGridProductos)dataGridUsuarios.SelectedItem;
                    if (dato2 != null)
                    {
                        id = Convert.ToInt16(dato2.idProducto);
                        nombre = dato2.Nombre;
                        canitdad = dato2.Cantidad;
                        preciUnitario = dato2.PresioUnitario;
                        descripcion = dato2.Descripcion;
                        codigo = dato2.codigo;
                        proveedor = Convert.ToInt16(dato2.idProveedor);
                    }
                    break;
                case Key.Up:
                    LlenadoDataGridProductos dato = (LlenadoDataGridProductos)dataGridUsuarios.SelectedItem;
                    if (dato != null)
                    {
                        id = Convert.ToInt16(dato.idProducto);
                        nombre = dato.Nombre;
                        canitdad = dato.Cantidad;
                        preciUnitario = dato.PresioUnitario;
                        descripcion = dato.Descripcion;
                        codigo = dato.codigo;
                        proveedor = Convert.ToInt16(dato.idProveedor);
                    }
                    break;
            }
        }

        private void dataGridUsuarios_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LlenadoDataGridProductos dato = (LlenadoDataGridProductos)dataGridUsuarios.SelectedItem;

            if (dato != null)
            {
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnAñadir.IsEnabled = true;
                id = Convert.ToInt16(dato.idProducto);
                nombre = dato.Nombre;
                canitdad = dato.Cantidad;
                preciUnitario = dato.PresioUnitario;
                descripcion = dato.Descripcion;
                codigo = dato.codigo;
                proveedor = Convert.ToInt16(dato.idProveedor);
            }
        }


        private void tbBusqueda_GotFocus(object sender, RoutedEventArgs e)
        {
            tbBusqueda.Text = "";
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            new NuevoProductoWindow1(idUsuario, id, nombre, canitdad, preciUnitario, descripcion, proveedor, codigo).Show();
            this.Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boton = MessageBox.Show("¿Realmente desea eliminar este producto?", "Alerta", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (boton == MessageBoxResult.OK)
            {
                new BaseDeDatos().eliminarProducto(id);
                dataGridUsuarios.Items.Clear();
                mostrarTabla();
            }
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            new AñadirProductos(idUsuario, id,canitdad,nombre,descripcion).Show();
            this.Close();
        }

        public void mostrarTabla()
        {
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnAñadir.IsEnabled = false;
            String consulta = "select idProducto,nombre,cantidad ,precioUnitario ,descripcion,idProvedor,Codigo, proveedores.nombreProvedor from productos" +
                " join proveedores on productos.idProvedor = proveedores.idProveedor;";
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
                    proveedor = row["nombreProvedor"].ToString(),
                };
                dataGridUsuarios.Items.Add(data);
            }
        }

        private void dataGridBuscador(int opcion, String dato)
        {
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            String prueba = "";
            switch (opcion)
            {
                case 1:
                    prueba = "select idProducto,nombre,cantidad ,precioUnitario ,descripcion,idProvedor,Codigo, proveedores.nombreProvedor from productos" +
                " join proveedores on productos.idProvedor = proveedores.idProveedor where nombre ='" + dato + "';";
                    break;
                case 2:
                    prueba = "select idProducto,nombre,cantidad ,precioUnitario ,descripcion,idProvedor,Codigo, proveedores.nombreProvedor from productos" +
                " join proveedores on productos.idProvedor = proveedores.idProveedor where Codigo ='" + dato + "';";
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
                    proveedor = row["nombreProvedor"].ToString(),
                };
                dataGridUsuarios.Items.Add(data);
            }
        }
    }
}
