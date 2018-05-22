using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using TmdbWrapper;
using System.Net;
using RelationMap.Models;
namespace RelationMap
{
    public partial class MovieFinder : Form
    {
        TmdbWrapper.Search.SearchResult<TmdbWrapper.Search.MovieSummary> movieSummaryList;
        TmdbWrapper.Movies.Movie selectedMovieInfo;
        Universe u;
        String movieToFind = String.Empty;
        public MovieFinder()
        {
            InitializeComponent();
            u = PersistanceBase.Load<Universe>(PrivateData.GetRelativePath(@"\Cache\universe.json"));
            TheMovieDb.Initialise(PrivateData.GetTMDBApiKey(), "en-US", true);
        }
        public MovieFinder(String movieName)
        {
            InitializeComponent();
            u = PersistanceBase.Load<Universe>(PrivateData.GetRelativePath(@"\Cache\universe.json"));
            TheMovieDb.Initialise(PrivateData.GetTMDBApiKey(), "en-US", true);
            movieToFind = movieName;
        }
        private void MovieFinder_Load(object sender, EventArgs e)
        {
            tbTitle.Text = movieToFind;
            if (movieToFind != String.Empty)
            {
                FindMovie(movieToFind);
            }
        }
        /// <summary>
        /// Find and display the movie specified.
        /// </summary>
        /// <param name="movieName"></param>
        private async void FindMovie(String movieName)
        {
            lbMovies.Items.Clear();

            TmdbWrapper.Search.SearchResult<TmdbWrapper.Search.MovieSummary> movieSummaryList = await TmdbWrapper.TheMovieDb.SearchMovieAsync(movieName);
            foreach (TmdbWrapper.Search.MovieSummary item in movieSummaryList.Results)
            {
                lbMovies.Items.Add(item);
            }
            //Select the first movie if there are movies
            if (lbMovies.Items.Count > 0)
            {
                lbMovies.SelectedIndex = 0;
            }
            //TmdbWrapper.Movies.Credits c = await TmdbWrapper.TheMovieDb.GetMovieCastAsync(299536);
            //foreach (var item in c.Cast)
            //{
            //    lbCharacters.Items.Add(item.Character);
            //    lbActors.Items.Add(item.Name);
            //}

        }
        private async void btnFind_Click(object sender, EventArgs e)
        {
            FindMovie(tbTitle.Text);
        }

        /// <summary>
        /// User Selected a movie from the list
        /// Display Summary and Poster Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lbMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                int movieId = (lbMovies.SelectedItem as TmdbWrapper.Search.MovieSummary).Id;
                //propertyGrid1.SelectedObject = lbMovies.SelectedItem; // Show the properties of the movie
                if ((lbMovies.SelectedItem as TmdbWrapper.Search.MovieSummary).PosterPath != null)
                {
                    TmdbWrapper.Images.Image i = await TheMovieDb.GetMovieImagesAsync(movieId);
                    Uri uri = (lbMovies.SelectedItem as TmdbWrapper.Search.MovieSummary).Uri(TmdbWrapper.Utilities.PosterSize.w342);
                    var wc = new WebClient();
                    Image x = Image.FromStream(wc.OpenRead(uri)); // TODO-Implement Image Cache
                    pbMoviePoster.BackgroundImage = x;

                }
                //Get Full movie information
                selectedMovieInfo = await TmdbWrapper.TheMovieDb.GetMovieAsync(movieId);

                listBox1.Items.Clear();
                foreach (TmdbWrapper.Movies.ProductionCompany pc in selectedMovieInfo.ProductionCompanies)
                {
                    listBox1.Items.Add(pc.Name);
                }
                lblRevenue.Text = "Revenue: " + selectedMovieInfo.Revenue.ToString("C0");
                propertyGrid1.SelectedObject = selectedMovieInfo; // Show the properties of the movie



            }
        }
        private async void btnFullMovieInfo_Click(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                int movieId = (lbMovies.SelectedItem as TmdbWrapper.Search.MovieSummary).Id;

                selectedMovieInfo = await TmdbWrapper.TheMovieDb.GetMovieAsync(movieId, MovieExtras.Casts);

                listBox1.Items.Clear();
                foreach (TmdbWrapper.Movies.ProductionCompany pc in selectedMovieInfo.ProductionCompanies)
                {
                    listBox1.Items.Add(pc.Name);
                } 
                //propertyGrid1.SelectedObject = movieInfo; // Show the properties of the movie

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            frmMain fm = new frmMain();
            fm.Show();
        }

        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                //TmdbWrapper.Search.MovieSummary ms = (lbMovies.SelectedItem as TmdbWrapper.Search.MovieSummary);
                //TmdbWrapper.Movies.Movie selectedMovieInfo
                foreach (Movie item in u.GetAllMovies())
                {
                    //Find Match
                    if (item.Title == selectedMovieInfo.Title && item.ReleaseYear == selectedMovieInfo.ReleaseDate.Value.Year)
                    {
                        movieInfoTip1.LoadMovieInfo(item);
                        if (MessageBox.Show("Are you sure you want to update: " + item.Title, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                            item.ReleaseDate = selectedMovieInfo.ReleaseDate.Value;
                            item.OriginalTitle = selectedMovieInfo.OriginalTitle;
                            item.DmdbId = selectedMovieInfo.Id;
                            item.BackdropPath = selectedMovieInfo.BackdropPath;
                            item.PosterPath = selectedMovieInfo.PosterPath;
                            item.ImdbId = selectedMovieInfo.ImdbId;
                            item.Overview = selectedMovieInfo.Overview;
                            item.Runtime = selectedMovieInfo.Runtime;
                            item.Revenue = selectedMovieInfo.Revenue;
                            item.ProductionCompanies = new HashSet<ProductionCompany>(); // Clear existing
                            foreach (var pc in selectedMovieInfo.ProductionCompanies)
                            {
                                ProductionCompany s = new ProductionCompany(pc.Name);
                                s.Id = pc.Id;
                                item.ProductionCompanies.Add(s);
                            }
                            PersistanceBase.Save(PrivateData.GetRelativePath(@"\Cache\universe.json"), u);
                        }

                        break;
                    }
                }
                
            }
            
        }


    }
}
