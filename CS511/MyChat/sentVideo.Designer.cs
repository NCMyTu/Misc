namespace MyChat
{
    partial class sentVideo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sentVideo));
            lbl_date = new Label();
            wmp_video = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)wmp_video).BeginInit();
            SuspendLayout();
            // 
            // lbl_date
            // 
            lbl_date.AutoSize = true;
            lbl_date.BackColor = Color.Black;
            lbl_date.Font = new Font("Segoe UI", 12F);
            lbl_date.ForeColor = Color.White;
            lbl_date.Location = new Point(566, 660);
            lbl_date.Name = "lbl_date";
            lbl_date.Size = new Size(64, 32);
            lbl_date.TabIndex = 0;
            lbl_date.Text = "Date";
            // 
            // wmp_video
            // 
            wmp_video.Enabled = true;
            wmp_video.Location = new Point(45, 50);
            wmp_video.Name = "wmp_video";
            wmp_video.OcxState = (AxHost.State)resources.GetObject("wmp_video.OcxState");
            wmp_video.Size = new Size(1400, 600);
            wmp_video.TabIndex = 1;
            // 
            // sentVideo
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.Black;
            Controls.Add(wmp_video);
            Controls.Add(lbl_date);
            Name = "sentVideo";
            Size = new Size(1490, 700);
            ((System.ComponentModel.ISupportInitialize)wmp_video).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_date;
        private AxWMPLib.AxWindowsMediaPlayer wmp_video;
    }
}
