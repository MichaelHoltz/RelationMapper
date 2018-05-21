using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace RelationMap
{
    /// <summary>
    /// Collection of functions used to be able to share the code and not give away private data / API Keys sepecifically 
    /// </summary>
    public static class PrivateData
    {
        /// <summary>
        /// Get the key from area not controlled by GIT
        /// </summary>
        /// <returns></returns>
        public static string GetTMDBApiKey()
        {
            //File Path 
            try
            {
                return File.ReadAllText(@"C:\projects\RelationMapper\Private\tmdbAPIKey.txt");
            }
            catch
            {
                MessageBox.Show(@"C:\projects\RelationMapper\Private\tmdbAPIKey.txt" + " Not Found. You need to provide your own API Key for TMDB");
                return "NoAPIKey";
            }
        }

    }
}
