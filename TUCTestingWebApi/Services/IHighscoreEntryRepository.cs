using System.Collections.Generic;

namespace TUCTestingWebApi.Services
{
    public interface IHighscoreEntryRepository
    {
        public List<HighscoreEntry> GetAll();
        public HighscoreEntry Get(int id);
        public HighscoreEntry RegisterNew(HighscoreEntry entry);
    }
}