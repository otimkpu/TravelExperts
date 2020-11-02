namespace TravelExperts
{
    partial class frmProductSup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAddProductSupplier = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(62, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(298, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnAddProductSupplier
            // 
            this.btnAddProductSupplier.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnAddProductSupplier.Location = new System.Drawing.Point(62, 189);
            this.btnAddProductSupplier.Name = "btnAddProductSupplier";
            this.btnAddProductSupplier.Size = new System.Drawing.Size(98, 48);
            this.btnAddProductSupplier.TabIndex = 1;
            this.btnAddProductSupplier.Text = "Add";
            this.btnAddProductSupplier.UseVisualStyleBackColor = false;
            this.btnAddProductSupplier.Click += new System.EventHandler(this.btnAddProductSupplier_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnModify.Location = new System.Drawing.Point(161, 189);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(98, 48);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnExit.Location = new System.Drawing.Point(262, 189);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(98, 48);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmProductSup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkTurquoise;
            this.ClientSize = new System.Drawing.Size(429, 258);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAddProductSupplier);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmProductSup";
            this.Text = "Product Supplier Packages";
            this.Load += new System.EventHandler(this.frmProductSup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAddProductSupplier;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnExit;
    }
}