namespace MyChat
{
    partial class uct_chat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uct_chat));
            pic_avatar = new PictureBox();
            txt_message = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pic_avatar).BeginInit();
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
            // txt_message
            // 
            txt_message.BackColor = Color.FromArgb(200, 230, 255);
            txt_message.BorderStyle = BorderStyle.None;
            txt_message.Font = new Font("Segoe UI", 12F);
            txt_message.Location = new Point(6, 37);
            txt_message.Name = "txt_message";
            txt_message.ReadOnly = true;
            txt_message.Size = new Size(261, 30);
            txt_message.TabIndex = 3;
            txt_message.Text = "";
            // 
            // uct_chat
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            Controls.Add(txt_message);
            Controls.Add(pic_avatar);
            Name = "uct_chat";
            Size = new Size(350, 100);
            ((System.ComponentModel.ISupportInitialize)pic_avatar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pic_avatar;
        private RichTextBox txt_message;
    }
}
