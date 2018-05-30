using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace RelationMap.Models
{
    /// <summary>
    /// Mapping between Crew and Movie and Person
    /// </summary>
    public class MovieCrewMap
    {
        [JsonProperty("M")]
        public int MovieId { get; set;}
        [JsonProperty("C")]
        public int CrewId { get; set; }
        [JsonProperty("P")]
        public int PersonId { get; set; }
        [JsonProperty("D")]
        public String CreditId { get; set; }
        public MovieCrewMap()
        {

        }
        public MovieCrewMap(int movieId, int personId, int crewId, String creditId)
        {
            MovieId = movieId;
            PersonId = personId;
            CrewId = crewId;
            CreditId = creditId;
        }
        #region Overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return MovieId + "," + CrewId + "," + PersonId + "," + CreditId;
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
            String key = this.GetType().Name + MovieId + CrewId + PersonId + CreditId;
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
