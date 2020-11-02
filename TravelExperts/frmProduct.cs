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
        public Product product;
        public frmProduct()
        {
            InitializeComponent();
        }
        public string oldprodname = null;
        public object ProductsDB;
        private void frmProduct_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ProductDB.GetAllProducts();
            dataGridView1.Rows[0].Selected = true;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            FrmAddModifyProducts addproducts = new FrmAddModifyProducts
            {
                addproducts = true
            };
            DialogResult result = addproducts.ShowDialog();
            if (result == DialogResult.OK)
            {
                product = addproducts.products;
                dataGridView1.DataSource = ProductDB.GetAllProducts();
            }
        }

        
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var cells = dataGridView1.CurrentRow.Cells;
            string rowId = cells[0].Value.ToString();
            string rowId1 = cells[1].Value.ToString();

            var selectedProductID = ProductDB.GetAllProducts(rowId);
            FrmAddModifyProducts modifyCustomerForm = new FrmAddModifyProducts
            {
                addproducts = false,
                modifyProduct = selectedProductID
            };
            DialogResult result = modifyCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                product = modifyCustomerForm.products;
                //refresh the grid view 
                dataGridView1.DataSource = ProductDB.GetAllProducts();
            }
        }
    }
}
