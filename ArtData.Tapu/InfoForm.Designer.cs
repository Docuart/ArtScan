namespace ArtData
{
    partial class InfoForm
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
            this._info = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _info
            // 
            this._info.Dock = System.Windows.Forms.DockStyle.Fill;
            this._info.FormattingEnabled = true;
            this._info.Location = new System.Drawing.Point(0, 0);
            this._info.Name = "_info";
            this._info.Size = new System.Drawing.Size(876, 380);
            this._info.TabIndex = 0;
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 380);
            this.Controls.Add(this._info);
            this.Name = "InfoForm";
            this.Text = "InfoForm";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox _info;



    }
}