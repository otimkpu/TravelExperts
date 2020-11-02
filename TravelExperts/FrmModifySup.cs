using Class_library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelExperts
{
    public partial class frmModifySup : Form
    {
        public bool addSuppliers;
        public Suppliers modifySuppliers;
        public Suppliers psp;

        public frmModifySup()
        {
            InitializeComponent();
        }

        private void putSuppliers(Suppliers suppliers)

        {
            suppliers.SuppliersId = Convert.ToInt32(txtSupplierId.Text);

            suppliers.SupName = txtSupName.Text;

        }
        private void DisplaySuppliers()
        {
            txtSupplierId.Text = Convert.ToString(psp.SuppliersId);
            txtSupName.Text = psp.SupName;
        }
        
        private bool IsValidData()
        {
            return
                Validator.IsPresent(txtSupName);
        }

        private void PutSuppliers_Data(Suppliers suppliers)
        {
            suppliers.SuppliersId = Convert.ToInt32(txtSupplierId.Text);
            suppliers.SupName = txtSupName.Text;

        }
       
        private void btnAccept_Click_1(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addSuppliers) // processing Add
                {
                    psp = new Suppliers();
                    PutSuppliers_Data(psp);

                    try
                    {
                        SuppliersDB.AddSuppliers(psp);
                        MessageBox.Show("New Item with Supplier Id of " + psp.SuppliersId + " and SupName " + psp.SupName + " was added");
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else // processing Modify
                {
                    Suppliers newSuppliers = new Suppliers();
                    newSuppliers.SupName = txtSupName.Text;
                    PutSuppliers_Data(newSuppliers);
                    try
                    {
                        if (!SuppliersDB.UpdateSuppliers(modifySuppliers, newSuppliers))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that Supplier.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            psp = newSuppliers;
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

        private void btnCancel_Click_1(object sender, EventArgs e)
        {

            this.Close();
        }

        private void frmModifySup_Load_1(object sender, EventArgs e)
        {
            if (!addSuppliers)
            {
                txtSupplierId.Text = modifySuppliers.SuppliersId.ToString();
                txtSupName.Text = modifySuppliers.SupName;
            }
        }
    }
}