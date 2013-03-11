namespace ArtDeskew
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
            this._progressHepsi = new System.Windows.Forms.ProgressBar();
            this._progressKlasor = new System.Windows.Forms.ProgressBar();
            this._klasorSec1 = new System.Windows.Forms.Button();
            this._egimGider = new System.Windows.Forms.Button();
            this._klasor1 = new System.Windows.Forms.TextBox();
            this._klasor2 = new System.Windows.Forms.TextBox();
            this._klasorSec2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _progressHepsi
            // 
            this._progressHepsi.Location = new System.Drawing.Point(12, 94);
            this._progressHepsi.Name = "_progressHepsi";
            this._progressHepsi.Size = new System.Drawing.Size(503, 23);
            this._progressHepsi.TabIndex = 0;
            // 
            // _progressKlasor
            // 
            this._progressKlasor.Location = new System.Drawing.Point(12, 65);
            this._progressKlasor.Name = "_progressKlasor";
            this._progressKlasor.Size = new System.Drawing.Size(503, 23);
            this._progressKlasor.TabIndex = 1;
            // 
            // _klasorSec1
            // 
            this._klasorSec1.Location = new System.Drawing.Point(320, 10);
            this._klasorSec1.Name = "_klasorSec1";
            this._klasorSec1.Size = new System.Drawing.Size(114, 23);
            this._klasorSec1.TabIndex = 2;
            this._klasorSec1.Text = "Kaynak Klasör Seç";
            this._klasorSec1.UseVisualStyleBackColor = true;
            this._klasorSec1.Click += new System.EventHandler(this.KlasorSec1Click);
            // 
            // _egimGider
            // 
            this._egimGider.Location = new System.Drawing.Point(440, 10);
            this._egimGider.Name = "_egimGider";
            this._egimGider.Size = new System.Drawing.Size(75, 48);
            this._egimGider.TabIndex = 3;
            this._egimGider.Text = "Eğim Gider";
            this._egimGider.UseVisualStyleBackColor = true;
            this._egimGider.Click += new System.EventHandler(this.EgimGiderClick);
            // 
            // _klasor1
            // 
            this._klasor1.Location = new System.Drawing.Point(12, 12);
            this._klasor1.Name = "_klasor1";
            this._klasor1.Size = new System.Drawing.Size(302, 20);
            this._klasor1.TabIndex = 4;
            // 
            // _klasor2
            // 
            this._klasor2.Location = new System.Drawing.Point(12, 38);
            this._klasor2.Name = "_klasor2";
            this._klasor2.Size = new System.Drawing.Size(302, 20);
            this._klasor2.TabIndex = 5;
            // 
            // _klasorSec2
            // 
            this._klasorSec2.Location = new System.Drawing.Point(320, 36);
            this._klasorSec2.Name = "_klasorSec2";
            this._klasorSec2.Size = new System.Drawing.Size(114, 23);
            this._klasorSec2.TabIndex = 6;
            this._klasorSec2.Text = "Hedef Klasör Seç";
            this._klasorSec2.UseVisualStyleBackColor = true;
            this._klasorSec2.Click += new System.EventHandler(this.KlasorSec2Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 129);
            this.Controls.Add(this._klasorSec2);
            this.Controls.Add(this._klasor2);
            this.Controls.Add(this._klasor1);
            this.Controls.Add(this._egimGider);
            this.Controls.Add(this._klasorSec1);
            this.Controls.Add(this._progressKlasor);
            this.Controls.Add(this._progressHepsi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Eğim Giderici";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar _progressHepsi;
        private System.Windows.Forms.ProgressBar _progressKlasor;
        private System.Windows.Forms.Button _klasorSec1;
        private System.Windows.Forms.Button _egimGider;
        private System.Windows.Forms.TextBox _klasor1;
        private System.Windows.Forms.TextBox _klasor2;
        private System.Windows.Forms.Button _klasorSec2;
    }
}

