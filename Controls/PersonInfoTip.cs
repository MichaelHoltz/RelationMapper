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
            
            pbProfile.BackgroundImage = p.GetProfileImage(TmdbWrapper.Utilities.ProfileSize.w185);
            //Find Character(s !!!) this person played
            HashSet<Character> cpba = movie.GetCharactersPlayedByActor(p.Id);
            Character c = cpba.First();

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
    }
}
