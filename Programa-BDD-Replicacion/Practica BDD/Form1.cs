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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvReservas.Columns.Add("CodigoReserva", "Codigo de Reserva");
            dgvReservas.Columns.Add("CodigoVehiculo","Codigo de Vehiculo");
            dgvReservas.Columns.Add("CodigoEmpleado", "Codigo de Empleado");
            dgvReservas.Columns.Add("FechaSalida", "Fecha de Salida");
            dgvReservas.Columns.Add("FechaEntrega", "Fecha de Entrega");
            dgvReservas.Columns.Add("Destino", "Destino");
            dgvReservas.Columns.Add("Kilometros", "Kilometros Recorridos");
            dgvReservas.Columns.Add("Usuario", "Usuario");
            dgvReservas.Columns.Add("PrecioDiario", "Precio Diario");

            
            dgvReservas.MultiSelect = false;
            dgvReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservas.AllowUserToAddRows = false;
            dgvReservas.AllowUserToDeleteRows = false;
            dgvReservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReservas.ReadOnly = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Variables globales
        ConexionSQL bd = new ConexionSQL();
        SqlConnection Conn = null;
        SqlCommand Command = null;
        string strCommand = string.Empty;

        string codReserva = "";
        string codEliminar = "";
        string ConectarServer = "";
        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                codReserva = txtCodigoBuscar.Text;
                dgvReservas.Rows.Clear();

                string strWhere = "where codReserva = " + codReserva;

                ActualizarDataGridView(strWhere);

                txtCodigoBuscar.Clear();       
                
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

        private void btnVerTodo_Click(object sender, EventArgs e)
        {
            try
            {
                string strWhere = null;
               ActualizarDataGridView(strWhere);

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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                codEliminar = txtEliminar.Text;
                string strWhere = null;

                if(cbSucursales.SelectedItem.ToString() == "Sucursal 1")
                {
                    ConectarServer = "Data Source=PRISCILA\\Sucursal1; Initial Catalog=RentaCarros; User ID=sa; Password=ingredes; Server=PRISCILA\\Sucursal1; Integrated Security=SSPI; Trusted_Connection=False; MultipleActiveResultSets=True";
                    MessageBox.Show("Conectado a la Sucursal 1", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if(cbSucursales.SelectedItem.ToString() == "Sucursal 2")
                    {
                        ConectarServer = "Data Source=PRISCILA\\Sucursal2; Initial Catalog=RentaCarros; User ID=sa; Password=ingredes; Server=PRISCILA\\Sucursal2; Integrated Security=SSPI; Trusted_Connection=False; MultipleActiveResultSets=True";
                        MessageBox.Show("Conectado a la Sucursal 2", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ConectarServer = "Data Source=PRISCILA\\S17100198; Initial Catalog=RentaCarros; User ID=sa; Password=ingredes; Server=PRISCILA\\S17100198; Integrated Security=SSPI; Trusted_Connection=False; MultipleActiveResultSets=True";
                        MessageBox.Show("Conectado a todas las sucursales", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                Conn = bd.iniciarConexion(ConectarServer);
                string qry = "delete from dbo.Reservas where codReserva = @codEliminar";
                SqlCommand SqlCom = new SqlCommand(qry, Conn);
                SqlCom.Parameters.Add(new SqlParameter("@codEliminar", codEliminar));
                SqlCom.ExecuteNonQuery();

                Conn.Close();


                ActualizarDataGridView(strWhere);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public void ActualizarDataGridView( string strWhere)
        {
            try
            {
                dgvReservas.Rows.Clear();

                if (cbSucursales.SelectedItem.ToString() == "Sucursal 1")
                {
                    ConectarServer = "Data Source=PRISCILA\\Sucursal1; Initial Catalog=RentaCarros; User ID=sa; Password=ingredes; Server=PRISCILA\\Sucursal1; Integrated Security=SSPI; Trusted_Connection=False; MultipleActiveResultSets=True";
                    MessageBox.Show("Conectado a la Sucursal 1", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (cbSucursales.SelectedItem.ToString() == "Sucursal 2")
                    {
                        ConectarServer = "Data Source=PRISCILA\\Sucursal2; Initial Catalog=RentaCarros; User ID=sa; Password=ingredes; Server=PRISCILA\\Sucursal2; Integrated Security=SSPI; Trusted_Connection=False; MultipleActiveResultSets=True";
                        MessageBox.Show("Conectado a la Sucursal 2", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        ConectarServer = "Data Source=PRISCILA\\S17100198; Initial Catalog=RentaCarros; User ID=sa; Password=ingredes; Server=PRISCILA\\S17100198; Integrated Security=SSPI; Trusted_Connection=False; MultipleActiveResultSets=True";
                        MessageBox.Show("Conectado a todas las sucursales", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                Conn = bd.iniciarConexion(ConectarServer);
                string qry = "select * from dbo.Reservas " + strWhere;
                SqlCommand SqlCom = new SqlCommand(qry, Conn);
                //SqlCom.Parameters.Add(new SqlParameter("@codReserva", codReserva));

                DataTable dtReservas = new DataTable();

                SqlDataReader rdr = SqlCom.ExecuteReader();

                while (rdr.Read())
                {
                    dgvReservas.Rows.Add(rdr.GetInt32(0).ToString(), rdr.GetInt32(1).ToString(), rdr.GetInt32(2).ToString(), rdr.GetDateTime(3).ToString(), rdr.GetDateTime(4).ToString(), rdr.GetString(5), rdr.GetInt32(6).ToString(), rdr.GetString(7), rdr.GetDouble(8).ToString());

                }

                //dtReservas.Load(rdr);


                //return dtReservas;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Actualizar ActualizarForm = new Actualizar(ConectarServer);
            ActualizarForm.Show();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Agregar AgregarForm = new Agregar(ConectarServer);
            AgregarForm.Show();
        }

        private void btnVerTodosSucursal_Click(object sender, EventArgs e)
        {

        }

        private void cbSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
