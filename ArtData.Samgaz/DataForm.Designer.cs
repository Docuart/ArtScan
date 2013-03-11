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
            this._ilgiliBirimNo = new System.Windows.Forms.TextBox();
            this._aboneNo = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._sil = new System.Windows.Forms.Button();
            this._veritabanindanGetir = new System.Windows.Forms.Button();
            this._oncekiGetir = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this._havaleKisileri = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this._standartDosyaKodu = new System.Windows.Forms.TextBox();
            this._gelenEvrakTarihi = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this._gelenEvrakNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._konusu = new System.Windows.Forms.TextBox();
            this._ciltBilgisi = new System.Windows.Forms.Label();
            this._evrakKayitTarihi = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._evrakKayitNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._adiSoyadi = new System.Windows.Forms.TextBox();
            this._kaydet = new System.Windows.Forms.Button();
            this._taranan = new System.Windows.Forms.PictureBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._backgroundProcess = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._taranan)).BeginInit();
            this.SuspendLayout();
            // 
            // _ilgiliBirimNo
            // 
            this._ilgiliBirimNo.Font = new System.Drawing.Font("Calibri", 12F);
            this._ilgiliBirimNo.Location = new System.Drawing.Point(168, 177);
            this._ilgiliBirimNo.Name = "_ilgiliBirimNo";
            this._ilgiliBirimNo.Size = new System.Drawing.Size(287, 27);
            this._ilgiliBirimNo.TabIndex = 6;
            this._ilgiliBirimNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // _aboneNo
            // 
            this._aboneNo.Font = new System.Drawing.Font("Calibri", 12F);
            this._aboneNo.Location = new System.Drawing.Point(168, 144);
            this._aboneNo.Name = "_aboneNo";
            this._aboneNo.Size = new System.Drawing.Size(287, 27);
            this._aboneNo.TabIndex = 5;
            this._aboneNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
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
            this.splitContainer1.Panel1.Controls.Add(this._sil);
            this.splitContainer1.Panel1.Controls.Add(this._veritabanindanGetir);
            this.splitContainer1.Panel1.Controls.Add(this._oncekiGetir);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this._havaleKisileri);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this._standartDosyaKodu);
            this.splitContainer1.Panel1.Controls.Add(this._gelenEvrakTarihi);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this._gelenEvrakNo);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this._ilgiliBirimNo);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this._konusu);
            this.splitContainer1.Panel1.Controls.Add(this._ciltBilgisi);
            this.splitContainer1.Panel1.Controls.Add(this._evrakKayitTarihi);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this._evrakKayitNo);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this._aboneNo);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this._adiSoyadi);
            this.splitContainer1.Panel1.Controls.Add(this._kaydet);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._taranan);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 562);
            this.splitContainer1.SplitterDistance = 458;
            this.splitContainer1.TabIndex = 1000;
            this.splitContainer1.TabStop = false;
            // 
            // _sil
            // 
            this._sil.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this._sil.Location = new System.Drawing.Point(129, 385);
            this._sil.Name = "_sil";
            this._sil.Size = new System.Drawing.Size(75, 47);
            this._sil.TabIndex = 1088;
            this._sil.Text = "SİL";
            this._sil.UseVisualStyleBackColor = true;
            this._sil.Click += new System.EventHandler(this.SilClick);
            // 
            // _veritabanindanGetir
            // 
            this._veritabanindanGetir.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._veritabanindanGetir.ForeColor = System.Drawing.SystemColors.ControlText;
            this._veritabanindanGetir.Location = new System.Drawing.Point(3, 385);
            this._veritabanindanGetir.Name = "_veritabanindanGetir";
            this._veritabanindanGetir.Size = new System.Drawing.Size(120, 47);
            this._veritabanindanGetir.TabIndex = 1087;
            this._veritabanindanGetir.Text = "Veritabanından Getir";
            this._veritabanindanGetir.UseVisualStyleBackColor = true;
            this._veritabanindanGetir.Click += new System.EventHandler(this.VeritabanindanGetirClick);
            // 
            // _oncekiGetir
            // 
            this._oncekiGetir.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._oncekiGetir.ForeColor = System.Drawing.SystemColors.ControlText;
            this._oncekiGetir.Location = new System.Drawing.Point(210, 385);
            this._oncekiGetir.Name = "_oncekiGetir";
            this._oncekiGetir.Size = new System.Drawing.Size(133, 47);
            this._oncekiGetir.TabIndex = 1086;
            this._oncekiGetir.Text = "Önceki Bilgileri Getir";
            this._oncekiGetir.UseVisualStyleBackColor = true;
            this._oncekiGetir.Click += new System.EventHandler(this.OncekiGetirClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.Location = new System.Drawing.Point(79, 315);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 15);
            this.label12.TabIndex = 1085;
            this.label12.Text = "Havale Kişileri";
            // 
            // _havaleKisileri
            // 
            this._havaleKisileri.Font = new System.Drawing.Font("Calibri", 12F);
            this._havaleKisileri.Location = new System.Drawing.Point(167, 309);
            this._havaleKisileri.MaxLength = 5000;
            this._havaleKisileri.Multiline = true;
            this._havaleKisileri.Name = "_havaleKisileri";
            this._havaleKisileri.Size = new System.Drawing.Size(288, 70);
            this._havaleKisileri.TabIndex = 10;
            this._havaleKisileri.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(41, 282);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 15);
            this.label11.TabIndex = 1083;
            this.label11.Text = "Standart Dosya Kodu";
            // 
            // _standartDosyaKodu
            // 
            this._standartDosyaKodu.Font = new System.Drawing.Font("Calibri", 12F);
            this._standartDosyaKodu.Location = new System.Drawing.Point(167, 276);
            this._standartDosyaKodu.MaxLength = 6;
            this._standartDosyaKodu.Name = "_standartDosyaKodu";
            this._standartDosyaKodu.Size = new System.Drawing.Size(289, 27);
            this._standartDosyaKodu.TabIndex = 9;
            this._standartDosyaKodu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // _gelenEvrakTarihi
            // 
            this._gelenEvrakTarihi.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._gelenEvrakTarihi.Location = new System.Drawing.Point(168, 210);
            this._gelenEvrakTarihi.Mask = "00/00/0000";
            this._gelenEvrakTarihi.Name = "_gelenEvrakTarihi";
            this._gelenEvrakTarihi.Size = new System.Drawing.Size(287, 27);
            this._gelenEvrakTarihi.TabIndex = 7;
            this._gelenEvrakTarihi.ValidatingType = typeof(System.DateTime);
            this._gelenEvrakTarihi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this._gelenEvrakTarihi.Leave += new System.EventHandler(this.TarihLeave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(73, 249);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 15);
            this.label9.TabIndex = 1081;
            this.label9.Text = "Gelen Evrak No";
            // 
            // _gelenEvrakNo
            // 
            this._gelenEvrakNo.Font = new System.Drawing.Font("Calibri", 12F);
            this._gelenEvrakNo.Location = new System.Drawing.Point(167, 243);
            this._gelenEvrakNo.MaxLength = 6;
            this._gelenEvrakNo.Name = "_gelenEvrakNo";
            this._gelenEvrakNo.Size = new System.Drawing.Size(288, 27);
            this._gelenEvrakNo.TabIndex = 8;
            this._gelenEvrakNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(59, 216);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 15);
            this.label10.TabIndex = 1080;
            this.label10.Text = "Gelen Evrak Tarihi";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(83, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 15);
            this.label8.TabIndex = 1077;
            this.label8.Text = "İlgili Birim No";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(113, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 15);
            this.label7.TabIndex = 1074;
            this.label7.Text = "Konusu";
            // 
            // _konusu
            // 
            this._konusu.Font = new System.Drawing.Font("Calibri", 12F);
            this._konusu.Location = new System.Drawing.Point(168, 45);
            this._konusu.Name = "_konusu";
            this._konusu.Size = new System.Drawing.Size(288, 27);
            this._konusu.TabIndex = 2;
            this._konusu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // _ciltBilgisi
            // 
            this._ciltBilgisi.Font = new System.Drawing.Font("Calibri", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._ciltBilgisi.Location = new System.Drawing.Point(12, 438);
            this._ciltBilgisi.Name = "_ciltBilgisi";
            this._ciltBilgisi.Size = new System.Drawing.Size(443, 70);
            this._ciltBilgisi.TabIndex = 1068;
            this._ciltBilgisi.Text = "Cilt Bekleniyor";
            // 
            // _evrakKayitTarihi
            // 
            this._evrakKayitTarihi.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._evrakKayitTarihi.Location = new System.Drawing.Point(169, 78);
            this._evrakKayitTarihi.Mask = "00/00/0000";
            this._evrakKayitTarihi.Name = "_evrakKayitTarihi";
            this._evrakKayitTarihi.Size = new System.Drawing.Size(287, 27);
            this._evrakKayitTarihi.TabIndex = 3;
            this._evrakKayitTarihi.ValidatingType = typeof(System.DateTime);
            this._evrakKayitTarihi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this._evrakKayitTarihi.Leave += new System.EventHandler(this.TarihLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(43, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 15);
            this.label4.TabIndex = 1067;
            this.label4.Text = "Genel Evrak Kayıt No";
            // 
            // _evrakKayitNo
            // 
            this._evrakKayitNo.Font = new System.Drawing.Font("Calibri", 12F);
            this._evrakKayitNo.Location = new System.Drawing.Point(168, 111);
            this._evrakKayitNo.MaxLength = 6;
            this._evrakKayitNo.Name = "_evrakKayitNo";
            this._evrakKayitNo.Size = new System.Drawing.Size(288, 27);
            this._evrakKayitNo.TabIndex = 4;
            this._evrakKayitNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(30, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 15);
            this.label3.TabIndex = 1066;
            this.label3.Text = "Genel Evrak Kayıt Tarihi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(100, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 1065;
            this.label2.Text = "Abone No";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(97, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 1064;
            this.label1.Text = "Adı Soyadı";
            // 
            // _adiSoyadi
            // 
            this._adiSoyadi.Font = new System.Drawing.Font("Calibri", 12F);
            this._adiSoyadi.Location = new System.Drawing.Point(168, 12);
            this._adiSoyadi.Name = "_adiSoyadi";
            this._adiSoyadi.Size = new System.Drawing.Size(288, 27);
            this._adiSoyadi.TabIndex = 1;
            this._adiSoyadi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // _kaydet
            // 
            this._kaydet.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._kaydet.ForeColor = System.Drawing.SystemColors.ControlText;
            this._kaydet.Location = new System.Drawing.Point(349, 385);
            this._kaydet.Name = "_kaydet";
            this._kaydet.Size = new System.Drawing.Size(107, 47);
            this._kaydet.TabIndex = 1062;
            this._kaydet.Text = "KAYDET";
            this._kaydet.UseVisualStyleBackColor = true;
            this._kaydet.Click += new System.EventHandler(this.KaydetClick);
            // 
            // _taranan
            // 
            this._taranan.BackColor = System.Drawing.Color.WhiteSmoke;
            this._taranan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._taranan.Dock = System.Windows.Forms.DockStyle.Fill;
            this._taranan.Location = new System.Drawing.Point(0, 0);
            this._taranan.Name = "_taranan";
            this._taranan.Size = new System.Drawing.Size(546, 562);
            this._taranan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._taranan.TabIndex = 0;
            this._taranan.TabStop = false;
            this._taranan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TarananMouseDown);
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
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._taranan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        //private Library.Scan.ZoomablePictureBox _taranan;
        private System.Windows.Forms.PictureBox _taranan;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Timer _backgroundProcess;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _konusu;
        private System.Windows.Forms.Label _ciltBilgisi;
        private System.Windows.Forms.MaskedTextBox _evrakKayitTarihi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _evrakKayitNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _adiSoyadi;
        private System.Windows.Forms.Button _kaydet;
        private System.Windows.Forms.MaskedTextBox _gelenEvrakTarihi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox _gelenEvrakNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox _standartDosyaKodu;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox _havaleKisileri;
        private System.Windows.Forms.TextBox _ilgiliBirimNo;
        private System.Windows.Forms.TextBox _aboneNo;
        private System.Windows.Forms.Button _oncekiGetir;
        private System.Windows.Forms.Button _veritabanindanGetir;
        private System.Windows.Forms.Button _sil;        


    }
}