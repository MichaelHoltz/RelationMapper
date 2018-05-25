using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmdbWrapper;
using TheMovieDb = TmdbWrapper.TheMovieDb;
using TmdbSearch = TmdbWrapper.Search;

namespace RelationMap
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            initTMDB();
            //Application.Run(new CharacterFinder());
            Application.Run(new WelcomeForm());
            //Application.Run(new frmMain());
        }
        /// <summary>
        /// Initialize The Movie DB
        /// </summary>
        private async static void initTMDB()
        {
            //There needs to be a global location for this as it 
            await TheMovieDb.Initialise(PrivateData.GetTMDBApiKey(), "en-US", true);
        }
    }
}
