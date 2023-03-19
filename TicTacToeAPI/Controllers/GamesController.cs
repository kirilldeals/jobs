using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TicTacToeAPI.Models;
using TicTacToeAPI.Services;

namespace TicTacToeAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GamesController : Controller
    {
        private readonly GamesService _gamesService;

        public GamesController(GamesService gamesService)
        {
            _gamesService = gamesService;
        }

        [HttpGet]
        public IActionResult GetGames()
        {
            return Ok(_gamesService.Get());
        }

        [HttpGet("getByGameState")]
        public IActionResult GetGamesByGameState([FromQuery] GameState state)
        {
            return Ok(_gamesService.Where(g => g.GameState == state));
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult GetGame(string id)
        {
            var game = _gamesService.Get(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost("create")]
        public IActionResult CreateGame()
        {
            var game = new Game();
            _gamesService.Create(game);
            return CreatedAtAction(nameof(GetGame), new { game.Id }, game);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult MakeMove(string id, [Required] int row, [Required] int col)
        {
            if (row < 1 || row > 3 || col < 1 || col > 3)
            {
                return BadRequest("Values should be in the range of 1 - 3");
            }

            var game = _gamesService.Get(id);
            if (game == null)
            {
                return NotFound();
            }
            if (game.IsGameFinished())
            {
                return BadRequest($"The game has been finished");
            }
            if (!game.IsCellEmpty(row - 1, col - 1, out string player))
            {
                return BadRequest($"Player \"{player}\" already made a move in the cell ({row},{col})");
            }

            game.Move(row - 1, col - 1);
            _gamesService.Update(id, game);

            return Ok(game);
        }

        [HttpDelete("delete/{id:length(24)}")]
        public IActionResult DeleteGame(string id)
        {
            var game = _gamesService.Get(id);

            if (game is null)
            {
                return NotFound();
            }

            _gamesService.Remove(id);

            return Ok(game);
        }
    }
}
