namespace MyChat
{
    partial class uct_user
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
            pic_avatar = new PictureBox();
            lbl_name = new Label();
            ((System.ComponentModel.ISupportInitialize)pic_avatar).BeginInit();
            SuspendLayout();
            // 
            // pic_avatar
            // 
            pic_avatar.Location = new Point(20, 20);
            pic_avatar.Name = "pic_avatar";
            pic_avatar.Size = new Size(60, 60);
            pic_avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_avatar.TabIndex = 0;
            pic_avatar.TabStop = false;
            // 
            // lbl_name
            // 
            lbl_name.AutoSize = true;
            lbl_name.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_name.Location = new Point(86, 34);
            lbl_name.Name = "lbl_name";
            lbl_name.Size = new Size(79, 32);
            lbl_name.TabIndex = 1;
            lbl_name.Text = "Name";
            // 
            // uct_user
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(200, 230, 255);
            Controls.Add(lbl_name);
            Controls.Add(pic_avatar);
            Name = "uct_user";
            Size = new Size(558, 100);
            ((System.ComponentModel.ISupportInitialize)pic_avatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pic_avatar;
        private Label lbl_name;
    }
}
