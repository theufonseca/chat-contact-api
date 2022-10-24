using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Mongodb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Mongodb.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IMongoCollection<FriendsModel> _mongoCollection;
        private readonly IMapper _mapper;
        private const string collectionName = "Friends";
        public FriendsService(IOptions<MongoDbConfig> mongoDbConfig, IMapper mapper)
        {
            var mongoClient = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbConfig.Value.DatabaseName);
            _mongoCollection = mongoDatabase.GetCollection<FriendsModel>(collectionName);
            _mapper = mapper;
        }

        public async Task Add(string id, Domain.Entities.Profile profile)
        {
            var friends = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (friends is null)
                throw new Exception("Lista de amigos não localizada");

            var friend = _mapper.Map<ProfileModel>(profile);
            friends.FriendList.Add(friend);

            await _mongoCollection.ReplaceOneAsync(x => x.Id == id, friends);
        }

        public async Task Create(string id)
        {
            var newFriends = new FriendsModel
            {
                Id = id,
                FriendList = new List<ProfileModel>()
            };

            await _mongoCollection.InsertOneAsync(newFriends);
        }

        public async Task<Friends> GetFriends(string id)
        {
            var friendsModel = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (friendsModel is null)
                return null!;

            var friends = _mapper.Map<Friends>(friendsModel);

            return friends;
        }
    }
}
