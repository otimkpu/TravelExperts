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
using System.Data.SqlClient;

namespace TravelExperts
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private suppliers productsupplier;

        private void Form1_Load_1(object sender, EventArgs e)
        {

            dataGridView1.DataSource = Product_supplierDB.GetSuppliers();
        }

        private void BtnGetProductSupplier_Click(object sender, EventArgs e)
        {

            if (Validator.IsPresent(txtProductSupplierId) &&
               Validator.IsInt32(txtProductSupplierId))
            {
                int productSupplierID;
                productSupplierID = Convert.ToInt32(txtProductSupplierId.Text);
                this.GetProductSupplierId(productSupplierID);
                if (productsupplier == null)
                {
                    MessageBox.Show("No Product Supplier ID found. " +
                         "Please try again.", "Product Supplier ID Not Found");
                    this.ClearControls();
                }
                else
                    this.DisplayProductSupplier();
            }
        }
        private void GetProductSupplierId(int productSupplierID)
        {
            try
            {
                productsupplier = Product_supplierDB.GetProductSupplierId(productSupplierID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void ClearControls()
        {
            txtProductSupplierId.Text = "";
            txtProductId.Text = "";
            txtSupplierId.Text = "";
            BtnModify.Enabled = false;
            BtnDelete.Enabled = false;
            txtProductSupplierId.Select();
        }

        private void DisplayProductSupplier()
        {
            txtProductId.Text = productsupplier.ProductId.ToString();
            txtSupplierId.Text = productsupplier.SupplierId.ToString();
            BtnModify.Enabled = true;
            BtnDelete.Enabled = true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FrmAddModifyDelete addproductsupplierform = new FrmAddModifyDelete
            {
                addproductsupplier = true
            };
            DialogResult result = addproductsupplierform.ShowDialog();
            if (result == DialogResult.OK)
            {
                productsupplier = addproductsupplierform.productsupplier;
                txtProductSupplierId.Text = productsupplier.ProductSupplierId.ToString();
                this.DisplayProductSupplier();
                dataGridView1.DataSource = Product_supplierDB.GetSuppliers();

            }
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {
            FrmAddModifyDelete modifyCustomerForm = new FrmAddModifyDelete
            {
                addproductsupplier = false,
                productsupplier = productsupplier
            };
            DialogResult result = modifyCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                productsupplier = modifyCustomerForm.productsupplier;
                this.DisplayProductSupplier();
                //refresh the grid view 
                dataGridView1.DataSource = Product_supplierDB.GetSuppliers();
            }
            else if (result == DialogResult.Retry)
            {
                this.GetProductSupplierId(productsupplier.ProductSupplierId);
                if (productsupplier != null)
                    this.DisplayProductSupplier();
                else
                    this.ClearControls();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Delete ProductID " + productsupplier.ProductSupplierId + "?",
             "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!Product_supplierDB.DeleteProductSupplier(productsupplier))
                    {
                        MessageBox.Show("Another user has updated or deleted " +
                            "that Product Supplier ID.", "Database Error");
                        this.GetProductSupplierId(productsupplier.ProductSupplierId);
                        if (productsupplier != null)
                        {
                            this.DisplayProductSupplier();

                        }
                        else
                            this.ClearControls();
                    }
                    else
                    {
                        this.ClearControls();
                        //refresh gridview
                        dataGridView1.DataSource = Product_supplierDB.GetSuppliers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
