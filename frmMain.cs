using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelationMap.Models;
using Newtonsoft.Json;
using TmdbWrapper;
namespace RelationMap
{
    public partial class frmMain : Form
    {
        Universe u;
        Studio marvel; 
        Studio dc;
        //Franchise mcu;
        //Franchise dcBatman;
        public frmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Seed and Testing of Universe
        /// </summary>
        private void InitializeUniverse()
        {
            u = new Universe(); // Contains all Studios
            
            marvel = u.AddStudio("Marvel"); // Add Studio to Universe
            //mcu = marvel.AddFranchise("Marvel Cinematic Universe"); //Add Franchise to Studio
            
            dc = u.AddStudio("DC"); //Add Studio to universe
            //dcBatman = dc.AddFranchise("DC Batman"); //Add Franchise to Studio

            InitializeStudio();
        }
        /// <summary>
        /// Seed and Testing of Studio
        /// </summary>
        private void InitializeStudio()
        {
            //TvShow Example added to Studio
            TvShow IncredibleHulk = marvel.AddTvShow("Incredible Hulk");
            IncredibleHulk.AddCharacter("Bruce Banner", "Bill Bixby");
            IncredibleHulk.AddCharacter("Hulk", "Lou Ferrigno");
            marvel.AddTvShow(IncredibleHulk);

            
            //Movies in MCU
            Movie Hulk = marvel.AddMovie("Hulk", 2003);
            Hulk.AddCharacter("Hulk", "Eric Bana");
            Hulk.AddCharacter("Betty Ross", "Jennifer Connelly");
            Hulk.AddCharacter("Thunderbolt Ross", "Sam Elliott");
            Hulk.AddCharacter("Brian Banner", "Nick Nolte");
            marvel.AddMovieToFranchise(Hulk, "Marvel Cinematic Universe");

            //Google: iron man 2008 cast
            Movie IronMan = marvel.AddMovie("Iron Man", 2008);
            IronMan.AddCharacter("Iron Man", "Robert Downey Jr.");
            IronMan.AddCharacter("Happy Hogan", "Jon Favreau");
            IronMan.AddCharacter("Pepper Potts", "Gwyneth Paltrow");
            IronMan.AddCharacter("War Machine", "Terrence Howard");
            IronMan.AddCharacter("Iron Monger", "Jeff Bridges");
            marvel.AddMovieToFranchise(IronMan, "Marvel Cinematic Universe");
            //Google iron man 2 cast

            Movie IronMan2 = marvel.AddMovie("Iron Man 2", 2010);
            IronMan2.AddCharacter("Iron Man", "Robert Downey Jr.");
            IronMan2.AddCharacter("Black Widow", "Scarlett Johansson");
            IronMan2.AddCharacter("Black Widow", "Charity Holtz"); // Testing multiple actors playing same role
            IronMan2.AddCharacter("Happy Hogan", "Jon Favreau");
            IronMan2.AddCharacter("War Machine", "Don Cheadle");
            IronMan2.AddCharacter("Ivan Vanko", "Mickey Rourke");
            IronMan2.AddCharacter("Pepper Potts", "Gwyneth Paltrow");
            IronMan2.AddCharacter("Nick Fury", "Samuel L. Jackson");
            IronMan2.AddCharacter("Justin Hammer", "Sam Rockwell");
            IronMan2.AddCharacter("Agent Phil Coulson", "Clark Gregg"); // Agent I believe..
            IronMan2.AddCharacter("Phil Coulson", "Clark Gregg"); // Test actor playing multiple roles
            //Console.WriteLine(marvel.GetFranchise("Marvel Cinematic Universe").IsMovieInFranchise(IronMan2));
            marvel.AddMovieToFranchise(IronMan2, "Marvel Cinematic Universe");
            //Console.WriteLine(marvel.GetFranchise("Marvel Cinematic Universe").IsMovieInFranchise(IronMan2));

            Movie IronMan3 = marvel.AddMovie("Iron Man 3", 2013);
            IronMan3.AddCharacter("Iron Man", "Robert Downey Jr.");
            marvel.AddMovieToFranchise(IronMan3, "Marvel Cinematic Universe");

            //Connected Data Idea https://www.themoviedb.org/person/74568-chris-hemsworth see also
            Movie ThorRagnorok = marvel.AddMovie("Thor: Ragnorok", 2017);
            ThorRagnorok.AddCharacter("Thor", "Chris Hemsworth");
            ThorRagnorok.AddCharacter("Hela", "Cate Blanchett");
            ThorRagnorok.AddCharacter("Valkyrie", "Tessa Thompson");
            ThorRagnorok.AddCharacter("Loki", "Tom Hiddleston");
            ThorRagnorok.AddCharacter("Korg", "Taika Waititi");
            ThorRagnorok.AddCharacter("Hulk", "Mark Ruffalo");
            ThorRagnorok.AddCharacter("Executioner", "Karl Urban");
            ThorRagnorok.AddCharacter("Grandmaster", "Jeff Goldblum");
            ThorRagnorok.AddCharacter("Heimdall", "Idris Elba");
            ThorRagnorok.AddCharacter("Odin", "Anthony Hopkins");
            ThorRagnorok.AddCharacter("Sif", "Jaimie Alexander");
            ThorRagnorok.AddCharacter("Doctor Steven Strange", "Benedict Cumberbatch");
            marvel.AddMovieToFranchise(ThorRagnorok, "Marvel Cinematic Universe");
            //Movies in DC and DCBatman
            //Change Studio 
            Movie batman = dc.AddMovie("Batman Returns", 1992);
            batman.AddCharacter("Batman", "Michael Keaton");
            batman.AddCharacter("Catwoman", "Michelle Pfeiffer");
            batman.AddCharacter("Penguin", "Danny DeVito");
            dc.AddMovieToFranchise(batman, "DC Batman");
            PersistanceBase.Save(PrivateData.GetRelativePath(@"\Cache\universe.json"), u);
        }
        private void BuildFranchise()
        {
            //foreach (Movie m in f.Movies)
            //{
            //    foreach (Actor a in m.Actors)
            //    {
            //        f.Actors.Add(a);
            //    }
            //    foreach (Character c in m.Characters)
            //    {
            //        f.Characters.Add(c);
            //    }
            //}
            //foreach (TvShow tv in f.TvShows)
            //{
            //    foreach (Actor a in tv.Actors)
            //    {
            //        f.Actors.Add(a);
            //    }
            //    foreach (Character c in tv.Characters)
            //    {
            //        f.Characters.Add(c);
            //    }
            //}
        }
        private void btnLoadFranchise_Click(object sender, EventArgs e)
        {
            u = PersistanceBase.Load<Universe>(PrivateData.GetRelativePath(@"\Cache\universe.json"));

            //PersistanceBase.Save(PrivateData.GetRelativePath(@"\Cache\universeBackup.json"), u); // Save a backup
            lbActors.Items.Clear();
            lbCharacters.Items.Clear();
            tbActor.Clear();
            tbCharacter.Clear();
            tbMovieTitle.Clear();
            tbReleaseYear.Clear();
            lbMovies.Items.Clear();
            lbTvShows.Items.Clear();
            refreshLists();
        }
        private void refreshLists()
        {
            //Clear Studio List 
            cbStudios.Items.Clear();
            cbStudios.Items.Add("All");
            //Load All Studios
            cbStudios.Items.AddRange(u.Studios.Select(o => o.Name).ToArray());
            cbStudios.SelectedIndex = 0;
        }
        private void lbMovies_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                lbTvShows.SelectedItems.Clear();
                Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
                lbActors.Items.Clear();
                lbCharacters.Items.Clear();
                HashSet<String> actors = new HashSet<string>(); // Prevent Duplicates in list
                foreach (Character c in m.Characters)
                {
                    lbCharacters.Items.Add(c.Name);
                    foreach (Actor a in c.Actors)
                    {
                        actors.Add(a.Name); // Use hash set to prevent duplicates
                        
                    }
                }
                foreach (String item in actors)
                {
                    lbActors.Items.Add(item);
                }
                HandleAddingButtons();
            }

        }
        private void lbTvShows_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbTvShows.SelectedIndex >= 0)
            {
                lbMovies.SelectedItems.Clear();
                TvShow tvs = u.GetTvShow(lbTvShows.SelectedItem.ToString());
                lbActors.Items.Clear();
                lbCharacters.Items.Clear();
                foreach (Character c in tvs.Characters)
                {
                    lbCharacters.Items.Add(c.Name);
                    foreach (Actor a in c.Actors)
                    {
                        lbActors.Items.Add(a.Name);
                    }
                }
                HandleAddingButtons();
            }
        }
        private void HandleAddingButtons()
        {
            btnAddToMovie.Enabled = lbMovies.SelectedIndex >= 0 && tbCharacter.TextLength > 0 && tbActor.TextLength > 0;
            btnAddToTvShow.Enabled = lbMovies.SelectedIndex >= 0 && tbCharacter.TextLength > 0 && tbActor.TextLength > 0;
            btnAddMovie.Enabled = cbStudios.SelectedItem.ToString() != "All" && tbMovieTitle.TextLength > 0 && tbReleaseYear.TextLength > 0; ; // Handle Toggle on Button
        }
        private void btnSaveFranchise_Click(object sender, EventArgs e)
        {
            ////BuildFranchise();
            PersistanceBase.Save(PrivateData.GetRelativePath(@"\Cache\universe.json"), u);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeUniverse();
           // InitializeFranchise();
        }

        private void btnAddToMovie_Click(object sender, EventArgs e)
        {
            Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
            m.AddCharacter(tbCharacter.Text, tbActor.Text);
            lbCharacters.Items.Add(tbCharacter.Text);
            lbActors.Items.Add(tbActor.Text);
        }

        private void btnAddToTvShow_Click(object sender, EventArgs e)
        {
            TvShow tvs = u.GetTvShow(lbTvShows.SelectedItem.ToString());
            tvs.AddCharacter(tbCharacter.Text, tbActor.Text);
            lbCharacters.Items.Add(tbCharacter.Text);
            lbActors.Items.Add(tbActor.Text);

        }

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            //Must have a selected Studio
            Studio s = u.GetStudio(cbStudios.SelectedItem.ToString());
            Movie m = s.AddMovie(tbMovieTitle.Text, Convert.ToInt32(tbReleaseYear.Text));
            String fr = cbFranchises.SelectedItem.ToString();

            if (fr != "All" && fr != "None")
            {
                s.AddMovieToFranchise(m, fr);
            }
        }

        /// <summary>
        /// Show the Character(s) played by this actor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbActors_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbActors.SelectedIndex >= 0)
            {
                tbActor.Text = lbActors.SelectedItem.ToString();
                lbCharacters.Items.Clear();
                HashSet<Character> cs = new HashSet<Character>();
                if (lbMovies.SelectedItems.Count > 0)
                {
                    Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
                    cs = m.GetCharactersPlayedByActor(tbActor.Text);
                    
                }
                else if(lbTvShows.SelectedItems.Count > 0)
                {
                    TvShow tv = u.GetTvShow(lbTvShows.SelectedItem.ToString());
                    cs = tv.GetCharactersPlayedByActor(tbActor.Text);
                }
                if (cs != null)
                {
                    foreach (Character item in cs)
                    {
                        lbCharacters.Items.Add(item.Name);
                    }
                }
            }
        }
        /// <summary>
        /// Show the Actor(s) played by this Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbCharacters_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbCharacters.SelectedIndex >= 0)
            {
                lbActors.Items.Clear();
                tbCharacter.Text = lbCharacters.SelectedItem.ToString();
                HashSet<Actor> a = null;
                if (lbMovies.SelectedItems.Count > 0)
                {
                    Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
                    a = m.GetActorsWhoPlayedCharacter(tbCharacter.Text);
                }
                else if (lbTvShows.SelectedItems.Count > 0)
                {
                    TvShow tv = u.GetTvShow(lbTvShows.SelectedItem.ToString());
                    a = tv.GetActorsWhoPlayedCharacter(tbCharacter.Text);
                }
                if (a != null)
                {
                    foreach (Actor item in a)
                    {
                        lbActors.Items.Add(item.Name);
                    }
                }
            }
        }
        /// <summary>
        /// When cbStudios changes Just Update Franchises
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbStudios_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbStudios.SelectedIndex >= 0)
            {
                //lbMovies.Items.Clear();
                //lbTvShows.Items.Clear();

                cbFranchises.Items.Clear();
                cbFranchises.Items.Add("All");
                cbFranchises.Items.Add("None");
                String studioStr = cbStudios.SelectedItem.ToString();
                if ( studioStr == "All")
                {
                    cbFranchises.Items.AddRange(u.GetAllFranchises().Select(o=>o.Name).ToArray());
                }
                else {
                    Studio s = u.GetStudio(studioStr);
                    cbFranchises.Items.AddRange(u.GetAllFranchises(s).Select(o => o.Name).ToArray());
                }

                cbFranchises.SelectedIndex = 0;
                HandleAddingButtons();
            }

        }

        private void cbFranchises_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbFranchises.SelectedIndex >= 0)
            {
                lbMovies.Items.Clear();
                lbTvShows.Items.Clear();
                String studioStr = cbStudios.SelectedItem.ToString();
                String franchiseStr = cbFranchises.SelectedItem.ToString();

                if (studioStr == "All") // All franchises
                {
                    foreach (Studio s in u.Studios)
                    {
                        if (franchiseStr == "All")
                        {
                            addFranchiseItems(s, null);
                        }
                        else if(franchiseStr == "None")
                        {
                            addNoFranchiseItems(s);
                        }
                        else
                        {
                            Franchise f = s.GetFranchise(franchiseStr);
                            if(f!=null)
                                addFranchiseItems(s, f);
                        }

                    }
                }
                else // Sepecific Franchise Choosen.
                {
                    Studio s = u.GetStudio(studioStr);
                    if (franchiseStr == "All")
                    {
                        addFranchiseItems(s, null);
                    }
                    else if (franchiseStr == "None")
                    {
                        addNoFranchiseItems(s);
                    }
                    else
                    {
                        Franchise f = s.GetFranchise(franchiseStr);
                        addFranchiseItems(s, f);
                    }
                }

            }
        }
        private void addFranchiseItems(Studio s, Franchise f)
        {
            foreach (Movie item in s.Movies)
            {
                if (f == null  || f.IsMovieInFranchise(item))
                {
                    lbMovies.Items.Add(item.Title);
                }
                
            }
            foreach (TvShow item in s.TvShows)
            {
                if (f == null || f.IsTvShowInFranchise(item))
                {

                    lbTvShows.Items.Add(item.Name);
                }
            }
        }
        private void addNoFranchiseItems(Studio s)
        {
            foreach (Franchise f in s.Franchises)
            {

                foreach (Movie item in s.Movies)
                {
                    if (!f.IsMovieInFranchise(item))
                    {
                        lbMovies.Items.Add(item.Title);
                    }

                }
                foreach (TvShow item in s.TvShows)
                {
                    if (!f.IsTvShowInFranchise(item))
                    {

                        lbTvShows.Items.Add(item.Name);
                    }
                }
            }

        }

        private void tbMovieTitle_TextChanged(object sender, EventArgs e)
        {
            HandleAddingButtons();
        }

        private void tbReleaseYear_TextChanged(object sender, EventArgs e)
        {
            HandleAddingButtons();
        }

        private void tbCharacter_TextChanged(object sender, EventArgs e)
        {
            HandleAddingButtons();
        }

        private void tbActor_TextChanged(object sender, EventArgs e)
        {
            HandleAddingButtons();
        }

        private void btnShowGraph_Click(object sender, EventArgs e)
        {
            frmGraphView fgv = new frmGraphView();
            fgv.ShowDialog();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            TheMovieDb.Initialise(PrivateData.GetTMDBApiKey(), "english", true);
           TmdbWrapper.Movies.Credits c = await TmdbWrapper.TheMovieDb.GetMovieCastAsync(299536); // ID for 
            foreach (var item in c.Cast)
            {
                lbCharacters.Items.Add(item.Character);
                lbActors.Items.Add(item.Name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Studio testStudio = u.AddStudio("TestStudio"); // Add Studio to Universe
            ////Movies in MCU
            //Movie TestMovie1 = testStudio.AddMovie("TestMovie1", 2018);
            //TestMovie1.AddCharacter("Character1", "Actor1");
            //TestMovie1.AddCharacter("Character2", "Actor2");
            //TestMovie1.AddCharacter("CharacterA", "ActorA");
            //testStudio.AddMovieToFranchise(TestMovie1, "TestFranchise");

            //Movie TestMovie2 = testStudio.AddMovie("TestMovie2", 2017);
            //TestMovie2.AddCharacter("Character1", "Actor1");
            //TestMovie2.AddCharacter("Character2", "Actor2");
            //TestMovie2.AddCharacter("CharacterB", "ActorB");
            //testStudio.AddMovieToFranchise(TestMovie2, "TestFranchise");

        }

        private void btnCharacterEditor_Click(object sender, EventArgs e)
        {
            Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
            CharacterFinder cf = new CharacterFinder(m, u);
            cf.ShowDialog();

        }
    }
}
