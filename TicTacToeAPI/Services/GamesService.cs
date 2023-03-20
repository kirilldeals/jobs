using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TicTacToeAPI.Models;

namespace TicTacToeAPI.Services
{
    public class GamesService
    {
        private readonly IMongoCollection<Game> _gamesCollection;

        public GamesService(IOptions<TictactoeDatabaseSettings> tictactoeDatabaseSettings)
        {
            var mongoClient = new MongoClient(tictactoeDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(tictactoeDatabaseSettings.Value.DatabaseName);
            _gamesCollection = mongoDatabase.GetCollection<Game>(tictactoeDatabaseSettings.Value.CollectionName);
        }

        public List<Game> Get()
        {
            return _gamesCollection.Find(_ => true).ToList();
        }

        public Game Get(string id)
        {
            return _gamesCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Game> Where(Expression<Func<Game, bool>> filter)
        {
            return _gamesCollection.Find(filter).ToList();
        }

        public void Create(Game game)
        {
            _gamesCollection.InsertOne(game);
        }
        public void Update(string id, Game updatedGame)
        {
            _gamesCollection.ReplaceOne(x => x.Id == id, updatedGame);
        }

        public void Remove(string id)
        {
            _gamesCollection.DeleteOne(x => x.Id == id);
        }
    }
}
