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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        private object product;
        public string oldprodname = null;
        private object GetAllProducts;
        public object ProductsDB { get; private set; }

        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                product = ProductsDB.GetAllProduct();
                dataGridView1.DataSource = product;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading customers data: " + ex.Message,
                    ex.GetType().ToString());
            }

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtCreate.Text != null)
            {
                ProductsDB.AddProduct(txtCreate.Text);
                txtCreate.Text = "";
            }
            product = ProductsDB.GetAllProducts();
            dataGridView1.DataSource = product;
            this.Refresh();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtUpdate.Text != null)
            {
                ProductsDB.UpdateProduct(txtUpdate.Text, oldprodname);
                txtUpdate.Text = "";
            }
            product = ProductsDB.GetAllProducts();
            dataGridView1.DataSource = product;
            this.Refresh();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (txtUpdate.Text != null)
            {
                string deactivate = ("inactive" + txtUpdate.Text);
                ProductsDB.UpdateProduct(deactivate, oldprodname);
                txtUpdate.Text = "";
            }
            product = ProductsDB.GetAllProducts();
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
