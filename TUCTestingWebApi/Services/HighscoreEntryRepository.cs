using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TUCTestingWebApi.Services
{
    public class HighscoreEntryRepository : IHighscoreEntryRepository
    {
        public static List<HighscoreEntry> entries = new List<HighscoreEntry>();


        public List<HighscoreEntry> ReadListFromFile()
        {
            if (System.IO.File.Exists("C:\\Users\\stefa\\Temp\\highscore.txt") == false)
                return new List<HighscoreEntry>();

            var filecontents =
                File.ReadAllText("C:\\Users\\stefa\\Temp\\highscore.txt");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<HighscoreEntry>>(filecontents);
        }

        public void SaveListToFile(List<HighscoreEntry> list)
        {
            var contents = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            System.IO.File.WriteAllText("C:\\Users\\stefa\\Temp\\highscore.txt", contents);
        }

        public HighscoreEntryRepository()
        {
            entries = ReadListFromFile();
        }

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
            SaveListToFile(entries);
            return entry;
        }
    }
}