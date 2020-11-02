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
    public partial class frmProductSup : Form
    {
        public frmProductSup()
        {
            InitializeComponent();
        }

        private void frmProductSup_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Products_suppliers_packagesDB.GetProducts_suppliers_packages();
            dataGridView1.Rows[0].Selected = true;
        }

        private void btnAddProductSupplier_Click(object sender, EventArgs e)
        {
            addModifySup addProductSupplierForm = new addModifySup();
            addProductSupplierForm.addProducts_suppliers_packages = true;
            DialogResult result = addProductSupplierForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataGridView1.DataSource = Products_suppliers_packagesDB.GetProducts_suppliers_packages();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            var cells = dataGridView1.CurrentRow.Cells;
            string rowId = cells[0].Value.ToString();
            string rowId1 = cells[1].Value.ToString();
            //int rowId = Convert.ToInt32(cells[0].Value);
            var selectedSupplierID = Products_suppliers_packagesDB.GetProducts_suppliers_package(rowId, rowId1);

            addModifySup modifyproductsPackagesFrm = new addModifySup();
            modifyproductsPackagesFrm.addProducts_suppliers_packages = false;
            modifyproductsPackagesFrm.modifyproducts_suppliers_packages = selectedSupplierID;
            DialogResult result = modifyproductsPackagesFrm.ShowDialog();
            if (result == DialogResult.OK)
            {

                dataGridView1.DataSource = Products_suppliers_packagesDB.GetProducts_suppliers_packages();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
