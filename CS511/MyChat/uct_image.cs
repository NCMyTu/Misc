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
    public partial class uct_image : UserControl
    {
        int betweenPadding = 10, leftPadding = 10, rightPadding = 10, verticalPadding = 10;
        Color lightBackground = Color.White, darkBackground = Color.FromArgb(38, 46, 53);

        public uct_image()
        {
            InitializeComponent();
        }

        public void SetImage(string imagePath, string avatarPath)
        {
            pic_avatar.Image = Image.FromFile(avatarPath);

            Image image = Image.FromFile(imagePath);

            double wFactor = (double)image.Width / pic_image.Width;
            double hFactor = (double)image.Height / pic_image.Height;
            double resizeFactor = Math.Max(wFactor, hFactor);

            int newWidth = Convert.ToInt16(image.Width / resizeFactor);
            int newHeight = Convert.ToInt16(image.Height / resizeFactor);

            pic_image.Size = new Size(newWidth, newHeight);
            pic_image.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_image.Image = image;

            this.Width = leftPadding + pic_image.Width + betweenPadding + pic_avatar.Width + rightPadding;
            this.Height = 2 * verticalPadding + pic_image.Height;

            int x = this.Width - rightPadding - pic_avatar.Width - betweenPadding - pic_image.Width;
            int y = verticalPadding;

            pic_image.Location = new Point(x, y);

            x = this.Width - rightPadding - pic_avatar.Width;
            y = this.Height - verticalPadding - pic_avatar.Height;

            pic_avatar.Location = new Point(x, y);
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
