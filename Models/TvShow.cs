using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Abandoned for now as the base structure is not finalized and it's too much to maintain both for refactoring.
    /// </summary>
    public class TvShow
    {
        public String Name { get; set; }
        public HashSet<Character> Characters { get; set; }
        public int HashCode
        {
            get
            {
                return GetHashCode();
            }
        }
        public TvShow()
        {
            Name = null;
            Characters = new HashSet<Character>();
        }
        public TvShow(String name)
        {
            Name = name;
            Characters = new HashSet<Character>();
        }
        /// <summary>
        /// Compile Error now.
        /// </summary>
        /// <returns></returns>
        //////public Character AddCharacter(String characterName, String actorName, int order =0)
        //////{
        //////    Character c = new Character(characterName, actorName, order);
        //////    Characters.Add(c);
        //////    return c;
        //////}
        //////public Character GetCharacter(String characterName)
        //////{
        //////    return Characters.First(o => o.Name == characterName);
        //////}
        //////public HashSet<Person> GetActorsWhoPlayedCharacter(String characterName)
        //////{
        //////    Character c = Characters.First(o => o.Name == characterName);
        //////    Person a = c.Actors.First(); //First Actor need to fix this
        //////    return c.Actors;
        //////}
        //////public HashSet<Character> GetCharactersPlayedByActor(String actorName)
        //////{
        //////    HashSet<Character> cs = new HashSet<Character>();
        //////    foreach (Character c in Characters)
        //////    {
        //////        if (c.Actors.Select(o => o.Name).Contains(actorName))
        //////        {
        //////            cs.Add(c);
        //////        }
        //////    }
        //////    return cs;
        //////}
        public override int GetHashCode()
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(this.GetType().Name + Name));
            int ivalue = BitConverter.ToInt32(hashed, 0);
            return ivalue; 
        }
        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }
    }
}
