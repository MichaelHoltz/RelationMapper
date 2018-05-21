using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Characters are in Movies and TvShows,
    /// Characters are played by Actors
    /// </summary>
    public class Character
    {
        public String Name { get; set; }
        public int Id { get; set; } //Can't find character ID only Actor so far
        public int Order { get; set; }
        public String ProfilePath { get; set; } // Same ID.
        
        //public String FirstName { get; set; }
        //public String MiddleName { get; set; } // Samuel L. Jackson
        //public String LastName { get; set; }
        //public String Suffix { get; set; } //Robert Downey Jr.
        public HashSet<String> Aliases {get; set;} // 
        public HashSet<Actor> Actors { get; set; } // Character has Actors.

        public Character()
        {
            Aliases = new HashSet<string>();
            Actors = new HashSet<Actor>();
        }
        public Character(String characterName)
        {
            Aliases = new HashSet<string>();
            Actors = new HashSet<Actor>();
            assignCharacter(characterName);
            
        }
        public Character(String characterName, String actorName)
        {
            Actors = new HashSet<Actor>();
            assignCharacter(characterName);
            Actors.Add(new Actor(actorName));
        }

        private void assignCharacter(String characterName)
        {
            if (characterName != null)
            {
                Name = characterName;
                Char delimiter = '/'; // Alias Splitter
                String[] substrings = characterName.Split(delimiter);
                //if (substrings.Length > 1)
                //{
                //    FirstName = substrings[0];
                //    LastName = substrings[1];
                //}
                //else
                //{
                //    FirstName = LastName = characterName;
                //}
                //Actors = new HashSet<Actor>();
                //Movies = new HashSet<Movie>();
            }

        }

        public override int GetHashCode()
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(this.GetType().Name + Name ));
            int ivalue = BitConverter.ToInt32(hashed, 0);
            Console.WriteLine(ivalue);
            return ivalue;
        }
        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }
    }
}
