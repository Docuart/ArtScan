namespace ArtControl
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._evrakTuru = new System.Windows.Forms.ComboBox();
            this._gonder = new System.Windows.Forms.Button();
            this._aboneNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._klasorNo = new System.Windows.Forms.MaskedTextBox();
            this._grid = new System.Windows.Forms.DataGridView();
            this._ara = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._yanlis = new System.Windows.Forms.Button();
            this._dogru = new System.Windows.Forms.Button();
            this._taranan = new System.Windows.Forms.PictureBox();
            this._yesil = new System.Windows.Forms.Button();
            this._sari = new System.Windows.Forms.Button();
            this._lacivert = new System.Windows.Forms.Button();
            this._turkuaz = new System.Windows.Forms.Button();
            this._pembe = new System.Windows.Forms.Button();
            this._kirmizi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._taranan)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._kirmizi);
            this.splitContainer1.Panel1.Controls.Add(this._pembe);
            this.splitContainer1.Panel1.Controls.Add(this._turkuaz);
            this.splitContainer1.Panel1.Controls.Add(this._lacivert);
            this.splitContainer1.Panel1.Controls.Add(this._sari);
            this.splitContainer1.Panel1.Controls.Add(this._yesil);
            this.splitContainer1.Panel1.Controls.Add(this._evrakTuru);
            this.splitContainer1.Panel1.Controls.Add(this._gonder);
            this.splitContainer1.Panel1.Controls.Add(this._aboneNo);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this._klasorNo);
            this.splitContainer1.Panel1.Controls.Add(this._grid);
            this.splitContainer1.Panel1.Controls.Add(this._ara);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this._yanlis);
            this.splitContainer1.Panel1.Controls.Add(this._dogru);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._taranan);
            this.splitContainer1.Size = new System.Drawing.Size(1139, 662);
            this.splitContainer1.SplitterDistance = 446;
            this.splitContainer1.TabIndex = 3;
            // 
            // _evrakTuru
            // 
            this._evrakTuru.FormattingEnabled = true;
            this._evrakTuru.Location = new System.Drawing.Point(88, 50);
            this._evrakTuru.Name = "_evrakTuru";
            this._evrakTuru.Size = new System.Drawing.Size(290, 27);
            this._evrakTuru.TabIndex = 12;
            // 
            // _gonder
            // 
            this._gonder.Location = new System.Drawing.Point(369, 120);
            this._gonder.Name = "_gonder";
            this._gonder.Size = new System.Drawing.Size(75, 23);
            this._gonder.TabIndex = 11;
            this._gonder.Text = "Gönder";
            this._gonder.UseVisualStyleBackColor = true;
            this._gonder.Click += new System.EventHandler(this.GonderClick);
            // 
            // _aboneNo
            // 
            this._aboneNo.Location = new System.Drawing.Point(88, 86);
            this._aboneNo.Name = "_aboneNo";
            this._aboneNo.Size = new System.Drawing.Size(290, 27);
            this._aboneNo.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Abone No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Evrak Türü";
            // 
            // _klasorNo
            // 
            this._klasorNo.Font = new System.Drawing.Font("Calibri", 12F);
            this._klasorNo.Location = new System.Drawing.Point(88, 12);
            this._klasorNo.Name = "_klasorNo";
            this._klasorNo.Size = new System.Drawing.Size(290, 27);
            this._klasorNo.TabIndex = 6;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._grid.Location = new System.Drawing.Point(12, 149);
            this._grid.Name = "_grid";
            this._grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(431, 428);
            this._grid.TabIndex = 5;
            this._grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCellClick);
            // 
            // _ara
            // 
            this._ara.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._ara.Location = new System.Drawing.Point(384, 12);
            this._ara.Name = "_ara";
            this._ara.Size = new System.Drawing.Size(59, 27);
            this._ara.TabIndex = 4;
            this._ara.Text = "Ara";
            this._ara.UseVisualStyleBackColor = true;
            this._ara.Click += new System.EventHandler(this.AraClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Klasör No";
            // 
            // _yanlis
            // 
            this._yanlis.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._yanlis.Location = new System.Drawing.Point(12, 583);
            this._yanlis.Name = "_yanlis";
            this._yanlis.Size = new System.Drawing.Size(212, 67);
            this._yanlis.TabIndex = 1;
            this._yanlis.Text = "Yanlış";
            this._yanlis.UseVisualStyleBackColor = true;
            this._yanlis.Click += new System.EventHandler(this.YanlisClick);
            // 
            // _dogru
            // 
            this._dogru.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._dogru.Location = new System.Drawing.Point(230, 583);
            this._dogru.Name = "_dogru";
            this._dogru.Size = new System.Drawing.Size(213, 67);
            this._dogru.TabIndex = 0;
            this._dogru.Text = "Doğru";
            this._dogru.UseVisualStyleBackColor = true;
            this._dogru.Click += new System.EventHandler(this.DogruClick);
            // 
            // _taranan
            // 
            this._taranan.Dock = System.Windows.Forms.DockStyle.Fill;
            this._taranan.Location = new System.Drawing.Point(0, 0);
            this._taranan.Name = "_taranan";
            this._taranan.Size = new System.Drawing.Size(689, 662);
            this._taranan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._taranan.TabIndex = 0;
            this._taranan.TabStop = false;
            // 
            // _yesil
            // 
            this._yesil.BackColor = System.Drawing.Color.Lime;
            this._yesil.Location = new System.Drawing.Point(16, 119);
            this._yesil.Name = "_yesil";
            this._yesil.Size = new System.Drawing.Size(35, 23);
            this._yesil.TabIndex = 13;
            this._yesil.UseVisualStyleBackColor = false;
            this._yesil.Click += new System.EventHandler(this.RenklendirClick);
            // 
            // _sari
            // 
            this._sari.BackColor = System.Drawing.Color.Yellow;
            this._sari.Location = new System.Drawing.Point(57, 119);
            this._sari.Name = "_sari";
            this._sari.Size = new System.Drawing.Size(35, 23);
            this._sari.TabIndex = 14;
            this._sari.UseVisualStyleBackColor = false;
            this._sari.Click += new System.EventHandler(this.RenklendirClick);
            // 
            // _lacivert
            // 
            this._lacivert.BackColor = System.Drawing.Color.Navy;
            this._lacivert.Location = new System.Drawing.Point(98, 119);
            this._lacivert.Name = "_lacivert";
            this._lacivert.Size = new System.Drawing.Size(35, 23);
            this._lacivert.TabIndex = 15;
            this._lacivert.UseVisualStyleBackColor = false;
            this._lacivert.Click += new System.EventHandler(this.RenklendirClick);
            // 
            // _turkuaz
            // 
            this._turkuaz.BackColor = System.Drawing.Color.Aqua;
            this._turkuaz.Location = new System.Drawing.Point(139, 120);
            this._turkuaz.Name = "_turkuaz";
            this._turkuaz.Size = new System.Drawing.Size(35, 23);
            this._turkuaz.TabIndex = 16;
            this._turkuaz.UseVisualStyleBackColor = false;
            this._turkuaz.Click += new System.EventHandler(this.RenklendirClick);
            // 
            // _pembe
            // 
            this._pembe.BackColor = System.Drawing.Color.Fuchsia;
            this._pembe.Location = new System.Drawing.Point(180, 120);
            this._pembe.Name = "_pembe";
            this._pembe.Size = new System.Drawing.Size(35, 23);
            this._pembe.TabIndex = 17;
            this._pembe.UseVisualStyleBackColor = false;
            this._pembe.Click += new System.EventHandler(this.RenklendirClick);
            // 
            // _kirmizi
            // 
            this._kirmizi.BackColor = System.Drawing.Color.Red;
            this._kirmizi.Location = new System.Drawing.Point(221, 119);
            this._kirmizi.Name = "_kirmizi";
            this._kirmizi.Size = new System.Drawing.Size(35, 23);
            this._kirmizi.TabIndex = 18;
            this._kirmizi.UseVisualStyleBackColor = false;
            this._kirmizi.Click += new System.EventHandler(this.RenklendirClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 662);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._taranan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.Button _ara;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _yanlis;
        private System.Windows.Forms.Button _dogru;
        private System.Windows.Forms.PictureBox _taranan;
        private System.Windows.Forms.MaskedTextBox _klasorNo;
        private System.Windows.Forms.TextBox _aboneNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _evrakTuru;
        private System.Windows.Forms.Button _gonder;
        private System.Windows.Forms.Button _kirmizi;
        private System.Windows.Forms.Button _pembe;
        private System.Windows.Forms.Button _turkuaz;
        private System.Windows.Forms.Button _lacivert;
        private System.Windows.Forms.Button _sari;
        private System.Windows.Forms.Button _yesil;
    }
}