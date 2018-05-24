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
        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            ////   System.Threading.Thread.Sleep(2000);
            //await TheMovieDb.Initialise(PrivateData.GetTMDBApiKey(), "en-US", true);
            //Uri burl = TheMovieDb.GetConfigurationSecureBaseUrl();
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
                lbActors.Items.Clear();
                movieInfoTip1.Clear();
                personInfoTip1.Clear();
                Movie m = u.GetMovie(lbMovies.SelectedItem.ToString()); // Need the Year or movies with same title will not show.
                if (m == null)
                {
                    selectedMovieSummary = lbMovies.SelectedItem as TmdbSearch.MovieSummary; //Could get Poster Path from here if faster response is desired..
                    Uri summaryPosterPath = selectedMovieSummary.Uri(TmdbWrapper.Utilities.PosterSize.w154);
                    int summaryId = selectedMovieSummary.Id;

                    //Get Full information for the movie from Web 
                    selectedMovieInfo = await selectedMovieSummary.MovieAsync();
                    movieInfoTip1.LoadMovieInfo(ref selectedMovieInfo); // Show info basic info
                }
                else
                {
                    selectedMovie = m; //Need for Person Info 
                    movieInfoTip1.LoadMovieInfo(ref selectedMovie, ref u);
                    
                    foreach (int pid in m.People)
                    {
                        Person p = u.People.First(o => o.Id == pid);
                        lbActors.Items.Add(p);
                    }
                }
            }
        }

        public async Task<bool> SaveFullInfo(TmdbWrapper.Movies.Movie selectedMovieInfo)
        {
            if (selectedMovieInfo == null)
            {
                return false;
            }
            //The movie might already exist in the universe so this would replace it
            selectedMovie = u.GetMovie(selectedMovieSummary.Id);
            if (selectedMovie != null)
            {
                if (MessageBox.Show("Replace " + selectedMovie.Title + " with web data?", "Confirm Replace", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return false;
                }
            }

            //Clear or create            
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

            //Add Collection Id to movie ONLY if one exists
            if (selectedMovieInfo.BelongsToCollection != null)
            {
                //Add Collection ID to Movie
                selectedMovie.CollectionId = selectedMovieInfo.BelongsToCollection.Id;

                //Add Collection to Universe
                MovieCollection m = u.AddMovieCollection(selectedMovieInfo.BelongsToCollection.Name, selectedMovieInfo.BelongsToCollection.Id, selectedMovieInfo.BelongsToCollection.PosterPath, selectedMovieInfo.BelongsToCollection.BackdropPath);
                //update the parts of the collection below for Universe
                TmdbWrapper.Collections.Collection coll = await selectedMovieInfo.BelongsToCollection.CollectionAsync();
                if (coll != null)
                {
                    foreach (TmdbWrapper.Collections.Part p in coll.Parts)
                    {
                        m.Parts.Add(p.Id); // Add the movie ids of the parts to the Movie Collection Part
                    }
                }
                else
                {
                    Console.WriteLine("Should never be here! Movie Belongs to a collection but Collection is null!");
                }
            }


            //Add Production Companies - Already have
            
            foreach (var item in selectedMovieInfo.ProductionCompanies)
            {
                // Add Production Company Id to Movie
                selectedMovie.ProductionCompanies.Add(item.Id);
                
                if (u.ProductionCompanies.Select(o => o.Id == item.Id).Contains(true))
                {
                    ProductionCompany testPC = u.ProductionCompanies.First(o => o.Id == item.Id);
                    if (testPC != null && testPC.Updated)
                        break; // Skip this Production Company
                }
                //id, logo_path, name, origin_country  probably already have the full info so don't want to get this over and over
                TmdbWrapper.Companies.Company c = await item.CompanyAsync(); // await FindProductionCompany(item.Id);
                ProductionCompany pc = new ProductionCompany(item.Name, item.Id, item.LogoPath, item.OriginCountry);
                pc.Description = c.Description;
                pc.Headquarters = c.Headquarters;
                pc.Homepage = c.Homepage;
                pc.Updated = true; // Flag so it will not cause a lookup again.
                //Add Parent Production Companies
                if (c.ParentCompany != null)
                {
                    pc.ParentCompanyId = c.ParentCompany.Id; // This could change based on movie release data.. (before purchase of A by B)
                }
                u.AddProductionCompany(pc);
            }
            
            //Get the Movie Credits Now.
            TmdbWrapper.Movies.Credits credits = await selectedMovieInfo.CreditsAsync();
            //credits.Cast
            int personCount = 0;
            bool skip = false;
            foreach (TmdbWrapper.Movies.CastPerson item in credits.Cast)
            {
                //Add Person to Movie
                selectedMovie.People.Add(item.Id); // Add Person id to People in Movie
                //Add Character to Movie.. but really think I just want to add any new aliases to the Character
                //Aliases are derived from item.Character. 
                //Item.Id is the Actor Id
                //Item.Order is the order of appearance of the Character
                //There are no Character IDs from TMDB - Wait what is the cast_id??!!??
                selectedMovie.AddCharacter(item.Character, item.Id, item.Order, item.CastId, item.CreditId);


                //Get full person Info
                //THis is a problem because TMDB will return delay if getting too many at once.
                //Also SERIOUSLY consider only getting the first 10% of the cast - but probably have them already from other movies..
                //Need on demand.
                Person p = new Person(item.Name);
                p.Id = item.Id;
                p.ProfilePath = item.ProfilePath;
                skip = false;
                //See if we already have the person in the list
                if (u.People.Select(o => o.Id == item.Id).Contains(true))
                {
                    //Get the person and see if they have been updated with full information
                    Person testPC = u.People.First(o => o.Id == item.Id);
                    if (testPC.Updated)
                    {
                        skip = true;
                    }
                }
                if (!skip && personCount < 5)
                {
                    Thread.Sleep(100); // Slow it down..
                    TmdbWrapper.Persons.Person tmdbPerson = await item.PersonAsync();
                    p.HomePage = tmdbPerson.Homepage;
                    p.Biography = tmdbPerson.Biography;
                    p.Birthday = tmdbPerson.Birthday;
                    p.Deathday = tmdbPerson.Deathday;
                    p.PlaceOfBirth = tmdbPerson.PlaceOfBirth;
                    p.Updated = true;
                    personCount++;
                }
                u.AddPerson(p); // Will fail to add existing person and add basic and updated people.



            }

            ////credits.Crew
            //foreach (TmdbWrapper.Movies.CrewPerson item in credits.Crew)
            //{

            //}

            TmdbWrapper.Movies.Trailers t = await selectedMovieInfo.TrailersAsync(); // Want to have.. does nothing now..


            u.AddMovie(selectedMovie);
            //Verify This is OK...
            PersistanceBase.Save(PrivateData.GetRelativePath(@"\Cache\uinverse2.json"), u);
            return true;
        }

        private async void btnSaveToMyMovies_Click(object sender, EventArgs e)
        {
            bool done = await SaveFullInfo(selectedMovieInfo);
            if (done)
            {
                showActorsInMovie(selectedMovie);
            }

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
