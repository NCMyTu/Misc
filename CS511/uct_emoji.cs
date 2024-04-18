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
    public partial class uct_emoji : UserControl
    {
        Color lightBackground = Color.White, darkBackground = Color.FromArgb(38, 46, 53);

        public uct_emoji()
        {
            InitializeComponent();
        }

        public void SetEmoji(string emojiPath, string avatarPath)
        {
            pic_avatar.Image = Image.FromFile(avatarPath);
            pic_emoji.Image = System.Drawing.Bitmap.FromFile(emojiPath);
        }

        public void SwitchToDarkMode()
        {
            this.BackColor = darkBackground;
        }

        public void SwitchToLightMode()
        {
            this.BackColor = lightBackground;
        }
    }
}
