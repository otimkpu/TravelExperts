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
    public partial class FrmAddModifyDelete : Form
    {
        public FrmAddModifyDelete()
        {
            InitializeComponent();
        }
        public bool addproductsupplier;
        public suppliers productsupplier; // current product supplier Id
        private void button2_Click(object sender, EventArgs e)
        {
            
                this.Close();
            
        }
        private void DisplayProductSupplier()
        {
            txtProductID.Text = productsupplier.ProductId.ToString();
            txtSupplierID.Text = productsupplier.SupplierId.ToString();
        }

        private bool IsValidData()
        {
            return
                Validator.IsPresent(txtProductID) &&
                Validator.IsPresent(txtSupplierID) &&
                Validator.IsInt32(txtProductID) &&
                Validator.IsInt32(txtSupplierID);
        }

        private void PutProductSupplierData(suppliers prosup)
        {
            prosup.ProductId = Convert.ToInt32(txtProductID.Text);
            prosup.SupplierId = Convert.ToInt32(txtSupplierID.Text);

        }


        private void BtnAccept_Click(object sender, EventArgs e)
        {

            if (IsValidData())
            {
                if (addproductsupplier) // processing Add
                {
                    productsupplier = new suppliers();
                    this.PutProductSupplierData(productsupplier);

                    try
                    {
                        productsupplier.ProductSupplierId = Product_supplierDB.AddProductSupplier(productsupplier);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else // processing Modify
                {
                    suppliers newProductSupplier = new suppliers
                    {
                        ProductSupplierId = productsupplier.ProductSupplierId

                    };
                    this.PutProductSupplierData(newProductSupplier);

                    try
                    {
                        if (!Product_supplierDB.UpdateProductSupplier(productsupplier, newProductSupplier))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that product supplier ID.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            productsupplier = newProductSupplier;
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

        private void FrmAddModifyDelete_Load(object sender, EventArgs e)
        {
            if (addproductsupplier)
            {
                this.Text = "Add Product Supplier";

            }
            else
            {
                this.Text = "Modify Product Supplier";
                this.DisplayProductSupplier();
            }
        }
    }
}
