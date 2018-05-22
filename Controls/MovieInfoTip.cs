using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmdbWrapper;
using RelationMap.Models;
using System.IO;
using System.Net;
namespace RelationMap.Controls
{
    public partial class MovieInfoTip : UserControl
    {
        private Movie selectedMovie;
        public MovieInfoTip()
        {
            InitializeComponent();
        }
        public void LoadMovieInfo(Movie movie)
        {
            selectedMovie = movie;
            lblTitle.Text = movie.Title;
            lblRunTime.Text = movie.Runtime.ToString() + " minutes";
            lblRevenue.Text = movie.Revenue.ToString("C0"); // Currancy no cents.
            GetMoviePoster();
            GetProductionCompanyLogos();
        }
        private async void GetMoviePoster()
        {
            //THis is cached already.. 
           // TmdbWrapper.Images.Image i = await TheMovieDb.GetMovieImagesAsync(selectedMovie.DmdbId);
           // selectedMovie.PosterPath

            String cachePath = PrivateData.GetAppPath() + @"\Cache\Images\Movie\" + selectedMovie.PosterPath.Replace("/","");
            Image x;
            if (File.Exists(cachePath))
            {
                x = Image.FromFile(cachePath);
            }
            else
            {
                //Need to fix this hack.. had to remove private set from base..
                TmdbWrapper.Search.MovieSummary ms = new TmdbWrapper.Search.MovieSummary();
                ms.PosterPath = selectedMovie.PosterPath;

                Uri uri = ms.Uri(TmdbWrapper.Utilities.PosterSize.w185);
                
                var wc = new WebClient();
                x = Image.FromStream(wc.OpenRead(uri)); // TODO-Implement Image Cache
                x.Save(cachePath);
            }
            pbPoster.BackgroundImage = x;
        }
        public async void GetProductionCompanyLogos()
        {
            flpProductionCompanies.Controls.Clear(); // Remove any Images from previous load
            foreach (ProductionCompany pc in selectedMovie.ProductionCompanies)
            {
                //TODO - need to do something about the time it takes to get Company info over and over..
                TmdbWrapper.Companies.Company c = await TheMovieDb.GetCompanyAsync(pc.Id);
                if (c.LogoPath != null)
                {
                    PictureBox pb = new PictureBox();
                    pb.BackgroundImageLayout = ImageLayout.Zoom;
                    pb.Width = 45;
                    pb.Height = 45;

                    String cachePath = PrivateData.GetAppPath() + @"\Cache\Images\production\" + c.LogoPath.Replace("/", "");
                    Image x;
                    if (File.Exists(cachePath))
                    {
                        x = Image.FromFile(cachePath);
                    }
                    else
                    {
                        Uri uri = c.Uri(TmdbWrapper.Utilities.LogoSize.w45);
                        var wc = new WebClient();
                        x = Image.FromStream(wc.OpenRead(uri)); // TODO-Implement Image Cache
                        x.Save(cachePath);
                    }
                    
                    pb.BackgroundImage = x;

                    flpProductionCompanies.Controls.Add(pb);

                }
            }
            
        }


    }
}
