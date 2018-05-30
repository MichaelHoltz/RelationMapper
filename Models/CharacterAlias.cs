using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace RelationMap.Models
{
    /// <summary>
    /// Alias of a Character
    /// </summary>
    public class CharacterAlias
    {
        /// <summary>
        /// ID of alias
        /// </summary>
        [JsonProperty("A")]
        public int AliasId { get; set; }
        /// <summary>
        /// Name of Alias
        /// </summary>
        [JsonProperty("N")]
        public String Name { get; set; }

        /// <summary>
        /// Path to Profile Image (TBD web or local Cache)
        /// </summary>
        [JsonProperty("P")]
        public String ProfilePath { get; set; }
        /// <summary>
        /// Path to ThumbNail Image (TBD web or local Cache)
        /// </summary>
        [JsonProperty("T")]
        public String ThumbNailPath { get; set; }
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
            String key = this.GetType().Name + Name + AliasId;
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
