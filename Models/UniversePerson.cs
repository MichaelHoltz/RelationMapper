using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// UniversePerson
    /// </summary>
    public partial class Universe
    {
        /// <summary>
        /// All Actual People related to movies, TV Shows, and the production of them.
        /// </summary>
        public HashSet<Person> People { get; set; }

        public Boolean AddPerson(Person person)
        {
            Boolean result = People.Add(person);
            if (!result) // Movie already there, but need to update (If they aren't already) //But I could be passing in a person with basic info and t
            {
                Person existingPerson = People.First(o => o.Id == person.Id);
                if (existingPerson.Updated)
                {
                    result = true;
                }
                else
                {
                    People.Remove(person); // Hack at update by just replacing. 
                    result = People.Add(person);
                }
            }
            return result;
        }

        public Person GetPerson(String Name)
        {
            return People.FirstOrDefault(o => o.Name == Name);
        }
        public HashSet<Person> GetActorsInMovie(int movieId)
        {
            HashSet<Person> retVal = new HashSet<Person>();
            //For each MovieCharacterMap Entry for the movieId
            foreach (var mcm in MovieCharacterMap.Where(o => o.MovieId == movieId))
            {
                Person p = People.FirstOrDefault(o => o.Id == mcm.PersonId);
                if (p != null)
                    retVal.Add(p);
            }
            return retVal;
        }
        public HashSet<Person> GetActorsWhoPlayedCharacter(int characterId, int movieId)
        {
            HashSet<Person> retVal = new HashSet<Person>();
            //For each MovieCharacterMap Entry for the movieId
            foreach (var mcm in MovieCharacterMap.Where(o => o.MovieId == movieId && o.CharacterId == characterId))
            {
                Person p = People.FirstOrDefault(o => o.Id == mcm.PersonId);
                if (p != null)
                    retVal.Add(p);
            }
            return retVal;

        }
        public HashSet<Person> GetAllActors()
        {
            //////HashSet<Person> actors = new HashSet<Person>();
            //////foreach (Character c in this.Characters)
            //////{
            //////    foreach (Person a in c.Actors)
            //////    {
            //////        actors.Add(a);
            //////    }

            //////}
            //////return actors;
            return null;
        }
        //public HashSet<int> GetActorsWhoPlayedCharacter(String characterName)
        //{
        //    HashSet<int> results = new HashSet<int>();
        //    if (Characters.Select(o => o.Name == characterName).Contains(true))
        //    {
        //        Character c = Characters.First(o => o.Name == characterName);
        //       // results = c.Actors;
        //    }
        //    //////Character c = Characters.First(o => o.Name == characterName);
        //    //////Person a = c.Actors.First(); //First Actor need to fix this
        //    //////return c.Actors;
        //    return results;
        //}
    }
}
