using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// UniverseCharacter - Code dealing with Character Relationships
    /// </summary>
    public partial class Universe
    {
        public int NextCharacterIndex { get; set; }
        
        /// <summary>
        /// All characters in all movies - (Seems could be role like Character or Crew - Producer etc.)
        /// </summary>
        public HashSet<Character> Characters { get; set; }
        public HashSet<MovieCharacterMap> MovieCharacterMap { get; set; }

        public int NextAliasIndex { get; set; }
        public HashSet<CharacterAlias> CharacterAliases { get; set; }
        public HashSet<CharacterAliasMap> CharacterAliasMap { get; set; }
        public Character AddCharacter(String characterName, int movieID, int personID, string creditID, int castID, int creditOrder)
        {
            //Try to add the actor if the character Already exists.
            Character c = Characters.FirstOrDefault(o => o.Name == characterName);
            //if (Characters.Select(o => o.Name).Contains(characterName))
            //{
            //    c = Characters.First(o => o.Name == characterName); //Direct Match of Character no need to worry about Alias Matching.
            //    c = Characters.FirstOrDefault(o => o.Name == characterName);
            //}
            //else
            if(c==null)
            {
                //See if there are Aliases to Search.
                HashSet<CharacterAlias> cas = findOrAddAliases(characterName);
                if (cas.Count > 0)
                {
                    c = new Character();
                    c.Name = characterName;
                    c.Id = NextCharacterIndex++;
                    Characters.Add(c);

                    //Already have Aliases but need to map them..
                    foreach (CharacterAlias ca in cas)
                    {
                        CharacterAliasMap cam = new Models.CharacterAliasMap();
                        cam.AliasID = ca.AliasId;
                        cam.CharacterID = c.Id;
                        CharacterAliasMap.Add(cam);
                    } 
                }
                else
                {
                    c = new Character();
                    c.Name = characterName;
                    c.Id = NextCharacterIndex++;
                    Characters.Add(c);
                }
            }

            //Add Mapping of Character / Actor / Role
            MovieCharacterMap mcm = new MovieCharacterMap(c.Id, c.Name, movieID, creditOrder, castID, creditID, personID);
            MovieCharacterMap.Add(mcm); // Add to Universe Map.

            return c;
        }
        /// <summary>
        /// Function to split names into aliases
        /// </summary>
        /// <param name="characterName"></param>
        private HashSet<CharacterAlias> findOrAddAliases(String characterName)
        {
            HashSet<CharacterAlias> retVal = new HashSet<CharacterAlias>(); // Default to empty set.
            //Name = characterName;
            Char delimiter = '/'; // Alias Splitter
            String[] substrings = characterName.Split(delimiter);
            if (substrings.Count() > 1)
            {
                foreach (String item in substrings)
                {
                    CharacterAlias ca = CharacterAliases.FirstOrDefault(o => o.Name == item);
                    if (ca == null)
                    {
                        ca = new CharacterAlias();
                        ca.Name = item.Trim();
                        ca.AliasId = NextAliasIndex++;
                        //Don't have the extended properties available from TMDB
                    }
                    CharacterAliases.Add(ca);
                    retVal.Add(ca);
                }
            }
            return retVal;
        }
        public String IdentifyCharacterFromTMDBAliasList(String aliasList)
        {
            //aliasList is Slash separated list
            HashSet<String> Aliases = new HashSet<string>();
            if (aliasList != null)
            {
                Char delimiter = '/'; // Alias Splitter
                String[] substrings = aliasList.Split(delimiter);
                foreach (String item in substrings)
                {
                    Aliases.Add(item.Trim());
                }
            }
            return aliasList;
        }
        private void AddAlias(String characterAliasName, int CharacterID)
        {

        }
        //public Character AddCharacter(String characterName,  int order, int castId, string creditId)
        //{
        //    //Try to add the actor if the character Already exists.
        //    Character c = null;
        //    if (Characters.Select(o => o.Name).Contains(characterName))
        //    {
        //        c = Characters.First(o => o.Name == characterName);
        //        c.Order = order;
        //        //c.Actors.Add(actorId);
        //    }
        //    else
        //    {
        //        c = new Character(characterName, actorId, order, castId, creditId);
        //        Characters.Add(c);
        //    }
        //    return c;
        //}
        //public Character GetCharacter(String characterName)
        //{
        //    return Characters.First(o => o.Name == characterName);
        //}
        //public HashSet<Character> GetCharacters(String )
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

        public HashSet<Character> GetCharactersPlayedByActor(int actorId)
        {
            HashSet<Character> cs = new HashSet<Character>();
            
            foreach (MovieCharacterMap c in MovieCharacterMap)
            {
                
                if (c.PersonId == actorId)
                {
                    Character mc = Characters.FirstOrDefault(o => o.Id == c.CharacterId);
                    if(mc != null)
                        cs.Add(mc);
                }
            }
            return cs;
        }
    }
}
