namespace chat
{
    partial class ForgotPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotPassword));
            label1 = new Label();
            txt_mail = new TextBox();
            label2 = new Label();
            btn_reset = new Button();
            panel1 = new Panel();
            lbl_noti = new Label();
            pbx_app_icon = new PictureBox();
            lbl_app_name = new Label();
            lbl_big_sign_in = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_app_icon).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(205, 247, 236);
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(509, 60);
            label1.TabIndex = 0;
            label1.Text = "   Enter your username and instructions will be sent \r\nto you!";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txt_mail
            // 
            txt_mail.Location = new Point(18, 109);
            txt_mail.Name = "txt_mail";
            txt_mail.Size = new Size(477, 31);
            txt_mail.TabIndex = 0;
            txt_mail.KeyDown += txt_enter_message_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(18, 69);
            label2.Name = "label2";
            label2.Size = new Size(126, 32);
            label2.TabIndex = 0;
            label2.Text = "Username:";
            // 
            // btn_reset
            // 
            btn_reset.BackColor = Color.FromArgb(115, 105, 238);
            btn_reset.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_reset.ForeColor = SystemColors.ButtonHighlight;
            btn_reset.Location = new Point(18, 179);
            btn_reset.Name = "btn_reset";
            btn_reset.Size = new Size(477, 58);
            btn_reset.TabIndex = 2;
            btn_reset.Text = "Reset";
            btn_reset.UseVisualStyleBackColor = false;
            btn_reset.Click += btn_reset_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lbl_noti);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btn_reset);
            panel1.Controls.Add(txt_mail);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(63, 165);
            panel1.Name = "panel1";
            panel1.Size = new Size(518, 245);
            panel1.TabIndex = 3;
            // 
            // lbl_noti
            // 
            lbl_noti.AutoSize = true;
            lbl_noti.Font = new Font("Segoe UI", 11F);
            lbl_noti.ForeColor = Color.Green;
            lbl_noti.Location = new Point(68, 143);
            lbl_noti.Name = "lbl_noti";
            lbl_noti.Size = new Size(382, 30);
            lbl_noti.TabIndex = 6;
            lbl_noti.Text = "Verification code has been sent to you";
            lbl_noti.Visible = false;
            // 
            // pbx_app_icon
            // 
            pbx_app_icon.Image = (Image)resources.GetObject("pbx_app_icon.Image");
            pbx_app_icon.Location = new Point(184, 9);
            pbx_app_icon.Name = "pbx_app_icon";
            pbx_app_icon.Size = new Size(75, 75);
            pbx_app_icon.SizeMode = PictureBoxSizeMode.StretchImage;
            pbx_app_icon.TabIndex = 5;
            pbx_app_icon.TabStop = false;
            // 
            // lbl_app_name
            // 
            lbl_app_name.AutoSize = true;
            lbl_app_name.Font = new Font("Segoe UI", 26F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_app_name.Location = new Point(259, 9);
            lbl_app_name.Name = "lbl_app_name";
            lbl_app_name.Size = new Size(207, 70);
            lbl_app_name.TabIndex = 4;
            lbl_app_name.Text = "MyChat";
            // 
            // lbl_big_sign_in
            // 
            lbl_big_sign_in.AutoSize = true;
            lbl_big_sign_in.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_big_sign_in.Location = new Point(176, 87);
            lbl_big_sign_in.Name = "lbl_big_sign_in";
            lbl_big_sign_in.Size = new Size(298, 54);
            lbl_big_sign_in.TabIndex = 6;
            lbl_big_sign_in.Text = "Reset password";
            // 
            // ForgotPassword
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 242, 249);
            ClientSize = new Size(628, 422);
            Controls.Add(lbl_big_sign_in);
            Controls.Add(pbx_app_icon);
            Controls.Add(lbl_app_name);
            Controls.Add(panel1);
            Name = "ForgotPassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reset password";
            Load += ForgotPassword_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_app_icon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_mail;
        private Label label2;
        private Button btn_reset;
        private Panel panel1;
        private PictureBox pbx_app_icon;
        private Label lbl_app_name;
        private Label lbl_big_sign_in;
        private Label lbl_noti;
    }
}