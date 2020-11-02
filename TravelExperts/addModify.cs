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
    public partial class addModifyForm : Form
    {
        public addModifyForm()
        {
            InitializeComponent();
        }

        public Packages pack;
        public bool addPackage;
        public Packages modifyPack;

      

        private void addModify_Load(object sender, EventArgs e)
        {
            if (addPackage)
            {
                this.Text = "Add Packages";
                labelId.Visible = false;
                txtPID.Visible = false;
            }
            else
            {
                this.Text = "Modify Packages";
                txtPID.Text = modifyPack.PackageId.ToString();
                txtPName.Text = modifyPack.PkgName;
                dTStart.Value = modifyPack.PkgStartDate;
                dTEnd.Value = modifyPack.PkgEndDate;
                txtPDesc.Text = modifyPack.PkgDesc;
                txtPBP.Text = modifyPack.PkgBasePrice.ToString();
                txtPAC.Text = modifyPack.PkgAgencyCommission.ToString();
            }

        }

        private void putPackageData(Packages pack)
        {
            pack.PkgName = txtPName.Text;
            pack.PkgStartDate = dTStart.Value;
            pack.PkgEndDate = dTEnd.Value;
            pack.PkgDesc = txtPDesc.Text;
            pack.PkgBasePrice = Convert.ToDecimal(txtPBP.Text);
            pack.PkgAgencyCommission = Convert.ToDecimal(txtPAC.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void btnAccept_Click_1(object sender, EventArgs e)
        {
            if (addPackage)
            {
                pack = new Packages();
                putPackageData(pack);
                int Id = PackagesDB.AddPackage(pack);
                MessageBox.Show("New Package with Id of" + Id + " was added");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Packages newPackage = new Packages();
                newPackage.PackageId = modifyPack.PackageId;
                this.putPackageData(newPackage);
                try
                {
                    if (!PackagesDB.UpdatePackage(modifyPack, newPackage))
                    {
                        MessageBox.Show("Another user has updated or " +
                            "deleted that customer.", "Database Error");
                        this.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        modifyPack = newPackage;
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
}
