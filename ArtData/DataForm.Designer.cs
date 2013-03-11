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
            this._taranan = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this._kaydet = new System.Windows.Forms.Button();
            this._aboneNo = new System.Windows.Forms.TextBox();
            this._tarayanBilgileri = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._kapat = new System.Windows.Forms.Button();
            this._ciltBilgisi = new System.Windows.Forms.Label();
            this.PrevImage = new System.Windows.Forms.Button();
            this._oncekiGetir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._90Dondur = new System.Windows.Forms.Button();
            this._solaHassasDondur = new System.Windows.Forms.Button();
            this._sagaHassasDondur = new System.Windows.Forms.Button();
            this._aci = new System.Windows.Forms.NumericUpDown();
            this._aciliDondur = new System.Windows.Forms.Button();
            this._otoEgimGider = new System.Windows.Forms.Button();
            this._270SolaDondur = new System.Windows.Forms.Button();
            this._sil = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._evrakTuru = new System.Windows.Forms.ComboBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._backgroundProcess = new System.Windows.Forms.Timer(this.components);
            this._kontrolSeviyesi = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._taranan)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._aci)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this._taranan);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._kontrolSeviyesi);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this._kaydet);
            this.splitContainer1.Panel2.Controls.Add(this._aboneNo);
            this.splitContainer1.Panel2.Controls.Add(this._tarayanBilgileri);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this._kapat);
            this.splitContainer1.Panel2.Controls.Add(this._ciltBilgisi);
            this.splitContainer1.Panel2.Controls.Add(this.PrevImage);
            this.splitContainer1.Panel2.Controls.Add(this._oncekiGetir);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this._sil);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this._evrakTuru);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 562);
            this.splitContainer1.SplitterDistance = 800;
            this.splitContainer1.TabIndex = 1000;
            this.splitContainer1.TabStop = false;
            // 
            // _taranan
            // 
            this._taranan.BackColor = System.Drawing.Color.WhiteSmoke;
            this._taranan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._taranan.Dock = System.Windows.Forms.DockStyle.Fill;
            this._taranan.Location = new System.Drawing.Point(0, 0);
            this._taranan.Name = "_taranan";
            this._taranan.Size = new System.Drawing.Size(800, 562);
            this._taranan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._taranan.TabIndex = 0;
            this._taranan.TabStop = false;
            this._taranan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TarananMouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(42, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 1102;
            this.label3.Text = "Seviye";
            // 
            // _kaydet
            // 
            this._kaydet.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._kaydet.ForeColor = System.Drawing.SystemColors.ControlText;
            this._kaydet.Location = new System.Drawing.Point(358, 113);
            this._kaydet.Name = "_kaydet";
            this._kaydet.Size = new System.Drawing.Size(107, 47);
            this._kaydet.TabIndex = 1062;
            this._kaydet.Text = "KAYDET";
            this._kaydet.UseVisualStyleBackColor = true;
            this._kaydet.Click += new System.EventHandler(this.KaydetClick);
            // 
            // _aboneNo
            // 
            this._aboneNo.Font = new System.Drawing.Font("Calibri", 12F);
            this._aboneNo.Location = new System.Drawing.Point(88, 46);
            this._aboneNo.Name = "_aboneNo";
            this._aboneNo.Size = new System.Drawing.Size(377, 27);
            this._aboneNo.TabIndex = 1;
            this._aboneNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // _tarayanBilgileri
            // 
            this._tarayanBilgileri.AutoSize = true;
            this._tarayanBilgileri.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._tarayanBilgileri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this._tarayanBilgileri.Location = new System.Drawing.Point(16, 333);
            this._tarayanBilgileri.Name = "_tarayanBilgileri";
            this._tarayanBilgileri.Size = new System.Drawing.Size(106, 18);
            this._tarayanBilgileri.TabIndex = 1100;
            this._tarayanBilgileri.Text = "Tarayan Bilgileri";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(22, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 1064;
            this.label1.Text = "Abone No";
            // 
            // _kapat
            // 
            this._kapat.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._kapat.ForeColor = System.Drawing.SystemColors.ControlText;
            this._kapat.Location = new System.Drawing.Point(435, 12);
            this._kapat.Name = "_kapat";
            this._kapat.Size = new System.Drawing.Size(30, 28);
            this._kapat.TabIndex = 1099;
            this._kapat.Text = "x";
            this._kapat.UseVisualStyleBackColor = true;
            this._kapat.Click += new System.EventHandler(this.KapatClick);
            // 
            // _ciltBilgisi
            // 
            this._ciltBilgisi.Font = new System.Drawing.Font("Calibri", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._ciltBilgisi.Location = new System.Drawing.Point(16, 351);
            this._ciltBilgisi.Name = "_ciltBilgisi";
            this._ciltBilgisi.Size = new System.Drawing.Size(443, 70);
            this._ciltBilgisi.TabIndex = 1068;
            this._ciltBilgisi.Text = "Cilt Bekleniyor";
            // 
            // PrevImage
            // 
            this.PrevImage.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this.PrevImage.Location = new System.Drawing.Point(13, 113);
            this.PrevImage.Name = "PrevImage";
            this.PrevImage.Size = new System.Drawing.Size(69, 47);
            this.PrevImage.TabIndex = 1098;
            this.PrevImage.Text = "<-";
            this.PrevImage.UseVisualStyleBackColor = true;
            this.PrevImage.Click += new System.EventHandler(this.PrevImageClick);
            // 
            // _oncekiGetir
            // 
            this._oncekiGetir.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._oncekiGetir.ForeColor = System.Drawing.SystemColors.ControlText;
            this._oncekiGetir.Location = new System.Drawing.Point(189, 113);
            this._oncekiGetir.Name = "_oncekiGetir";
            this._oncekiGetir.Size = new System.Drawing.Size(163, 47);
            this._oncekiGetir.TabIndex = 1086;
            this._oncekiGetir.Text = "Önceki Bilgileri Getir";
            this._oncekiGetir.UseVisualStyleBackColor = true;
            this._oncekiGetir.Click += new System.EventHandler(this.OncekiGetirClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._90Dondur);
            this.groupBox1.Controls.Add(this._solaHassasDondur);
            this.groupBox1.Controls.Add(this._sagaHassasDondur);
            this.groupBox1.Controls.Add(this._aci);
            this.groupBox1.Controls.Add(this._aciliDondur);
            this.groupBox1.Controls.Add(this._otoEgimGider);
            this.groupBox1.Controls.Add(this._270SolaDondur);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(13, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 164);
            this.groupBox1.TabIndex = 1097;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resim Düzenleme";
            // 
            // _90Dondur
            // 
            this._90Dondur.Location = new System.Drawing.Point(6, 21);
            this._90Dondur.Name = "_90Dondur";
            this._90Dondur.Size = new System.Drawing.Size(219, 66);
            this._90Dondur.TabIndex = 1105;
            this._90Dondur.Text = "90+ Döndür";
            this._90Dondur.UseVisualStyleBackColor = true;
            this._90Dondur.Click += new System.EventHandler(this._90Dondur_Click);
            // 
            // _solaHassasDondur
            // 
            this._solaHassasDondur.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this._solaHassasDondur.Location = new System.Drawing.Point(342, 20);
            this._solaHassasDondur.Name = "_solaHassasDondur";
            this._solaHassasDondur.Size = new System.Drawing.Size(106, 66);
            this._solaHassasDondur.TabIndex = 1104;
            this._solaHassasDondur.Text = "Sola Hassas Döndür";
            this._solaHassasDondur.UseVisualStyleBackColor = true;
            this._solaHassasDondur.Click += new System.EventHandler(this.SolaHassasDondurClick);
            // 
            // _sagaHassasDondur
            // 
            this._sagaHassasDondur.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this._sagaHassasDondur.Location = new System.Drawing.Point(343, 92);
            this._sagaHassasDondur.Name = "_sagaHassasDondur";
            this._sagaHassasDondur.Size = new System.Drawing.Size(105, 66);
            this._sagaHassasDondur.TabIndex = 1103;
            this._sagaHassasDondur.Text = "Sağa Hassas Döndür";
            this._sagaHassasDondur.UseVisualStyleBackColor = true;
            this._sagaHassasDondur.Click += new System.EventHandler(this.SagaHassasDondurClick);
            // 
            // _aci
            // 
            this._aci.Font = new System.Drawing.Font("Calibri", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._aci.Location = new System.Drawing.Point(117, 92);
            this._aci.Maximum = new decimal(new int[] {
            179,
            0,
            0,
            0});
            this._aci.Minimum = new decimal(new int[] {
            179,
            0,
            0,
            -2147483648});
            this._aci.Name = "_aci";
            this._aci.Size = new System.Drawing.Size(105, 66);
            this._aci.TabIndex = 1102;
            // 
            // _aciliDondur
            // 
            this._aciliDondur.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this._aciliDondur.Location = new System.Drawing.Point(232, 92);
            this._aciliDondur.Name = "_aciliDondur";
            this._aciliDondur.Size = new System.Drawing.Size(105, 66);
            this._aciliDondur.TabIndex = 1101;
            this._aciliDondur.Text = "Açılı Döndür";
            this._aciliDondur.UseVisualStyleBackColor = true;
            this._aciliDondur.Click += new System.EventHandler(this.AciliDondurClick);
            // 
            // _otoEgimGider
            // 
            this._otoEgimGider.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this._otoEgimGider.Location = new System.Drawing.Point(6, 93);
            this._otoEgimGider.Name = "_otoEgimGider";
            this._otoEgimGider.Size = new System.Drawing.Size(105, 66);
            this._otoEgimGider.TabIndex = 1100;
            this._otoEgimGider.Text = "Otomatik Eğim Gider";
            this._otoEgimGider.UseVisualStyleBackColor = true;
            this._otoEgimGider.Click += new System.EventHandler(this.OtoEgimGiderClick);
            // 
            // _270SolaDondur
            // 
            this._270SolaDondur.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this._270SolaDondur.Location = new System.Drawing.Point(231, 21);
            this._270SolaDondur.Name = "_270SolaDondur";
            this._270SolaDondur.Size = new System.Drawing.Size(106, 66);
            this._270SolaDondur.TabIndex = 1099;
            this._270SolaDondur.Text = "270 Derece Döndür";
            this._270SolaDondur.UseVisualStyleBackColor = true;
            this._270SolaDondur.Click += new System.EventHandler(this._270SolaDondur_Click);
            // 
            // _sil
            // 
            this._sil.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this._sil.Location = new System.Drawing.Point(88, 113);
            this._sil.Name = "_sil";
            this._sil.Size = new System.Drawing.Size(95, 47);
            this._sil.TabIndex = 1088;
            this._sil.Text = "SİL";
            this._sil.UseVisualStyleBackColor = true;
            this._sil.Click += new System.EventHandler(this.SilClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(20, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 1090;
            this.label2.Text = "Evrak Türü";
            // 
            // _evrakTuru
            // 
            this._evrakTuru.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._evrakTuru.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._evrakTuru.FormattingEnabled = true;
            this._evrakTuru.Items.AddRange(new object[] {
            "BAĞLANTI ANLAŞMASI",
            "FATURA",
            "KİMLİK",
            "TAPU",
            "DİLEKÇE",
            "ABONE KAYIT BİLGİ VE ONAY FORMU"});
            this._evrakTuru.Location = new System.Drawing.Point(88, 80);
            this._evrakTuru.Name = "_evrakTuru";
            this._evrakTuru.Size = new System.Drawing.Size(377, 27);
            this._evrakTuru.TabIndex = 2;
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
            // _kontrolSeviyesi
            // 
            this._kontrolSeviyesi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._kontrolSeviyesi.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._kontrolSeviyesi.FormattingEnabled = true;
            this._kontrolSeviyesi.Items.AddRange(new object[] {
            "KONTROL-1-Sadece Kırpma-[BEK]",
            "KONTROL-2-Belge Türü Seç-[BE2]",
            "KONTROL-3-Abone No Gir-[BE3]"});
            this._kontrolSeviyesi.Location = new System.Drawing.Point(88, 12);
            this._kontrolSeviyesi.Name = "_kontrolSeviyesi";
            this._kontrolSeviyesi.Size = new System.Drawing.Size(341, 27);
            this._kontrolSeviyesi.TabIndex = 1101;
            this._kontrolSeviyesi.SelectedIndexChanged += new System.EventHandler(this.KontrolSeviyesiSelectedIndexChanged);
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "DataForm";
            this.Text = "Veri Girişi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataFormFormClosed);
            this.Load += new System.EventHandler(this.DataFormLoad);
            this.SizeChanged += new System.EventHandler(this.DataFormSizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataFormKeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._taranan)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._aci)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        //private Library.Scan.ZoomablePictureBox _taranan;
        private System.Windows.Forms.PictureBox _taranan;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Timer _backgroundProcess;
        private System.Windows.Forms.Label _ciltBilgisi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _aboneNo;
        private System.Windows.Forms.Button _kaydet;
        private System.Windows.Forms.Button _oncekiGetir;
        private System.Windows.Forms.Button _sil;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _evrakTuru;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown _aci;
        private System.Windows.Forms.Button _aciliDondur;
        private System.Windows.Forms.Button _otoEgimGider;
        private System.Windows.Forms.Button _270SolaDondur;
        private System.Windows.Forms.Button _solaHassasDondur;
        private System.Windows.Forms.Button _sagaHassasDondur;
        private System.Windows.Forms.Button PrevImage;
        private System.Windows.Forms.Button _kapat;
        private System.Windows.Forms.Label _tarayanBilgileri;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button _90Dondur;
        private System.Windows.Forms.ComboBox _kontrolSeviyesi;        


    }
}