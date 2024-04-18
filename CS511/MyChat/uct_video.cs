using AxWMPLib;
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

    public partial class uct_video : UserControl
    {
        Color lightBackground = Color.White, darkBackground = Color.FromArgb(38, 46, 53);

        bool isPlaying = false;

        public uct_video()
        {
            InitializeComponent();
        }

        public void ToggleVideo()
        {
            if (isPlaying)
                media_video.Ctlcontrols.pause();
            else
                media_video.Ctlcontrols.play();

            isPlaying = !isPlaying;
        }

        public bool isInvideoRegion(Point p)
        {
            Rectangle videoBound = media_video.ClientRectangle;
            return videoBound.Contains(p);
        }

        public void SetVideo(string videoPath, string avatarPath)
        {
            pic_avatar.Image = Image.FromFile(avatarPath);

            media_video.uiMode = "mini";
            media_video.settings.autoStart = false;
            media_video.stretchToFit = true;
            media_video.URL = videoPath;
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
