using System;
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
    /// <summary>
    /// Graphical Display of Person information
    /// 
    /// Wanting to display Character in role for Character 
    /// Ex. Drax the Destroyer vs Dave Bautista
    ///     War Machine vs. Don Cheadle
    /// </summary>
    public partial class PersonInfoTip : UserControl
    {
        private Movie selectedMovie;
        private Universe u;
        private Person p;
        public PersonInfoTip()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(ref Person person, ref Movie movie, ref Universe univ)
        {
            p = person;
            selectedMovie = movie;
            u = univ;
            doWork();


            
        }
        private async void doWork()
        {
            //Check to see if the person has all available information
            if (!p.Updated)
            {
                p = await asyncUpdatePerson(p);
            }

            pbProfile.BackgroundImage = p.GetProfileImage(TmdbWrapper.Utilities.ProfileSize.w185);

            //Find Character(s !!!) this person played
            HashSet<Character> cpba = selectedMovie.GetCharactersPlayedByActor(p.Id);
            Character c = cpba.First();
            String tnPath = PrivateData.GetAppPath() + @"\Cache\Images\Characters\tn_" + c.Name.Replace("/","") + ".png";
            if (File.Exists(tnPath))
            {
                pbRole.BackgroundImage = Image.FromFile(tnPath);
            }
            else
            {
                pbRole.BackgroundImage = null;
            }

            lblRole.Text = c.Name;
            lblName.Text = p.Name;
            if (p.Deathday != null)
            {
                if (p.Birthday.HasValue && p.Deathday.HasValue)
                {
                    DateTime deathDay = p.Deathday.Value.Date;
                    lblBirthday.Text = p.Birthday.Value.ToShortDateString() + " - " + deathDay.ToShortDateString();
                }
            }
            else
            {
                if (p.Birthday.HasValue)
                {
                    lblBirthday.Text = p.Birthday.Value.Date.ToShortDateString();
                }
                else
                {
                    lblBirthday.Text = "";
                }
            }
            lblPlaceOfBirth.Text = p.PlaceOfBirth;
            lblBiography.Text = p.Biography;
        }
        public void Clear()
        {
            pbProfile.BackgroundImage = null;
            lblBiography.Text = null;
            lblBirthday.Text = null;
            lblName.Text = null;
            lblPlaceOfBirth.Text = null;
            lblRole.Text = null;
        }
        private void lblPlaceOfBirth_Click(object sender, EventArgs e)
        {
            //Bring Up Google Maps to see place.
            if (lblPlaceOfBirth.Text.Length > 0)
            {
                String url = "https://www.google.com/maps/place/" + lblPlaceOfBirth.Text.Replace(" ", "+").Trim();
                System.Diagnostics.Process.Start(url);
            }

        }
        private async Task<Person> asyncUpdatePerson(Person p)
        {
            TmdbWrapper.Persons.Person fullPerson = await TheMovieDb.GetPersonAsync(p.Id);
            p.HomePage = fullPerson.Homepage;
            p.Biography = fullPerson.Biography;
            p.Birthday = fullPerson.Birthday;
            p.Deathday = fullPerson.Deathday;
            p.PlaceOfBirth = fullPerson.PlaceOfBirth;
            p.Updated = true;
            return p;
        }

    }
}
