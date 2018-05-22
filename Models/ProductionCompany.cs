using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    /// <summary>
    /// Just for Movies - 
    /// </summary>
    public class ProductionCompany
    {
        public String Name { get; set; }
        /// <summary>
        /// Id of the production company
        /// </summary>
        public int Id { get; set; }

        public ProductionCompany()
        {
        }

        public ProductionCompany(String pcName)
        {
            Name = pcName;
        }
        public ProductionCompany(String pcName, int id)
        {
            Name = pcName;
            Id = id;
        }

    }
}
