using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class CharacterFinder : Form
    {
        Universe u = new Universe();
        Movie m = new Movie();
        TmdbWrapper.Movies.Credits credits = new TmdbWrapper.Movies.Credits();
        public CharacterFinder()
        {
            InitializeComponent();
            u = PersistanceBase.Load<Universe>(PrivateData.GetRelativePath(@"\Cache\uinverse2.json"));
            //InitTheMovieDb();        
        }
        public CharacterFinder(Movie m1, Universe u1)
        {
            InitializeComponent();
            u = u1;
            m = m1;
           // InitTheMovieDb();
        }
        private void InitTheMovieDb()
        {
           // TheMovieDb.Initialise(PrivateData.GetTMDBApiKey(), "en-US", true);
        }

        private void CharacterFinder_Load(object sender, EventArgs e)
        {
            foreach (Movie item in u.GetAllMovies())
            {
                cbMovie.Items.Add(item.Title);
            }
            if (m != null)
            {
                cbMovie.SelectedItem = m.Title;
            }
        }

        private async void cbMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMovie.SelectedIndex >= 0)
            {
                lbCharacters.Items.Clear();
                lbActors.Items.Clear(); ;

                m = u.GetMovie(cbMovie.SelectedItem.ToString());
                credits = await TmdbWrapper.TheMovieDb.GetMovieCreditsAsync(m.DmdbId);
                foreach (TmdbWrapper.Movies.CastPerson item in credits.Cast)
                {
                    lbCharacters.Items.Add(item.Character);
                    //item.Character
                    //item.Id
                    //item.Name // Actor Name
                    //item.Order // Order of Character in Credits
                    //item.
                    lbActors.Items.Add(item.Name);
                }
            }
        }
    

        private void lbCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCharacters.SelectedIndex >= 0)
            {
                lbActors.SelectedIndex = lbCharacters.SelectedIndex; // Sync
            }
        }

        private void lbActor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbActors.SelectedIndex >= 0)
            {
                HashSet<Person> allActors = m.GetAllActors();
                foreach (Person a in allActors)
                {
                    if (a.Name == lbActors.SelectedItem.ToString())
                    {
                        
                        TmdbWrapper.Movies.CastPerson cp = credits.Cast.First(o => o.Name == a.Name);
                        propertyGrid1.SelectedObject = cp;
                        //This would be an update..
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //All Existing Characters
            foreach (Character c in m.Characters)
            {
                ////////foreach (Person a in c.Actors)
                ////////{
                ////////    TmdbWrapper.Movies.CastPerson cp = credits.Cast.First(o => o.Name == a.Name); // Match Actor To Character..
                ////////    //Some Cases where "Security Guard" is listed in Credits multiple times 1 for each actor, but that would mess up the ids and 
                ////////    //possibility of looking up an image as they are actually different security guards.. so probably include sort ID as part of 
                ////////    //Hash code to allow duplicate looking characters.

                ////////}

            }
            //Movie m = u.GetMovie(lbMovies.SelectedItem.ToString());
            //m.AddCharacter(tbCharacter.Text, tbActor.Text);
            //lbCharacters.Items.Add(tbCharacter.Text);
            //lbActors.Items.Add(tbActor.Text);
        }
    }
}
