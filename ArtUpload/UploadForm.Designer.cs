namespace ArtUpload
{
    partial class UploadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadForm));
            this._bolge = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._sehir = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this._mudurluk = new System.Windows.Forms.ComboBox();
            this._yukle = new System.Windows.Forms.Button();
            this._ciltKlasoru = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._klasorSec = new System.Windows.Forms.Button();
            this._ciltler = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // _bolge
            // 
            this._bolge.FormattingEnabled = true;
            this._bolge.Location = new System.Drawing.Point(81, 38);
            this._bolge.Name = "_bolge";
            this._bolge.Size = new System.Drawing.Size(252, 21);
            this._bolge.TabIndex = 3;
            this._bolge.SelectedIndexChanged += new System.EventHandler(this.BolgeSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(33, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bölge";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(36, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Şehir";
            // 
            // _sehir
            // 
            this._sehir.FormattingEnabled = true;
            this._sehir.Location = new System.Drawing.Point(81, 65);
            this._sehir.Name = "_sehir";
            this._sehir.Size = new System.Drawing.Size(252, 21);
            this._sehir.TabIndex = 4;
            this._sehir.SelectedIndexChanged += new System.EventHandler(this.SehirSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(16, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Müdürlük";
            // 
            // _mudurluk
            // 
            this._mudurluk.FormattingEnabled = true;
            this._mudurluk.Location = new System.Drawing.Point(81, 92);
            this._mudurluk.Name = "_mudurluk";
            this._mudurluk.Size = new System.Drawing.Size(252, 21);
            this._mudurluk.TabIndex = 5;
            // 
            // _yukle
            // 
            this._yukle.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._yukle.Location = new System.Drawing.Point(219, 119);
            this._yukle.Name = "_yukle";
            this._yukle.Size = new System.Drawing.Size(114, 32);
            this._yukle.TabIndex = 6;
            this._yukle.Text = "Ciltleri Yükle";
            this._yukle.UseVisualStyleBackColor = true;
            this._yukle.Click += new System.EventHandler(this.YukleClick);
            // 
            // _ciltKlasoru
            // 
            this._ciltKlasoru.Location = new System.Drawing.Point(81, 12);
            this._ciltKlasoru.Name = "_ciltKlasoru";
            this._ciltKlasoru.Size = new System.Drawing.Size(184, 20);
            this._ciltKlasoru.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(8, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Cilt Klasörü";
            // 
            // _klasorSec
            // 
            this._klasorSec.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this._klasorSec.Location = new System.Drawing.Point(265, 10);
            this._klasorSec.Name = "_klasorSec";
            this._klasorSec.Size = new System.Drawing.Size(68, 23);
            this._klasorSec.TabIndex = 1;
            this._klasorSec.Text = "Gözat";
            this._klasorSec.UseVisualStyleBackColor = true;
            this._klasorSec.Click += new System.EventHandler(this.KlasorSecClick);
            // 
            // _ciltler
            // 
            this._ciltler.FormattingEnabled = true;
            this._ciltler.Location = new System.Drawing.Point(339, 12);
            this._ciltler.Name = "_ciltler";
            this._ciltler.Size = new System.Drawing.Size(214, 259);
            this._ciltler.TabIndex = 10;
            // 
            // UploadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 284);
            this.Controls.Add(this._ciltler);
            this.Controls.Add(this._klasorSec);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._ciltKlasoru);
            this.Controls.Add(this._yukle);
            this.Controls.Add(this._mudurluk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._sehir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._bolge);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UploadForm";
            this.Text = "UploadForm";
            this.Load += new System.EventHandler(this.UploadFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _bolge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _sehir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _mudurluk;
        private System.Windows.Forms.Button _yukle;
        private System.Windows.Forms.TextBox _ciltKlasoru;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button _klasorSec;
        private System.Windows.Forms.CheckedListBox _ciltler;

    }
}