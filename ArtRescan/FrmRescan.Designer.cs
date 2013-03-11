namespace ArtRescan
{
    partial class FrmRescan
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
            this._taranacaklar = new System.Windows.Forms.ListBox();
            this._tara = new System.Windows.Forms.Button();
            this._hataliResim = new System.Windows.Forms.PictureBox();
            this._kaydet = new System.Windows.Forms.Button();
            this._secili = new System.Windows.Forms.Label();
            this._aciklama = new System.Windows.Forms.Label();
            this._sil = new System.Windows.Forms.Button();
            this._yevmiyeTarihi = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._yevmiyeNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._hataliResim)).BeginInit();
            this.SuspendLayout();
            // 
            // _taranacaklar
            // 
            this._taranacaklar.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._taranacaklar.FormattingEnabled = true;
            this._taranacaklar.ItemHeight = 15;
            this._taranacaklar.Location = new System.Drawing.Point(14, 164);
            this._taranacaklar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._taranacaklar.Name = "_taranacaklar";
            this._taranacaklar.Size = new System.Drawing.Size(293, 334);
            this._taranacaklar.TabIndex = 0;
            this._taranacaklar.SelectedIndexChanged += new System.EventHandler(this.TaranacaklarSelectedIndexChanged);
            // 
            // _tara
            // 
            this._tara.Font = new System.Drawing.Font("Calibri", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._tara.Location = new System.Drawing.Point(14, 14);
            this._tara.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._tara.Name = "_tara";
            this._tara.Size = new System.Drawing.Size(115, 67);
            this._tara.TabIndex = 1;
            this._tara.Text = "TARAMAYI BAŞLAT";
            this._tara.UseVisualStyleBackColor = true;
            this._tara.Click += new System.EventHandler(this.TaraClick);
            // 
            // _hataliResim
            // 
            this._hataliResim.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._hataliResim.Location = new System.Drawing.Point(314, 39);
            this._hataliResim.Name = "_hataliResim";
            this._hataliResim.Size = new System.Drawing.Size(634, 459);
            this._hataliResim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._hataliResim.TabIndex = 2;
            this._hataliResim.TabStop = false;
            // 
            // _kaydet
            // 
            this._kaydet.Font = new System.Drawing.Font("Calibri", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._kaydet.Location = new System.Drawing.Point(137, 14);
            this._kaydet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._kaydet.Name = "_kaydet";
            this._kaydet.Size = new System.Drawing.Size(103, 67);
            this._kaydet.TabIndex = 3;
            this._kaydet.Text = "KAYDET";
            this._kaydet.UseVisualStyleBackColor = true;
            this._kaydet.Click += new System.EventHandler(this.KaydetClick);
            // 
            // _secili
            // 
            this._secili.AutoSize = true;
            this._secili.Location = new System.Drawing.Point(11, 147);
            this._secili.Name = "_secili";
            this._secili.Size = new System.Drawing.Size(43, 15);
            this._secili.TabIndex = 4;
            this._secili.Text = "_secili";
            // 
            // _aciklama
            // 
            this._aciklama.AutoSize = true;
            this._aciklama.Location = new System.Drawing.Point(314, 14);
            this._aciklama.Name = "_aciklama";
            this._aciklama.Size = new System.Drawing.Size(64, 15);
            this._aciklama.TabIndex = 5;
            this._aciklama.Text = "_aciklama";
            // 
            // _sil
            // 
            this._sil.Font = new System.Drawing.Font("Calibri", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._sil.ForeColor = System.Drawing.Color.Red;
            this._sil.Location = new System.Drawing.Point(248, 14);
            this._sil.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._sil.Name = "_sil";
            this._sil.Size = new System.Drawing.Size(59, 67);
            this._sil.TabIndex = 6;
            this._sil.Text = "SİL";
            this._sil.UseVisualStyleBackColor = true;
            this._sil.Click += new System.EventHandler(this.SilClick);
            // 
            // _yevmiyeTarihi
            // 
            this._yevmiyeTarihi.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._yevmiyeTarihi.Location = new System.Drawing.Point(98, 87);
            this._yevmiyeTarihi.Mask = "00/00/0000";
            this._yevmiyeTarihi.Name = "_yevmiyeTarihi";
            this._yevmiyeTarihi.Size = new System.Drawing.Size(209, 27);
            this._yevmiyeTarihi.TabIndex = 52;
            this._yevmiyeTarihi.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(26, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 55;
            this.label4.Text = "Yevmiye No";
            // 
            // _yevmiyeNo
            // 
            this._yevmiyeNo.Font = new System.Drawing.Font("Calibri", 12F);
            this._yevmiyeNo.Location = new System.Drawing.Point(98, 118);
            this._yevmiyeNo.MaxLength = 6;
            this._yevmiyeNo.Name = "_yevmiyeNo";
            this._yevmiyeNo.Size = new System.Drawing.Size(209, 27);
            this._yevmiyeNo.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(13, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 54;
            this.label3.Text = "Yevmiye Tarihi";
            // 
            // FrmRescan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 515);
            this.Controls.Add(this._yevmiyeTarihi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._yevmiyeNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._sil);
            this.Controls.Add(this._aciklama);
            this.Controls.Add(this._secili);
            this.Controls.Add(this._kaydet);
            this.Controls.Add(this._hataliResim);
            this.Controls.Add(this._tara);
            this.Controls.Add(this._taranacaklar);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmRescan";
            this.Text = "FrmRescan";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.SizeChanged += new System.EventHandler(this.OnFormSizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this._hataliResim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _taranacaklar;
        private System.Windows.Forms.Button _tara;
        private System.Windows.Forms.PictureBox _hataliResim;
        private System.Windows.Forms.Button _kaydet;
        private System.Windows.Forms.Label _secili;
        private System.Windows.Forms.Label _aciklama;
        private System.Windows.Forms.Button _sil;
        private System.Windows.Forms.MaskedTextBox _yevmiyeTarihi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _yevmiyeNo;
        private System.Windows.Forms.Label label3;
    }
}