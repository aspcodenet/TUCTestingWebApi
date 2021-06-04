using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TUCTestingWebApi.Services;
using TUCTestingWebApi.ViewModels;

namespace TUCTestingWebApi.Controllers
{
    //   /highscore
    public class HighscoreController : Controller
    {
        private readonly IHighscoreEntryRepository _highscoreEntryRepository;

        public HighscoreController(IHighscoreEntryRepository highscoreEntryRepository)
        {
            _highscoreEntryRepository = highscoreEntryRepository;
        }

        // /Highscore    
        public IActionResult Index()
        {
            var viewModel = new HighscoreListaViewModel();
            viewModel.Items = _highscoreEntryRepository.GetAll().Select(r=>
                new HighscoreListaViewModel.HighscoreListEntry
                {
                    Game = r.Game,
                    Id = r.Id,
                    Namn = r.Namn,
                    Points = r.Points
                }).ToList();
            return View(viewModel);
        }

        // Highscore/Edit/0    Highscore/Edit/1 
        public IActionResult Edit(int id)
        {
            var viewModel = new HighscoreEditViewModel();

            var entryFromDatabase = _highscoreEntryRepository.Get(id);
            viewModel.Country = entryFromDatabase.Country;
            viewModel.Game = entryFromDatabase.Game;
            viewModel.Id = entryFromDatabase.Id;
            viewModel.Namn = entryFromDatabase.Namn;
            viewModel.Points = entryFromDatabase.Points;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(HighscoreEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entryFromDatabase = _highscoreEntryRepository.Get(viewModel.Id);
                entryFromDatabase.Country = viewModel.Country;
                entryFromDatabase.Game = viewModel.Game;
                entryFromDatabase.Id = viewModel.Id;
                entryFromDatabase.Namn = viewModel.Namn;
                entryFromDatabase.Points = viewModel.Points;
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }




        // /Highscore/New

        public IActionResult New()
        {
            var viewModel = new HighscoreNewViewModel();
            return View(viewModel);
        }




        [HttpPost]
        public IActionResult New(HighscoreNewViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _highscoreEntryRepository.RegisterNew(new HighscoreEntry
                {
                    Game = viewModel.Game,
                    Points = viewModel.Points,
                    Namn = viewModel.Namn
                });
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }


    }
}