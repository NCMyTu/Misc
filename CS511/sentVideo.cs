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
    public partial class sentVideo : UserControl
    {
        public sentVideo()
        {
            InitializeComponent();
        }

        public void SetVideoAndDate(string videoPath, string date)
        {
            lbl_date.Text = date;
            int x = (this.Width - wmp_video.Width)/ 2;
            lbl_date.Location = new Point(x, lbl_date.Location.Y);

            wmp_video.uiMode = "full";
            wmp_video.settings.autoStart = false;
            wmp_video.stretchToFit = true;
            wmp_video.URL = videoPath;
        }
    }
}
