using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para NuevoProveedor.xaml
    /// </summary>
    public partial class NuevoProveedor : Window
    {
        private int id, idUsuario;
        private string nombre = "", apellidoP = "", apellidom = "", telefono = "", empresa = "";

        
        DispatcherTimer timer;
        ToolTip tt;
        public NuevoProveedor(int idUsuario)
        {
            InitializeComponent();
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3), IsEnabled = true };
            tt = new ToolTip();
            this.idUsuario = idUsuario;
        }
        //Constructor con parametros para modificar
        public NuevoProveedor(int idProveedor,String nombre, String apellidoP,String apellidoM,String telefono,String empresa)
        {
            
            this.id = idProveedor;
            this.apellidom = apellidoP;
            this.apellidom = apellidoM;
            this.nombre = nombre;
            this.telefono = telefono;
            this.empresa = empresa;

            InitializeComponent();
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3), IsEnabled = true };
            tt = new ToolTip();


            if (id != 0)
            {
                tbNombre.Text = nombre;
                tbApellidoP.Text = apellidoP;
                tbApellidoM.Text = apellidoM;
                tbTelefono.Text = telefono;
                tbEmpresa.Text = empresa;
                Title = "Modificar proveedor";
            }
        }

        //Acciones de key
        private void tbNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (tbNombre.Text.Length >= 1 || tbNombre.Text.Length == 0)
                {
                    nombre = tbNombre.Text;
                }
            }
            else if (Regex.IsMatch(tbNombre.Text, "^([a-zA-Z]{1,10}[ ]?[a-zA-Z]{0,7})$"))
            {
                nombre = tbNombre.Text;
            }
            else if (tbNombre.Text.Length != 0)
            {
                tt.Content = "No se aceptan numeros";
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

        private void tbApellidoP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (tbApellidoP.Text.Length >= 0)
                {
                    apellidoP = tbApellidoP.Text;
                }
            }
            else if (Regex.IsMatch(tbApellidoP.Text, "^([a-zA-Z]{1,10})$"))
            {
                apellidoP = tbApellidoP.Text;
            }
            else if (tbApellidoP.Text.Length != 0)
            {
                tt.Content = "No se aceptan numeros";
                tbApellidoP.ToolTip = tt;
                tt.IsOpen = true;
                timer.Tick += new EventHandler(delegate (object timerSender, EventArgs timerArgs)//Para desaparecer el tooltip en un determinado tiempo
                {
                    tt.IsOpen = false;

                });
                int cursor = tbApellidoP.SelectionStart;
                tbApellidoP.Text = apellidoP;
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbApellidoP.SelectionStart = cursor;
                    }
                    else
                    {
                        tbApellidoP.SelectionStart = cursor - 1;
                    }

                }
            }
        }

        private void tbApellidoM_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (tbApellidoM.Text.Length >= 1 || tbApellidoM.Text.Length == 0)
                {
                    apellidom = tbApellidoM.Text;
                }
            }
            else if (Regex.IsMatch(tbApellidoM.Text, "^([a-zA-Z]{1,10})$"))
            {
                apellidom = tbApellidoM.Text;
            }
            else if (tbApellidoM.Text.Length != 0)
            {
                tt.Content = "No se aceptan numeros";
                tbApellidoM.ToolTip = tt;
                tt.IsOpen = true;
                timer.Tick += new EventHandler(delegate (object timerSender, EventArgs timerArgs)//Para desaparecer el tooltip en un determinado tiempo
                {
                    tt.IsOpen = false;

                });
                int cursor = tbApellidoM.SelectionStart;
                tbApellidoM.Text = apellidom;

                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbApellidoM.SelectionStart = cursor;
                    }
                    else
                    {
                        tbApellidoM.SelectionStart = cursor - 1;
                    }

                }
            }
        }

        private void tbEmpresa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (tbEmpresa.Text.Length >= 0)
                {
                    empresa = tbEmpresa.Text;
                }
            }
            else if (Regex.IsMatch(tbEmpresa.Text, "^([a-zA-Z]{1,15})$"))
            {
                empresa = tbEmpresa.Text;
            }
            else if (tbEmpresa.Text.Length != 0)
            {
                tt.Content = "No se aceptan numeros";
                tbEmpresa.ToolTip = tt;
                tt.IsOpen = true;
                timer.Tick += new EventHandler(delegate (object timerSender, EventArgs timerArgs)//Para desaparecer el tooltip en un determinado tiempo
                {
                    tt.IsOpen = false;

                });
                int cursor = tbEmpresa.SelectionStart;
                tbEmpresa.Text = empresa;
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbEmpresa.SelectionStart = cursor;
                    }
                    else
                    {
                        tbEmpresa.SelectionStart = cursor - 1;
                    }

                }
            }
        }

        private void tbTelefono_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (tbTelefono.Text.Length >= 0)
                {
                    telefono = tbTelefono.Text;
                }
            }
            else if (Regex.IsMatch(tbTelefono.Text, "^(\\d{1,10})$"))
            {
                telefono = tbTelefono.Text;
            }
            else if (tbTelefono.Text.Length != 0)
            {
                tt.Content = "Dato no aceptado";
                tbTelefono.ToolTip = tt;
                tt.IsOpen = true;
                timer.Tick += new EventHandler(delegate (object timerSender, EventArgs timerArgs)//Para desaparecer el tooltip en un determinado tiempo
                {
                    tt.IsOpen = false;
                });
                int cursor = tbTelefono.SelectionStart;
                tbTelefono.Text = telefono;
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbTelefono.SelectionStart = cursor;
                    }
                    else
                    {
                        tbTelefono.SelectionStart = cursor - 1;
                    }

                }
            }
        }

        //Acciones de botones
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            new Proveedores(idUsuario).Show();
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            String nombre = tbNombre.Text,
                apellidoP = tbApellidoP.Text,
                apellidoM = tbApellidoM.Text,
                telefono = tbTelefono.Text,
                empresa = tbEmpresa.Text;

            if (nombre == "" || apellidoP == "" ||
                apellidoM == ""|| telefono == ""||empresa == "")
            {
                MessageBox.Show("Rellene todos los campos","Alerta",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                //validar que la contraseña no tenga menos de 6 caracteres
                
                if (!Regex.IsMatch(tbTelefono.Text, "^(7[46]7\\d{7})"))
                {
                    MessageBoxResult boton = MessageBox.Show("El formato del telefono no es permitido\n" +
                        "tiene que iniciar por lo menos con '747'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (id == 0)//Si no se esta usando para modificar entonces se usa para agregar
                {
                    int resultado;
                    resultado = new BaseDeDatos().agregarProveedor(nombre, apellidoP, apellidoM,telefono,empresa);
                    if (resultado != 1)
                    {
                        new Proveedores(idUsuario).Show();
                        this.Close();
                    }

                }
                else//en caso de ser un id que ya se tenga entonces se usa para modificar
                {
                    int resultado;
                    resultado = new BaseDeDatos().modificarProveedor(id,nombre, apellidoP, apellidoM,telefono, empresa);
                    if (resultado != 1)
                    {
                        new Proveedores(idUsuario).Show();
                        this.Close();
                    }
                }

            }
        }
    }
}
