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
    public partial class addModifySup : Form
    {
        public addModifySup()
        {
            InitializeComponent();
        }
        public bool addProducts_suppliers_packages;
        public Products_suppliers_packages psp; // current product supplier packages
        public Products_suppliers_packages modifyproducts_suppliers_packages;
        private void addModifySup_Load(object sender, EventArgs e)
        {
            if (!addProducts_suppliers_packages)
            {
                this.Text = "Modify Product Supplier Package";

                pkgIDtextBox3.Text = modifyproducts_suppliers_packages.packageId.ToString();

                prdSuppIDtextBox4.Text = modifyproducts_suppliers_packages.productSupplierId.ToString();

            }
        }
        private void putProductSuppliesPackages(Products_suppliers_packages products_suppliers_packages)

        {
            products_suppliers_packages.packageId = Convert.ToInt32(pkgIDtextBox3.Text);

            products_suppliers_packages.productSupplierId = Convert.ToInt32(prdSuppIDtextBox4.Text);

        }
        private void DisplayProducts_suppliers_packages()
        {
            pkgIDtextBox3.Text = Convert.ToString(psp.packageId);
            prdSuppIDtextBox4.Text = Convert.ToString(psp.productSupplierId);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addProducts_suppliers_packages) // processing Add
                {
                    psp = new Products_suppliers_packages();
                    PutProducts_suppliers_packagesData(psp);

                    try
                    {
                        Products_suppliers_packagesDB.AddProducts_suppliers_packages(psp);
                        MessageBox.Show("New Item with Package Id of " + psp.packageId + " and productSupplier Id of " + psp.productSupplierId + " was added");
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else // processing Modify
                {
                    Products_suppliers_packages newProducts_suppliers_packages = new Products_suppliers_packages();
                    newProducts_suppliers_packages.packageId = Convert.ToInt32(pkgIDtextBox3.Text);
                    newProducts_suppliers_packages.productSupplierId = Convert.ToInt32(prdSuppIDtextBox4.Text);
                    PutProducts_suppliers_packagesData(newProducts_suppliers_packages);
                    try
                    {
                        if (!Products_suppliers_packagesDB.UpdateProducts_suppliers_packages(modifyproducts_suppliers_packages, newProducts_suppliers_packages))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that customer.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            psp = newProducts_suppliers_packages;
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
        private bool IsValidData()
        {
            return
                Validator.IsPresent(pkgIDtextBox3) &&
                Validator.IsPresent(prdSuppIDtextBox4);
        }

        private void PutProducts_suppliers_packagesData(Products_suppliers_packages products_suppliers_packages)
        {
            products_suppliers_packages.packageId = Convert.ToInt32(pkgIDtextBox3.Text);
            products_suppliers_packages.productSupplierId = Convert.ToInt32(prdSuppIDtextBox4.Text);

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
