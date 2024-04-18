using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat
{
    public partial class ForgotPassword : Form
    {
        DataTable dt = new DataTable();
        const string user_info_path = @"C:\Users\PC MY TU\Downloads\MyChat\data\users_info.txt";

        public DataTable LoadInfo(string path)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("userID", typeof(string));
            dt.Columns.Add("username", typeof(string));
            dt.Columns.Add("password", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("avatarPath", typeof(string));

            string[] lines = File.ReadAllLines(path);

            if (lines.Length == 0)
                return dt;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] values = line.Split("*");

                DataRow row = dt.NewRow();

                row["userID"] = values[0];
                row["username"] = values[1];
                row["password"] = values[2];
                row["name"] = values[3];
                row["email"] = values[4];
                row["avatarPath"] = values[5];

                dt.Rows.Add(row);
            }

            return dt;
        }

        public ForgotPassword()
        {
            InitializeComponent();
        }

        public void SendMail(string sendToAddress)
        {
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress("21522740@gm.uit.edu.vn");
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mail.From.Address, "rsxt qrag boim ceuo");
            smtp.Host = "smtp.gmail.com";

            //recipient
            mail.To.Add(new MailAddress(sendToAddress));
            mail.IsBodyHtml = true;
            mail.Subject = "Verification Code";
            mail.Body = "Why would you forget your password?";

            smtp.Send(mail);
        }

        public void btn_reset_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["username"].ToString() == txt_mail.Text.Trim())
                {
                    SendMail(row["email"].ToString());
                    break;
                }
            }

            lbl_noti.Visible = true;
            lbl_noti.ForeColor = Color.Green;
        }

        private void txt_enter_message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (!String.IsNullOrWhiteSpace(txt_mail.Text))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["username"].ToString() == txt_mail.Text.Trim())
                        {
                            SendMail(row["email"].ToString());
                            break;
                        }
                    }

                    lbl_noti.Visible = true;
                }
            }
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {
            dt = LoadInfo(user_info_path);
        }
    }
}
