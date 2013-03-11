namespace CopyArt
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
                this.TearDown();
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
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._adi = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._modKalibrasyon = new System.Windows.Forms.RadioButton();
            this._timer = new System.Windows.Forms.NumericUpDown();
            this._modTimer = new System.Windows.Forms.RadioButton();
            this._modUser = new System.Windows.Forms.RadioButton();
            this._baslat = new System.Windows.Forms.Button();
            this._picture = new System.Windows.Forms.PictureBox();
            this._log = new System.Windows.Forms.ListBox();
            this._time = new System.Windows.Forms.Timer(this.components);
            this._timerLiveView = new System.Windows.Forms.Timer(this.components);
            this._timerBattery = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._sayfaNo = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this._batteryDescription = new System.Windows.Forms.Label();
            this._battery = new System.Windows.Forms.ProgressBar();
            this._isSonu = new System.Windows.Forms.Button();
            this._splitTypeCift = new System.Windows.Forms.RadioButton();
            this._splitTypeSag = new System.Windows.Forms.RadioButton();
            this._splitTypeSol = new System.Windows.Forms.RadioButton();
            this._splitTypeNone = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._cikis = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._picture)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Cift.png");
            this.imageList.Images.SetKeyName(1, "Sag.png");
            this.imageList.Images.SetKeyName(2, "Sol.png");
            this.imageList.Images.SetKeyName(3, "Dondur0.png");
            this.imageList.Images.SetKeyName(4, "Dondur90.png");
            this.imageList.Images.SetKeyName(5, "Dondur180.png");
            this.imageList.Images.SetKeyName(6, "Dondur270.png");
            this.imageList.Images.SetKeyName(7, "Kullanici.png");
            this.imageList.Images.SetKeyName(8, "Zaman.png");
            this.imageList.Images.SetKeyName(9, "Kalibrasyon.png");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._adi);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(455, 64);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "İşin Adı";
            // 
            // _adi
            // 
            this._adi.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._adi.Location = new System.Drawing.Point(6, 21);
            this._adi.Name = "_adi";
            this._adi.Size = new System.Drawing.Size(443, 37);
            this._adi.TabIndex = 8;
            this._adi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdiKeyDown);
            this._adi.Leave += new System.EventHandler(this.AdiLeave);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this._modKalibrasyon);
            this.groupBox4.Controls.Add(this._timer);
            this.groupBox4.Controls.Add(this._modTimer);
            this.groupBox4.Controls.Add(this._modUser);
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox4.Location = new System.Drawing.Point(577, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(197, 64);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Çekim Modu";
            // 
            // _modKalibrasyon
            // 
            this._modKalibrasyon.Appearance = System.Windows.Forms.Appearance.Button;
            this._modKalibrasyon.AutoSize = true;
            this._modKalibrasyon.Checked = true;
            this._modKalibrasyon.ImageIndex = 9;
            this._modKalibrasyon.ImageList = this.imageList;
            this._modKalibrasyon.Location = new System.Drawing.Point(6, 20);
            this._modKalibrasyon.Name = "_modKalibrasyon";
            this._modKalibrasyon.Size = new System.Drawing.Size(38, 38);
            this._modKalibrasyon.TabIndex = 9;
            this._modKalibrasyon.TabStop = true;
            this._modKalibrasyon.UseVisualStyleBackColor = true;
            // 
            // _timer
            // 
            this._timer.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._timer.Location = new System.Drawing.Point(138, 20);
            this._timer.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._timer.Name = "_timer";
            this._timer.Size = new System.Drawing.Size(51, 37);
            this._timer.TabIndex = 8;
            // 
            // _modTimer
            // 
            this._modTimer.Appearance = System.Windows.Forms.Appearance.Button;
            this._modTimer.AutoSize = true;
            this._modTimer.ImageIndex = 8;
            this._modTimer.ImageList = this.imageList;
            this._modTimer.Location = new System.Drawing.Point(94, 20);
            this._modTimer.Name = "_modTimer";
            this._modTimer.Size = new System.Drawing.Size(38, 38);
            this._modTimer.TabIndex = 4;
            this._modTimer.UseVisualStyleBackColor = true;
            this._modTimer.CheckedChanged += new System.EventHandler(this.ModTimerCheckedChanged);
            // 
            // _modUser
            // 
            this._modUser.Appearance = System.Windows.Forms.Appearance.Button;
            this._modUser.AutoSize = true;
            this._modUser.ImageIndex = 7;
            this._modUser.ImageList = this.imageList;
            this._modUser.Location = new System.Drawing.Point(50, 20);
            this._modUser.Name = "_modUser";
            this._modUser.Size = new System.Drawing.Size(38, 38);
            this._modUser.TabIndex = 3;
            this._modUser.UseVisualStyleBackColor = true;
            this._modUser.CheckedChanged += new System.EventHandler(this.ModUserCheckedChanged);
            // 
            // _baslat
            // 
            this._baslat.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._baslat.Location = new System.Drawing.Point(780, 19);
            this._baslat.Name = "_baslat";
            this._baslat.Size = new System.Drawing.Size(98, 56);
            this._baslat.TabIndex = 8;
            this._baslat.Text = "Başlat";
            this._baslat.UseVisualStyleBackColor = true;
            this._baslat.Click += new System.EventHandler(this.BaslatClick);
            // 
            // _picture
            // 
            this._picture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._picture.Location = new System.Drawing.Point(12, 82);
            this._picture.Name = "_picture";
            this._picture.Size = new System.Drawing.Size(1000, 674);
            this._picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._picture.TabIndex = 9;
            this._picture.TabStop = false;
            this._picture.WaitOnLoad = true;
            this._picture.Click += new System.EventHandler(this.PictureClick);
            // 
            // _log
            // 
            this._log.Location = new System.Drawing.Point(1027, 21);
            this._log.Name = "_log";
            this._log.Size = new System.Drawing.Size(324, 732);
            this._log.TabIndex = 10;
            // 
            // _time
            // 
            this._time.Interval = 1000;
            this._time.Tick += new System.EventHandler(this.TimeTick);
            // 
            // _timerLiveView
            // 
            this._timerLiveView.Interval = 1000;
            this._timerLiveView.Tick += new System.EventHandler(this.TimerLiveViewTick);
            // 
            // _timerBattery
            // 
            this._timerBattery.Enabled = true;
            this._timerBattery.Interval = 1000;
            this._timerBattery.Tick += new System.EventHandler(this.TimerBatteryTick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._sayfaNo);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox2.Location = new System.Drawing.Point(473, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(98, 64);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sayfa No";
            // 
            // _sayfaNo
            // 
            this._sayfaNo.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._sayfaNo.Location = new System.Drawing.Point(6, 21);
            this._sayfaNo.Name = "_sayfaNo";
            this._sayfaNo.Size = new System.Drawing.Size(86, 37);
            this._sayfaNo.TabIndex = 8;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this._batteryDescription);
            this.groupBox5.Controls.Add(this._battery);
            this.groupBox5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox5.Location = new System.Drawing.Point(1018, 76);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(150, 64);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Batarya Durumu";
            this.groupBox5.Visible = false;
            // 
            // _batteryDescription
            // 
            this._batteryDescription.Location = new System.Drawing.Point(4, 17);
            this._batteryDescription.Name = "_batteryDescription";
            this._batteryDescription.Size = new System.Drawing.Size(140, 15);
            this._batteryDescription.TabIndex = 14;
            this._batteryDescription.Text = "_batteryDescription";
            // 
            // _battery
            // 
            this._battery.Location = new System.Drawing.Point(6, 34);
            this._battery.Name = "_battery";
            this._battery.Size = new System.Drawing.Size(138, 22);
            this._battery.TabIndex = 13;
            // 
            // _isSonu
            // 
            this._isSonu.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._isSonu.Location = new System.Drawing.Point(884, 19);
            this._isSonu.Name = "_isSonu";
            this._isSonu.Size = new System.Drawing.Size(94, 56);
            this._isSonu.TabIndex = 15;
            this._isSonu.Text = "İş Sonu";
            this._isSonu.UseVisualStyleBackColor = true;
            this._isSonu.Click += new System.EventHandler(this._isSonu_Click);
            // 
            // _splitTypeCift
            // 
            this._splitTypeCift.Appearance = System.Windows.Forms.Appearance.Button;
            this._splitTypeCift.AutoSize = true;
            this._splitTypeCift.ImageIndex = 0;
            this._splitTypeCift.ImageList = this.imageList;
            this._splitTypeCift.Location = new System.Drawing.Point(50, 20);
            this._splitTypeCift.Name = "_splitTypeCift";
            this._splitTypeCift.Size = new System.Drawing.Size(38, 38);
            this._splitTypeCift.TabIndex = 3;
            this._splitTypeCift.UseVisualStyleBackColor = true;
            // 
            // _splitTypeSag
            // 
            this._splitTypeSag.Appearance = System.Windows.Forms.Appearance.Button;
            this._splitTypeSag.AutoSize = true;
            this._splitTypeSag.ImageIndex = 1;
            this._splitTypeSag.ImageList = this.imageList;
            this._splitTypeSag.Location = new System.Drawing.Point(93, 20);
            this._splitTypeSag.Name = "_splitTypeSag";
            this._splitTypeSag.Size = new System.Drawing.Size(38, 38);
            this._splitTypeSag.TabIndex = 4;
            this._splitTypeSag.UseVisualStyleBackColor = true;
            // 
            // _splitTypeSol
            // 
            this._splitTypeSol.Appearance = System.Windows.Forms.Appearance.Button;
            this._splitTypeSol.AutoSize = true;
            this._splitTypeSol.ImageIndex = 2;
            this._splitTypeSol.ImageList = this.imageList;
            this._splitTypeSol.Location = new System.Drawing.Point(137, 20);
            this._splitTypeSol.Name = "_splitTypeSol";
            this._splitTypeSol.Size = new System.Drawing.Size(38, 38);
            this._splitTypeSol.TabIndex = 5;
            this._splitTypeSol.UseVisualStyleBackColor = true;
            // 
            // _splitTypeNone
            // 
            this._splitTypeNone.Appearance = System.Windows.Forms.Appearance.Button;
            this._splitTypeNone.AutoSize = true;
            this._splitTypeNone.Checked = true;
            this._splitTypeNone.ImageList = this.imageList;
            this._splitTypeNone.Location = new System.Drawing.Point(6, 20);
            this._splitTypeNone.MaximumSize = new System.Drawing.Size(38, 38);
            this._splitTypeNone.MinimumSize = new System.Drawing.Size(38, 38);
            this._splitTypeNone.Name = "_splitTypeNone";
            this._splitTypeNone.Size = new System.Drawing.Size(38, 38);
            this._splitTypeNone.TabIndex = 6;
            this._splitTypeNone.TabStop = true;
            this._splitTypeNone.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._splitTypeNone);
            this.groupBox1.Controls.Add(this._splitTypeSol);
            this.groupBox1.Controls.Add(this._splitTypeSag);
            this.groupBox1.Controls.Add(this._splitTypeCift);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(1018, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 64);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bölme";
            this.groupBox1.Visible = false;
            // 
            // _cikis
            // 
            this._cikis.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._cikis.Location = new System.Drawing.Point(984, 19);
            this._cikis.Name = "_cikis";
            this._cikis.Size = new System.Drawing.Size(28, 56);
            this._cikis.TabIndex = 16;
            this._cikis.Text = "x";
            this._cikis.UseVisualStyleBackColor = true;
            this._cikis.Click += new System.EventHandler(this.CikisClick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 768);
            this.Controls.Add(this._cikis);
            this.Controls.Add(this._isSonu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._log);
            this.Controls.Add(this._picture);
            this.Controls.Add(this._baslat);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CopyArt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMainLoad);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._picture)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox _adi;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton _modTimer;
        private System.Windows.Forms.RadioButton _modUser;
        private System.Windows.Forms.NumericUpDown _timer;
        private System.Windows.Forms.Button _baslat;
        private System.Windows.Forms.PictureBox _picture;
        private System.Windows.Forms.ListBox _log;
        private System.Windows.Forms.Timer _time;
        private System.Windows.Forms.Timer _timerLiveView;
        private System.Windows.Forms.Timer _timerBattery;
        private System.Windows.Forms.RadioButton _modKalibrasyon;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox _sayfaNo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label _batteryDescription;
        private System.Windows.Forms.ProgressBar _battery;
        private System.Windows.Forms.Button _isSonu;
        private System.Windows.Forms.RadioButton _splitTypeCift;
        private System.Windows.Forms.RadioButton _splitTypeSag;
        private System.Windows.Forms.RadioButton _splitTypeSol;
        private System.Windows.Forms.RadioButton _splitTypeNone;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _cikis;
    }
}