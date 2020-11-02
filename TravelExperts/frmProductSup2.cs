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
            dataGridView1.Rows[0].Selected = true;
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
                dataGridView1.DataSource = Product_supplierDB.GetSuppliers();

            }
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {
            var cells = dataGridView1.CurrentRow.Cells;
            string rowId = cells[1].Value.ToString();
            string rowId1 = cells[2].Value.ToString();

            var selectedProductSupplierId = Product_supplierDB.GetSuppliers(rowId, rowId1);
            FrmAddModifyDelete modifyCustomerForm = new FrmAddModifyDelete
            {
                addproductsupplier = false,
                modifyproductsupplier = selectedProductSupplierId
            };
            DialogResult result = modifyCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                productsupplier = modifyCustomerForm.productsupplier;
                //this.DisplayProductSupplier();
                //refresh the grid view 
                dataGridView1.DataSource = Product_supplierDB.GetSuppliers();
            }
            //else if (result == DialogResult.Retry)
            //{
            //    this.GetProductSupplierId(productsupplier.ProductSupplierId);
            //    if (productsupplier != null)
            //        this.DisplayProductSupplier();
            //    else
            //        this.ClearControls();
            //}
        }

        //private void BtnDelete_Click(object sender, EventArgs e)
        //{
        //    DialogResult result = MessageBox.Show("Delete ProductID " + productsupplier.ProductSupplierId + "?",
        //     "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (result == DialogResult.Yes)
        //    {
        //        try
        //        {
        //            if (!Product_supplierDB.DeleteProductSupplier(productsupplier))
        //            {
        //                MessageBox.Show("Another user has updated or deleted " +
        //                    "that Product Supplier ID.", "Database Error");
        //                this.GetProductSupplierId(productsupplier.ProductSupplierId);
        //                if (productsupplier != null)
        //                {
        //                    this.DisplayProductSupplier();

        //                }
        //                else
        //                    this.ClearControls();
        //            }
        //            else
        //            {
        //                this.ClearControls();
        //                //refresh gridview
        //                dataGridView1.DataSource = Product_supplierDB.GetSuppliers();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, ex.GetType().ToString());
        //        }
        //    }
        //}

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
