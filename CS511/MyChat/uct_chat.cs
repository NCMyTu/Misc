using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyChat
{
    public partial class uct_chat : UserControl
    {
        int betweenPadding = 10, leftPadding = 10, rightPadding = 10, verticalPadding = 10;
        Color lightBackground = Color.White, darkBackground = Color.FromArgb(38, 46, 53);

        public uct_chat()
        {
            InitializeComponent();
        }

        public void SetComponentLocation()
        {
            int x = leftPadding;
            int y = this.Height / 2 - txt_message.Height / 2;
            txt_message.Location = new Point(x, y);

            x = leftPadding + txt_message.Width + betweenPadding;
            y = verticalPadding / 2;
            pic_avatar.Location = new Point(x, y);
        }

        public void DynamicSizing()
        {
            Size textSize = TextRenderer.MeasureText(txt_message.Text, txt_message.Font);
            int avatarWidth = pic_avatar.Width, avatarHeight = pic_avatar.Height;
            int height;

            txt_message.Width = textSize.Width + 30;
            txt_message.Height = textSize.Height + 6;

            
            height = avatarHeight;
            

            this.Width = leftPadding + txt_message.Width + betweenPadding + avatarWidth + rightPadding;
            this.Height = height + verticalPadding;
        }

        public void SelectText(string text)
        {
            txt_message.DeselectAll();
            pic_avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            string t = txt_message.Text;
            int i = t.IndexOf(text);

            if (i != -1)
            {
                // this is fking stupid.
                // somehow when deselecting the SelectionBackColor gets combined with BackColor ????
                string temp = txt_message.Text;

                txt_message.Text = temp;
                txt_message.SelectionAlignment = HorizontalAlignment.Center;

                DynamicSizing();

                SetComponentLocation();

                txt_message.Select(i, text.Length);
                txt_message.Focus();
                txt_message.SelectionBackColor = Color.Red;
                txt_message.SelectionColor = Color.White;
            }
            else
            {
                Deselect();
            }
        }

        public void Deselect()
        {
            txt_message.DeselectAll();
            string temp = txt_message.Text;

            txt_message.Text = temp;
            txt_message.SelectionAlignment = HorizontalAlignment.Center;

            DynamicSizing();

            SetComponentLocation();
        }

        public Color GetBackColor()
        {
            return txt_message.BackColor;
        }

        public void SetMessage(string message, string avatarPath)
        {
            txt_message.Text = message;
            txt_message.SelectionAlignment = HorizontalAlignment.Center;

            txt_message.HideSelection = false;

            pic_avatar.Image = System.Drawing.Image.FromFile(avatarPath);
            pic_avatar.SizeMode = PictureBoxSizeMode.StretchImage;

            DynamicSizing();

            SetComponentLocation();
        }

        public string GetMessage()
        {
            return txt_message.Text;
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
