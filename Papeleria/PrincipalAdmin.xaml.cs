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

namespace Papeleria
{
    /// <summary>
    /// Lógica de interacción para PrincipalAdmin.xaml
    /// </summary>
    public partial class PrincipalAdmin : Window
    {
        private int id;
        public PrincipalAdmin(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        public PrincipalAdmin(String puesto)
        {
            InitializeComponent();

            switch (puesto)
            {
                case "Cajero":
                    btnUsuarios.IsEnabled = false;
                    break;


                default:
                    break;
            }
        }

        private void clicUsuarios(object sender, RoutedEventArgs e)
        {

        }

        private void clickUsuarios(object sender, RoutedEventArgs e)
        {
            new Usuarios(id).Show();
            this.Close();
        }

        private void btnProveedores_Click(object sender, RoutedEventArgs e)
        {
            new Proveedores(id).Show();
            this.Close();
        }

        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {
            new Productos(id).Show();
            this.Close();
        }

        private void btnVentas_Click(object sender, RoutedEventArgs e)
        {
            new Ventas(id).Show();
            this.Close();
        }

        private void btnCaja_Copy_Click(object sender, RoutedEventArgs e)
        {
            new Vender(id).Show();
            this.Close();
        }

        private void btnCaja_Click(object sender, RoutedEventArgs e)
        {
            new Caja(id).Show();
            this.Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void btnCorte_Click(object sender, RoutedEventArgs e)
        {
            new CortesDeCaja(id).Show();
            this.Close();
        }
    }
}
