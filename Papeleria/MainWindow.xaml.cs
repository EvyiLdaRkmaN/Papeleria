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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Papeleria
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BaseDeDatos objBase;
        public MainWindow()
        {
            objBase = new BaseDeDatos();
            InitializeComponent();
            tbUsuario.Focus();
        }
        
        public void entrar(object sender, RoutedEventArgs e)
        {
            entrar();
        }

        public void entrar()
        {
            String usuario = tbUsuario.Text, contraseña = boxPassword.Password;

            if (usuario == "" ||  contraseña == "")
            {
                System.Windows.MessageBox.Show("Llene todos los campos");
            }
            else
            {
                if (usuario.Equals("Admin") && contraseña.Equals("1234"))
                {
                    new PrincipalAdmin(0).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Datos incorrectos.\nPor favor revise sus datos");
                }
                /*usuario = tbUsuario.Text;
                contraseña = boxPassword.Password;
                String puesto = objBase.inicioSecion(usuario, contraseña);
                int id = objBase.obteberid(usuario, contraseña);

                switch (puesto)
                {
                    case "Administrador":
                        new PrincipalAdmin(id).Show();
                        this.Close();
                        break;
                    case "Cajero":
                        new Caja(id).Show();
                        this.Close();
                        break;
                    case "Vendedor":
                        new Vender(id).Show();
                        this.Close();
                        break;
                    case "Almacenista":
                        new VerInventario().Show();
                        this.Close();
                        break;

                    default:
                        break;
                }*/

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void boxPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                entrar();
            }
        }
    }
}
