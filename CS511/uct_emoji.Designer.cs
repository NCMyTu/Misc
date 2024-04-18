namespace MyChat
{
    partial class uct_emoji
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uct_emoji));
            pic_avatar = new PictureBox();
            pic_emoji = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pic_avatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_emoji).BeginInit();
            SuspendLayout();
            // 
            // pic_avatar
            // 
            pic_avatar.Image = (Image)resources.GetObject("pic_avatar.Image");
            pic_avatar.Location = new Point(282, 20);
            pic_avatar.Name = "pic_avatar";
            pic_avatar.Size = new Size(60, 60);
            pic_avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_avatar.TabIndex = 0;
            pic_avatar.TabStop = false;
            // 
            // pic_emoji
            // 
            pic_emoji.Image = (Image)resources.GetObject("pic_emoji.Image");
            pic_emoji.Location = new Point(200, 15);
            pic_emoji.Name = "pic_emoji";
            pic_emoji.Size = new Size(70, 70);
            pic_emoji.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_emoji.TabIndex = 0;
            pic_emoji.TabStop = false;
            // 
            // uct_emoji
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            Controls.Add(pic_emoji);
            Controls.Add(pic_avatar);
            Name = "uct_emoji";
            Size = new Size(350, 100);
            ((System.ComponentModel.ISupportInitialize)pic_avatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_emoji).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pic_avatar;
        private PictureBox pic_emoji;
    }
}
