using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    public class Movie
    {

        public String Title { get; set; }
        public String OriginalTitle { get; set; }

        //public DateTime ReleaseDate {get; set; }
        public int ReleaseYear { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String BackdropPath { get; set; }
        public String PosterPath { get; set; }
        public int DmdbId { get; set; }

        public HashSet<Character> Characters { get; set; }
        public int HashCode
        {
            get
            {
                return GetHashCode();
            }
        }
        
        public Movie()
        {
            Title = null;
            ReleaseYear = 1900;
            Characters = new HashSet<Character>();

        }
        public Movie(String name, int releaseYear)
        {
            Title = name;
            ReleaseYear = releaseYear;
            Characters = new HashSet<Character>(); // Characters in this Movie
        }
        public Character AddCharacter(String characterName, String actorName)
        {
            //Try to add the actor if the character Already exists.
            Character c = null;
            if (Characters.Select(o => o.Name).Contains(characterName))
            {
                c = Characters.First(o => o.Name == characterName);
                c.Actors.Add(new Actor(actorName));
            }
            else
            {
                c = new Character(characterName, actorName);
                Characters.Add(c);
            }
            return c;
        }
        public Character GetCharacter(String characterName)
        {
            return Characters.First(o => o.Name == characterName);
        }
        //public HashSet<Character> GetCharacters(String )
        public HashSet<Actor> GetActorsWhoPlayedCharacter(String characterName)
        {
            Character c = Characters.First(o => o.Name == characterName);
            Actor a = c.Actors.First(); //First Actor need to fix this
            return c.Actors;
        }
        public HashSet<Actor> GetAllActors()
        {
            HashSet<Actor> actors = new HashSet<Actor>();
            foreach (Character c in this.Characters)
            {
                foreach (Actor a in c.Actors)
                {
                    actors.Add(a);
                }
                
            }
            return actors;
        }

        public HashSet<Character> GetCharactersPlayedByActor(String actorName)
        {
            HashSet<Character> cs = new HashSet<Character>();
            foreach (Character c in Characters)
            {
                if (c.Actors.Select(o => o.Name).Contains(actorName))
                {
                    cs.Add(c);
                }
            }
            return cs;
        }
        public override int GetHashCode()
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(this.GetType().Name + Title + ReleaseYear.ToString()));
            int ivalue = BitConverter.ToInt32(hashed, 0);
            return ivalue; //base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
            //return base.Equals(obj);
        }

    }
}
