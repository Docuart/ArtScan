namespace ArtCrop
{
    partial class LoadingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingForm));
            this._info = new System.Windows.Forms.TextBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._progress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // _info
            // 
            this._info.Enabled = false;
            this._info.Location = new System.Drawing.Point(12, 32);
            this._info.Multiline = true;
            this._info.Name = "_info";
            this._info.Size = new System.Drawing.Size(737, 285);
            this._info.TabIndex = 0;
            this._info.WordWrap = false;
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // _progress
            // 
            this._progress.Location = new System.Drawing.Point(13, 8);
            this._progress.Name = "_progress";
            this._progress.Size = new System.Drawing.Size(736, 18);
            this._progress.TabIndex = 1;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 329);
            this.Controls.Add(this._progress);
            this.Controls.Add(this._info);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoadingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yükleniyor...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _info;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.ProgressBar _progress;
    }
}