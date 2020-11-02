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
    public partial class FrmAddModifyProducts : Form
    {
        public FrmAddModifyProducts()
        {
            InitializeComponent();
        }
        public bool addproducts;
        public Product products; // current product  Id
        public Product modifyProduct;

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addproducts) // processing Add
                {
                    products = new Product();
                    PutProduct(products);

                    try
                    {
                        ProductDB.AddProduct(products);
                        MessageBox.Show("New Item with Product Id of " + products.ProductID + " and Product Name of " + products.ProdName + " was added");
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else // processing Modify
                {
                    Product newProduct = new Product();
                    newProduct.ProductID = Convert.ToInt32(txtProductID.Text);
                    txtProductID.Enabled = false;
                    newProduct.ProdName = txtProdName.Text;
                    PutProduct(newProduct);
                    try
                    {
                        if (!ProductDB.UpdateProduct(newProduct, modifyProduct))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that product.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            products = newProduct;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayProducts()
        {
            txtProductID.Text = modifyProduct.ProductID.ToString();
            txtProdName.Text = modifyProduct.ProdName.ToString();
        }

        private bool IsValidData()
        {
            return
                Validator.IsPresent(txtProdName);
        }

        private void PutProduct(Product pro)
        {
            pro.ProdName = txtProdName.Text;
        }

        private void FrmAddModifyProducts_Load(object sender, EventArgs e)
        {
            if (addproducts)
            {
                txtProductID.Visible = false;
                label1.Visible = false;
                this.Text = "Add Products";

            }
            else
            {
                this.Text = "Modify Product";
                this.DisplayProducts();
            }
        }
    }
}
