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
            ////Studio testStudio = u.AddStudio("TestStudio"); // Add Studio to Universe
            //////Movies in MCU
            ////Movie TestMovie1 = testStudio.AddMovie("TestMovie1", 2018);
            ////TestMovie1.AddCharacter("Character1", "Actor1");
            ////TestMovie1.AddCharacter("Character2", "Actor2");
            ////TestMovie1.AddCharacter("CharacterA", "ActorA");
            ////testStudio.AddMovieToFranchise(TestMovie1, "TestFranchise");

            ////Movie TestMovie2 = testStudio.AddMovie("TestMovie2", 2017);
            ////TestMovie2.AddCharacter("Character1", "Actor1");
            ////TestMovie2.AddCharacter("Character2", "Actor2");
            ////TestMovie2.AddCharacter("CharacterB", "ActorB");
            ////testStudio.AddMovieToFranchise(TestMovie2, "TestFranchise");
            //PersistanceBase.Save(PrivateData.GetRelativePath(@"\Cache\uinverse2.json"), u);
        }

        private void btnLoadFranchise_Click(object sender, EventArgs e)
        {
            u = PersistanceBase.Load<Universe>(PrivateData.GetRelativePath(@"\Cache\uinverse2.json"));

            //PersistanceBase.Save(PrivateData.GetRelativePath(@"\Cache\universeBackup.json"), u); // Save a backup
            lbActors.Items.Clear();
            lbCharacters.Items.Clear();
            tbActor.Clear();
            tbCharacter.Clear();
            tbMovieTitle.Clear();
            tbReleaseYear.Clear();
            lbMovies.Items.Clear();
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
                Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
                lbActors.Items.Clear();
                lbCharacters.Items.Clear();
                HashSet<String> actors = new HashSet<string>(); // Prevent Duplicates in list
                foreach (Character c in m.Characters)
                {
                    lbCharacters.Items.Add(c.Name);
                    //////foreach (Person a in c.Actors)
                    //////{
                    //////    actors.Add(a.Name); // Use hash set to prevent duplicates
                        
                    //////}
                }
                foreach (String item in actors)
                {
                    lbActors.Items.Add(item);
                }
                HandleAddingButtons();
            }

        }
        private void HandleAddingButtons()
        {
            btnAddToMovie.Enabled = lbMovies.SelectedIndex >= 0 && tbCharacter.TextLength > 0 && tbActor.TextLength > 0;
            btnAddMovie.Enabled = cbStudios.SelectedItem.ToString() != "All" && tbMovieTitle.TextLength > 0 && tbReleaseYear.TextLength > 0; ; // Handle Toggle on Button
        }
        private void btnSaveUniverse_Click(object sender, EventArgs e)
        {

            PersistanceBase.Save(PrivateData.GetRelativePath(@"\Cache\uinverse2.json"), u);
        }



        private void btnAddToMovie_Click(object sender, EventArgs e)
        {
            Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
            //////m.AddCharacter(tbCharacter.Text, tbActor.Text);
            lbCharacters.Items.Add(tbCharacter.Text);
            lbActors.Items.Add(tbActor.Text);
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
                    //////cs = m.GetCharactersPlayedByActor(tbActor.Text);
                    
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
                HashSet<Person> a = null;
                if (lbMovies.SelectedItems.Count > 0)
                {
                    Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
                    a = m.GetActorsWhoPlayedCharacter(tbCharacter.Text);
                }

                if (a != null)
                {
                    foreach (Person item in a)
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
                else // Specific Franchise Chosen.
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
            //foreach (TvShow item in s.TvShows)
            //{
            //    if (f == null || f.IsTvShowInFranchise(item))
            //    {

            //        lbTvShows.Items.Add(item.Name);
            //    }
            //}
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
                //foreach (TvShow item in s.TvShows)
                //{
                //    if (!f.IsTvShowInFranchise(item))
                //    {

                //        lbTvShows.Items.Add(item.Name);
                //    }
                //}
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





        private void btnCharacterEditor_Click(object sender, EventArgs e)
        {
            Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
            CharacterFinder cf = new CharacterFinder(m, u);
            cf.ShowDialog();

        }
        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            //Must have a selected Studio for relationships to work
            Studio s = u.GetStudio(cbStudios.SelectedItem.ToString());

            Movie m = s.AddMovie(tbMovieTitle.Text, Convert.ToInt32(tbReleaseYear.Text));

            String fr = cbFranchises.SelectedItem.ToString();

            if (fr != "All" && fr != "None")
            {
                s.AddMovieToFranchise(m, fr);
            }
        }
        private void btnFindMovie_Click(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                MovieFinder mf = new MovieFinder(lbMovies.SelectedItem.ToString());
                mf.ShowDialog();
            }
            else
            {
                MovieFinder mf = new MovieFinder();
                mf.ShowDialog();
            }

        }


    }
}
