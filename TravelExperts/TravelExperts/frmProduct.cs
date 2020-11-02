using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Class_library;

namespace TravelExperts
{
    public partial class frmProduct : Form
    {
        private object product;
        public frmProduct()
        {
            InitializeComponent();
        }
        public string oldprodname = null;
        public object ProductsDB;
        private void frmProduct_Load(object sender, EventArgs e)
        {
            try
            {
                
                product = ProductDB.GetAllProducts();
                dataGridView1.DataSource = product;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading customers data: " + ex.Message,
                    ex.GetType().ToString());
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtUpdate.Text = row.Cells["dataGridView1TextBoxColumn2"].Value.ToString();
                oldprodname = txtUpdate.Text;
            }

        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtCreate.Text != null)
            {
                ProductDB.AddProduct(txtCreate.Text);
                txtCreate.Text = "";
            }
            product = ProductDB.GetAllProducts();
            dataGridView1.DataSource = product;
            this.Refresh();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtUpdate.Text != null)
            {
                ProductDB.UpdateProduct(txtUpdate.Text, oldprodname);
                txtUpdate.Text = "";
            }
            product = ProductDB.GetAllProducts();
            dataGridView1.DataSource = product;
            this.Refresh();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (txtUpdate.Text != null)
            {
                string deactivate = ("inactive" + txtUpdate.Text);
                ProductDB.UpdateProduct(deactivate, oldprodname);
                txtUpdate.Text = "";
            }
            product = ProductDB.GetAllProducts();
            dataGridView1.DataSource = product;
            this.Refresh();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            //new HomePage().Show();
            Close();
        }
    }
}
