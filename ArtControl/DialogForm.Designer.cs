namespace ArtControl
{
    partial class DialogForm
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
            this.label1 = new System.Windows.Forms.Label();
            this._evet = new System.Windows.Forms.Button();
            this._hayir = new System.Windows.Forms.Button();
            this._text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tarama HATALI olarak işaretlenecektir. Onaylıyor musunuz?";
            // 
            // _evet
            // 
            this._evet.Location = new System.Drawing.Point(216, 82);
            this._evet.Name = "_evet";
            this._evet.Size = new System.Drawing.Size(75, 23);
            this._evet.TabIndex = 1;
            this._evet.Text = "Evet";
            this._evet.UseVisualStyleBackColor = true;
            this._evet.Click += new System.EventHandler(this.EvetClick);
            // 
            // _hayir
            // 
            this._hayir.Location = new System.Drawing.Point(297, 82);
            this._hayir.Name = "_hayir";
            this._hayir.Size = new System.Drawing.Size(75, 23);
            this._hayir.TabIndex = 2;
            this._hayir.Text = "Hayır";
            this._hayir.UseVisualStyleBackColor = true;
            this._hayir.Click += new System.EventHandler(this.HayirClick);
            // 
            // _text
            // 
            this._text.Location = new System.Drawing.Point(12, 28);
            this._text.Multiline = true;
            this._text.Name = "_text";
            this._text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._text.Size = new System.Drawing.Size(360, 48);
            this._text.TabIndex = 0;
            // 
            // DialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 116);
            this.ControlBox = false;
            this.Controls.Add(this._text);
            this.Controls.Add(this._hayir);
            this.Controls.Add(this._evet);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Onay";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _evet;
        private System.Windows.Forms.Button _hayir;
        private System.Windows.Forms.TextBox _text;
    }
}