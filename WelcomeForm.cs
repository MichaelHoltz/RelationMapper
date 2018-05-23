using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RelationMap.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using TmdbWrapper;
using TheMovieDb = TmdbWrapper.TheMovieDb;
using TmdbSearch = TmdbWrapper.Search;

namespace RelationMap
{
    public partial class WelcomeForm : Form
    {
        Universe u;
        TmdbSearch.MovieSummary selectedMovieSummary = null;
        TmdbWrapper.Movies.Movie selectedMovieInfo = null;
        Movie selectedMovie = null;
        public WelcomeForm()
        {
            InitializeComponent();
            //Since this is a demo This is loaded for the full data universe
            u = PersistanceBase.Load<Universe>(PrivateData.GetRelativePath(@"\Cache\uinverse2.json"));
            
            
        }
        private async void WelcomeForm_Load(object sender, EventArgs e)
        {
            //   System.Threading.Thread.Sleep(2000);
            await TheMovieDb.Initialise(PrivateData.GetTMDBApiKey(), "en-US", true);
            Uri burl = TheMovieDb.GetConfigurationSecureBaseUrl();
           // tbTitleSearch.Text = "Iron Man";

            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
            acsc.AddRange(u.Movies.Select(o => o.Title).ToArray());
            
            tbTitleSearch.AutoCompleteCustomSource = acsc;
        }

        private async void btnTempGetStarted_Click(object sender, EventArgs e)
        {
            lbMovies.Items.Clear();
            String movieName = tbTitleSearch.Text;
            int searchYear = 0;

            //Lookup all the Movie information
            TmdbSearch.SearchResult<TmdbSearch.MovieSummary> movieSummaryList = await TheMovieDb.SearchMovieAsync(movieName, 1, false, searchYear);
            
            loadList(movieSummaryList); // Just for visual.. (and selection since not a single item typically)

        }
        private void loadList(TmdbSearch.SearchResult<TmdbSearch.MovieSummary> movieSummaryList)
        {
            foreach (TmdbSearch.MovieSummary item in movieSummaryList.Results)
            {
                lbMovies.Items.Add(item);
            }
            //Select the first movie if there are movies
            if (lbMovies.Items.Count > 0)
            {
                lbMovies.SelectedIndex = 0;
            }

        }
        /// <summary>
        /// A movie is selected and it's just supposed to be the preview information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lbMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                selectedMovieSummary = lbMovies.SelectedItem as TmdbSearch.MovieSummary; //Could get Poster Path from here if faster response is desired..
                selectedMovie = u.GetMovie(selectedMovieSummary.Id);
                if (selectedMovie == null)
                {
                    //Lookup Movie from Web
                    selectedMovieInfo = await selectedMovieSummary.MovieAsync();

                    selectedMovie = new Movie(selectedMovieInfo.Title, selectedMovieInfo.ReleaseDate.Value.Year);
                    selectedMovie.BackdropPath = selectedMovieInfo.BackdropPath;
                    selectedMovie.DmdbId = selectedMovieInfo.Id;
                    selectedMovie.ImdbId = selectedMovieInfo.ImdbId;
                    selectedMovie.OriginalTitle = selectedMovieInfo.OriginalTitle;
                    selectedMovie.Overview = selectedMovieInfo.Overview;
                    selectedMovie.PosterPath = selectedMovieInfo.PosterPath;
                    selectedMovie.ReleaseDate = selectedMovieInfo.ReleaseDate;
                    selectedMovie.Revenue = selectedMovieInfo.Revenue;
                    selectedMovie.Runtime = selectedMovieInfo.Runtime;
                    selectedMovie.HomePage = selectedMovieInfo.Homepage;
                    //Add Collection Id if one exists
                    if (selectedMovieInfo.BelongsToCollection != null)
                    {
                        selectedMovie.CollectionId = selectedMovieInfo.BelongsToCollection.Id;
                    }

                }
                else
                {
                    showActorsInMovie(selectedMovie);
                }

