namespace ArtData
{
    partial class KontrolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KontrolForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tarananZoomed = new Library.Scan.ZoomablePictureBox();
            this._yevmiyeNo = new System.Windows.Forms.Label();
            this._ciltBilgisi = new System.Windows.Forms.Label();
            this._yevmiyeTarihi = new System.Windows.Forms.Label();
            this._dogru = new System.Windows.Forms.Button();
            this._yanlis = new System.Windows.Forms.Button();
            this._tarananTam = new Library.Scan.ZoomablePictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._backgroundProcess = new System.Windows.Forms.Timer(this.components);
            this._mudurluk = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tarananZoomed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._tarananTam)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tarananZoomed);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._mudurluk);
            this.splitContainer1.Panel2.Controls.Add(this._yevmiyeNo);
            this.splitContainer1.Panel2.Controls.Add(this._ciltBilgisi);
            this.splitContainer1.Panel2.Controls.Add(this._yevmiyeTarihi);
            this.splitContainer1.Panel2.Controls.Add(this._dogru);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this._yanlis);
            this.splitContainer1.Panel2.Controls.Add(this._tarananTam);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 562);
            this.splitContainer1.SplitterDistance = 612;
            this.splitContainer1.TabIndex = 0;
            // 
            // _tarananZoomed
            // 
            this._tarananZoomed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._tarananZoomed.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tarananZoomed.Image = null;
            this._tarananZoomed.Location = new System.Drawing.Point(0, 0);
            this._tarananZoomed.Name = "_tarananZoomed";
            this._tarananZoomed.Size = new System.Drawing.Size(612, 562);
            this._tarananZoomed.TabIndex = 44;
            this._tarananZoomed.TabStop = false;
            // 
            // _yevmiyeNo
            // 
            this._yevmiyeNo.AutoSize = true;
            this._yevmiyeNo.Font = new System.Drawing.Font("Calibri", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._yevmiyeNo.Location = new System.Drawing.Point(94, 62);
            this._yevmiyeNo.Name = "_yevmiyeNo";
            this._yevmiyeNo.Size = new System.Drawing.Size(125, 23);
            this._yevmiyeNo.TabIndex = 42;
            this._yevmiyeNo.Text = "Cilt Bekleniyor";
            // 
            // _ciltBilgisi
            // 
            this._ciltBilgisi.AutoSize = true;
            this._ciltBilgisi.Font = new System.Drawing.Font("Calibri", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._ciltBilgisi.Location = new System.Drawing.Point(12, 116);
            this._ciltBilgisi.Name = "_ciltBilgisi";
            this._ciltBilgisi.Size = new System.Drawing.Size(125, 23);
            this._ciltBilgisi.TabIndex = 38;
            this._ciltBilgisi.Text = "Cilt Bekleniyor";
            // 
            // _yevmiyeTarihi
            // 
            this._yevmiyeTarihi.AutoSize = true;
            this._yevmiyeTarihi.Font = new System.Drawing.Font("Calibri", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._yevmiyeTarihi.Location = new System.Drawing.Point(94, 35);
            this._yevmiyeTarihi.Name = "_yevmiyeTarihi";
            this._yevmiyeTarihi.Size = new System.Drawing.Size(125, 23);
            this._yevmiyeTarihi.TabIndex = 41;
            this._yevmiyeTarihi.Text = "Cilt Bekleniyor";
            // 
            // _dogru
            // 
            this._dogru.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._dogru.ForeColor = System.Drawing.SystemColors.ControlText;
            this._dogru.Location = new System.Drawing.Point(245, 142);
            this._dogru.Name = "_dogru";
            this._dogru.Size = new System.Drawing.Size(142, 47);
            this._dogru.TabIndex = 3;
            this._dogru.Text = "DOĞRU";
            this._dogru.UseVisualStyleBackColor = true;
            this._dogru.Click += new System.EventHandler(this.DogruClick);
            // 
            // _yanlis
            // 
            this._yanlis.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._yanlis.ForeColor = System.Drawing.SystemColors.ControlText;
            this._yanlis.Location = new System.Drawing.Point(98, 142);
            this._yanlis.Name = "_yanlis";
            this._yanlis.Size = new System.Drawing.Size(141, 47);
            this._yanlis.TabIndex = 39;
            this._yanlis.Text = "YANLIŞ";
            this._yanlis.UseVisualStyleBackColor = true;
            this._yanlis.Click += new System.EventHandler(this.YanlisClick);
            // 
            // _tarananTam
            // 
            this._tarananTam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._tarananTam.Image = null;
            this._tarananTam.Location = new System.Drawing.Point(3, 195);
            this._tarananTam.Name = "_tarananTam";
            this._tarananTam.Size = new System.Drawing.Size(384, 500);
            this._tarananTam.TabIndex = 37;
            this._tarananTam.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(13, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 32;
            this.label3.Text = "Yevmiye Tarihi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(27, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 35;
            this.label4.Text = "Yevmiye No";
            // 
            // _timer
            // 
            this._timer.Interval = 3000;
            this._timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // _backgroundProcess
            // 
            this._backgroundProcess.Enabled = true;
            this._backgroundProcess.Interval = 3000;
            this._backgroundProcess.Tick += new System.EventHandler(this.BackgroundProcessTick);
            // 
            // _mudurluk
            // 
            this._mudurluk.AutoSize = true;
            this._mudurluk.Font = new System.Drawing.Font("Calibri", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._mudurluk.Location = new System.Drawing.Point(94, 8);
            this._mudurluk.Name = "_mudurluk";
            this._mudurluk.Size = new System.Drawing.Size(125, 23);
            this._mudurluk.TabIndex = 40;
            this._mudurluk.Text = "Cilt Bekleniyor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(34, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 28;
            this.label1.Text = "Klasör Adı";
            // 
            // KontrolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "KontrolForm";
            this.Text = "Veri Kontrolü";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KontrolFormFormClosed);
            this.Load += new System.EventHandler(this.DataFormLoad);
            this.SizeChanged += new System.EventHandler(this.DataFormSizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._tarananZoomed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._tarananTam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button _dogru;
        private Library.Scan.ZoomablePictureBox _tarananTam;
        private System.Windows.Forms.Label _ciltBilgisi;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Button _yanlis;
        private System.Windows.Forms.Label _yevmiyeNo;
        private System.Windows.Forms.Label _yevmiyeTarihi;
        private Library.Scan.ZoomablePictureBox _tarananZoomed;
        private System.Windows.Forms.Timer _backgroundProcess;
        private System.Windows.Forms.Label _mudurluk;
        private System.Windows.Forms.Label label1;


    }
}