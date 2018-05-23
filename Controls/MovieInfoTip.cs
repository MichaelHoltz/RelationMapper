using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using TmdbWrapper;
using System.IO;
using System.Net;
using RelationMap.Models;
namespace RelationMap.Controls
{
    public partial class MovieInfoTip : UserControl
    {
        private Movie selectedMovie;
        private Universe u;
        public MovieInfoTip()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Universe Reference should only be used for Movies in the my Universe.
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="univ"></param>
        public void LoadMovieInfo(ref Movie movie, ref Universe univ)
        {
            selectedMovie = movie;
            u = univ;
            lblTitle.Text = movie.Title;
            lblReleaseDate.Text = movie.ReleaseDate.Value.ToShortDateString();
            lblRunTime.Text = movie.Runtime.ToString() + " minutes";
            lblRevenue.Text = movie.Revenue.ToString("C0"); // Currancy no cents.
            if (movie.HomePage != null)
            {
                pbPoster.Tag = movie.HomePage.AbsoluteUri;
            }
            else
            {
                pbPoster.Tag = null;
            }
            GetMoviePoster();
            GetProductionCompanyLogos();
        }
        private void GetMoviePoster()
        {
           
            pbPoster.BackgroundImage = selectedMovie.GetMoviePoster(TmdbWrapper.Utilities.PosterSize.w185);
        }
        public async void GetProductionCompanyLogos()
        {
            flpProductionCompanies.Controls.Clear(); // Remove any Images from previous load
            //Loop through each production Company IDs
            foreach (int pcId in selectedMovie.ProductionCompanies)
            {
                Image x;
                ProductionCompany c = u.GetProductionCompany(pcId); //Cached Version
                if (c != null)
                    x = await c.GetLogo(TmdbWrapper.Utilities.LogoSize.w45);
                else
                    x = await ProductionCompany.GetLogo(pcId, TmdbWrapper.Utilities.LogoSize.w45);
                if (x != null)
                {
                    PictureBox pb = new PictureBox();
                    pb.BackgroundImageLayout = ImageLayout.Zoom;
                    pb.Width = 45;
                    pb.Height = 45;
                    if (c.Homepage != null)
                    {
                        pb.Cursor = Cursors.Hand;
                        pb.Click += Pb_Click;
                        pb.Tag = c.Homepage;
                    }
                    pb.BackgroundImage = x;

                    flpProductionCompanies.Controls.Add(pb);

                }
            }
            
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            if ((sender as PictureBox).Tag != null)
            {
                String url = (sender as PictureBox).Tag.ToString();
                System.Diagnostics.Process.Start(url);
            }
        }
        private void pbPoster_Click(object sender, EventArgs e)
        {
            if ((sender as PictureBox).Tag != null)
            {
                String url = (sender as PictureBox).Tag.ToString();
                System.Diagnostics.Process.Start(url);
            }
        }
    }
}
