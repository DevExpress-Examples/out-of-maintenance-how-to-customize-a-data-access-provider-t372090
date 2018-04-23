namespace QB
{
    partial class QBForm
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
            this.buttonsRunQb = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // buttonsRunQb
            // 
            this.buttonsRunQb.Location = new System.Drawing.Point(12, 12);
            this.buttonsRunQb.Name = "buttonsRunQb";
            this.buttonsRunQb.Size = new System.Drawing.Size(166, 34);
            this.buttonsRunQb.TabIndex = 0;
            this.buttonsRunQb.Text = "Run QB";
            this.buttonsRunQb.Click += new System.EventHandler(this.buttonsRunQb_Click);
            // 
            // QBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 58);
            this.Controls.Add(this.buttonsRunQb);
            this.Name = "QBForm";
            this.Text = "QbForm";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton buttonsRunQb;
    }
}

