namespace MyChat
{
    partial class uct_image
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uct_image));
            pic_avatar = new PictureBox();
            pic_image = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pic_avatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_image).BeginInit();
            SuspendLayout();
            // 
            // pic_avatar
            // 
            pic_avatar.Image = (Image)resources.GetObject("pic_avatar.Image");
            pic_avatar.Location = new Point(380, 210);
            pic_avatar.Name = "pic_avatar";
            pic_avatar.Size = new Size(60, 60);
            pic_avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_avatar.TabIndex = 0;
            pic_avatar.TabStop = false;
            // 
            // pic_image
            // 
            pic_image.Image = (Image)resources.GetObject("pic_image.Image");
            pic_image.Location = new Point(10, 10);
            pic_image.Name = "pic_image";
            pic_image.Size = new Size(360, 260);
            pic_image.SizeMode = PictureBoxSizeMode.Zoom;
            pic_image.TabIndex = 0;
            pic_image.TabStop = false;
            // 
            // uct_image
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            Controls.Add(pic_image);
            Controls.Add(pic_avatar);
            Name = "uct_image";
            Size = new Size(450, 280);
            ((System.ComponentModel.ISupportInitialize)pic_avatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_image).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pic_avatar;
        private PictureBox pic_image;
    }
}
