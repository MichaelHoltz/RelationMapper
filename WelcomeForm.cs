﻿using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Net;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using RelationMap.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TmdbWrapper;
using TheMovieDb = TmdbWrapper.TheMovieDb;
using TmdbSearch = TmdbWrapper.Search;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Google.Apis.Customsearch.v1.Data;

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
            u = PersistenceBase.Load<Universe>(PrivateData.GetRelativePath(@"\Cache\uinverse3.json"));
            
            
        }
        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            //Allow Auto Complete of movies in known universe
            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
            //acsc.AddRange(u.Movies.Select(o => o.Title + " (" + o.ReleaseYear + ")").ToArray()); // Add Year - but it doesn't work for searching
            acsc.AddRange(u.Movies.Select(o => o.Title ).ToArray());

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
                lbMovies.Items.Add(item); //Whole Movie Summary is added to list 
            }
            //Select the first movie if there are movies
            if (lbMovies.Items.Count > 0)
            {
                lbMovies.SelectedIndex = 0;
                lbMovies.Focus(); // Set focus to Movie List
            }
            else
            {
                btnTempGetStarted.Focus(); // Set focus to Search Button
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
                TmdbSearch.MovieSummary ms = lbMovies.SelectedItem as TmdbSearch.MovieSummary;
                Movie m = null;
                // Need the Year or movies with same title will not show.
                if (ms.ReleaseDate.HasValue)
                {
                    m = u.GetMovie(ms.Title, ms.ReleaseDate.Value.Year); 
                }
                else
                {
                    m = u.GetMovie(ms.Title); //movie Summary has no release year so try to get it without. Only issue for unreleased movies it seems
                }
                
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
                    selectedMovieSummary = lbMovies.SelectedItem as TmdbSearch.MovieSummary;
                    selectedMovieInfo = await selectedMovieSummary.MovieAsync();
                    selectedMovie = m; //Need for Person Info 
                    movieInfoTip1.LoadMovieInfo(ref selectedMovie, ref u);
                    showActorsInMovie(selectedMovie);
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
            //This is a search Result for a specific movie
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
            selectedMovie.TmdbId = selectedMovieInfo.Id;
            selectedMovie.ImdbId = selectedMovieInfo.ImdbId;
            selectedMovie.OriginalTitle = selectedMovieInfo.OriginalTitle;
            selectedMovie.Overview = selectedMovieInfo.Overview;
            selectedMovie.PosterPath = selectedMovieInfo.PosterPath;
            selectedMovie.ReleaseDate = selectedMovieInfo.ReleaseDate;
            selectedMovie.Revenue = selectedMovieInfo.Revenue;
            selectedMovie.Runtime = selectedMovieInfo.Runtime;
            selectedMovie.HomePage = selectedMovieInfo.Homepage;
            //TODO - add remaining properties

            //Add Collection Id to movie ONLY if one exists
            if (selectedMovieInfo.BelongsToCollection != null)
            {
                //Add Collection ID to Movie
                //selectedMovie.CollectionId = selectedMovieInfo.BelongsToCollection.Id;
                MovieCollection mc =  u.GetMovieCollection(selectedMovieInfo.Id);
                if (mc == null)
                {
                    //Lookup selectedMovieInfo.BelongsToCollection.Id to see if it exists..
                    //Add Collection to Universe
                    MovieCollection m = u.AddMovieCollection(selectedMovieInfo.BelongsToCollection.Name, selectedMovieInfo.BelongsToCollection.Id, selectedMovieInfo.BelongsToCollection.PosterPath, selectedMovieInfo.BelongsToCollection.BackdropPath);

                    //ONly need to do this once per movie in the collection!!
                    //update the parts of the collection below for Universe
                    TmdbWrapper.Collections.Collection coll = await selectedMovieInfo.BelongsToCollection.CollectionAsync();
                    if (coll != null)
                    {
                        m.Overview = coll.Overview; //Update the Movie Collection Overview.
                        foreach (TmdbWrapper.Collections.Part p in coll.Parts)
                        {
                            u.AddMovieToCollection(m.Id, p.Id); // Add the movie ids of the parts to the Movie Collection Part

                            //m.Parts.Add(p.Id); // Add the movie ids of the parts to the Movie Collection Part
                        }
                    }
                    else
                    {
                        Console.WriteLine("Should never be here! Movie Belongs to a collection but Collection is null!");
                    }
                }
            }


            //Add Production Companies - Already have
            
            foreach (var item in selectedMovieInfo.ProductionCompanies)
            {
                //Try to get the Production Company and see if it has been updated if Null or not updated then Do web lookup

                ProductionCompany pc = u.GetProductionCompany(item.Id);
                if (u.GetProductionCompany(item.Id) == null || pc.Updated == false)
                {
                    TmdbWrapper.Companies.Company c = await item.CompanyAsync(); // await FindProductionCompany(item.Id);
                    pc = new ProductionCompany(item.Name, item.Id, item.LogoPath, item.OriginCountry);
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
                MovieProductionCompanyMap mpcm = new MovieProductionCompanyMap();
                mpcm.MovieId = selectedMovieInfo.Id;
                mpcm.ProductionCompanyId = item.Id;
                u.MovieProductionCompanyMap.Add(mpcm);

            }
            
            //Get the Movie Credits Now.
            TmdbWrapper.Movies.Credits credits = await selectedMovieInfo.CreditsAsync();
            //credits.Cast
            int personCount = 0;
            bool skip = false;
            int castLimit = 100;
            foreach (TmdbWrapper.Movies.CastPerson cp in credits.Cast)
            {
                //try
                //{
                    String cpCharacter = cp.Character;
                    if (cpCharacter.Length == 0) // Seriously??!!??
                    {
                        switch (cp.Gender)
                        {
                            case 0:
                                cpCharacter = "Themself";
                                break;
                            case 1:
                                cpCharacter = "Herself";
                                break;
                            case 2:
                                cpCharacter = "Himself";
                                break;
                        }
                       
                    }
                    ////Add Character To Movie (Could be Alias able character so mapping would be necessary)
                    Character c = u.AddCharacter(cpCharacter,
                        selectedMovie.TmdbId,
                        cp.Id,
                        cp.CreditId,
                        cp.CastId,
                        cp.Order
                        );
                //}
                //catch (Exception err)
                //{

                //}
                //Get full person Info
                //THis is a problem because TMDB will return delay if getting too many at once.
                //Also SERIOUSLY consider only getting the first 10% of the cast - but probably have them already from other movies..
                //Need on demand.
                Person p = new Person(cp.Name);
                p.Id = cp.Id;
                p.ProfilePath = cp.ProfilePath;
                skip = false;
                //See if we already have the person in the list
                if (u.People.Select(o => o.Id == cp.Id).Contains(true))
                {
                    //Get the person and see if they have been updated with full information
                    Person testPC = u.People.First(o => o.Id == cp.Id);
                    if (testPC.Updated)
                    {
                        skip = true;
                    }
                }
                if (!skip && personCount < 2)
                {
                    Thread.Sleep(100); // Slow it down..
                    TmdbWrapper.Persons.Person tmdbPerson = await cp.PersonAsync();
                    p.HomePage = tmdbPerson.Homepage;
                    p.Biography = tmdbPerson.Biography;
                    p.Birthday = tmdbPerson.Birthday;
                    p.Deathday = tmdbPerson.Deathday;
                    p.PlaceOfBirth = tmdbPerson.PlaceOfBirth;
                    p.Updated = true;
                    personCount++;
                }
                bool personAdded = u.AddPerson(p); // Will fail to add existing person and add basic and updated people.
                if (--castLimit == 0)
                    break;


            }

            ////credits.Crew - Adds significant numbers of People and data Entry
            #region Crew import
            int crewLimit = 10;
            foreach (TmdbWrapper.Movies.CrewPerson crewPerson in credits.Crew)
            {
                u.AddCrewMember(crewPerson, selectedMovie);
                if (--crewLimit == 0)
                    break;
            }
            #endregion

            //Want this for movie search results..
            TmdbWrapper.Movies.Trailers t = await selectedMovieInfo.TrailersAsync(); // Want to have.. does nothing now..
            if (t != null && t.Youtube.Count > 0)
            {
                String firstSource = t.Youtube[0].Source;
                //https://www.youtube.com/watch?v=CmRih_VtVAs // Example of how to use the source.
                selectedMovie.TrailerLink = "https://www.youtube.com/watch?v=" + firstSource;
            }
            

            u.AddMovie(selectedMovie);
            //Verify This is OK...
            PersistenceBase.Save(PrivateData.GetRelativePath(@"\Cache\uinverse3.json"), u);
            return true;
        }

        private async void btnSaveToMyMovies_Click(object sender, EventArgs e)
        {
            lbActors.Items.Clear();
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
            lbActors.Items.AddRange(u.GetActorsInMovie(m.TmdbId).ToArray());
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

        private void btnCharacterFinder_Click(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                Movie m = u.GetMovie(lbMovies.SelectedItem.ToString()); // Need the Year or movies with same title will not show.
                
                if (m != null)
                {
                    CharacterFinder cf = new CharacterFinder(m, u);
                    cf.ShowDialog();
                }
            }
            
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {

            ////Debugging Character /Alias Additions
            //u = new Universe(); // Clear previous
            //int m = 1;
            //int p = 1;
            //int cr = 1;
            //int cid = 1;
            //int cro = 0;
            ////u.AddCharacter("Agent Maria Hill", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Maria Hill", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Agent Maria Hill", m, p, "Cr" + cr++, cid++, cro++);

            //u.AddCharacter("Nick Fury (uncredited)", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Nick Fury", m, p, "Cr" + cr++, cid++, cro++);

            //u.AddCharacter("James \"Rhodey\" Rhodes / War Machine", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Lt. Col. James \"Rhodey\" Rhodes / War Machine", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Rhodey Rhodes", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Rhodey Rhodes / War Machine", m, p, "Cr" + cr++, cid++, cro++);


            //u.AddCharacter("Virginia \"Pepper\" Potts", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Pepper Potts", m, p, "Cr" + cr++, cid++, cro++);

            //u.AddCharacter("Bruce Banner / Hulk", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Bruce Banner / The Hulk", m, p, "Cr" + cr++, cid++, cro++);

            //u.AddCharacter("Black Widow (uncredited)", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Natasha Romanoff / Black Widow", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Natalie Rushman / Natasha Romanoff / Black Widow", m, p, "Cr" + cr++, cid++, cro++);

            //u.AddCharacter("Stephen Strange", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Doctor Stephen Strange", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Stephen Strange / Doctor Strange", m, p, "Cr" + cr++, cid++, cro++);

            //u.AddCharacter("Thor Odinson", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Thor", m, p, "Cr" + cr++, cid++, cro++);

            //u.AddCharacter("Loki", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Actor Loki", m, p, "Cr" + cr++, cid++, cro++);

            //u.AddCharacter("Cop", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Brave Cop", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Cop", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Brave Cop", m, p, "Cr" + cr++, cid++, cro++);

            //u.AddCharacter("Helicopter Pilot", m, p, "Cr" + cr++, cid++, cro++);
            //u.AddCharacter("Apache Helicopter Pilot", m, p, "Cr" + cr++, cid++, cro++);

            //////Try to use what I've Got...
            ////Movie m = u.GetMovie("Avengers: Infinity War"); // Check
            ////MovieCollection mc = u.GetMovieCollection(m.TmdbId);
            ////HashSet<Movie> movies =  u.GetMoviesInCollection(mc.Id);
            ////Person p = u.GetPerson("Chris Hemsworth");
            ////HashSet<Character> cs = u.GetCharactersPlayedByActor(p.Id); // Chris Hemsworth -> Thor and Thor Odinson - Need Alias Case
            ////p = u.GetPerson("Mark Ruffalo");
            ////cs = u.GetCharactersPlayedByActor(p.Id); // Mark Ruffalo - Need Alias Case (Bruce Banner / Hulk and Bruce Banner / The Hulk) Need Alias Case

            ////p = u.GetPerson("Scarlett Johansson");
            ////cs = u.GetCharactersPlayedByActor(p.Id); // Scarlett Johansson 
            ////p = u.GetPerson("Benedict Cumberbatch");
            ////cs = u.GetCharactersPlayedByActor(p.Id); // Scarlett Johansson 

            //PersistenceBase.Save(PrivateData.GetRelativePath(@"\Cache\uinverse3.json"), u);
            //foreach (Movie m in u.GetMoviesWithCharacter("Peggy Carter"))
            //{
            //    MessageBox.Show(m.Title);
            //}
            //List<Character> top20Characters = u.GetTopCharacters();
            String msg = "";
            foreach (Character item in u.GetTopCharacters(30))
            {
                msg = item.Name + " is in: \r\n\r\n";
                foreach (Movie m in u.GetMoviesWithCharacter(item.Id))
                {
                    msg += "( " + u.GetActorsWhoPlayedCharacter(item.Id, m.TmdbId).FirstOrDefault() + " )";
                    msg += m.Title + "\r\n";
                    
                }
                MessageBox.Show(msg);
            }
            //foreach (Movie m in u.GetMoviesWithCharacter("Peggy Carter"))
            //{
            //    MessageBox.Show(m.Title);
            //}

        }
    }
}
