namespace ArtScan
{
    partial class TarayiciForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TarayiciForm));
            this.btnScan = new System.Windows.Forms.Button();
            this._cilt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._folder = new System.Windows.Forms.TextBox();
            this._sayfa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._logs = new System.Windows.Forms.TextBox();
            this._sonTaranan = new System.Windows.Forms.PictureBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this._sn = new System.Windows.Forms.TextBox();
            this._ciltTamam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._sonTaranan)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnScan.Location = new System.Drawing.Point(12, 168);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(218, 70);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Taramayı Başlat";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.BtnScanClick);
            // 
            // _cilt
            // 
            this._cilt.Location = new System.Drawing.Point(12, 64);
            this._cilt.Name = "_cilt";
            this._cilt.Size = new System.Drawing.Size(364, 20);
            this._cilt.TabIndex = 20;
            this._cilt.Leave += new System.EventHandler(this.CiltLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cilt Bilgisi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tarama Klasörü";
            // 
            // _folder
            // 
            this._folder.Location = new System.Drawing.Point(12, 25);
            this._folder.Name = "_folder";
            this._folder.Size = new System.Drawing.Size(364, 20);
            this._folder.TabIndex = 10;
            this._folder.Text = "D:\\Scanned\\";
            // 
            // _sayfa
            // 
            this._sayfa.Location = new System.Drawing.Point(12, 103);
            this._sayfa.Name = "_sayfa";
            this._sayfa.Size = new System.Drawing.Size(364, 20);
            this._sayfa.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Taranacak Sayfa Numarası";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 647);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(450, 16);
            this.label2.TabIndex = 100;
            this.label2.Text = "Her bir cilt için tarama işlemi tamamlandığında programı yeniden başlatınız.";
            // 
            // _logs
            // 
            this._logs.Enabled = false;
            this._logs.Location = new System.Drawing.Point(12, 244);
            this._logs.Multiline = true;
            this._logs.Name = "_logs";
            this._logs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._logs.Size = new System.Drawing.Size(364, 400);
            this._logs.TabIndex = 101;
            this._logs.WordWrap = false;
            this._logs.TextChanged += new System.EventHandler(this.LogsTextChanged);
            // 
            // _sonTaranan
            // 
            this._sonTaranan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._sonTaranan.Location = new System.Drawing.Point(384, 25);
            this._sonTaranan.Name = "_sonTaranan";
            this._sonTaranan.Size = new System.Drawing.Size(431, 619);
            this._sonTaranan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._sonTaranan.TabIndex = 102;
            this._sonTaranan.TabStop = false;
            // 
            // _timer
            // 
            this._timer.Interval = 1000;
            this._timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(203, 13);
            this.label5.TabIndex = 103;
            this.label5.Text = "Otomatik Tarama Bekleme Süresi (saniye)";
            // 
            // _sn
            // 
            this._sn.Location = new System.Drawing.Point(12, 142);
            this._sn.Name = "_sn";
            this._sn.Size = new System.Drawing.Size(364, 20);
            this._sn.TabIndex = 104;
            this._sn.Text = "3";
            this._sn.TextChanged += new System.EventHandler(this.SnTextChanged);
            // 
            // _ciltTamam
            // 
            this._ciltTamam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._ciltTamam.ForeColor = System.Drawing.Color.DarkBlue;
            this._ciltTamam.Location = new System.Drawing.Point(236, 168);
            this._ciltTamam.Name = "_ciltTamam";
            this._ciltTamam.Size = new System.Drawing.Size(140, 70);
            this._ciltTamam.TabIndex = 105;
            this._ciltTamam.Text = "Cildi Tamamladım";
            this._ciltTamam.UseVisualStyleBackColor = true;
            this._ciltTamam.Click += new System.EventHandler(this.CiltTamamClick);
            // 
            // TarayiciForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 672);
            this.Controls.Add(this._ciltTamam);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._sn);
            this.Controls.Add(this._sonTaranan);
            this.Controls.Add(this._logs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._sayfa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._folder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cilt);
            this.Controls.Add(this.btnScan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TarayiciForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tarayıcı";
            ((System.ComponentModel.ISupportInitialize)(this._sonTaranan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox _cilt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _folder;
        private System.Windows.Forms.TextBox _sayfa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _logs;
        private System.Windows.Forms.PictureBox _sonTaranan;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _sn;
        private System.Windows.Forms.Button _ciltTamam;
    }
}

