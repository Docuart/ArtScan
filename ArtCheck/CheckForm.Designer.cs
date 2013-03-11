namespace ArtCheck
{
    partial class CheckForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._sil = new System.Windows.Forms.Button();
            this._ciltKlasoru = new System.Windows.Forms.Label();
            this._dosya = new System.Windows.Forms.Label();
            this._tekrar = new System.Windows.Forms.Button();
            this._uygun = new System.Windows.Forms.Button();
            this._kullanici = new System.Windows.Forms.Label();
            this._cilt = new System.Windows.Forms.Label();
            this._tarananList = new System.Windows.Forms.ListBox();
            this._taranan = new System.Windows.Forms.PictureBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._yukleniyor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._taranan)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._taranan);
            this.splitContainer1.Size = new System.Drawing.Size(869, 477);
            this.splitContainer1.SplitterDistance = 445;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._yukleniyor);
            this.splitContainer2.Panel1.Controls.Add(this._sil);
            this.splitContainer2.Panel1.Controls.Add(this._ciltKlasoru);
            this.splitContainer2.Panel1.Controls.Add(this._dosya);
            this.splitContainer2.Panel1.Controls.Add(this._tekrar);
            this.splitContainer2.Panel1.Controls.Add(this._uygun);
            this.splitContainer2.Panel1.Controls.Add(this._kullanici);
            this.splitContainer2.Panel1.Controls.Add(this._cilt);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._tarananList);
            this.splitContainer2.Size = new System.Drawing.Size(445, 477);
            this.splitContainer2.SplitterDistance = 155;
            this.splitContainer2.TabIndex = 0;
            // 
            // _sil
            // 
            this._sil.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._sil.ForeColor = System.Drawing.Color.Red;
            this._sil.Location = new System.Drawing.Point(315, 92);
            this._sil.Name = "_sil";
            this._sil.Size = new System.Drawing.Size(127, 58);
            this._sil.TabIndex = 16;
            this._sil.Text = "Sil";
            this._sil.UseVisualStyleBackColor = true;
            this._sil.Click += new System.EventHandler(this.SilClick);
            // 
            // _ciltKlasoru
            // 
            this._ciltKlasoru.AutoSize = true;
            this._ciltKlasoru.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._ciltKlasoru.ForeColor = System.Drawing.Color.DarkBlue;
            this._ciltKlasoru.Location = new System.Drawing.Point(12, 9);
            this._ciltKlasoru.Name = "_ciltKlasoru";
            this._ciltKlasoru.Size = new System.Drawing.Size(88, 20);
            this._ciltKlasoru.TabIndex = 15;
            this._ciltKlasoru.Text = "Cilt Klasörü";
            // 
            // _dosya
            // 
            this._dosya.AutoSize = true;
            this._dosya.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._dosya.ForeColor = System.Drawing.Color.DarkBlue;
            this._dosya.Location = new System.Drawing.Point(12, 69);
            this._dosya.Name = "_dosya";
            this._dosya.Size = new System.Drawing.Size(81, 20);
            this._dosya.TabIndex = 14;
            this._dosya.Text = "Dosya Adı";
            // 
            // _tekrar
            // 
            this._tekrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._tekrar.ForeColor = System.Drawing.Color.Green;
            this._tekrar.Location = new System.Drawing.Point(159, 92);
            this._tekrar.Name = "_tekrar";
            this._tekrar.Size = new System.Drawing.Size(150, 58);
            this._tekrar.TabIndex = 13;
            this._tekrar.Text = "Tekrar Tara";
            this._tekrar.UseVisualStyleBackColor = true;
            this._tekrar.Click += new System.EventHandler(this.TekrarClick);
            // 
            // _uygun
            // 
            this._uygun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._uygun.ForeColor = System.Drawing.Color.DarkBlue;
            this._uygun.Location = new System.Drawing.Point(3, 92);
            this._uygun.Name = "_uygun";
            this._uygun.Size = new System.Drawing.Size(150, 58);
            this._uygun.TabIndex = 12;
            this._uygun.Text = "Uygun";
            this._uygun.UseVisualStyleBackColor = true;
            this._uygun.Click += new System.EventHandler(this.UygunClick);
            // 
            // _kullanici
            // 
            this._kullanici.AutoSize = true;
            this._kullanici.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._kullanici.ForeColor = System.Drawing.Color.DarkBlue;
            this._kullanici.Location = new System.Drawing.Point(12, 49);
            this._kullanici.Name = "_kullanici";
            this._kullanici.Size = new System.Drawing.Size(220, 20);
            this._kullanici.TabIndex = 11;
            this._kullanici.Text = "Kullanıcı Bilgisi@Bilgisayar Adı";
            // 
            // _cilt
            // 
            this._cilt.AutoSize = true;
            this._cilt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._cilt.ForeColor = System.Drawing.Color.DarkBlue;
            this._cilt.Location = new System.Drawing.Point(12, 29);
            this._cilt.Name = "_cilt";
            this._cilt.Size = new System.Drawing.Size(75, 20);
            this._cilt.TabIndex = 10;
            this._cilt.Text = "Cilt Bilgisi";
            // 
            // _tarananList
            // 
            this._tarananList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tarananList.FormattingEnabled = true;
            this._tarananList.Location = new System.Drawing.Point(0, 0);
            this._tarananList.Name = "_tarananList";
            this._tarananList.Size = new System.Drawing.Size(445, 318);
            this._tarananList.TabIndex = 0;
            this._tarananList.SelectedIndexChanged += new System.EventHandler(this.TarananListSelectedIndexChanged);
            // 
            // _taranan
            // 
            this._taranan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._taranan.Dock = System.Windows.Forms.DockStyle.Fill;
            this._taranan.Location = new System.Drawing.Point(0, 0);
            this._taranan.Name = "_taranan";
            this._taranan.Size = new System.Drawing.Size(420, 477);
            this._taranan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._taranan.TabIndex = 0;
            this._taranan.TabStop = false;
            // 
            // _timer
            // 
            this._timer.Interval = 1000;
            this._timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // _yukleniyor
            // 
            this._yukleniyor.Dock = System.Windows.Forms.DockStyle.Fill;
            this._yukleniyor.Font = new System.Drawing.Font("Calibri", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._yukleniyor.ForeColor = System.Drawing.Color.DarkBlue;
            this._yukleniyor.Location = new System.Drawing.Point(0, 0);
            this._yukleniyor.Name = "_yukleniyor";
            this._yukleniyor.Size = new System.Drawing.Size(445, 155);
            this._yukleniyor.TabIndex = 17;
            this._yukleniyor.Text = "Dosya Bekleniyor...";
            this._yukleniyor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._yukleniyor.Visible = false;
            // 
            // CheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 477);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CheckForm";
            this.Text = "Tarama Kontrolü";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CheckFormLoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._taranan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox _taranan;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label _dosya;
        private System.Windows.Forms.Button _tekrar;
        private System.Windows.Forms.Button _uygun;
        private System.Windows.Forms.Label _kullanici;
        private System.Windows.Forms.Label _cilt;
        private System.Windows.Forms.Label _ciltKlasoru;
        private System.Windows.Forms.ListBox _tarananList;
        private System.Windows.Forms.Button _sil;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Label _yukleniyor;

    }
}

