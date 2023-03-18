using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GamesController : Controller
    {
        static List<Game> games = new List<Game> 
        {
            new Game(),
            new Game(),
            new Game()
        };


        [HttpGet]
        public IActionResult GetGames()
        {
            return Ok(games);
        }

        [HttpGet("getByGameState")]
        public IActionResult GetGamesWithGameState([FromQuery] GameState state)
        {
            return Ok(games.Where(g => g.State == state));
        }

        [HttpGet("getByCurrentPlayer")]
        public IEnumerable<Game> GetGamesWithCurrentPlayer([FromQuery] Player player)
        {
            return games.Where(g => g.CurrentPlayer == player);
        }

        [HttpGet("{id}")]
        public IActionResult GetGame(Guid id)
        {
            var game = games.FirstOrDefault(x => x.Id == id);
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
            games.Add(game);
            return Ok(game);
        }

        [HttpPut("{id}")]
        public IActionResult MakeMove(Guid id, [Required] int row, [Required] int col)
        {
            if (row < 1 || row > 3 || col < 1 || col > 3)
            {
                return BadRequest("Values should be in the range of 1 - 3");
            }

            var game = games.FirstOrDefault(x => x.Id == id);
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
            return Ok(game);
        }
    }
}
