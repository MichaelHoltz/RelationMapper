using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelationMap.Utility;
using System.Net;

namespace RelationMap.HelperForms
{
    public partial class CharacterImageMaker : Form
    {
        String CharacterName;
        String SourcePath;
        private Point mdown;
        private Point mup;

        public CharacterImageMaker()
        {
            InitializeComponent();
        }
        public CharacterImageMaker(String characterName, String sourcePath)
        {
            InitializeComponent();
            CharacterName = characterName;
            SourcePath = sourcePath;
        }
        private void CharacterImageMaker_Load(object sender, EventArgs e)
        {
            if (SourcePath != null)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                        | SecurityProtocolType.Tls11
                                                        | SecurityProtocolType.Tls12
                                                        | SecurityProtocolType.Ssl3;
                var wc = new WebClient();
                Image tnImage = Image.FromStream(wc.OpenRead(SourcePath)); //Read from the Internet
                pbProfile.BackgroundImage = tnImage;
                tnImage.Save(PrivateData.GetAppPath() + @"\Cache\Images\Characters\source_" + CharacterName + ".png");
            }
        }
        private void pbProfile_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mup = e.Location;
                updateSelection();
                //pbProfile.Refresh();
                //using (Graphics g = pbProfile.CreateGraphics())
                //{
                //    g.SmoothingMode = SmoothingMode.AntiAlias;
                //    Utility.ImageHelper.DrawCircle(g, Pens.Red, mdown, e.Location);
                //}
            }
        }
        private void updateSelection()
        {
            pbProfile.Refresh();
            using (Graphics g = pbProfile.CreateGraphics())
            {
                if (rbCircle.Checked)
                {
                    ImageHelper.DrawCircle(g, Pens.Red, mdown, mup);
                }
                else
                {
                    ImageHelper.DrawConstrainedRectangle(g, Pens.Red, mdown, mup, ((float)pbOfficialProfile.Width / (float)pbOfficialProfile.Height));
                }
            }
        }
        private void pbProfile_MouseDown(object sender, MouseEventArgs e)
        {
            mdown = e.Location;
        }

        private void btnMakeThumbNail_Click(object sender, EventArgs e)
        {
            using (Graphics g = pbProfile.CreateGraphics())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                pbDest.BackgroundImage = ImageHelper.CropToCircle(g, pbProfile.BackgroundImage, mdown, mup);
                pbDest.BackgroundImage.Save(PrivateData.GetAppPath() + @"\Cache\Images\Characters\tn_" + CharacterName +".png");
            }
        }
        private void btnMakeProfile_Click(object sender, EventArgs e)
        {
            //using (Graphics g = pbProfile.CreateGraphics())
            //{
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            int pw = pbProfile.Width;
            int ph = pbProfile.Height;

            int opw = pbOfficialProfile.Width;
            int oph = pbOfficialProfile.Height;
            
            Bitmap newImage = ImageHelper.CropToRectangle(pbProfile.BackgroundImage, mdown, mup, opw, oph, pw, ph);
            pbOfficialProfile.BackgroundImage = newImage;
            newImage.Save(PrivateData.GetAppPath() + @"\Cache\Images\Characters\pi_" + CharacterName + ".png"); //dpi doesn't matter so what is going on!!
                //pbOfficialProfile.BackgroundImage.Save(PrivateData.GetAppPath() + @"\Cache\Images\Characters\profile.png");
            //}
        }
        private void pbProfile_MouseUp(object sender, MouseEventArgs e)
        {
            mup = e.Location;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (!mdown.IsEmpty && !mup.IsEmpty)
            {
                mdown.X -= (e.OldValue - e.NewValue);
                mup.X -= (e.OldValue - e.NewValue);
                updateSelection();
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (!mdown.IsEmpty && !mup.IsEmpty)
            {
                mdown.Y -= (e.OldValue - e.NewValue);
                mup.Y -= (e.OldValue - e.NewValue);
                updateSelection();
            }
        }


    }
}
