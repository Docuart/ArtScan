namespace ArtRename
{
    partial class Form1
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
            this._klasor = new System.Windows.Forms.TextBox();
            this._yeni = new System.Windows.Forms.TextBox();
            this._gozat = new System.Windows.Forms.Button();
            this._kopyala = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _klasor
            // 
            this._klasor.Location = new System.Drawing.Point(12, 12);
            this._klasor.Name = "_klasor";
            this._klasor.Size = new System.Drawing.Size(386, 20);
            this._klasor.TabIndex = 0;
            // 
            // _yeni
            // 
            this._yeni.Location = new System.Drawing.Point(12, 38);
            this._yeni.Name = "_yeni";
            this._yeni.Size = new System.Drawing.Size(386, 20);
            this._yeni.TabIndex = 1;
            // 
            // _gozat
            // 
            this._gozat.Location = new System.Drawing.Point(404, 10);
            this._gozat.Name = "_gozat";
            this._gozat.Size = new System.Drawing.Size(75, 23);
            this._gozat.TabIndex = 2;
            this._gozat.Text = "Gözat";
            this._gozat.UseVisualStyleBackColor = true;
            this._gozat.Click += new System.EventHandler(this.GozatClick);
            // 
            // _kopyala
            // 
            this._kopyala.Location = new System.Drawing.Point(404, 36);
            this._kopyala.Name = "_kopyala";
            this._kopyala.Size = new System.Drawing.Size(75, 23);
            this._kopyala.TabIndex = 3;
            this._kopyala.Text = "Kopyala";
            this._kopyala.UseVisualStyleBackColor = true;
            this._kopyala.Click += new System.EventHandler(this._kopyala_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 262);
            this.Controls.Add(this._kopyala);
            this.Controls.Add(this._gozat);
            this.Controls.Add(this._yeni);
            this.Controls.Add(this._klasor);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _klasor;
        private System.Windows.Forms.TextBox _yeni;
        private System.Windows.Forms.Button _gozat;
        private System.Windows.Forms.Button _kopyala;
    }
}

