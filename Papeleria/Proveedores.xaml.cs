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
    /// Lógica de interacción para Proveedores.xaml
    /// </summary>
    public partial class Proveedores : Window
    {
        private int id = 0, idUsuario;
        public String nombre, apellidoP, apellidoM, telefono, empresa;
        public Proveedores(int idUsuario)
        {
            InitializeComponent();
            mostrarTabla();
            this.idUsuario = idUsuario;
        }

        public void mostrarTabla()
        {
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            String consulta = "select idProveedor,nombreProvedor,apellido_P ,apellido_M ,telefono,empresa from proveedores;";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new LlenadoDataGridProveedores
                {
                    idProveedor = row["idProveedor"].ToString(),
                    Nombre = row["nombreProvedor"].ToString(),
                    ApellidoP = row["apellido_P"].ToString(),
                    ApellidoM = row["apellido_M"].ToString(),
                    Telefono = row["telefono"].ToString(),
                    Empresa = row["empresa"].ToString()
                };
                dataGridProveedores.Items.Add(data);
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
                    prueba = "select idProveedor,nombreProvedor,apellido_P ,apellido_M ,telefono,empresa from proveedores where nombreProvedor ='" + dato + "';";
                    break;
                case 2:
                    prueba = "select idProveedor,nombreProvedor,apellido_P ,apellido_M ,telefono,empresa from proveedores where apellido_P ='" + dato + "';";
                    break;
            }

            SqlDataAdapter dataAdapter = new SqlDataAdapter(prueba, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new LlenadoDataGridProveedores
                {
                    idProveedor = row["idProveedor"].ToString(),
                    Nombre = row["nombreProvedor"].ToString(),
                    ApellidoP = row["apellido_P"].ToString(),
                    ApellidoM = row["apellido_M"].ToString(),
                    Telefono = row["Telefono"].ToString(),
                    Empresa = row["empresa"].ToString()
                };
                dataGridProveedores.Items.Add(data);
            }
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
                dataGridProveedores.Items.Clear();
                dataGridBuscador(index, dato);
                tbBusqueda.Text = "Ingrese su busqueda";
                cmbBuscarPor.SelectedIndex = 0;
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

        private void dataGridProveedores_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LlenadoDataGridProveedores dato = (LlenadoDataGridProveedores)dataGridProveedores.SelectedItem;

            if (dato != null)
            {
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;

                id = Convert.ToInt16(dato.idProveedor);
                nombre = dato.Nombre;
                apellidoP = dato.ApellidoP;
                apellidoM = dato.ApellidoM;
                telefono = dato.Telefono;
                empresa = dato.Empresa;
            }
        }

        private void tbBusqueda_GotFocus(object sender, RoutedEventArgs e)
        {
            tbBusqueda.Text = "";
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dataGridProveedores.Items.Clear();
            mostrarTabla();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boton = MessageBox.Show("¿Realmente desea eliminar este usuario?", "Alerta", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (boton == MessageBoxResult.OK)
            {
                new BaseDeDatos().eliminarProveedor(id);
                dataGridProveedores.Items.Clear();
                mostrarTabla();
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            new NuevoProveedor(idUsuario).Show();
            this.Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            new NuevoProveedor(id, nombre, apellidoP, apellidoM,telefono,empresa).Show();
            this.Close();
        }

        private void dataGridProveedores_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    LlenadoDataGridProveedores dato2 = (LlenadoDataGridProveedores)dataGridProveedores.SelectedItem;
                    if (dato2 != null)
                    {
                        id = Convert.ToInt16(dato2.idProveedor);
                        nombre = dato2.Nombre;
                        apellidoP = dato2.ApellidoP;
                        apellidoM = dato2.ApellidoM;
                        telefono = dato2.Telefono;
                        empresa = dato2.Empresa;
                    }
                    break;
                case Key.Up:
                    LlenadoDataGridProveedores dato = (LlenadoDataGridProveedores)dataGridProveedores.SelectedItem;
                    if (dato != null)
                    {
                        id = Convert.ToInt16(dato.idProveedor);
                        nombre = dato.Nombre;
                        apellidoP = dato.ApellidoP;
                        apellidoM = dato.ApellidoM;
                        telefono = dato.Telefono;
                        empresa = dato.Empresa;
                    }
                    break;
            }
        }
    }
}
