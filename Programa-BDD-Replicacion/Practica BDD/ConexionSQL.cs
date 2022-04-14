using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Practica_BDD
{
    internal class ConexionSQL
    {
        SqlConnection Conexion = new SqlConnection();
        string ConexionString;

        public SqlConnection iniciarConexion(string Conexion)
        {
           // try
            //{
                //ConexionString = Conexion//ConfigurationManager.ConnectionStrings["CadenaConexionSQL"].ConnectionString;//
                //Conexion.ConnectionString = ConexionString;
                //Conexion.Open();
                SqlConnection con = new SqlConnection(Conexion);
                con.Open();
                return con;

            //}
            //catch (SqlException)
            //{
              //  MessageBox.Show("Error al conectarse al servidor");
            //}
           
        }

        public void CerrarConexion()
        {
            try
            {
                Conexion.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show("Error al cerrar la conexion con el servidor");
            }
        }


    }
}
