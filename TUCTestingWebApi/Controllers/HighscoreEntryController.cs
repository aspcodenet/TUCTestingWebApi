using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUCTestingWebApi.Services;

namespace TUCTestingWebApi.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/highscore")]
    public class HighscoreEntryController : ControllerBase
    {
        private readonly IHighscoreEntryRepository _highscoreEntryRepository;

        public HighscoreEntryController(IHighscoreEntryRepository highscoreEntryRepository)
        {
            _highscoreEntryRepository = highscoreEntryRepository;
        }

        [HttpGet]
        public IEnumerable<HighscoreEntry> Get()
        {
            return _highscoreEntryRepository.GetAll();
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var player = _highscoreEntryRepository.Get(id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<HighscoreEntryRepository> Create(HighscoreEntry player)
        {
            player = _highscoreEntryRepository.RegisterNew(player);

            return CreatedAtAction(nameof(Get), new { id = player.Id }, player);
        }


    }
}