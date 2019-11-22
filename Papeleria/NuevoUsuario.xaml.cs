using System;
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
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace Papeleria
{
    /// <summary>
    /// Lógica de interacción para NuevoUsuario.xaml
    /// </summary>
    public partial class NuevoUsuario : Window
    {
        private int id,idUsuario;
        private string nombre = "", apellidoP = "", apellidom = "", edad = "", telefono = "";
        DispatcherTimer timer;
        ToolTip tt;
        public NuevoUsuario(int idUsuario)
        {
            InitializeComponent();
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3), IsEnabled = true };
            tt = new ToolTip();
            this.idUsuario = idUsuario;
        }

        public NuevoUsuario(int idUsuario,int idUsuarioOriginal,
            String nombre, String apellidoP, 
            String apellidoM, String sexo, String telefono, 
            String edad, String puesto,String nombreUsuario,
            String contraseña)
        {
            InitializeComponent();
            this.id = idUsuario;
            this.apellidom = apellidoP;
            this.apellidom = apellidoM;
            this.nombre = nombre;
            this.telefono = telefono;
            this.edad = edad;
            this.idUsuario = idUsuarioOriginal;
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3), IsEnabled = true };
            tt = new ToolTip();


            if (id != 0)
            {
                tbNombre.Text = nombre;
                tbApellidoP.Text = apellidoP;
                tbApellidoM.Text = apellidoM;
                cmbSexo.Text = sexo;
                tbEdad.Text = edad;
                tbTelefono.Text = telefono;
                if (puesto == "Administrador")//En caso de que sea administrador se agrega el campo para que sea seleccionado
                {
                    cmbPuesto.Items.Clear();
                    cmbPuesto.Items.Add(puesto);
                }
                cmbPuesto.Text = puesto;
                tbNombreUsuario.Text = nombreUsuario;
                tbContraseña.Text = contraseña;
                Title = "Modificar usuario";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String nombre = tbNombre.Text,
                apellidoP = tbApellidoP.Text,
                apellidoM = tbApellidoM.Text,
                sexo = cmbSexo.SelectionBoxItem.ToString(),
                puesto = cmbPuesto.SelectionBoxItem.ToString(),
                edad = tbEdad.Text,
                telefono = tbTelefono.Text,
                usuario = tbNombreUsuario.Text,
                contraseña = tbContraseña.Text;

            //Validacion de que no exista ningun campo bacio
            if (nombre == ""|| apellidoP == ""||
                apellidoM == ""||sexo == ""||
                puesto == ""||edad == ""||
                telefono == ""||usuario == ""||
                contraseña == "")
            {
                System.Windows.MessageBox.Show("Rellene todos los campos");
            }
            else
            {
                //validar que la contraseña no tenga menos de 6 caracteres
                if (contraseña.Length<6)
                {
                    MessageBoxResult boton = MessageBox.Show("La contrasela tiene que tener un minimo de 6 caracteres", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (!Regex.IsMatch(tbTelefono.Text,"^(7[46]7\\d{7})"))
                {
                    MessageBoxResult boton = MessageBox.Show("El formato del telefono no es permitido\n" +
                        "tiene que iniciar por lo menos con '747'" , "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (id == 0)//Si no se esta usando para modificar entonces se usa para agregar
                {
                    int resultado;
                    resultado = new BaseDeDatos().agregarUsuario(usuario, nombre, apellidoP, apellidoM, sexo, telefono, edad, puesto, contraseña);
                    if (resultado != 1)
                    {
                        new Usuarios(idUsuario).Show();
                        this.Close();
                    }
                    
                }
                else//en caso de ser un id que ya se tenga entonces se usa para modificar
                {
                    int resultado;
                    resultado = new BaseDeDatos().modificarUsuario(id,usuario, nombre, apellidoP, apellidoM, sexo, telefono, edad, puesto, contraseña);
                    if (resultado != 1)
                    {
                        new Usuarios(idUsuario).Show();
                        this.Close();
                    }
                }
            }
        }
        
        private void btncancelar_Click_1(object sender, RoutedEventArgs e)
        { 
            new Usuarios(idUsuario).Show();
            this.Close();
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

        private void tbEdad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back|| e.Key == Key.Delete)
            {
                if (tbEdad.Text.Length >= 1 || tbEdad.Text.Length == 0)
                {
                    edad = tbEdad.Text;
                }
            }
            else if (Regex.IsMatch(tbEdad.Text, "(^\\d{1,2}$)"))
            {
                edad = tbEdad.Text;
            }
            else if (tbEdad.Text.Length != 0)
            {
                int cursor = tbEdad.SelectionStart;
                tbEdad.Text = edad;
                if (cursor >= 0)
                {
                    tbEdad.SelectionStart = cursor;
                }
            }
        }

        private void tbNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (tbNombre.Text.Length >= 1|| tbNombre.Text.Length  ==0)
                {
                    nombre = tbNombre.Text;
                    Console.WriteLine("Entro en if 1");
                }
            }
            else if (Regex.IsMatch(tbNombre.Text, "^([a-zA-Z]{1,10}[ ]?[a-zA-Z]{0,7})$"))
            {
                Console.WriteLine("Entro en if 2");
                nombre = tbNombre.Text;
            }
            else if (tbNombre.Text.Length != 0)
            {
                Console.WriteLine("Entro en if 3");
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
    }
}
