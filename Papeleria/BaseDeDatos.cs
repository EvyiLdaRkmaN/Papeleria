using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Papeleria
{
    class BaseDeDatos
    {

        
        //public SqlConnection conexion = new SqlConnection();
        //private String estadoConexion;
        
        //SqlCommand objSqlComand = new SqlCommand();
        

        //metodo para realizar la conexion que retorna el estado de la conexion
        public MySqlConnection obtenerConexion()
        {

            //Cadena de conexion con la informacion de la base de datos
            //conexion = new SqlConnection("Persist Security Info=False;User ID=yordy;Password=7671067498;Initial Catalog=papeleria;");
            //capturar en caso de que haya error en la conexion
            string connStr = "server=localhost;user=root;database=zapateriaelena;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                Console.WriteLine("Connecting to MySQL..." + conn.State.ToString());
                // Perform database operations
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Conección establecida");
                }
                return conn;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            
        }

        private void cerrarConexionBase()
        {
            //conn.Close();
        }

        //verificamos el puesto los datos del inicio de secion de los usuarios.
        //COLLATE Latin1_General_CS_AS ayuda a que se tomen en cuenta mayusculas y minusculas en la consulta y evitar que se traiga algun dato no deseado
        /**public String inicioSecion(String usuario, String contraseña)
        {
            String puesto = "";
            try
            {
                //Se indica la instruccion a atraer
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select Puesto from usuarios where NombreUsuario  = '" + usuario+ "' COLLATE Latin1_General_CS_AS and Contraseña = '" + contraseña+ "' COLLATE Latin1_General_CS_AS;";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    leer.Read();
                    puesto = leer.GetString(0);
                }
                else
                {
                    System.Windows.MessageBox.Show("Usuario o contraseña incorrecta");
                }
                leer.Close();

            }

            catch (Exception e)
            {

                throw e;
            }
            return puesto;
        }

        public int obteberid(String usuario, String contraseña)
        {
            int id = 0;
            try
            {
                //Se indica la instruccion a atraer
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select idUsuario from usuarios where NombreUsuario  = '" + usuario + "' COLLATE Latin1_General_CS_AS and Contraseña = '" + contraseña + "' COLLATE Latin1_General_CS_AS;";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    leer.Read();
                    id = leer.GetInt32(0);
                }
                leer.Close();

            }

            catch (Exception e)
            {

                throw e;
            }
            return id;
        }

        //Agregar un nuevo usuario verificando que no haya nigun repetido 
        public int agregarUsuario(String usuario, String nombre,String apellidoP, String apellidoM, String sexo, String telefono, String edad, String puesto, String contraseña)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select NombreUsuario from usuarios where Nombre = '" + nombre + "' and ApellidoP = '" + apellidoP+"';";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    System.Windows.MessageBox.Show("El usuario ya se encuentra registrado");
                    return 1;
                }
                leer.Close();
                comando.CommandText = "select NombreUsuario from usuarios where NombreUsuario = '" + usuario + "' COLLATE Latin1_General_CS_AS;";
                SqlDataReader leer2 = comando.ExecuteReader();
                if (leer2.HasRows)
                {
                    leer2.Read();
                    if (usuario == leer2.GetString(0))
                    {
                        System.Windows.MessageBox.Show("Este nombre de usuario ya se encuentra en uso ");
                        return 1;
                    }
                }
                leer2.Close();
                //System.Windows.MessageBox.Show("Entra despues de los if");
                comando.CommandText = "EXECUTE insertarUsuario '"+usuario+"', '"+nombre+"','"+apellidoP+"', '"+apellidoM+"','"+sexo+"','"+telefono+"','"+edad+"','"+puesto+"','"+contraseña+"';";
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            System.Windows.MessageBox.Show("Usuario registrado");
            cerrarConexionBase();
            return 0;
        }

        public int modificarUsuario(int idUsuario, String usuario, String nombre, String apellidoP, String apellidoM, String sexo, String telefono, String edad, String puesto, String contraseña)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select idUsuario,NombreUsuario from usuarios where Nombre = '" + nombre + "' and ApellidoP = '" + apellidoP + "';";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    int repit = 0;
                    while (leer.Read())
                    {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        Console.WriteLine(leer.GetInt32(0)+" "+leer.GetString(1));
                        repit++;
                    }
                    if (repit>1)
                    {
                        System.Windows.MessageBox.Show( "ya hay un registro con los mismos datos");
                        return 1;
                    }
                }
                leer.Close();

                comando.CommandText = "select idUsuario from usuarios where NombreUsuario = '" + usuario + "' COLLATE Latin1_General_CS_AS;";
                SqlDataReader leer2 = comando.ExecuteReader();
                if (leer2.HasRows)
                {
                    leer2.Read();
                    if (idUsuario != leer2.GetInt32(0))
                    {
                        System.Windows.MessageBox.Show("Este nombre de usuario ya se encuentra en uso ");
                        return 1;
                    }
                }
                leer2.Close();

                comando.CommandText = "update usuarios set  " +
                    "NombreUsuario = '" + usuario + "'," +
                    "Nombre =  '" + nombre + "'," +
                    "ApellidoP = '" + apellidoP + "'," +
                    "ApellidoM = '" + apellidoM + "'," +
                    "Sexo = '" + sexo + "'," +
                    "Telefono = '" + telefono + "'," +
                    "Edad = '" + edad + "'," +
                    "Puesto = '" + puesto + "'," +
                    "Contraseña = '" + contraseña + "'" +
                    "where idUsuario = "+idUsuario+";";
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
            return 0;
        }

        public String getPuesto(int idUsuario)
        {
            String id = "";
            try
            {
                //Se indica la instruccion a atraer
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select Puesto from usuarios where idUsuario = "+idUsuario+"";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    leer.Read();
                    id = leer.GetString(0);
                }
                leer.Close();

            }

            catch (Exception e)
            {

                throw e;
            }
            return id;
        }


        public int eliminarUsuario(int idUsuario, String puesto)
        {
            int result = 1;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                
                switch (puesto)
                {
                    case "Cajero":
                        comando.CommandText = "select * from liquidacionCaja where idUsuario = " + idUsuario + ";";
                        SqlDataReader leer = comando.ExecuteReader();
                        if (leer.HasRows)
                        {
                            result =  0;
                        }
                        leer.Close();
                        comando.CommandText = "select * from pagoVenta where idUsuario = " + idUsuario + ";";
                        SqlDataReader leer2 = comando.ExecuteReader();
                        if (leer2.HasRows)
                        {
                            result = 0;
                        }
                        leer2.Close();
                        break;
                    case "Vendedor":
                        comando.CommandText = "select * from ventaProducto where idUsuario = " + idUsuario + ";";
                        SqlDataReader leer3 = comando.ExecuteReader();
                        if (leer3.HasRows)
                        {
                            result = 0;
                        }
                        leer3.Close();
                        break;
                    default:
                        break;
                }
                if (result != 0 && idUsuario != 1)
                {
                    comando.CommandText = "delete from usuarios where idUsuario = " + idUsuario + ";";
                    comando.ExecuteNonQuery();
                    System.Windows.MessageBox.Show("Eliminado con exito");
                }
                cerrarConexionBase();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public int eliminarRegistrosUsuario(int idUsuario, String puesto)
        {
            int result = 1;
            try
            {
                SqlCommand comando = new SqlCommand();
                SqlCommand comando2 = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando2.Connection = obtenerConexion();//Conexion abierta

                switch (puesto)
                {
                    case "Cajero":
                        comando.CommandText = "delete  from liquidacionCaja where idUsuario = " + idUsuario + ";";
                        comando.ExecuteNonQuery();
                        comando.CommandText = "delete from pagoVenta where idUsuario = " + idUsuario + ";";
                        comando.ExecuteNonQuery();
                        break;

                    case "Vendedor":
                        comando.CommandText = "select * from ventaProducto where idUsuario = " + idUsuario + " and EstadoVenta = 'Pendiente';";
                        SqlDataReader leer = comando.ExecuteReader();
                        if (leer.HasRows)
                        {
                            System.Windows.MessageBox.Show("Este usuario no puede ser eliminado ya que tiene ventas que no han sido pagadas","Error",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Error);
                            cerrarConexionBase();
                            return 0;
                        }
                        leer.Close();

                        comando.CommandText = "select idVentaProducto from ventaProducto where idUsuario = " + idUsuario + ";";
                        SqlDataReader leer2 = comando.ExecuteReader();
                        if (leer2.HasRows)
                        {

                            leer2.Read();
                            comando2.CommandText = "delete from pagoVenta where idVenta = " + leer2.GetInt32(0) + ";";
                            comando2.ExecuteNonQuery();
                        }
                        leer2.Close();

                        comando.CommandText = "delete from ventaProducto where idUsuario = " + idUsuario + ";";
                        comando.ExecuteNonQuery();
                        break;

                    default:
                        break;
                }
                if (result != 0 && idUsuario != 1)
                {
                    comando.CommandText = "delete from usuarios where idUsuario = " + idUsuario + ";";
                    comando.ExecuteNonQuery();
                    System.Windows.MessageBox.Show("Eliminado con exito");
                }
                cerrarConexionBase();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        //Gestion de productos y proveedores
        public int agregarProveedor(String nombre, String apellidoP, String apellidoM,String telefono,String empresa)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select nombreProvedor from proveedores where nombreProvedor = '" + nombre + "' and apellido_P = '" + apellidoP + "';";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    System.Windows.MessageBox.Show("El usuario ya se encuentra registrado");
                    return 1;
                }
                leer.Close();
                //System.Windows.MessageBox.Show("Entra despues de los if");
                comando.CommandText = "EXECUTE insertarProveedor '" + nombre + "','" + apellidoP + "', '" + apellidoM + "','" + telefono + "','"+empresa+"';";
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            System.Windows.MessageBox.Show("Proveedor registrado");
            cerrarConexionBase();
            return 0;
        }

        public int modificarProveedor(int idProveedor, String nombre, String apellidoP, String apellidoM, String telefono, String empresa)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select nombreProvedor from proveedores where nombreProvedor = '" + nombre + "' and apellido_P = '" + apellidoP + "' and idProveedor != "+idProveedor+";";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    int repit = 0;
                    while (leer.Read())
                    {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        Console.WriteLine(leer.GetString(0));
                        repit++;
                    }
                    if (repit > 1)
                    {
                        System.Windows.MessageBox.Show("ya hay un registro con los mismos datos");
                        return 1;
                    }
                }
                leer.Close();
                
                comando.CommandText = "update proveedores set  " +
                    "nombreProvedor =  '" + nombre + "'," +
                    "apellido_P = '" + apellidoP + "'," +
                    "apellido_M = '" + apellidoM + "'," +
                    "telefono = '" + telefono + "'," +
                    "empresa = '" + empresa + "'" +
                    "where idProveedor = " + idProveedor + ";";
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
            return 0;
        }

        public int eliminarProveedor(int idProveedor)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                SqlCommand comando2 = new SqlCommand();
                SqlCommand comando3 = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando2.Connection = obtenerConexion();//Conexion abierta
                comando3.Connection = obtenerConexion();//Conexion abierta

                comando.CommandText = "select idProducto from productos where idProvedor = " + idProveedor + ";";
                SqlDataReader leer2 = comando.ExecuteReader();
                if (leer2.HasRows)
                {
                    System.Windows.MessageBoxResult res = System.Windows.MessageBox.Show("No puede eliminar este proveedor por que hay productos relacionado con este proveedor\n" +
                        "¿Desea eliminar los productos?", "Error", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Error);
                    if (res == System.Windows.MessageBoxResult.Yes)
                    {
                        leer2.Read();
                        comando3.CommandText = "select * from productoVenta where idProducto = " + leer2.GetInt32(0) + ";";
                        SqlDataReader leer3 = comando3.ExecuteReader();
                        if (leer3.HasRows)
                        {
                            System.Windows.MessageBox.Show("Estos productos estan siendo utilizados para el proseo de una venta\n" +
                                "para eliminar al proveedor se recomienda cambiar a los productos " +
                                "asociados a este proveedor, por otro proveedor provicional", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        }
                        else
                        {
                            comando2.CommandText = "delete from productos where idProvedor = " + idProveedor + ";";
                            comando2.ExecuteNonQuery();
                        }
                        leer3.Close();
                    }
                }
                else
                {
                    leer2.Close();
                    comando.CommandText = "delete from proveedores where idProveedor = " + idProveedor + ";";
                    comando.ExecuteNonQuery();
                    System.Windows.MessageBox.Show("Eliminado con exito");
                    cerrarConexionBase();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return 0;
        }

        public int agregarProducto(String nombre, String canitdad, String preciUnitario, String descripcion, int idProveedor,String codigo)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select Codigo from productos where Codigo = '" + codigo + "';";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    System.Windows.MessageBox.Show("Ya existe un producto con este codigo");
                    return 1;
                }
                leer.Close();

                comando.CommandText = "select nombre from productos where nombre = '" + nombre + "';";
                SqlDataReader leer2 = comando.ExecuteReader();
                if (leer2.HasRows)
                {
                    leer2.Read();
                    if (nombre == leer2.GetString(0))
                    {
                        System.Windows.MessageBox.Show("Este producto ya se encuentra con otro codigo");
                        return 1;
                    }
                }
                leer2.Close();
                //System.Windows.MessageBox.Show("Entra despues de los if");
                comando.CommandText = "EXECUTE insertarProducto '" + nombre + "'," + canitdad + ", " + preciUnitario + ",'" + descripcion + "'," + idProveedor + ",'"+codigo+"';";
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            System.Windows.MessageBox.Show("Producto registrado");
            cerrarConexionBase();
            return 0;
        }

        public int modificarProducto(int idProducto, String nombre, String canitdad, String preciUnitario, String descripcion, int idProveedor,String codigo)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select Codigo from productos where Codigo = '" + codigo + "' and idProducto != "+idProducto+";";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        //Console.WriteLine(leer.GetString(0));
                    
                        System.Windows.MessageBox.Show("ya hay un producto con ese codigo");
                        return 1;
                }
                leer.Close();

                comando.CommandText = "update productos set  " +
                    "nombre =  '" + nombre + "'," +
                    "cantidad = " + canitdad + "," +
                    "precioUnitario = " + preciUnitario + "," +
                    "descripcion = '" + descripcion + "'," +
                    "idProvedor = " + idProveedor + "," +
                    "Codigo = '"+codigo+"'"+
                    "where idProducto = " + idProducto + ";";
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
            return 0;
        }

        public int eliminarProducto(int idProducto)
        {
            try
            {

                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta

                comando.CommandText = "select idProductoVenta from ProductoVenta where idProducto = " + idProducto + ";";
                SqlDataReader leer2 = comando.ExecuteReader();
                if (leer2.HasRows)
                {
                    System.Windows.MessageBoxResult res = System.Windows.MessageBox.Show("No se puede eliminar este producto por que esta siendo utilizado para un proceso de venta\n" +
                        "", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);

                }
                else
                {

                    comando.CommandText = "delete from productos where idProducto = " + idProducto + ";";
                    comando.ExecuteNonQuery();
                    System.Windows.MessageBox.Show("Eliminado con exito");
                    cerrarConexionBase();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return 0;
        }

        public int añadirProductos(int idProducto,String cantidad)
        {
            int temp = 0;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select cantidad from productos where idProducto = " + idProducto + ";";
                
                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    int repit = 0;
                    while (leer.Read())
                    {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        temp=leer.GetInt32(0);
                        //temp = leer.GetInt32(0);
                        repit++;
                    }
                }
                leer.Close();
                temp = temp + Convert.ToInt32(cantidad);

                comando.CommandText = "update productos set  " +
                    "cantidad = " + temp + " " +
                    "where idProducto = " + idProducto + ";";
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
            System.Windows.MessageBox.Show("Ahora son " + temp, "Correcto", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            return 0;
        }

        public int añadirProductosdeRegreso(int idProducto, String cantidad)
        {
            int temp = 0;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select cantidad from productos where idProducto = " + idProducto + ";";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    int repit = 0;
                    while (leer.Read())
                    {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        temp = leer.GetInt32(0);
                        //temp = leer.GetInt32(0);
                        repit++;
                    }
                }
                leer.Close();
                temp = temp + Convert.ToInt32(cantidad);

                comando.CommandText = "update productos set  " +
                    "cantidad = " + temp + " " +
                    "where idProducto = " + idProducto + ";";
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
            return 0;
        }

        //Todos los metodos sobre realizacion de una venta
        public void registrarVenta(double total,int idUsuario,int idVenta,int confirmado)
        {
            //modificar 0 = no, 1 = si
            try
            {
                if (confirmado == 0)
                {
                    Console.WriteLine("Entro");
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = obtenerConexion();//Conexion abierta
                    comando.CommandText = "insert into ventaProducto values ('" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "'," + total + "," +idUsuario+ ",'Pendiente');";
                    comando.ExecuteNonQuery();
                }
                else if (confirmado ==1)
                {
                    Console.WriteLine("update ventaProducto set FechaHora = '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "', total = " + total + ", " + idUsuario + ", 'Pendiente';");
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = obtenerConexion();//Conexion abierta
                    comando.CommandText = "update ventaProducto set FechaHora ='" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "', total = " +total+ " where idVentaProducto = " + idVenta+";";
                    comando.ExecuteNonQuery();
                }

                //String dato = DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("nl - BE"));
                //DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("nl - BE"));
                //System.Windows.MessageBox.Show("Entra despues de los if");
                //Console.WriteLine("insert into ventaProducto (FechaHora,idUsuario,EstadoVenta) values ('"+ DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "',total,"+idUsuario+")");
                comando.CommandText = "insert into ventaProducto '" + nombre + "'," + canitdad + ", " + preciUnitario + ",'" + descripcion + "'," + idProveedor + ",'" + codigo + "';";
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            System.Windows.MessageBox.Show("venta Registrada");
            cerrarConexionBase();
            
        }

        public int getsetCantidadProducto(int idProducto, int nuevaCantidad,int modificar)
        {
            //modificar 0= no, 1 = si
            int cantidad = 0;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                if (modificar == 0)
                {
                    comando.CommandText = "select cantidad from productos where idProducto = " + idProducto + ";";
                }
                else
                {
                    comando.CommandText = "update productos set cantidad = "+nuevaCantidad+"  where idProducto = "+idProducto+";";
                    comando.ExecuteNonQuery();
                }
                

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        cantidad = leer.GetInt32(0);
                    }
                }
                leer.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            Console.WriteLine("Nueva cantidad o cantidad anterior "+cantidad);
            cerrarConexionBase();
            return cantidad;
        }

        public int obtenerUltimaVenta()
        {
            int resultado =0;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select idVentaProducto from VentaProducto;";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        resultado =  leer.GetInt32(0);
                    }
                }
                leer.Close();
                //String dato = DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("nl - BE"));
                //DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("nl - BE"));
                //System.Windows.MessageBox.Show("Entra despues de los if");
                //Console.WriteLine("insert into ventaProducto (FechaHora,idUsuario,EstadoVenta) values ('"+ DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "',total,"+idUsuario+")");
                comando.CommandText = "insert into ventaProducto '" + nombre + "'," + canitdad + ", " + preciUnitario + ",'" + descripcion + "'," + idProveedor + ",'" + codigo + "';";
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
            return resultado;
        }

        public void ventaProducto(int idProducto,int idventa, int cantidad)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "insert into ProductoVenta values ("+idventa+","+cantidad+","+idProducto+");";
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
        }

        public void eliminarProductoDeVenta(int idUnico)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "delete from ProductoVenta where idUnico = "+idUnico+";";
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
        }

        public void eliminaTodalaVenta(int idVenta)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "delete from ProductoVenta where idProductoVenta = " + idVenta + ";";
                comando.ExecuteNonQuery();

                comando.CommandText = "delete from ventaProducto where idVentaProducto = " + idVenta + ";";
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
        }

        public double obtenerTotalVenta(int idVenta)
        {
            double total = 0;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "Select sum(productos.precioUnitario*ProductoVenta.Cantidad) as total from ProductoVenta " +
                    "join productos on ProductoVenta.idProducto = productos.idProducto " +
                    "where idProductoVenta = "+idVenta+";";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        if (leer.IsDBNull(0))
                        {
                            total = 0;
                        }
                        else
                        {
                            total = leer.GetDouble(0);
                        }
                    }
                }
                leer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
            return total;
        }

        public void eliminarVenta(int idVenta)
        {
            double total = 0;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "delete from ventaProducto where idVentaProducto = "+idVenta+";";
                comando.ExecuteNonQuery();
                
                comando.CommandText = "delete from ProductoVenta where idProductoVenta = " + idVenta + ";";
                comando.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cerrarConexionBase();
        }

        //Registro de los pagos
        public void registrarPago(double pago, int idUsuario, int idVenta)
        {
            try
            {
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = obtenerConexion();//Conexion abierta
                    comando.CommandText = "insert into pagoVenta values (" + idUsuario + "," + idVenta + "," + pago + ",'" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "',0);";
                    comando.ExecuteNonQuery();
                
                    comando.CommandText = "update ventaProducto set EstadoVenta ='Pagado' where idVentaProducto = "+idVenta+";";
                    comando.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            System.Windows.MessageBox.Show("venta Registrada");
            cerrarConexionBase();

        }

        public void realizarCorte(int idUsuario)
        {
            double total = 0;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta

                comando.CommandText = "select sum(pagoTotal) from pagoVenta where idUsuario = " + idUsuario + " and corte = 0;";
                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                         
                        if (!leer.IsDBNull(0))//Si el resultado es diferente de nul
                        {
                            total = Convert.ToDouble(leer.GetDecimal(0));
                        }

                    }
                }
                leer.Close();
                //ingresa el total de la liquidacion de caja
                comando.CommandText = "insert into liquidacionCaja values(" + idUsuario+","+total+", '"+ DateTime.Now.ToString("dd/MM/yyyy") + "');";
                comando.ExecuteNonQuery();
                //cambia todos los valores a corte de caja para que ya  no se visualizen despues
                comando.CommandText = "update pagoVenta set corte =1 where idUsuario = " + idUsuario + ";";
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            System.Windows.MessageBox.Show("venta Registrada");
            cerrarConexionBase();

        }

        public String obtenerVendedor(int idVenta)
        {
            
            string nombre = "", p = "",m = "";
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select usuarios.Nombre, usuarios.ApellidoP, usuarios.ApellidoM from ventaProducto join usuarios on ventaProducto.idUsuario = usuarios.idUsuario where idVentaProducto = "+idVenta+";";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        nombre = leer.GetString(0);
                        p = leer.GetString(1);
                        m = leer.GetString(2);
                    }
                }
                leer.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            cerrarConexionBase();
            nombre = nombre + " " + p + " " + m;
            return nombre;
        }

        public String obtenerCajero(int idVenta)
        {
            string nombre = "", p = "", m = "";
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select usuarios.Nombre, usuarios.ApellidoP, usuarios.ApellidoM from pagoVenta join usuarios on pagoVenta.idUsuario = usuarios.idUsuario where idVenta = "+idVenta+";";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        nombre = leer.GetString(0);
                        p = leer.GetString(1);
                        m = leer.GetString(2);
                    }
                }
                leer.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            cerrarConexionBase();
            nombre = nombre + " " + p + " " + m;
            return nombre;
        }

        public int validarSalidaCorte(int idUsuario)
        {
            //salida 0 = no, 1 = si
            int salida = 1;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select corte from pagoVenta where idUsuario = "+idUsuario+" and corte = 0;";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    salida = 0;
                }
                leer.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            cerrarConexionBase();
            return salida;
        }

        public double getTotalCobrado(int idUsuario)
        {

            double total = 0;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = obtenerConexion();//Conexion abierta
                comando.CommandText = "select sum(pagoTotal) from pagoVenta where idUsuario = " + idUsuario + " and corte = 0;";

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        //solo para imprimir en consola de forma para mostrar todas las filas que  hay 
                        if (!leer.IsDBNull(0))
                        {
                            total =Convert.ToDouble( leer.GetDecimal(0));
                        }
                        
                    }
                }
                leer.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            cerrarConexionBase();
            return total;
        }*/        
    }
}