                movieInfoTip1.LoadMovieInfo(ref selectedMovie, ref u);
               

                
            }
        }

        public async Task<bool> SaveFullInfo(Movie m)
        {
            if (selectedMovieInfo == null)
            {
                //Lookup Movie from Web
                selectedMovieInfo = await selectedMovieSummary.MovieAsync();
            }
            //Add Production Companies
            foreach (var item in selectedMovieInfo.ProductionCompanies)
            {
                m.ProductionCompanies.Add(item.Id); // Add Ids to Movie
                TmdbWrapper.Companies.Company c = await item.CompanyAsync(); // await FindProductionCompany(item.Id);
                ProductionCompany pc = new ProductionCompany(item.Name, item.Id);
                pc.Headquarters = c.Headquarters;
                pc.Homepage = c.Homepage;
                pc.LogoPath = c.LogoPath;
                pc.OriginCountry = c.OriginCountry;
                //Add Parent Production Companies
                if (c.ParentCompany != null)
                {
                    pc.ParentCompanyId = c.ParentCompany.Id; // This could change based on movie release data.. (before purchase of A by B)
                }
                u.AddProductionCompany(pc);
            }
            //m.Characters
            //m.People
            //m.ProductionCompanies

            TmdbWrapper.Movies.Credits credits = await selectedMovieInfo.CastAsync();
            //credits.Cast
            foreach (TmdbWrapper.Movies.CastPerson item in credits.Cast)
            {

                m.People.Add(item.Id); // Add Person id to People in Movie

                //Get full person Info
                //THis is a problem because TMDB will return delay if getting too many at once.
                Thread.Sleep(1000); // Slow it down..
                TmdbWrapper.Persons.Person tmdbPerson = await item.PersonAsync(); 
                Person p = new Person(item.Name);
                p.Id = item.Id;
                p.ProfilePath = item.ProfilePath;
                p.HomePage = tmdbPerson.Homepage;
                p.Biography = tmdbPerson.Biography;
                p.Birthday = tmdbPerson.Birthday;
                p.Deathday = tmdbPerson.Deathday;
                p.PlaceOfBirth = tmdbPerson.PlaceOfBirth;
               

                u.AddPerson(p);
                m.AddCharacter(item.Character, item.Id, item.Order);
            }

            ////credits.Crew
            //foreach (TmdbWrapper.Movies.CrewPerson item in credits.Crew)
            //{

            //}

            TmdbWrapper.Movies.Trailers t = await selectedMovieInfo.TrailersAsync(); // Want to have.. does nothing now..


            u.AddMovie(m);
            PersistanceBase.Save(PrivateData.GetRelativePath(@"\Cache\uinverse2.json"), u);
            return true;
        }

        private async void btnSaveToMyMovies_Click(object sender, EventArgs e)
        {
            bool done = await SaveFullInfo(selectedMovie);
            showActorsInMovie(selectedMovie);

        }

        private void showActorsInMovie(Movie m)
        {
            //If viewing from Cache then the actors will be added at the same time as the move otherwise they must be added first..
            lbActors.Items.Clear();
            foreach (int item in m.People)
            {
                Person p = u.People.First(o => o.Id == item); // Look up person from Universe Person ID.
                lbActors.Items.Add(p);
            }
            

        }

        private void lbActors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbActors.SelectedIndex >= 0)
            {
                Person p = (lbActors.SelectedItem as Person);
                personInfoTip1.LoadPersonInfo(ref p, ref selectedMovie, ref u);
            }
        }

        private void btnFrmMain_Click(object sender, EventArgs e)
        {
            frmMain fm = new frmMain();
            fm.ShowDialog();
        }

        //public async Task<TmdbWrapper.Movies.Movie> FindFullMovieInfo(int movieId)
        //{
        //    //Use ID
        //    return await TheMovieDb.GetMovieAsync(movieId);
        //}

        //public async Task<TmdbWrapper.Companies.Company> FindProductionCompany(int pcId)
        //{
        //    return await TheMovieDb.GetCompanyAsync(pcId);
        //}
    }
}
