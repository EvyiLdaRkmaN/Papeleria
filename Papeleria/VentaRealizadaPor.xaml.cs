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
    /// Lógica de interacción para VentaRealizadaPor.xaml
    /// </summary>
    public partial class VentaRealizadaPor : Window
    {
        private int idVenta,idUsuario;
        public VentaRealizadaPor(int idVenta, int idUsuario)
        {
            /*InitializeComponent();
            this.idVenta = idVenta;
            this.idUsuario = idUsuario;
            lbVendedor.Content = new BaseDeDatos().obtenerVendedor(idVenta);
            lbCajero.Content = new BaseDeDatos().obtenerCajero(idVenta);*/
        }

        private void btnVisualizar_Click(object sender, RoutedEventArgs e)
        {
            new NuevaVenta(idUsuario, idVenta, 2).Show();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            new Ventas(idUsuario).Show();
            this.Close();

        }
    }
}
