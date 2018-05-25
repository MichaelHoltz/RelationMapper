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
        private Point pStart;
        private Point pEnd;

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
            //Needed to avoid issues with some SSL image links.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                        | SecurityProtocolType.Tls11
                                                        | SecurityProtocolType.Tls12
                                                        | SecurityProtocolType.Ssl3;
            if (SourcePath != null)
            {
                var wc = new WebClient();
                try
                {
                    Image tnImage = Image.FromStream(wc.OpenRead(SourcePath)); //Read from the Internet
                    pbSourceProfile.BackgroundImage = tnImage;
                    tnImage.Save(PrivateData.GetAppPath() + @"\Cache\Images\Characters\source_" + CharacterName + ".png");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message + "\r\n" + SourcePath);
                }
            }
        }
        private void cbTestImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTestImages.SelectedIndex >= 0)
            {
                String testImage = cbTestImages.SelectedItem.ToString();
                CharacterName = testImage;
                String testImagePath = PrivateData.GetAppPath() + @"\Cache\Images\Characters\" + testImage + ".png";
                pbSourceProfile.BackgroundImage = Image.FromFile(testImagePath);
            }
        }
        private void pbProfile_MouseMove(object sender, MouseEventArgs e)
        {
            //Still Dragging
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                
                //Swap points if needed
                //if (ImageHelper.GetTopLeft(pStart, e.Location) == e.Location)
                //{
                //    pEnd = pStart;
                //    pStart = e.Location;
                //}
                //else
                //{
                    pEnd = e.Location;
                //}
                updateSelection();
            }

            lblMouseNormal.Text = "Mouse X, Y " + e.X + ", " + e.Y;
        }
        private void updateSelection()
        {
            pbSourceProfile.Refresh();
            using (Graphics g = pbSourceProfile.CreateGraphics())
            {


                if (rbCircle.Checked)
                {
                                        double dx = pEnd.X - pStart.X;
                    double dy = pEnd.Y - pStart.Y;
                    double radius = Math.Sqrt(dx * dx + dy * dy);
                    lblRadius.Text = "Radius: " +radius.ToString();
                    ImageHelper.DrawCircle(g, Pens.Red, pStart, pEnd);

                }
                else
                {

                    Rectangle selection = ImageHelper.DrawConstrainedRectangle(g, Pens.Red, pStart, pEnd, ((float)pbOfficialProfile.Width / (float)pbOfficialProfile.Height));
                    //Could invert if needed if drawing from bottom right.
                    pEnd.X = selection.Right;
                    pEnd.Y = selection.Bottom;
                    lbWidth.Text = "Width: " +(pEnd.X - pStart.X).ToString();
                    lbHeight.Text = "Height: " +(pEnd.Y - pStart.Y).ToString();

                }
                lblStart.Text = "Start X,Y " + pStart.X.ToString() + ", " + pStart.Y.ToString();
                lblEnd.Text = "End X,Y " + pEnd.X.ToString() + ", " + pEnd.Y.ToString();
            }
        }
        private void pbProfile_MouseDown(object sender, MouseEventArgs e)
        {
            //Assumes top left to bottom right drawing
            pStart = e.Location;
            lblStart.Text = "Start X,Y " + e.X.ToString() + ", " + e.Y.ToString();
        }
        private void pbProfile_MouseUp(object sender, MouseEventArgs e)
        {
            pEnd = e.Location;
            lblEnd.Text = "End X, Y" + e.X.ToString() + ", " + e.Y.ToString();
            updateSelection();
        }

        private void btnMakeThumbNail_Click(object sender, EventArgs e)
        {
            using (Graphics g = pbSourceProfile.CreateGraphics())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                //pbDest.BackgroundImage = ImageHelper.CropToCircle111(g, pbSourceProfile.BackgroundImage, pStart, pEnd);
                pbDest.BackgroundImage = ImageHelper.CropToCircle(pbSourceProfile, pStart, pEnd, pbDest);
                pbDest.BackgroundImage.Save(PrivateData.GetAppPath() + @"\Cache\Images\Characters\tn_" + CharacterName +".png");
            }
        }
        private void btnMakeProfile_Click(object sender, EventArgs e)
        {
            //using (Graphics g = pbProfile.CreateGraphics())
            //{
            //g.SmoothingMode = SmoothingMode.AntiAlias;

            int opw = pbOfficialProfile.Width;
            int oph = pbOfficialProfile.Height;
            
            Bitmap newImage = ImageHelper.CropToRectangle(pbSourceProfile, pStart, pEnd, pbOfficialProfile);
            pbOfficialProfile.BackgroundImage = newImage;
            newImage.Save(PrivateData.GetAppPath() + @"\Cache\Images\Characters\pi_" + CharacterName + ".png"); 
                //pbOfficialProfile.BackgroundImage.Save(PrivateData.GetAppPath() + @"\Cache\Images\Characters\profile.png");
            //}
        }


        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (!pStart.IsEmpty && !pEnd.IsEmpty)
            {
                pStart.X -= (e.OldValue - e.NewValue);
                pEnd.X -= (e.OldValue - e.NewValue);
                updateSelection();
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (!pStart.IsEmpty && !pEnd.IsEmpty)
            {
                pStart.Y -= (e.OldValue - e.NewValue);
                pEnd.Y -= (e.OldValue - e.NewValue);
                updateSelection();
            }
        }


    }
}
