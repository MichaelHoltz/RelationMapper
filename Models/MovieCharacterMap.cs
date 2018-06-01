using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RelationMap.Models
{
    /// <summary>
    /// Mapping class to allow characters to be shared between movies.
    /// Property Names are Short to conserve space in Universe..
    /// </summary>
    public class MovieCharacterMap
    {
        /// <summary>
        /// ID of Character
        /// </summary>
        [JsonProperty("C")]
        public int CharacterId { get; set; }
        /// <summary>
        /// Original Character Name 
        /// </summary>
        [JsonProperty("O")]
        public String OriginalCharacterName { get; set; }
        /// <summary>
        /// ID of Movie
        /// </summary>
        [JsonProperty("M")]
        public int MovieId { get; set; }
        /// <summary>
        /// Credit Order
        /// Order of Character appears in Movie
        /// </summary>
        [JsonProperty("CO")]
        public int CreditOrder { get; set; }
        /// <summary>
        /// Cast ID TMDB field related to Seasons and Episodes
        /// </summary>
        [JsonProperty("CI")]
        public int CastId { get; set; }
        /// <summary>
        /// Credit Id - unique to movie
        /// </summary>
        [JsonProperty("CRI")]
        public String CreditId { get; set; }
        /// <summary>
        /// ID of Person in this Role
        /// </summary>
        [JsonProperty("P")]
        public int PersonId { get; set; }
        public MovieCharacterMap()
        {
        }
        public MovieCharacterMap(int characterId, String originalCharacterName, int movieId, int creditOrder, int castId, String creditId, int personId)
        {
            CharacterId = characterId;
            OriginalCharacterName = originalCharacterName;
            MovieId = movieId;
            CreditOrder = creditOrder;
            CastId = castId;
            CreditId = creditId;
            PersonId = personId;

        }
        #region Overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return OriginalCharacterName;
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
            String key = this.GetType().Name + CharacterId + MovieId + CreditId + CreditOrder + CastId + PersonId + OriginalCharacterName;
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
            //if (obj is int)
            //{
            //    return HashCode.Equals((int)obj);
            //}
            //else
            //{
                return obj.GetHashCode().Equals(HashCode); // == this.GetHashCode();
            //}
        }

        #endregion
    }
}
