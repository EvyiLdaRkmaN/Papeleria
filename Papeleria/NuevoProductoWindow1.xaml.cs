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
    /// Lógica de interacción para NuevoProductoWindow1.xaml
    /// </summary>
    public partial class NuevoProductoWindow1 : Window
    {
        private int id, proveedor, idUsuario;
        private string nombre = "", canitdad = "", preciUnitario = "", descripcion = "", codigo = "";
        DispatcherTimer timer;
        ToolTip tt;
        public NuevoProductoWindow1(int idUsuario)
        {
            InitializeComponent();
            llenarcmbProveedores();
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3), IsEnabled = true };
            tt = new ToolTip();
            this.idUsuario = idUsuario;
        }

        //Constructor con parametros para modificar
        public NuevoProductoWindow1(int idUsuario,int idProducto,String nombre, String canitdad, String preciUnitario, String descripcion, int idProveedor,String codigo)
        {
            InitializeComponent();
            llenarcmbProveedores();
            this.idUsuario = idUsuario;
            this.id = idProducto;
            this.nombre = nombre;
            this.canitdad = canitdad;
            this.preciUnitario = preciUnitario;
            this.descripcion = descripcion;
            this.proveedor = idProveedor;
            this.codigo = codigo;
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3), IsEnabled = true };
            tt = new ToolTip();


            if (id != 0)
            {
                tbNombre.Text = nombre;
                tbCantidad.Text = canitdad;
                tbPrecioUnitario.Text = preciUnitario;
                tbDescripcion.Text = descripcion;
                cmbProveedores.SelectedValue = idProveedor.ToString();
                
                tbCodigo.Text = codigo;
                Title = "Modificar producto";
            }
        }

        //Acciones de botones
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            new Productos(idUsuario).Show();
            this.Close();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*MessageBoxResult boton =  MessageBoxResult.None;
            String nombre = tbNombre.Text,
                cantidad = tbCantidad.Text,
                precioUnitario = tbPrecioUnitario.Text,
                descripcion = tbDescripcion.Text,
                codgio = tbCodigo.Text;
            if (cmbProveedores.SelectedIndex == -1)
            {
                boton = MessageBox.Show("El producto no tiene un proveedor asignado","Alerta",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            proveedor = Convert.ToInt32(cmbProveedores.SelectedValue);;
            //Validacion de que no exista ningun campo bacio
            if (nombre == "" || cantidad == "" ||
                precioUnitario == "" || descripcion == "")
            {
                MessageBox.Show("Rellene todos los campos","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {   
               
                if (id == 0)//Si no se esta usando para modificar entonces se usa para agregar
                {
                    int resultado;
                    resultado = new BaseDeDatos().agregarProducto(nombre, canitdad, preciUnitario, descripcion, proveedor,codigo);
                    if (resultado != 1)
                    {
                        new Productos(idUsuario).Show();
                        this.Close();
                    }

                }
                else//en caso de ser un id que ya se tenga entonces se usa para modificar
                {
                    int resultado;
                    resultado = new BaseDeDatos().modificarProducto(id, nombre, canitdad, preciUnitario, descripcion, proveedor,codigo);
                    if (resultado != 1)
                    {
                        new Productos(idUsuario).Show();
                        this.Close();
                    }
                }
            }*/
        }

        //todos los eventos de key
        private void tbNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (tbNombre.Text.Length >= 1 || tbNombre.Text.Length == 0)
                {
                    nombre = tbNombre.Text;
                }
            }
            else if (Regex.IsMatch(tbNombre.Text, "^([a-zA-Z0-9]{1,10}[ ]?[a-zA-Z0-9]{0,7})$"))
            {
                nombre = tbNombre.Text;
            }
            else if (tbNombre.Text.Length != 0)
            {
                tt.Content = "Dato no aceptado";
                tbNombre.ToolTip = tt;
                tt.IsOpen = true;
                int cursor = tbNombre.SelectionStart;
                tbNombre.Text = nombre;
                timer.Tick += new EventHandler(delegate (object timerSender, EventArgs timerArgs)//Para desaparecer el tooltip en un determinado tiempo
                {
                    tt.IsOpen = false;

                });
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbNombre.SelectionStart = cursor;
                    }
                    else
                    {
                        tbNombre.SelectionStart = cursor - 1;
                    }
                }

            }
        }

        private void tbCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (tbCodigo.Text.Length >= 0)
                {
                    codigo = tbCodigo.Text;
                }
            }
            else if (Regex.IsMatch(tbCodigo.Text, "(^\\d{1,10}$)"))
            {
                codigo = tbCodigo.Text;
            }
            else if (tbCodigo.Text.Length != 0)
            {
                int cursor = tbCodigo.SelectionStart;
                tbCodigo.Text = codigo;
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbCodigo.SelectionStart = cursor;
                    }
                    else
                    {
                        tbCodigo.SelectionStart = cursor - 1;
                    }
                }
            }
        }

        private void tbCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (tbCantidad.Text.Length >= 1 || tbCantidad.Text.Length == 0)
                {
                    canitdad = tbCantidad.Text;
                }
            }
            else if (Regex.IsMatch(tbCantidad.Text, "^(\\d{1,5})$"))
            {
                canitdad = tbCantidad.Text;
            }
            else if (tbCantidad.Text.Length != 0)
            {
                int cursor = tbCantidad.SelectionStart;
                tbCantidad.Text = canitdad;
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

        private void tbPrecioUnitario_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (tbPrecioUnitario.Text.Length >= 0)
                {
                    preciUnitario = tbPrecioUnitario.Text;
                }
            }
            else if (Regex.IsMatch(tbPrecioUnitario.Text, "^(\\d{1,5}([.]?(\\d{1,2})?))$"))
            {
                preciUnitario = tbPrecioUnitario.Text;
            }
            else if (tbPrecioUnitario.Text.Length != 0)
            {
                int cursor = tbPrecioUnitario.SelectionStart;
                tbPrecioUnitario.Text = preciUnitario;
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbPrecioUnitario.SelectionStart = cursor;
                    }
                    else
                    {
                        tbPrecioUnitario.SelectionStart = cursor - 1;
                    }
                }
            }
        }

        private void Descripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (tbDescripcion.Text.Length >= 0)
                {
                    descripcion = tbDescripcion.Text;
                }
            }
            else if (Regex.IsMatch(tbDescripcion.Text, "^(.{1,90})$"))
            {
                descripcion = tbDescripcion.Text;
            }
            else if (tbDescripcion.Text.Length != 0)
            {
                tt.Content = "Dato no aceptado";
                tbDescripcion.ToolTip = tt;
                tt.IsOpen = true;
                int cursor = tbDescripcion.SelectionStart;
                tbDescripcion.Text = descripcion;
                timer.Tick += new EventHandler(delegate (object timerSender, EventArgs timerArgs)//Para desaparecer el tooltip en un determinado tiempo
                {
                    tt.IsOpen = false;

                });
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbDescripcion.SelectionStart = cursor;
                    }
                    else
                    {
                        tbDescripcion.SelectionStart = cursor - 1;
                    }
                }

            }
        }

        private void llenarcmbProveedores()
        {
            /*DataSet dsd = new DataSet();
            SqlDataAdapter dad = new SqlDataAdapter("Select idProveedor,nombreProvedor from proveedores", new BaseDeDatos().obtenerConexion());
            //se indica con que tabla se llena
            dad.Fill(dsd, "proveedores");
            cmbProveedores.ItemsSource = dsd.Tables[0].DefaultView;
            //comboBoxPrueba.v
            cmbProveedores.SelectedValuePath = "idProveedor";
            //Console.WriteLine("selectedItem = "+comboBoxTrabajo.SelectedItem+" selectedIndex"+comboBoxTrabajo.SelectedIndex);
            cmbProveedores.DisplayMemberPath = "nombreProvedor";*/
        }
    }
}
