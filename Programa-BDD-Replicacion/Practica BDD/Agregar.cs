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
    public partial class Agregar : Form
    {
        public Agregar(string Conectar)
        {
            InitializeComponent();
            ConServer = Conectar;
        }

        #region Variable Globales
        ConexionSQL bd = new ConexionSQL();
        SqlConnection Conn = null;
        SqlCommand Command = null;
        string strCommand = string.Empty;

        string ConServer = "";

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
                Conn = bd.iniciarConexion(ConServer);
                Command = Conn.CreateCommand();
                strCommand = "insert into dbo.Reservas values (" + CodReserva + "," + CodAuto + "," + CodEmpleado + "," + "'" + FechaSalida.ToShortDateString() + "'" + "," + "'" + FechaEntrega.ToShortDateString() + "'" + "," + "'" + Destino + "'" + "," + KilometrosRecorridos + "," + "'" + Usuario + "'" + "," + PrecoDiario + ")";
                Command = new SqlCommand(strCommand, Conn);
                Command.ExecuteNonQuery();

                MessageBox.Show("Datos ingresados correctamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }
    }
}
