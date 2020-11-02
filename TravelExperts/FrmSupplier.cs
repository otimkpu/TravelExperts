using Class_library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelExperts
{
    public partial class FrmSupplier : Form
    {

        public FrmSupplier()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.Close();


        }


        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = SuppliersDB.GetSuppliers();
            dataGridView1.Rows[0].Selected = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmModifySup addSupplierForm = new frmModifySup();
            addSupplierForm.addSuppliers = true;
            DialogResult result = addSupplierForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataGridView1.DataSource = SuppliersDB.GetSuppliers();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var cells = dataGridView1.CurrentRow.Cells;
            string rowId = cells[0].Value.ToString();
            string rowId1 = cells[1].Value.ToString();
            //int rowId = Convert.ToInt32(cells[0].Value);
            var selectedSupplierID = SuppliersDB.GetSuppliers(rowId);

            frmModifySup modifySupplierFrm = new frmModifySup();
            modifySupplierFrm.addSuppliers = false;
            modifySupplierFrm.modifySuppliers = selectedSupplierID;
            DialogResult result = modifySupplierFrm.ShowDialog();
            if (result == DialogResult.OK)
            {

                dataGridView1.DataSource = SuppliersDB.GetSuppliers();
            }

        }
    }
}


