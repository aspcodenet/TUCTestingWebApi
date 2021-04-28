using System.Collections.Generic;
using System.Linq;

namespace TUCTestingWebApi.Services
{
    public class HighscoreEntryRepository : IHighscoreEntryRepository
    {
        public static List<HighscoreEntry> entries = new List<HighscoreEntry>();



        public List<HighscoreEntry> GetAll()
        {
            return entries;
        }

        public HighscoreEntry Get(int id)
        {
            return entries.FirstOrDefault(r=>r.Id == id);
        }

        public HighscoreEntry RegisterNew(HighscoreEntry entry)
        {
            entry.Id = entries.Any() ? entries.Max(e => e.Id) + 1 : 0;
            entries.Add(entry);
            return entry;
        }
    }
}