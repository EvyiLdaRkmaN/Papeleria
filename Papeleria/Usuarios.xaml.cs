using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
namespace Papeleria
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Window
    {
        private int id = 0,idUsuario;
        public String nombre, apellidoP, apellidoM, sexo, telefono, edad, puesto,nombreUsuario,contraseña;

        public Usuarios(int idUsuario)
        {
            InitializeComponent();
            mostrarTabla();
            this.idUsuario = idUsuario;
            
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            /*MessageBoxResult boton = MessageBox.Show("¿Realmente desea eliminar este usuario?","Alerta",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
            if (boton == MessageBoxResult.OK)
            {
                if (id == 1)
                {
                    System.Windows.MessageBox.Show("No se puede eliminar ya que esta como administrador", "Alerta", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    
                }
                else if (new BaseDeDatos().eliminarUsuario(id, puesto) != 1)
                {
                    MessageBoxResult result = MessageBox.Show("No se puede eliminar este usuario\n" +
                                "tiene registros hechos\n" +
                                "¿Desea eliminar todas las acciones hechas por este usuario?.", "Alerta", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        new BaseDeDatos().eliminarRegistrosUsuario(id,puesto);
                    }
                }
                dataGridUsuarios.Items.Clear();
                mostrarTabla();
            }*/
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

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            new PrincipalAdmin(idUsuario).Show();
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            new NuevoUsuario(idUsuario).Show();
            this.Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            new NuevoUsuario(id, idUsuario, nombre,apellidoP,apellidoM,sexo,telefono,edad,puesto,nombreUsuario,contraseña).Show();
            this.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String dato = tbBusqueda.Text;
            int index = cmbBuscarPor.SelectedIndex;
            if (dato != ""  && dato != "Ingrese su busqueda" && index != 0)
            {
                dataGridUsuarios.Items.Clear();
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

        public void mostrarTabla()
        {
            /*btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            String consulta = "select idUsuario,Nombre,ApellidoP ,ApellidoM ,Sexo,Telefono,Edad,Puesto,NombreUsuario,Contraseña from usuarios;";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new PruebaDeLLenadoDataGrid {
                    idUsuario = row["idUsuario"].ToString(), Nombre = row["Nombre"].ToString(),
                    ApellidoP = row["ApellidoP"].ToString(), ApellidoM = row["ApellidoM"].ToString(),
                    Sexo = row["Sexo"].ToString(), Telefono = row["Telefono"].ToString(),
                    Edad = row["Edad"].ToString(), Puesto = row["Puesto"].ToString(),
                    nombreUsuario = row["NombreUsuario"].ToString(),
                    contraseña = row["Contraseña"].ToString()
                };
                dataGridUsuarios.Items.Add(data);
            }*/
        }

        private void dataGridUsuarios_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    PruebaDeLLenadoDataGrid dato2 = (PruebaDeLLenadoDataGrid)dataGridUsuarios.SelectedItem;
                    if (dato2 != null)
                    {
                        id = Convert.ToInt16(dato2.idUsuario);
                        nombre = dato2.Nombre;
                        apellidoP = dato2.ApellidoP;
                        apellidoM = dato2.ApellidoM;
                        sexo = dato2.Sexo;
                        telefono = dato2.Telefono;
                        edad = dato2.Edad;
                        puesto = dato2.Puesto;
                        nombreUsuario = dato2.nombreUsuario;
                        contraseña = dato2.contraseña;
                    }
                    break;
                case Key.Up:
                    PruebaDeLLenadoDataGrid dato = (PruebaDeLLenadoDataGrid)dataGridUsuarios.SelectedItem;
                    if (dato != null)
                    {
                        id = Convert.ToInt16(dato.idUsuario);
                        nombre = dato.Nombre;
                        apellidoP = dato.ApellidoP;
                        apellidoM = dato.ApellidoM;
                        sexo = dato.Sexo;
                        telefono = dato.Telefono;
                        edad = dato.Edad;
                        puesto = dato.Puesto;
                        nombreUsuario = dato.nombreUsuario;
                        contraseña = dato.contraseña;
                    }
                    break;
            }
        }

        private void dataGridUsuarios_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PruebaDeLLenadoDataGrid dato = (PruebaDeLLenadoDataGrid)dataGridUsuarios.SelectedItem;
            
            if (dato != null)
            {
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;

                id = Convert.ToInt16(dato.idUsuario);
                nombre = dato.Nombre;
                apellidoP = dato.ApellidoP;
                apellidoM = dato.ApellidoM;
                sexo = dato.Sexo;
                telefono = dato.Telefono;
                edad = dato.Edad;
                puesto = dato.Puesto;
                nombreUsuario = dato.nombreUsuario;
                contraseña = dato.contraseña;
            }
        }

        private void dataGridBuscador(int opcion,String dato)
        {
            /*btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            String prueba = "";
            switch (opcion)
            {
                case 1:
                    prueba = "select idUsuario,Nombre,ApellidoP ,ApellidoM ,Sexo,Telefono,Edad,Puesto,NombreUsuario,Contraseña from usuarios where Nombre ='"+dato+"';";
                    break;
                case 2:
                    prueba = "select idUsuario,Nombre,ApellidoP ,ApellidoM ,Sexo,Telefono,Edad,Puesto,NombreUsuario,Contraseña from usuarios where ApellidoP ='" + dato + "';";
                    break;
                case 3:
                    prueba = "select idUsuario,Nombre,ApellidoP ,ApellidoM ,Sexo,Telefono,Edad,Puesto,NombreUsuario,Contraseña from usuarios where Puesto ='" + dato + "';";
                    break;
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(prueba, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new PruebaDeLLenadoDataGrid
                {
                    idUsuario = row["idUsuario"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    ApellidoP = row["ApellidoP"].ToString(),
                    ApellidoM = row["ApellidoM"].ToString(),
                    Sexo = row["Sexo"].ToString(),
                    Telefono = row["Telefono"].ToString(),
                    Edad = row["Edad"].ToString(),
                    Puesto = row["Puesto"].ToString(),
                    nombreUsuario = row["NombreUsuario"].ToString(),
                    contraseña = row["Contraseña"].ToString()
                };
                dataGridUsuarios.Items.Add(data);
            }*/
        }
    }
}
