using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.IO;
using RelationMap.Models;
using TmdbWrapper;
using TheMovieDb = TmdbWrapper.TheMovieDb;
using TmdbSearch = TmdbWrapper.Search;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Google.Apis.Customsearch.v1.Data;
using System.Drawing.Drawing2D;

namespace RelationMap
{
    public partial class CharacterFinder : Form
    {
        Universe3 u = new Universe3();
        Movie m = new Movie();
        TmdbWrapper.Movies.Credits credits = new TmdbWrapper.Movies.Credits();
        public CharacterFinder()
        {
            InitializeComponent();
            u = PersistanceBase.Load<Universe3>(PrivateData.GetRelativePath(@"\Cache\uinverse3.json"));
            //InitTheMovieDb();        
        }
        public CharacterFinder(Movie m1, Universe3 u1)
        {
            InitializeComponent();
            u = u1;
            m = m1;

        }
        private void CharacterFinder_Load(object sender, EventArgs e)
        {
            foreach (Movie item in u.GetAllMovies())
            {
                cbMovie.Items.Add(item.Title);
            }
            if (m != null)
            {
                cbMovie.SelectedItem = m.Title;
            }
        }

        private async void cbMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMovie.SelectedIndex >= 0)
            {
                lbCharacters.Items.Clear();

                m = u.GetMovie(cbMovie.SelectedItem.ToString());
                credits = await TmdbWrapper.TheMovieDb.GetMovieCreditsAsync(m.TmdbId);
                foreach (TmdbWrapper.Movies.CastPerson item in credits.Cast)
                {
                    lbCharacters.Items.Add(item.Character);
                    //item.Character
                    //item.Id
                    //item.Name // Actor Name
                    //item.Order // Order of Character in Credits
                    //item.
                }
            }
        }
    

        private void lbCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCharacters.SelectedIndex >= 0)
            {
               // lbActors.SelectedIndex = lbCharacters.SelectedIndex; // Sync
            }
        }
        private void createImage(string thumbUrl, string fullUrl, int tnWidth, int tnHeight)
        {
            var wc = new WebClient();
            Image tnImage = Image.FromStream(wc.OpenRead(thumbUrl)); //Read from the Internet
            PictureBox pb = new PictureBox();
            pb.BackgroundImageLayout = ImageLayout.Zoom;
            if (tnWidth == 0)
                tnWidth = 100;
            if (tnHeight == 0)
                tnHeight = 100;
            pb.Width = tnWidth;
            pb.Height = tnHeight;
            pb.Cursor = Cursors.Hand;
            pb.Click += Pb_Click;
            pb.Tag = fullUrl;
            pb.BackgroundImage = tnImage;

            flpThumbNails.Controls.Add(pb);
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            if ((sender as PictureBox).Tag != null)
            {
                //Below to view it in Web Browser.
                String url = (sender as PictureBox).Tag.ToString();
                //System.Diagnostics.Process.Start(url);
                //HelperForms.CharacterImageMaker cim = new HelperForms.CharacterImageMaker();
                //cim.ShowDialog();
                String charName = lbCharacters.SelectedItem.ToString().Replace("/", "");
                HelperForms.CharacterImageMaker cim = new HelperForms.CharacterImageMaker(charName, url);
                cim.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flpThumbNails.Controls.Clear();
            //Example Image Search using Google API
            //Get my private API key
            string apiKey = PrivateData.GetGoogleApiKey();
            //Image Search engine.. Can change to other search engines
            string searchEngineId = PrivateData.GetGoogleSearchId();
            //Base Query
            Movie m = u.GetMovie(cbMovie.SelectedItem.ToString());
            int movieReleaseYear = m.ReleaseYear;
            String movieTitle = m.Title;
            String characterName = lbCharacters.SelectedItem.ToString().Replace("/", ""); // Remove slash if it exists.
            String actor = "";
            HashSet<int> peopleIDs = new HashSet<int>(); // m.GetActorsWhoPlayedCharacter(characterName);

            if (peopleIDs.Count() > 0)
            {
                int person1 = peopleIDs.First();
                actor = u.People.First(o => o.Id == person1).Name;
            }
            else
            {
                //Problem with lookup
            }

            string query = actor + " as " + characterName + " in " + movieTitle; // Need Movie and Actor example: Jared Leto as The Joker in Suicide Squad // 2016
            CharacterImageSearchResults cisr = new CharacterImageSearchResults();
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            var features = customSearchService.Features;
            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = searchEngineId;
            listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            listRequest.ImgType = CseResource.ListRequest.ImgTypeEnum.Face;
            listRequest.ExcludeTerms = "toys, collectables, figure";
            listRequest.Filter = CseResource.ListRequest.FilterEnum.Value1; // Turn on duplicate content filter.
            Console.WriteLine("Image Search for: " + query);
            IList<Result> paging = new List<Result>();
            var count = 0;
            while (paging != null)
            {
              //  Console.WriteLine($"Page {count}");
                listRequest.Start = count * 10 + 1;
                if (listRequest.Start >= 50)
                {
                    break; // DOn't want all results..  just first 5 pages (50 or so.
                }
                paging = listRequest.Execute().Items;
                if (paging != null)
                    foreach (var item in paging)
                    {
                        //Can and should filter by known Titles that would not be relevant (first through search engine and second here.)
                        //+ "Image :" + item.Image.ContextLink+
                        string tFilter = item.Title.ToLower();
                        if (tFilter.Contains("toys") || tFilter.Contains("tv") || tFilter.Contains("collectibles") || tFilter.Contains("figure") || tFilter.Contains("halloween"))
                        {
                            continue; //skip this one
                        }
                        Console.WriteLine("Title : " + item.Title + Environment.NewLine
                                        + "ContextLink: " + item.Image.ContextLink + Environment.NewLine
                                        + "MIME: " + item.Mime + Environment.NewLine
                                        + "Image Link: " + item.Link + Environment.NewLine
                                        + "Image Width: " + item.Image.Width + Environment.NewLine
                                        + "Image Height: " + item.Image.Height + Environment.NewLine
                                        + "Thumbnail Link: " + item.Image.ThumbnailLink + Environment.NewLine
                                        + "Thumbnail Width: " + item.Image.ThumbnailWidth + Environment.NewLine
                                        + "Thumbnail Height: " + item.Image.ThumbnailHeight + Environment.NewLine
                                        + Environment.NewLine);
                        CharacterImageSearchResult cis = new CharacterImageSearchResult();
                        cis.Title = item.Title;
                        cis.ContextLink = item.Image.ContextLink;
                        cis.Mime = item.Mime;
                        cis.ImageLink = item.Link;
                        cis.ImageWidth = (int)item.Image.Width;
                        cis.ImageHeight = (int)item.Image.Height;
                        cis.ThumbnailLink = item.Image.ThumbnailLink;
                        cis.ThumbnailWidth = (int)item.Image.ThumbnailWidth;
                        cis.ThumbnailHeight = (int)item.Image.ThumbnailHeight;
                        cisr.CharacterImageSearchResultsList.Add(cis);

                        createImage(item.Image.ThumbnailLink, item.Link, (int)item.Image.ThumbnailWidth, (int)item.Image.ThumbnailHeight);

                    }
                count++;
            }
            //Console.WriteLine("Done.");
            //Console.ReadLine();
            //No Slashes
            //No Colon
            //Can't have quotes (Escaped or otherwise)
            PersistanceBase.Save(PrivateData.GetAppPath() + @"\Private\" + query.Replace("/", "_").Replace(":", "") + ".json", cisr);
        }
        /// <summary>
        /// This function was created to avoid multiple google searches since it is limited to 100 a day.
        /// Saving the search results to File and loading from it saves the search and helps to figure out how to refine
        /// as well as developing the desired feature of being able to crop / scale and save for Character Profile and Icon (for graph)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            if (lbCharacters.SelectedIndex >= 0)
            {
                //todo - prefer Jared Leto as The Joker in Suicide Squad.json to just character name as they could be duplicates
                //Need same lookup as CharacterImageMaker to do that..
                string query = lbCharacters.SelectedItem.ToString(); 
                string filePath = PrivateData.GetAppPath() + @"\Private\" + query.Replace("/", "_") + ".json";
                if (File.Exists(filePath))
                {
                    CharacterImageSearchResults cisr =  PersistanceBase.Load<CharacterImageSearchResults>(filePath);
                    foreach (CharacterImageSearchResult r in cisr.CharacterImageSearchResultsList)
                    {
                        string tFilter = r.Title.ToLower();
                        if (tFilter.Contains("toys") || tFilter.Contains("tv") || tFilter.Contains("collectibles") || tFilter.Contains("figure") || tFilter.Contains("halloween"))
                        {
                            continue; //skip this one
                        }
                        createImage(r.ThumbnailLink, r.ImageLink, r.ThumbnailWidth, r.ThumbnailHeight);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HelperForms.CharacterImageMaker cim = new HelperForms.CharacterImageMaker();
            cim.ShowDialog();
        }
    }
}
