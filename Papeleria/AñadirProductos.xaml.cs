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
    /// Lógica de interacción para AñadirProductos.xaml
    /// </summary>
    public partial class AñadirProductos : Window
    {
        private int id, idUsuario;
        private String cantidad = "";
        public AñadirProductos(int idUsuario, int idProducto,String Cantidad,String nombre,String descripcion)
        {
            InitializeComponent();
            this.cantidad = Cantidad;
            this.id = idProducto;
            this.idUsuario = idUsuario;

            lbCantidad.Content = Cantidad;
            lbDescripcion.Content = descripcion;
            lbNombre.Content = nombre;
        }

        private void tbCantidadAgregar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (tbCantidadAgregar.Text.Length >= 0)
                {
                    cantidad = tbCantidadAgregar.Text;
                }
            }
            else if (Regex.IsMatch(tbCantidadAgregar.Text, "(^\\d{1,5}$)"))
            {
                cantidad = tbCantidadAgregar.Text;
            }
            else if (tbCantidadAgregar.Text.Length != 0)
            {
                int cursor = tbCantidadAgregar.SelectionStart;
                tbCantidadAgregar.Text = cantidad;
                if (cursor >= 0)
                {
                    if (cursor == 0)
                    {
                        tbCantidadAgregar.SelectionStart = cursor;
                    }
                    else
                    {
                        tbCantidadAgregar.SelectionStart = cursor - 1;
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            /*int resultado;
            resultado = new BaseDeDatos().añadirProductos(id,cantidad);
            if (resultado != 1)
            {
                new Productos(idUsuario).Show();
                this.Close();
            }*/
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            new Productos(idUsuario).Show();
            this.Close();
        }
    }
}
