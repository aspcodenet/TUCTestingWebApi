using System.Collections.Generic;

namespace TUCTestingWebApi.ViewModels
{
    public class HighscoreListaViewModel
    {
        public List<HighscoreListEntry> Items { get; set; }

        public class HighscoreListEntry
        {
            public int Id { get; set; }
            public string Game { get; set; }
            public string Namn { get; set; }
            public int Points { get; set; }
        }
    }
}