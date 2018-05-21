using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Actors Play Characters
    /// </summary>
    public class Actor
    {
        public String Name { get; set; }
        public String Prefix { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; } // Samuel L. Jackson
        public String LastName { get; set; }
        public String Suffix { get; set; } //Robert Downey Jr.
        public Actor(String actorName)
        {
            if (actorName != null)
            {
                Name = actorName;
                Char delimiter = ' ';
                String[] substrings = actorName.Split(delimiter);
                if (substrings.Length == 1)
                {
                    FirstName = LastName = actorName;
                }
                else if (substrings.Length == 2)
                {
                    FirstName = substrings[0];
                    LastName = substrings[1];
                }
                else if(substrings.Length == 3 ) // Middle name or Suffix or Doctor Steven Strange
                {
                    if (isSuffix(substrings[2]))
                    {
                        FirstName = substrings[0];
                        LastName = substrings[1];
                        Suffix = substrings[2];
                    }
                    else if (isPrefix(substrings[0]))
                    {
                        Prefix = substrings[0];
                        FirstName = substrings[1];
                        LastName = substrings[2];
                    }
                    else
                    {
                        FirstName = substrings[0];
                        MiddleName = substrings[1];
                        LastName = substrings[2];
                    }
                }
                

            }
            //Characters = new HashSet<Character>();
            //Movies = new HashSet<Movie>();
        }
        private Boolean isSuffix(String item)
        {
            Boolean result = false;
            switch (item.ToUpper())
            {
                case "JR":
                case "JR.":
                case "SR":
                case "SR.":
                    result = true;
                    break;
                default:
                    break;
            }
            return result;
        }
        private Boolean isPrefix(String item)
        {
            Boolean result = false;
            switch (item.ToUpper())
            {
                case "DOCTOR":
                case "DR":
                case "DR.":
                case "MR":
                case "MR.":
                case "MS":
                case "MS.":
                case "MRS":
                case "MRS.":

                    result = true;
                    break;
                default:
                    break;
            }
            return result;
        }
        public override int GetHashCode()
        {
            //var mystring = "abcd";
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(this.GetType().Name + Name));
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
