namespace ArtData
{
    partial class DataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._veritabaniBilgileri = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._dosyaAdi = new System.Windows.Forms.TextBox();
            this._oncekiYevmiyeNo = new System.Windows.Forms.Label();
            this._oncekiYevmiyeTarihi = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._ciltBilgisi = new System.Windows.Forms.Label();
            this._yevmiyeTarihi = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._yevmiyeNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._yil = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._mudurluk = new System.Windows.Forms.TextBox();
            this._kaydet = new System.Windows.Forms.Button();
            this._tarananTam = new Library.Scan.ZoomablePictureBox();
            this._tarananZoomed = new Library.Scan.ZoomablePictureBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._backgroundProcess = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tarananTam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._tarananZoomed)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._tarananZoomed);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 562);
            this.splitContainer1.SplitterDistance = 392;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._veritabaniBilgileri);
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            this.splitContainer2.Panel1.Controls.Add(this._dosyaAdi);
            this.splitContainer2.Panel1.Controls.Add(this._oncekiYevmiyeNo);
            this.splitContainer2.Panel1.Controls.Add(this._oncekiYevmiyeTarihi);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this._ciltBilgisi);
            this.splitContainer2.Panel1.Controls.Add(this._yevmiyeTarihi);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this._yevmiyeNo);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this._yil);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this._mudurluk);
            this.splitContainer2.Panel1.Controls.Add(this._kaydet);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._tarananTam);
            this.splitContainer2.Size = new System.Drawing.Size(392, 562);
            this.splitContainer2.SplitterDistance = 269;
            this.splitContainer2.TabIndex = 0;
            // 
            // _veritabaniBilgileri
            // 
            this._veritabaniBilgileri.AutoSize = true;
            this._veritabaniBilgileri.Font = new System.Drawing.Font("Calibri", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._veritabaniBilgileri.ForeColor = System.Drawing.Color.Blue;
            this._veritabaniBilgileri.Location = new System.Drawing.Point(13, 214);
            this._veritabaniBilgileri.Name = "_veritabaniBilgileri";
            this._veritabaniBilgileri.Size = new System.Drawing.Size(103, 15);
            this._veritabaniBilgileri.TabIndex = 56;
            this._veritabaniBilgileri.Text = "Veritabanı Bilgileri";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(31, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 15);
            this.label7.TabIndex = 55;
            this.label7.Text = "Dosya Adı";
            // 
            // _dosyaAdi
            // 
            this._dosyaAdi.Enabled = false;
            this._dosyaAdi.Font = new System.Drawing.Font("Calibri", 12F);
            this._dosyaAdi.Location = new System.Drawing.Point(98, 36);
            this._dosyaAdi.Name = "_dosyaAdi";
            this._dosyaAdi.Size = new System.Drawing.Size(289, 27);
            this._dosyaAdi.TabIndex = 54;
            this._dosyaAdi.TabStop = false;
            // 
            // _oncekiYevmiyeNo
            // 
            this._oncekiYevmiyeNo.AutoSize = true;
            this._oncekiYevmiyeNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._oncekiYevmiyeNo.Location = new System.Drawing.Point(133, 198);
            this._oncekiYevmiyeNo.Name = "_oncekiYevmiyeNo";
            this._oncekiYevmiyeNo.Size = new System.Drawing.Size(31, 15);
            this._oncekiYevmiyeNo.TabIndex = 53;
            this._oncekiYevmiyeNo.Text = "____";
            // 
            // _oncekiYevmiyeTarihi
            // 
            this._oncekiYevmiyeTarihi.AutoSize = true;
            this._oncekiYevmiyeTarihi.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._oncekiYevmiyeTarihi.Location = new System.Drawing.Point(133, 183);
            this._oncekiYevmiyeTarihi.Name = "_oncekiYevmiyeTarihi";
            this._oncekiYevmiyeTarihi.Size = new System.Drawing.Size(61, 15);
            this._oncekiYevmiyeTarihi.TabIndex = 52;
            this._oncekiYevmiyeTarihi.Text = "__.__.____";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(27, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 15);
            this.label6.TabIndex = 51;
            this.label6.Text = "Önceki Yevmiye No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(13, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 15);
            this.label5.TabIndex = 50;
            this.label5.Text = "Önceki Yevmiye Tarihi";
            // 
            // _ciltBilgisi
            // 
            this._ciltBilgisi.AutoSize = true;
            this._ciltBilgisi.Font = new System.Drawing.Font("Calibri", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._ciltBilgisi.Location = new System.Drawing.Point(12, 161);
            this._ciltBilgisi.Name = "_ciltBilgisi";
            this._ciltBilgisi.Size = new System.Drawing.Size(125, 23);
            this._ciltBilgisi.TabIndex = 49;
            this._ciltBilgisi.Text = "Cilt Bekleniyor";
            // 
            // _yevmiyeTarihi
            // 
            this._yevmiyeTarihi.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._yevmiyeTarihi.Location = new System.Drawing.Point(99, 67);
            this._yevmiyeTarihi.Mask = "00/00/0000";
            this._yevmiyeTarihi.Name = "_yevmiyeTarihi";
            this._yevmiyeTarihi.Size = new System.Drawing.Size(288, 27);
            this._yevmiyeTarihi.TabIndex = 39;
            this._yevmiyeTarihi.ValidatingType = typeof(System.DateTime);
            this._yevmiyeTarihi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.YevmiyeTarihiKeyDown);
            this._yevmiyeTarihi.Leave += new System.EventHandler(this.YevmiyeTarihiLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(26, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 47;
            this.label4.Text = "Yevmiye No";
            // 
            // _yevmiyeNo
            // 
            this._yevmiyeNo.Font = new System.Drawing.Font("Calibri", 12F);
            this._yevmiyeNo.Location = new System.Drawing.Point(98, 98);
            this._yevmiyeNo.MaxLength = 6;
            this._yevmiyeNo.Name = "_yevmiyeNo";
            this._yevmiyeNo.Size = new System.Drawing.Size(289, 27);
            this._yevmiyeNo.TabIndex = 40;
            this._yevmiyeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.YevmiyeNoKeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(13, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 46;
            this.label3.Text = "Yevmiye Tarihi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(53, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 45;
            this.label2.Text = "Cilt Yılı";
            // 
            // _yil
            // 
            this._yil.Font = new System.Drawing.Font("Calibri", 12F);
            this._yil.Location = new System.Drawing.Point(99, 128);
            this._yil.Name = "_yil";
            this._yil.Size = new System.Drawing.Size(288, 27);
            this._yil.TabIndex = 41;
            this._yil.KeyDown += new System.Windows.Forms.KeyEventHandler(this.YilKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(34, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 44;
            this.label1.Text = "Müdürlük";
            // 
            // _mudurluk
            // 
            this._mudurluk.Enabled = false;
            this._mudurluk.Font = new System.Drawing.Font("Calibri", 12F);
            this._mudurluk.Location = new System.Drawing.Point(98, 6);
            this._mudurluk.Name = "_mudurluk";
            this._mudurluk.Size = new System.Drawing.Size(289, 27);
            this._mudurluk.TabIndex = 43;
            this._mudurluk.TabStop = false;
            // 
            // _kaydet
            // 
            this._kaydet.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._kaydet.ForeColor = System.Drawing.SystemColors.ControlText;
            this._kaydet.Location = new System.Drawing.Point(259, 164);
            this._kaydet.Name = "_kaydet";
            this._kaydet.Size = new System.Drawing.Size(128, 47);
            this._kaydet.TabIndex = 42;
            this._kaydet.Text = "KAYDET";
            this._kaydet.UseVisualStyleBackColor = true;
            this._kaydet.Click += new System.EventHandler(this.KaydetClick);
            // 
            // _tarananTam
            // 
            this._tarananTam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._tarananTam.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tarananTam.Image = null;
            this._tarananTam.Location = new System.Drawing.Point(0, 0);
            this._tarananTam.Name = "_tarananTam";
            this._tarananTam.Size = new System.Drawing.Size(392, 289);
            this._tarananTam.TabIndex = 49;
            this._tarananTam.TabStop = false;
            // 
            // _tarananZoomed
            // 
            this._tarananZoomed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._tarananZoomed.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tarananZoomed.Image = null;
            this._tarananZoomed.Location = new System.Drawing.Point(0, 0);
            this._tarananZoomed.Name = "_tarananZoomed";
            this._tarananZoomed.Size = new System.Drawing.Size(612, 562);
            this._tarananZoomed.TabIndex = 0;
            this._tarananZoomed.TabStop = false;
            // 
            // _timer
            // 
            this._timer.Interval = 3000;
            this._timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // _backgroundProcess
            // 
            this._backgroundProcess.Enabled = true;
            this._backgroundProcess.Interval = 1000;
            this._backgroundProcess.Tick += new System.EventHandler(this.BackgroundProcessTick);
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DataForm";
            this.Text = "Veri Girişi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataFormFormClosed);
            this.Load += new System.EventHandler(this.DataFormLoad);
            this.SizeChanged += new System.EventHandler(this.DataFormSizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._tarananTam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._tarananZoomed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Library.Scan.ZoomablePictureBox _tarananZoomed;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label _ciltBilgisi;
        private System.Windows.Forms.MaskedTextBox _yevmiyeTarihi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _yevmiyeNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _yil;
        private System.Windows.Forms.Button _kaydet;
        private Library.Scan.ZoomablePictureBox _tarananTam;
        private System.Windows.Forms.Label _oncekiYevmiyeNo;
        private System.Windows.Forms.Label _oncekiYevmiyeTarihi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _dosyaAdi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _mudurluk;
        private System.Windows.Forms.Timer _backgroundProcess;
        private System.Windows.Forms.Label _veritabaniBilgileri;


    }
}