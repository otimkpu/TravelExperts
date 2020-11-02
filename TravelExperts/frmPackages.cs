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
    public partial class frmPackages : Form
    {
        public frmPackages()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Packages";
            dataGridView1.DataSource = PackagesDB.GetPackages();
            dataGridView1.Rows[0].Selected = true;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            addModifyForm addCustomerForm = new addModifyForm();
            addCustomerForm.addPackage = true;
            DialogResult result = addCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataGridView1.DataSource = PackagesDB.GetPackages();
            }

        }
       
        private void btnModify_Click(object sender, EventArgs e)
        {
            var cells = dataGridView1.CurrentRow.Cells;
            string rowId = cells[0].Value.ToString();
            var selectedPackage = PackagesDB.GetPackage(rowId);
            
            addModifyForm modifyPackageForm = new addModifyForm();
            modifyPackageForm.addPackage = false;
            modifyPackageForm.modifyPack = selectedPackage;
            DialogResult result = modifyPackageForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataGridView1.DataSource = PackagesDB.GetPackages();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();

        }
        //Supplier table
        private void btnSupplier_Click(object sender, EventArgs e)
        {
            FrmSupplier f5 = new FrmSupplier();
                f5.Show();
        }
        //product table
        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProduct f2 = new frmProduct();
            f2.Show();
        }
        //supplier-product-packages table
        private void btnSup_Click(object sender, EventArgs e)
        {
            frmProductSup f1 = new frmProductSup();
            f1.Show();
        }
    }
}
