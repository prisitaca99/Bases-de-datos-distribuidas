using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace Practica_BDD
{
    public partial class Actualizar : Form
    {
        public Actualizar(string ConectarServer)
        {
            InitializeComponent();
            ConServer = ConectarServer;
        }

        string ConServer = "";

        private void Actualizar_Load(object sender, EventArgs e)
        {

        }

        #region Variable Globales
        ConexionSQL bd = new ConexionSQL();
        SqlConnection Conn = null;
        SqlCommand Command = null;
        string strCommand = string.Empty;

        #endregion

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            int CodReserva;
            int CodAuto;
            int CodEmpleado;
            DateTime FechaSalida;
            DateTime FechaEntrega;
            string Destino;
            int KilometrosRecorridos;
            string Usuario;
            float PrecoDiario;


            CodReserva = int.Parse(txtCodReserva.Text);
            CodAuto = int.Parse(txtCodigoAuto.Text);
            CodEmpleado = int.Parse(txtCodEmpleado.Text);
            FechaSalida = dtpFechaSalida.Value;
            FechaEntrega = dtpFechaEntrega.Value;
            Destino = txtDestino.Text;
            KilometrosRecorridos = int.Parse(txtKilometros.Text);
            Usuario = txtUsuario.Text;
            PrecoDiario = float.Parse(txtPrecioDiario.Text);

            try
            {
                //Actualizar CodEmpleado
                Conn = bd.iniciarConexion(ConServer);
                Command = Conn.CreateCommand();
                strCommand = "Update dbo.Reservas set codEmpleado = " + "'" + CodEmpleado + "'" + " where codReserva = " + "'" + CodReserva + "'";
                Command = new SqlCommand(strCommand, Conn);
                Command.ExecuteNonQuery();

                //Actualizar Fecha Salida
                Command = Conn.CreateCommand();
                strCommand = "Update dbo.Reservas set fechaSalida = " + "'" + FechaSalida.ToShortDateString() + "'" + " where codReserva = " + "'" + CodReserva + "'";
                Command = new SqlCommand(strCommand, Conn);
                Command.ExecuteNonQuery();

                //Actualizar Auto
                Command = Conn.CreateCommand();
                strCommand = "Update dbo.Reservas set codVehiculo = " + "'" + CodAuto + "'" + " where codReserva = " + "'" + CodReserva + "'";
                Command = new SqlCommand(strCommand, Conn);
                Command.ExecuteNonQuery();

                //Actualizar Fecha Entrega
                Command = Conn.CreateCommand();
                strCommand = "Update dbo.Reservas set fechaEntrega = " + "'" + FechaEntrega.ToShortDateString() + "'" + " where codReserva = " + "'" + CodReserva + "'";
                Command = new SqlCommand(strCommand, Conn);
                Command.ExecuteNonQuery();

                //Actualizar Destino 
                Command = Conn.CreateCommand();
                strCommand = "Update dbo.Reservas set destino = " + "'" + Destino + "'" + " where codReserva = " + "'" + CodReserva + "'";
                Command = new SqlCommand(strCommand, Conn);
                Command.ExecuteNonQuery();

                //Actualizar Kilometros
                Command = Conn.CreateCommand();
                strCommand = "Update dbo.Reservas set kilometros = " + "'" + KilometrosRecorridos + "'" + " where codReserva = " + "'" + CodReserva + "'";
                Command = new SqlCommand(strCommand, Conn);
                Command.ExecuteNonQuery();

                //Actualizar Usuario
                Command = Conn.CreateCommand();
                strCommand = "Update dbo.Reservas set nombreUsuario = " + "'" + Usuario + "'" + " where codReserva = " + "'" + CodReserva + "'";
                Command = new SqlCommand(strCommand, Conn);
                Command.ExecuteNonQuery();

                //Actualizar Precio
                Command = Conn.CreateCommand();
                strCommand = "Update dbo.Reservas set precioDiario = " + "'" + PrecoDiario + "'" + " where codReserva = " + "'" + CodReserva + "'";
                Command = new SqlCommand(strCommand, Conn);
                Command.ExecuteNonQuery();

                MessageBox.Show("Datos Actualizados", "", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Form1 Form = new Form1();
            //Form.Show(); 
            this.Close();

        }
    }
}
