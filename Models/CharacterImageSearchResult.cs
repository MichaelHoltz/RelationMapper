using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationMap.Models
{
    public class CharacterImageSearchResult
    {
        public String Title { get; set; }
        public String ContextLink { get; set; }
        public String Mime { get; set; }
        public String ImageLink { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public String ThumbnailLink { get; set; }
        public int ThumbnailWidth { get; set; }
        public int ThumbnailHeight { get; set; }
    }
}
