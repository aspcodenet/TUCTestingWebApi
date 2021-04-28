﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TUCTestingWebApi.Services;
using TUCTestingWebApi.ViewModels;

namespace TUCTestingWebApi.Controllers
{
    public class HighscoreController : Controller
    {
        private readonly IHighscoreEntryRepository _highscoreEntryRepository;

        public HighscoreController(IHighscoreEntryRepository highscoreEntryRepository)
        {
            _highscoreEntryRepository = highscoreEntryRepository;
        }

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
                    Country = viewModel.Country,
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