namespace MyChat
{
    partial class uct_video
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uct_video));
            media_video = new AxWMPLib.AxWindowsMediaPlayer();
            pic_avatar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)media_video).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_avatar).BeginInit();
            SuspendLayout();
            // 
            // media_video
            // 
            media_video.Enabled = true;
            media_video.Location = new Point(10, 10);
            media_video.Name = "media_video";
            media_video.OcxState = (AxHost.State)resources.GetObject("media_video.OcxState");
            media_video.Size = new Size(360, 260);
            media_video.TabIndex = 0;
            // 
            // pic_avatar
            // 
            pic_avatar.Image = (Image)resources.GetObject("pic_avatar.Image");
            pic_avatar.Location = new Point(380, 210);
            pic_avatar.Name = "pic_avatar";
            pic_avatar.Size = new Size(60, 60);
            pic_avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_avatar.TabIndex = 1;
            pic_avatar.TabStop = false;
            // 
            // uct_video
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            Controls.Add(pic_avatar);
            Controls.Add(media_video);
            Name = "uct_video";
            Size = new Size(450, 280);
            ((System.ComponentModel.ISupportInitialize)media_video).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_avatar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer media_video;
        private PictureBox pic_avatar;
    }
}
