using Class_library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelExperts
{
    public partial class FrmSupplier : Form
    {

        public FrmSupplier()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
         
              this.Close();
            
            
        }
        void Clear()
        {
            txtSupplierId.Text = txtSupName.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;

        }

        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = SuppliersDB.GetSuppliers();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connection = TravelExpertsDB.GetConnection();
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Suppliers values(@SupplierId,@SupName)", connection);
                cmd.Parameters.AddWithValue("@SupplierId", int.Parse(txtSupplierId.Text));
                cmd.Parameters.AddWithValue("@SupName", txtSupName.Text);
                cmd.ExecuteNonQuery();
                connection.Close();
                if (txtSupName.Text != "")
                {
                    MessageBox.Show("Successfully Added");
                }
                else
                {
                    MessageBox.Show("Supplier's details cannot be found");
                }


            }
            catch (Exception p)
            {
                MessageBox.Show(p.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connection = TravelExpertsDB.GetConnection();
                connection.Open();
                SqlCommand cmd = new SqlCommand("Update Suppliers set SupName=@SupName where SupplierId= @SupplierId", connection);
                cmd.Parameters.AddWithValue("@SupplierId", int.Parse(txtSupplierId.Text));
                cmd.Parameters.AddWithValue("@SupName", txtSupName.Text);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Successfully Updated");
            }

            catch (Exception p)
            {
                MessageBox.Show(p.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = TravelExpertsDB.GetConnection();
                connection.Open();
                SqlCommand cmd = new SqlCommand("Delete Suppliers where SupplierId = @SupplierId", connection);
                cmd.Parameters.AddWithValue("@SupplierId", int.Parse(txtSupplierId.Text));
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Successfully Deleted");
            }
            catch (Exception p)
            {
                MessageBox.Show(p.Message);
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = TravelExpertsDB.GetConnection();
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select *  from Suppliers where SupplierId = @SupplierId", connection);
                cmd.Parameters.AddWithValue("@SupplierId", int.Parse(txtSupplierId.Text));
                cmd.Parameters.AddWithValue("@SupName", txtSupName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                MessageBox.Show("Successfully found");
            }
            catch (Exception p)
            {
                MessageBox.Show(p.Message);
            }
        }
    }
}


