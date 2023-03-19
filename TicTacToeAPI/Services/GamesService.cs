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

        public GamesService(IOptions<TictactoeDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _gamesCollection = mongoDatabase.GetCollection<Game>(bookStoreDatabaseSettings.Value.CollectionName);
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
