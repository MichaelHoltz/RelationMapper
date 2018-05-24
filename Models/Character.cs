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
    /// 
    /// Characters are people and people play roles so this should be role
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Name of Character (Varies from movie to movie so see Aliases
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; } //Can't find character ID only Actor so far
        /// <summary>
        /// Order of Appearance in Movie
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Path to Picture of Character - But that would need to be manual and marked as such
        /// </summary>
        public String ProfilePath { get; set; } 
       
        /// <summary>
        /// Still not sure what this is.
        /// </summary>
        public int CastId { get; set; }

        public String CreditId { get; set; }
        /// <summary>
        /// Character Aliases
        /// </summary>
        public HashSet<String> Aliases {get; set;} 

        /// <summary>
        /// Actors that play this Character in this movie
        /// </summary>
        public HashSet<int> Actors { get; set; } 

        public Character()
        {
            Aliases = new HashSet<string>();
            Actors = new HashSet<int>();
        }
        public Character(String characterName, int actorId, int order, int castId, String creditId)
        {
            Actors = new HashSet<int>();
            Aliases = new HashSet<string>();
            Order = order;
            CastId = castId;
            CreditId = creditId;
            assignCharacter(characterName);
            Actors.Add(actorId);
        }
        /// <summary>
        /// Function to split names into aliases
        /// </summary>
        /// <param name="characterName"></param>
        private void assignCharacter(String characterName)
        {
            if (characterName != null)
            {
                Name = characterName;
                Char delimiter = '/'; // Alias Splitter
                String[] substrings = characterName.Split(delimiter);
                foreach (String item in substrings)
                {
                    Aliases.Add(item.Trim());
                }
            }

        }
        #region Overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        #endregion
        #region HashCodes / Object Identification
        //TODO - use / include the "correct" id..
        private int _hashCode = 0;
        public int HashCode
        {
            get
            {
                return _hashCode == 0 ? generateHashCode() : _hashCode;
            }
            //Need set for persistance to restore 
            set
            {
                _hashCode = value;
            }
        }
        private int generateHashCode()
        {
            //THis is expensive and should be done only once since it will not be changing
            //TODO - use / include the "correct" id..
            String key = this.GetType().Name + Name;
            //Google: "disable fips mode" if the line below fails
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));
            int ivalue = BitConverter.ToInt32(hashed, 0);
            return ivalue;

        }
        public override int GetHashCode()
        {
            return HashCode;
        }
        public override bool Equals(object obj)
        {
            return obj.GetHashCode().Equals(HashCode); // == this.GetHashCode();
        }
        #endregion
    }
}
