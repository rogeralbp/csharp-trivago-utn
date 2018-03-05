﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace CapaNegocios
{
    public class Metodos_Usuarios
    {

        static NpgsqlConnection conexion;
        static NpgsqlCommand cmd;

        public static void Conexion()
        {
            string servidor = "localhost";
            int puerto = 5432;
            string usuario = "postgres";
            string claveAnthonny = "1414250816ma";
            string claveRoger = "Saborio17";

            string baseDatos = "gestion_vuelos";

            string cadenaConexion = "Server=" + servidor + ";" + "Port=" + puerto + ";" + "User Id=" + usuario + ";" + "Password=" + claveRoger + ";" + "Database=" + baseDatos;
            conexion = new NpgsqlConnection(cadenaConexion);

            if (conexion != null)
            {

                Console.WriteLine("Conexion con la DB nombre : " + baseDatos + " , Exitosa!!");
            }
            else
            {

                Console.WriteLine("Error en la conexion con la DB");
            }
        }




        //Este metodo llena el comboBox de nombres de paises en la seccion de alojamiento
        public void LlenarNombresPaises(ComboBox nombre_pais)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT nombre FROM paises", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        nombre_pais.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }



        //Este metodo llena el comboBox de nombres de lugares en la seccion de alojamiento
        public void LlenarNombresLugares(ComboBox nombre_lugar)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT nombre FROM lugares", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        nombre_lugar.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }


        //Este metodo llena el comboBox de nombres de paises de origen en la seccion de vuelos
        public void LlenarNombresPaisOrigen(ComboBox nombre_pais)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT pais_origen FROM rutas", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        nombre_pais.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }

        //Este metodo llena el comboBox de nombres de paises de destino en la seccion de vuelos
        public void LlenarNombresPaisDestino(ComboBox nombre_pais)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT pais_destino FROM rutas", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        nombre_pais.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }




        //Este metodo llena el comboBox de nombres de hoteles en la seccion de vuelos
        public void LlenarNombresHoteles(ComboBox hoteles)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT nombre FROM hotel", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        hoteles.Items.Add(dr.GetString(0));

                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }

        //Metodo que llena el combo de identificadores de vehiculos , en la ventana de seccion de vuelo
        public void LlenarVehiculos(ComboBox placa)
        {

            try
            {
                Conexion();
                conexion.Open();
                List<String> lista = new List<String>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT placa FROM vehiculos", conexion);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        placa.Items.Add(dr.GetInt64(0));
                    }
                }
                conexion.Close();


            }
            catch (Exception error)
            {
                Console.WriteLine(error);

            }

        }


        //Este metodo muestra la informacion del vehiculo seleccionado en el combobox por el usuario
        public void MostrarInformacionVehiculos(ComboBox placa, TextBox marca, TextBox modelo, TextBox tipo_vehiculo, TextBox precio)
        {

            try
            {
                Conexion();
                conexion.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT marca, modelo , tipo_vehiculo , precio FROM vehiculos WHERE placa = '" + placa.SelectedItem.ToString() + "'", conexion);
                NpgsqlDataReader leer = cmd.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        marca.Text = leer.GetString(0);
                        modelo.Text = leer.GetString(1);
                        tipo_vehiculo.Text = leer.GetString(2);
                        precio.Text = leer.GetDouble(3).ToString();

                    }
                    conexion.Close();

                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }

        }

        public void LlenarDtVistaPreliminarHotelsPaisASC(DataGridView agregar_hotels_pais, string pais)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, lugar , habitaciones , tarifa_hotel FROM hotel WHERE pais = '" + pais + "' ORDER BY tarifa_hotel ASC", conexion);
                adapter.Fill(dataset, "rutas");
                agregar_hotels_pais.DataSource = dataset.Tables[0];
                agregar_hotels_pais.Columns[0].HeaderCell.Value = "identificador";
                agregar_hotels_pais.Columns[1].HeaderCell.Value = "nombre";
                agregar_hotels_pais.Columns[2].HeaderCell.Value = "lugar";
                agregar_hotels_pais.Columns[3].HeaderCell.Value = "habitaciones";
                agregar_hotels_pais.Columns[4].HeaderCell.Value = "tarifa_hotel";
                //DataGridViewImageColumn imag = new DataGridViewImageColumn();
                //imag.HeaderText = "Foto_Hotel";
                //imag.Name = "Foto_Hotel";
                //agregar_hotels_pais.Columns.Add(imag);
           
                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public ArrayList CargarFotosHotelesPaisASC(string pais) {

            ArrayList v = new ArrayList();

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT  foto FROM hotel WHERE pais = '" + pais + "' ORDER BY tarifa_hotel ASC", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    //cedula, nombre y tipo de usuario
                    v.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();

            return v;
        }

        public ArrayList CargarFotosHotelesPaisDESC(string pais)
        {

            ArrayList v = new ArrayList();

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT  foto FROM hotel WHERE pais = '" + pais + "' ORDER BY tarifa_hotel DESC", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                while (lectorConsulta.Read())
                {
                    //cedula, nombre y tipo de usuario
                    v.Add(lectorConsulta.GetString(0));
                }
            }
            conexion.Close();

            return v;
        }

        public void InsertarFotoHotelPaisASC(){

            
        }

        public void LlenarDtVistaPreliminarHotelsPaisDESC(DataGridView agregar_hotels_pais, string pais)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, lugar , habitaciones , tarifa_hotel FROM hotel WHERE pais = '" + pais + "' ORDER BY tarifa_hotel DESC", conexion);
                adapter.Fill(dataset, "rutas");
                agregar_hotels_pais.DataSource = dataset.Tables[0];
                agregar_hotels_pais.Columns[0].HeaderCell.Value = "identificador";
                agregar_hotels_pais.Columns[1].HeaderCell.Value = "nombre";
                agregar_hotels_pais.Columns[2].HeaderCell.Value = "lugar";
                agregar_hotels_pais.Columns[3].HeaderCell.Value = "habitaciones";
                agregar_hotels_pais.Columns[4].HeaderCell.Value = "tarifa_hotel";
                //DataGridViewImageColumn imag = new DataGridViewImageColumn();
                //imag.HeaderText = "Foto_Hotel";
                //imag.Name = "Foto_Hotel";
                //agregar_hotels_pais.Columns.Add(imag);
                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void LlenarDtVistaPreliminarHotelsCiudadASC(DataGridView agregar_hotels_ciudad, string lugar)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, habitaciones , tarifa_hotel FROM hotel WHERE lugar = '" + lugar + "' ORDER BY tarifa_hotel ASC", conexion);
                adapter.Fill(dataset, "hotel");
                agregar_hotels_ciudad.DataSource = dataset.Tables[0];
                agregar_hotels_ciudad.Columns[0].HeaderCell.Value = "identificador";
                agregar_hotels_ciudad.Columns[1].HeaderCell.Value = "nombre";
                agregar_hotels_ciudad.Columns[2].HeaderCell.Value = "habitaciones";
                agregar_hotels_ciudad.Columns[3].HeaderCell.Value = "tarifa_hotel";

                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void LlenarDtVistaPreliminarHotelsCiudadDESC(DataGridView agregar_hotels_ciudad, string lugar)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, habitaciones , tarifa_hotel FROM hotel WHERE lugar = '" + lugar + "' ORDER BY tarifa_hotel DESC", conexion);
                adapter.Fill(dataset, "hotel");
                agregar_hotels_ciudad.DataSource = dataset.Tables[0];
                agregar_hotels_ciudad.Columns[0].HeaderCell.Value = "identificador";
                agregar_hotels_ciudad.Columns[1].HeaderCell.Value = "nombre";
                agregar_hotels_ciudad.Columns[2].HeaderCell.Value = "habitaciones";
                agregar_hotels_ciudad.Columns[3].HeaderCell.Value = "tarifa_hotel";

                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public void LlenarDtVistaPreliminarBusquedaHotel(DataGridView agregar_busqueda_hotel, string nombreHotel)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  identificador , nombre, pais , lugar , habitaciones , tarifa_hotel FROM hotel WHERE nombre = '" + nombreHotel + "'", conexion);
                adapter.Fill(dataset, "hotel");
                agregar_busqueda_hotel.DataSource = dataset.Tables[0]; 
                agregar_busqueda_hotel.Columns[0].HeaderCell.Value = "identificador";
                agregar_busqueda_hotel.Columns[1].HeaderCell.Value = "nombre";
                agregar_busqueda_hotel.Columns[2].HeaderCell.Value = "pais";
                agregar_busqueda_hotel.Columns[3].HeaderCell.Value = "lugar";
                agregar_busqueda_hotel.Columns[4].HeaderCell.Value = "habitaciones";
                agregar_busqueda_hotel.Columns[5].HeaderCell.Value = "tarifa_hotel";
                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public bool BanderaHotel(String nombreHotel)
        {
            bool bandera;

            Conexion();
            conexion.Open();
            NpgsqlCommand consulta = new NpgsqlCommand("SELECT * FROM hotel WHERE nombre='" + nombreHotel + "'", conexion);
            NpgsqlDataReader lectorConsulta = consulta.ExecuteReader();
            if (lectorConsulta.HasRows)
            {
                bandera = true;
            }
            else {

                bandera = false;
            }
            conexion.Close();

            return bandera;
        }

        public void InsertarDatosReservacionHotel(int idHotel,string fechaLlegada,string fechaSalida, int cantidadHabitaciones, int cantidadPersonas, int idCliente, int cantidadPersonasMayores, int cantidadPersonasMenores,double precioTotalHospedaje)
        {
            try
            {

                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("INSERT INTO informacion_reservaciones_hoteles (id_hotel,fecha_llegada,fecha_salida,cantidad_habitaciones,cantidad_personas,id_cliente,personas_mayores,personas_menores,precio_total_reservacion) VALUES ('" + idHotel+"','"+fechaLlegada+"','"+fechaSalida+"','"+cantidadHabitaciones+"','"+cantidadPersonas+"','"+idCliente+"','"+cantidadPersonasMayores+"','"+cantidadPersonasMenores+"','"+precioTotalHospedaje+"')", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Error--- \n" + error);
            }

        }

        public void InsertarDatosReservacionHotelDesdePrereservacion(int idHotel, DateTime fechaLlegada, DateTime fechaSalida, int cantidadHabitaciones, int cantidadPersonas, int idCliente, int cantidadPersonasMayores, int cantidadPersonasMenores, double precioTotalHospedaje)
        {
            try
            {

                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("INSERT INTO informacion_reservaciones_hoteles (id_hotel,fecha_llegada,fecha_salida,cantidad_habitaciones,cantidad_personas,id_cliente,personas_mayores,personas_menores,precio_total_reservacion) VALUES ('" + idHotel + "','" + fechaLlegada + "','" + fechaSalida + "','" + cantidadHabitaciones + "','" + cantidadPersonas + "','" + idCliente + "','" + cantidadPersonasMayores + "','" + cantidadPersonasMenores + "','" + precioTotalHospedaje + "')", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Error--- \n" + error);
            }

        }

        public void InsertarDatosPreReservacionHotel(int idHotel, string fechaLlegada, string fechaSalida, int cantidadHabitaciones, int cantidadPersonas, int idCliente, int cantidadPersonasMayores, int cantidadPersonasMenores, double precioTotalHospedaje)
        {
            try
            {

                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("INSERT INTO informacion_prereservaciones_hoteles (id_hotel,fecha_llegada,fecha_salida,cantidad_habitaciones,cantidad_personas,id_cliente,personas_mayores,personas_menores,precio_total_prereservacion) VALUES ('" + idHotel + "','" + fechaLlegada + "','" + fechaSalida + "','" + cantidadHabitaciones + "','" + cantidadPersonas + "','" + idCliente + "','" + cantidadPersonasMayores + "','" + cantidadPersonasMenores + "','" + precioTotalHospedaje + "')", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Error--- \n" + error);
            }

        }


        public void LlenarDtVistaPrereservacionesHotelPais(DataGridView agregar_busqueda_hotel, int cedulaUsuario)
        {
            try
            {
                Conexion();
                conexion.Open();
                DataSet dataset = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT  id_hotel, fecha_llegada, fecha_salida,cantidad_habitaciones, cantidad_personas,id_cliente,personas_menores,personas_mayores,precio_total_prereservacion FROM informacion_prereservaciones_hoteles WHERE id_cliente = '" + cedulaUsuario + "'", conexion);
                adapter.Fill(dataset, "rutas");
                agregar_busqueda_hotel.DataSource = dataset.Tables[0];
                agregar_busqueda_hotel.Columns[0].HeaderCell.Value = "id_hotel";
                agregar_busqueda_hotel.Columns[1].HeaderCell.Value = "fecha_llegada";
                agregar_busqueda_hotel.Columns[2].HeaderCell.Value = "fecha_salida";
                agregar_busqueda_hotel.Columns[3].HeaderCell.Value = "cantidad_habitaciones";
                agregar_busqueda_hotel.Columns[4].HeaderCell.Value = "cantidad_personas";
                agregar_busqueda_hotel.Columns[5].HeaderCell.Value = "id_cliente";
                agregar_busqueda_hotel.Columns[6].HeaderCell.Value = "personas_menores";
                agregar_busqueda_hotel.Columns[7].HeaderCell.Value = "personas_mayores";
                agregar_busqueda_hotel.Columns[8].HeaderCell.Value = "precio_total_prereservacion ";
                conexion.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }


        public void EliminarDatosPreReservacionHotel(int idHotel, int cantidadHabitaciones, int cantidadPersonas, int idCliente)
        {
            try
            {
                Conexion();
                conexion.Open();
                cmd = new NpgsqlCommand("DELETE FROM informacion_prereservaciones_hoteles WHERE id_hotel='"+idHotel+"' AND cantidad_habitaciones='"+cantidadHabitaciones+"' AND cantidad_personas='"+cantidadPersonas+"' AND id_cliente='"+idCliente+"'", conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {   MessageBox.Show("Error--- \n" + error);
            }
        }
    }
    
}