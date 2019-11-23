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
    /// Lógica de interacción para CortesDeCaja.xaml
    /// </summary>
    public partial class CortesDeCaja : Window
    {
        public int idUsuario;
        public CortesDeCaja(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            mostrarTabla();
        }

        public void mostrarTabla()
        {
            /*String consulta = "select idLiquidacion, usuarios.Nombre,usuarios.ApellidoP,usuarios.ApellidoM, totalCaja,FechaCorte from liquidacionCaja join usuarios on liquidacionCaja.idUsuario = usuarios.idUsuario;";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new DGLiquidacion
                {
                    idLiquidacion = row["idLiquidacion"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    ApellidoP = row["ApellidoP"].ToString(),
                    ApellidoM = row["ApellidoM"].ToString(),
                    totalCaja = row["totalCaja"].ToString(),
                    FechaCorte = row["FechaCorte"].ToString()
                };
                dataGridVenta.Items.Add(data);
            }*/
        }


        public void mostrarTablaBusqueda(String dato, int opcion)
        {
            /*String prueba = "";
            switch (opcion)
            {
                case 1:
                    prueba = "select idLiquidacion, usuarios.Nombre,usuarios.ApellidoP,usuarios.ApellidoM, totalCaja,FechaCorte from liquidacionCaja join usuarios on liquidacionCaja.idUsuario = usuarios.idUsuario where Nombre = '"+dato+"';";
                    break;
                case 2:
                    prueba = "select idLiquidacion, usuarios.Nombre,usuarios.ApellidoP,usuarios.ApellidoM, totalCaja,FechaCorte from liquidacionCaja join usuarios on liquidacionCaja.idUsuario = usuarios.idUsuario where ApellidoP = '"+dato+"';";
                    break;
                case 3:
                    prueba = "select idLiquidacion, usuarios.Nombre,usuarios.ApellidoP,usuarios.ApellidoM, totalCaja,FechaCorte from liquidacionCaja join usuarios on liquidacionCaja.idUsuario = usuarios.idUsuario where FechaCorte = '" + dato + "';";
                    break;
            }
            
            SqlDataAdapter dataAdapter = new SqlDataAdapter(prueba, new BaseDeDatos().obtenerConexion());
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTableCollection collection = ds.Tables;
            DataTable table = collection[0];

            foreach (DataRow row in table.Rows)
            {
                var data = new DGLiquidacion
                {
                    idLiquidacion = row["idLiquidacion"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    ApellidoP = row["ApellidoP"].ToString(),
                    ApellidoM = row["ApellidoM"].ToString(),
                    totalCaja = row["totalCaja"].ToString(),
                    FechaCorte = row["FechaCorte"].ToString()
                };
                dataGridVenta.Items.Add(data);
            }*/
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            new PrincipalAdmin(idUsuario).Show();
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dataGridVenta.Items.Clear();
            mostrarTabla();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String dato = tbBusqueda.Text;
            int index = cmbBuscarPor.SelectedIndex;
            if (dato != "" && dato != "Ingrese su busqueda" && index != 0)
            {
                dataGridVenta.Items.Clear();
                mostrarTablaBusqueda(tbBusqueda.Text,index);
                cmbBuscarPor.SelectedIndex = 0;
                tbBusqueda.Text = "Ingrese su busqueda";
            }
            else if (dato == "" || dato == "Ingrese su busqueda")
            {
                MessageBox.Show("Ingrese un texto para la busqueda");
            }
            else if (index == 0)
            {
                MessageBox.Show("seleccione una opcion de busqueda");
            }
        }

        private void tbBusqueda_GotFocus(object sender, RoutedEventArgs e)
        {
            tbBusqueda.Text = "";
        }
    }
}
