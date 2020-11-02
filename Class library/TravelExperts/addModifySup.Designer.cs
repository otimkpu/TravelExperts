namespace TravelExperts
{
    partial class addModifySup
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
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pkgIDtextBox3 = new System.Windows.Forms.TextBox();
            this.prdSuppIDtextBox4 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnAccept.Location = new System.Drawing.Point(62, 207);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 38);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnCancel.Location = new System.Drawing.Point(251, 206);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 39);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "PackageID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Product SupplierID";
            // 
            // pkgIDtextBox3
            // 
            this.pkgIDtextBox3.Location = new System.Drawing.Point(160, 58);
            this.pkgIDtextBox3.Name = "pkgIDtextBox3";
            this.pkgIDtextBox3.Size = new System.Drawing.Size(100, 20);
            this.pkgIDtextBox3.TabIndex = 4;
            // 
            // prdSuppIDtextBox4
            // 
            this.prdSuppIDtextBox4.Location = new System.Drawing.Point(160, 125);
            this.prdSuppIDtextBox4.Name = "prdSuppIDtextBox4";
            this.prdSuppIDtextBox4.Size = new System.Drawing.Size(100, 20);
            this.prdSuppIDtextBox4.TabIndex = 5;
            // 
            // addModifySup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkTurquoise;
            this.ClientSize = new System.Drawing.Size(414, 295);
            this.Controls.Add(this.prdSuppIDtextBox4);
            this.Controls.Add(this.pkgIDtextBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Name = "addModifySup";
            this.Text = "addModifySup";
            this.Load += new System.EventHandler(this.addModifySup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pkgIDtextBox3;
        private System.Windows.Forms.TextBox prdSuppIDtextBox4;
    }
}