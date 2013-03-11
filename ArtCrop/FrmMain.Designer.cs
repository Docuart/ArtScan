namespace ArtCrop
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._durum = new System.Windows.Forms.ToolStripLabel();
            this._kirp = new System.Windows.Forms.ToolStripButton();
            this._kirpmaTuru = new System.Windows.Forms.ToolStripComboBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.rgbPoint = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._durum,
            this._kirp,
            this._kirpmaTuru,
            this.rgbPoint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(680, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _durum
            // 
            this._durum.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._durum.Name = "_durum";
            this._durum.Size = new System.Drawing.Size(44, 22);
            this._durum.Text = "Durum";
            // 
            // _kirp
            // 
            this._kirp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._kirp.Image = ((System.Drawing.Image)(resources.GetObject("_kirp.Image")));
            this._kirp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._kirp.Name = "_kirp";
            this._kirp.Size = new System.Drawing.Size(32, 22);
            this._kirp.Text = "Kırp";
            this._kirp.Click += new System.EventHandler(this.KirpClick);
            // 
            // _kirpmaTuru
            // 
            this._kirpmaTuru.Items.AddRange(new object[] {
            "IKI-Her İki Sayfayı Kırp",
            "SOL-Sadece Sol Sayfayı Kırp",
            "SAG-Sadece Sağ Sayfayı Kırp"});
            this._kirpmaTuru.Name = "_kirpmaTuru";
            this._kirpmaTuru.Size = new System.Drawing.Size(121, 25);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Sag.png");
            this.imageList.Images.SetKeyName(1, "Sol.png");
            // 
            // rgbPoint
            // 
            this.rgbPoint.Name = "rgbPoint";
            this.rgbPoint.Size = new System.Drawing.Size(100, 25);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 423);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "ArtCrop";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel _durum;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton _kirp;
        private System.Windows.Forms.ToolStripComboBox _kirpmaTuru;
        private System.Windows.Forms.ToolStripTextBox rgbPoint;
    }
}