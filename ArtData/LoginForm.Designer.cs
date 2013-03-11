using System;

namespace ArtData
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this._kontrol = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this._giris = new System.Windows.Forms.Button();
            this._userName = new System.Windows.Forms.Label();
            this._istatistik = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _kontrol
            // 
            this._kontrol.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._kontrol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._kontrol.ImageIndex = 0;
            this._kontrol.ImageList = this.imageList;
            this._kontrol.Location = new System.Drawing.Point(63, 117);
            this._kontrol.Name = "_kontrol";
            this._kontrol.Size = new System.Drawing.Size(195, 72);
            this._kontrol.TabIndex = 0;
            this._kontrol.Text = "Veri Kontrolü";
            this._kontrol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._kontrol.UseVisualStyleBackColor = true;
            this._kontrol.Click += new System.EventHandler(this.KontrolClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "check.png");
            this.imageList.Images.SetKeyName(1, "database.png");
            this.imageList.Images.SetKeyName(2, "chart-icon.png");
            // 
            // _giris
            // 
            this._giris.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._giris.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._giris.ImageIndex = 1;
            this._giris.ImageList = this.imageList;
            this._giris.Location = new System.Drawing.Point(63, 39);
            this._giris.Name = "_giris";
            this._giris.Size = new System.Drawing.Size(195, 72);
            this._giris.TabIndex = 1;
            this._giris.Text = "Veri Girişi";
            this._giris.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._giris.UseVisualStyleBackColor = true;
            this._giris.Click += new System.EventHandler(this.GirisClick);
            // 
            // _userName
            // 
            this._userName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._userName.Location = new System.Drawing.Point(12, 9);
            this._userName.Name = "_userName";
            this._userName.Size = new System.Drawing.Size(313, 27);
            this._userName.TabIndex = 2;
            this._userName.Text = "Kullanıcı Adı";
            this._userName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _istatistik
            // 
            this._istatistik.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._istatistik.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._istatistik.ImageIndex = 2;
            this._istatistik.ImageList = this.imageList;
            this._istatistik.Location = new System.Drawing.Point(63, 195);
            this._istatistik.Name = "_istatistik";
            this._istatistik.Size = new System.Drawing.Size(195, 72);
            this._istatistik.TabIndex = 3;
            this._istatistik.Text = "İstatistikler";
            this._istatistik.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._istatistik.UseVisualStyleBackColor = true;
            this._istatistik.Click += new System.EventHandler(this.IstatistikClick);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 277);
            this.Controls.Add(this._istatistik);
            this.Controls.Add(this._userName);
            this.Controls.Add(this._giris);
            this.Controls.Add(this._kontrol);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.Text = "ArtData Login";
            this.Load += new System.EventHandler(this.LoginFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _kontrol;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button _giris;
        private System.Windows.Forms.Label _userName;
        private System.Windows.Forms.Button _istatistik;
    }
}