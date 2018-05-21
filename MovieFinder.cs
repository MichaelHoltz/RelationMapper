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
        Universe u;
        public MovieFinder()
        {
            InitializeComponent();
            u = PersistanceBase.Load<Universe>(@"C:\projects\RelationMapper\Cache\universe.json");
            TheMovieDb.Initialise(PrivateData.GetTMDBApiKey(), "english", true);
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            lbMovies.Items.Clear();
            
            TmdbWrapper.Search.SearchResult<TmdbWrapper.Search.MovieSummary> movieSummaryList = await TmdbWrapper.TheMovieDb.SearchMovieAsync(tbTitle.Text);
            foreach (TmdbWrapper.Search.MovieSummary item in movieSummaryList.Results)
            {
                lbMovies.Items.Add(item);
            }
            //TmdbWrapper.Movies.Credits c = await TmdbWrapper.TheMovieDb.GetMovieCastAsync(299536);
            //foreach (var item in c.Cast)
            //{
            //    lbCharacters.Items.Add(item.Character);
            //    lbActors.Items.Add(item.Name);
            //}
        }

        private async void lbMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                propertyGrid1.SelectedObject = lbMovies.SelectedItem;
                if ((lbMovies.SelectedItem as TmdbWrapper.Search.MovieSummary).PosterPath != null)
                {
                    TmdbWrapper.Images.Image i = await TheMovieDb.GetMovieImagesAsync((lbMovies.SelectedItem as TmdbWrapper.Search.MovieSummary).Id);
                    Uri uri = (lbMovies.SelectedItem as TmdbWrapper.Search.MovieSummary).Uri(TmdbWrapper.Utilities.PosterSize.w342);
                    var wc = new WebClient();
                    Image x = Image.FromStream(wc.OpenRead(uri));
                    pictureBox1.BackgroundImage = x;
                }
            }
        }

        private void MovieFinder_Load(object sender, EventArgs e)
        {
            tbTitle.Text = "Avengers: Infinity War";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmMain fm = new frmMain();
            fm.Show();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                TmdbWrapper.Search.MovieSummary ms = (lbMovies.SelectedItem as TmdbWrapper.Search.MovieSummary);
                foreach (Movie item in u.GetAllMovies())
                {
                    //Find Match
                    if (item.Title == ms.Title && item.ReleaseYear == ms.ReleaseDate.Value.Year)
                    {
                        item.ReleaseDate = ms.ReleaseDate.Value;
                        item.OriginalTitle = ms.OriginalTitle;
                        item.DmdbId = ms.Id;
                        item.BackdropPath = ms.BackdropPath;
                        item.PosterPath = ms.PosterPath;
                        if (MessageBox.Show("Are you sure you want to update: " + item.Title, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            PersistanceBase.Save(@"C:\projects\RelationMapper\Cache\universe.json", u);
                        }

                        break;
                    }
                }
                
            }
            
        }
    }
}
