﻿using System;
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
            if (movie.ReleaseDate.HasValue)
            {
                lblReleaseDate.Text = movie.ReleaseDate.Value.ToShortDateString();
                lblReleaseDate.Text += " (" + CalculateAge(movie.ReleaseDate.Value) + " Years ago)";

                //TimeSpan timeago = new TimeSpan();
                //timeago = DateTime.Now - movie.ReleaseDate.Value;
                //lblReleaseDate.Text += " (" + timeago.TotalDays + " days ago)";
            }
            else
            {
                lblReleaseDate.Text = "TBD";
            }

            lblRunTime.Text = movie.Runtime.ToString() + " minutes";
            lblRevenue.Text = movie.Revenue.ToString("C0"); // Currency no cents.
            //if (movie.HomePage != null)
            //{
            //    pbPoster.Tag = movie.HomePage.AbsoluteUri;
            //}
            //else
            //{
            //    pbPoster.Tag = null;
            //}
            if (movie.TrailerLink != null)
            {
                pbPoster.Tag = movie.TrailerLink;
            }
            else
            {
                pbPoster.Tag = null;
            }
            GetMoviePoster();
            GetProductionCompanyLogos();
        }
        public static int CalculateAge(DateTime BirthDate)
        {
            int YearsPassed = DateTime.Now.Year - BirthDate.Year;
            // Are we before the birth date this year? If so subtract one year from the mix
            if (DateTime.Now.Month < BirthDate.Month || (DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day < BirthDate.Day))
            {
                YearsPassed--;
            }
            return YearsPassed;
        }
        /// <summary>
        /// Load using Preview information..
        /// </summary>
        /// <param name="selectedMovieInfo"></param>
        public void LoadMovieInfo(ref TmdbWrapper.Movies.Movie selectedMovieInfo)
        {
            lblTitle.Text = selectedMovieInfo.Title;
            if (selectedMovieInfo.ReleaseDate.HasValue)
            {
                lblReleaseDate.Text = selectedMovieInfo.ReleaseDate.Value.ToShortDateString();
                lblReleaseDate.Text += " (" + CalculateAge(selectedMovieInfo.ReleaseDate.Value) + " Years ago)";
            }
            else
            {
                lblReleaseDate.Text = "TBD";
            }
            lblRunTime.Text = selectedMovieInfo.Runtime.ToString() + " minutes";
            lblRevenue.Text = selectedMovieInfo.Revenue.ToString("C0"); // Currency no cents.
            if (selectedMovieInfo.Homepage != null)
            {
                pbPoster.Tag = selectedMovieInfo.Homepage.AbsoluteUri;
            }
            else
            {
                pbPoster.Tag = null;
            }
            GetMoviePoster(ref selectedMovieInfo);
            //TmdbWrapper.Movies.ProductionCompany 
            IReadOnlyList<TmdbWrapper.Movies.ProductionCompany> prodCos = selectedMovieInfo.ProductionCompanies;
            GetProductionCompanyLogos(prodCos);
        }
        public void Clear()
        {
            pbPoster.BackgroundImage = null;
            lblTitle.Text = null;
            lblReleaseDate.Text = null;
            lblRevenue.Text = null;
            lblRunTime.Text = null;
        }
        /// <summary>
        /// Function for use with Cached Data
        /// </summary>
        private void GetMoviePoster()
        {
            pbPoster.BackgroundImage = selectedMovie.GetMoviePoster(TmdbWrapper.Utilities.PosterSize.w185);
        }
        /// <summary>
        /// Function for use with Preview Data
        /// </summary>
        /// <param name="selectedMovieInfo"></param>
        private void GetMoviePoster(ref TmdbWrapper.Movies.Movie selectedMovieInfo)
        {
            Uri posterUri = selectedMovieInfo.Uri(TmdbWrapper.Utilities.PosterSize.w185);
            var wc = new WebClient();
            pbPoster.BackgroundImage = Image.FromStream(wc.OpenRead(posterUri)); //Read from the Internet

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
                {
                    ProductionCompany pc = new ProductionCompany("dummy", pcId, null, null); // Hack
                    x = await pc.GetLogo(pcId, TmdbWrapper.Utilities.LogoSize.w45);
                }
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
        public void GetProductionCompanyLogos(IReadOnlyList<TmdbWrapper.Movies.ProductionCompany> prodCos)
        {
            flpProductionCompanies.Controls.Clear(); // Remove any Images from previous load
            //Loop through each production Company IDs
            foreach (TmdbWrapper.Movies.ProductionCompany pc in prodCos)
            {
                if (pc.LogoPath != null)
                {
                    PictureBox pb = new PictureBox();
                    pb.BackgroundImageLayout = ImageLayout.Zoom;
                    pb.Width = 45;
                    pb.Height = 45;
                    //Preview Production Companies don't have Homepage from Movie Info at this time.
                    //if (c.Homepage != null)
                    //{
                    //    pb.Cursor = Cursors.Hand;
                    //    pb.Click += Pb_Click;
                    //    pb.Tag = c.Homepage;
                    //}
                    Uri logoUri = pc.Uri(TmdbWrapper.Utilities.LogoSize.w45);
                    var wc = new WebClient();
                    pb.BackgroundImage = Image.FromStream(wc.OpenRead(logoUri)); //Read from the Internet
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
