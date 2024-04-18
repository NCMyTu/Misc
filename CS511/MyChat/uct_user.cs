using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyChat
{
    public partial class uct_user : UserControl
    {
        string userID = "";

        public uct_user()
        {
            InitializeComponent();
        }

        public void SetUserInfo(string userID, string name, string avatarPath, string lastMessage)
        {
            this.userID = userID;
            pic_avatar.Image = Image.FromFile(avatarPath);
            lbl_name.Text = name;
        }

        public string GetUserID()
        {
            return userID;
        }

        public void DarkenBackground()
        {
            Color originalColor = Color.FromArgb(200, 230, 255);
            int darkenAmount = 30; // Adjust this value as needed

            int r = Math.Max(originalColor.R - darkenAmount, 0);
            int g = Math.Max(originalColor.G - darkenAmount, 0);
            int b = Math.Max(originalColor.B - darkenAmount, 0);

            this.BackColor = Color.FromArgb(r, g, b);
        }

        public void UndarkenBackground()
        {
            this.BackColor = Color.FromArgb(200, 230, 255);
        }
    }
}
