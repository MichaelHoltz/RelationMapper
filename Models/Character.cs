using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace RelationMap.Models
{
    /// <summary>
    /// Characters are in Movies and TvShows,
    /// Characters are played by Actors
    /// 
    /// Characters are people and people play roles so this should be role
    /// Characters have the most complicated relationship as they come from movies.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Character ID must be generated Manually as TMDB doesn't have a character ID
        /// </summary>
        [JsonProperty("A")]
        public int Id { get; set; } //Can't find character ID only Actor so far
        /// <summary>
        /// Name of Character (Varies from movie to movie so see Aliases
        /// </summary>
        [JsonProperty("B")]
        public String Name { get; set; }

        /// <summary>
        /// Path to Profile Image (TBD web or local Cache)
        /// </summary>
        [JsonProperty("C")]
        public String ProfilePath { get; set; }
        /// <summary>
        /// Path to ThumbNail Image (TBD web or local Cache)
        /// </summary>
        [JsonProperty("D")]
        public String ThumbNailPath { get; set; }

        public Boolean IsMatch(String name)
        {
            //Much more efficient would be to see if u.RoleMater Contains exact match and move on.

            //Split Name into parts
            Char delimiter = '/'; // Alias Splitter
            String[] substringsName = Name.Split(delimiter); // Split Source
            String[] substringsSearch = name.Split(delimiter); // Split search
            int maxConfidence = substringsName.Count();
            int confidence = 0;
            foreach (String n in substringsName)
            {
                foreach (String ns in substringsSearch)
                {
                    if (maxConfidence > 1 && ns.Contains(n))
                    {

                        confidence++;
                    }
                    else if (maxConfidence == 1 && ns == n)
                    {
                        confidence++;
                    }
                }
                if (confidence >= maxConfidence)
                {
                    break;
                }
            }
            return confidence >= maxConfidence;
        }


        public Character()
        {

        }
        public Character(String characterName)
        {
            Name = characterName;
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
        [JsonIgnore]
        [JsonProperty("H")]
        public int HashCode
        {
            get
            {
                return _hashCode == 0 ? generateHashCode() : _hashCode;
            }
            //Need set for persistence to restore 
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
