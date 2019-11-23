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

namespace Papeleria
{
    /// <summary>
    /// Lógica de interacción para CobrarVenta.xaml
    /// </summary>
    public partial class CobrarVenta : Window
    {
        private int idVenta, idUsuario;
        private String pago;
        
        public CobrarVenta(int idventa,int idUsuario, double total)
        {
            InitializeComponent();
            this.idVenta = idventa;
            lbTotal.Content = total;
            this.idUsuario = idUsuario;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            /*if (Convert.ToDouble(pago) < Convert.ToDouble(lbTotal.Content))
            {
                MessageBox.Show("El pago no puede ser menor al total de la venta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                new BaseDeDatos().registrarPago(Convert.ToDouble(lbTotal.Content), idUsuario, idVenta);
                new Caja(idUsuario).Show();
                this.Close();
            }*/
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            new Caja(idUsuario).Show();
            this.Close();
        }

        private void tbCobro_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                int cursor = tbCobro.SelectionStart;
                
                if (tbCobro.Text.Length > 0)
                {
                    pago = tbCobro.Text;
                    cambio();
                }
                else
                {
                    pago = "0";
                    cambio();
                    if (cursor >= 0)
                    {
                        if (cursor == 0)
                        {
                            tbCobro.SelectionStart = cursor;
                        }
                        else
                        {
                            tbCobro.SelectionStart = cursor - 1;
                        }
                    }
                }
                
            }
            else if (Regex.IsMatch(tbCobro.Text, "^(\\d{1,5}([.]?(\\d{1,2})?))$"))
            {
                pago = tbCobro.Text;
                cambio();
            }
            else if (tbCobro.Text.Length != 0)
            {
                int cursor = tbCobro.SelectionStart;
                tbCobro.Text = pago;
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbCobro.SelectionStart = cursor;
                    }
                    else
                    {
                        tbCobro.SelectionStart = cursor - 1;
                    }
                }
            }
        }

        public void cambio()
        {
            double result = 0;
            String specifier = "#,#.00#";
            result = Convert.ToDouble(pago) - Convert.ToDouble(lbTotal.Content);
            lbCambio.Content = result.ToString(specifier);
        }

    }
}
