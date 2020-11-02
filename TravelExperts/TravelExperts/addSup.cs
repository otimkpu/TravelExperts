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
    public partial class addSup : Form
    {
        public addSup()
        {
            InitializeComponent();
        }
        public Supplier sup;
        internal bool addSupplier;
        internal object modifySup;
        private object modifysup;
        internal bool addsupplier;
        internal Suppliers supplier;

        private void addSup_Load(object sender, EventArgs e)
        {
            if (addsupplier)
            {
                this.Text = "Add Supplier";

            }
            else
            {
                this.Text = "Modify  Supplier";
                this.DisplaySupplier();
            }
        }

        private void DisplaySupplier()
        {
            
            txtSupplierID.Text = supplier.SupplierId.ToString();
            txtSupName.Text = supplier.SupName.ToString();
        }
        private bool IsValidData()
        {
            return
                Validator.IsPresent(txtSupplierID) &&
                Validator.IsPresent(txtSupName) &&
                Validator.IsInt32(txtSupplierID);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addsupplier) // processing Add
                {
                    supplier = new Suppliers();
                    this.PutSupplier(supplier);

                    try
                    {
                        supplier.SupplierId = SuppliersDB.AddSupplier(supplier);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else // processing Modify
                {
                    suppliers newSupplier = new suppliers
                    {
                        SupplierId = supplier.SupplierId

                    };
                    this.PutSupplier(newSupplier);

                    try
                    {
                        if (!SuppliersDB.UpdateSupplier(supplier, newSupplier))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that  supplier ID.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            supplier = newSupplier;
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

        private void PutSupplier(suppliers newSupplier)
        {
            throw new NotImplementedException();
        }

        private void PutSupplier(Suppliers supplier)
        {
            throw new NotImplementedException();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
