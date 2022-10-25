using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Mongodb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mongodb.Services
{
    public class BlockedsService : IBlockedsService
    {
        private readonly IMongoCollection<BlockedsModel> _mongoCollection;
        private readonly IMapper _mapper;
        private const string collectionName = "Blockeds";

        public BlockedsService(IOptions<MongoDbConfig> mongoDbConfig, IMapper mapper)
        {
            var mongoClient = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbConfig.Value.DatabaseName);
            _mongoCollection = mongoDatabase.GetCollection<BlockedsModel>(collectionName);
            _mapper = mapper;
        }

        public async Task Add(string id, Domain.Entities.Profile profile)
        {
            var blockedsList = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (blockedsList is null)
            {
                await Create(id);

                blockedsList = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }

            if (blockedsList is null)
                throw new Exception("Lista de bloqueados não localizada");

            var profileModel = _mapper.Map<ProfileModel>(profile);

            blockedsList.BlockedList.Add(profileModel);
            await _mongoCollection.ReplaceOneAsync(x => x.Id == id, blockedsList);
        }

        public async Task Create(string id)
        {
            var newBlockeds = new BlockedsModel
            {
                Id = id,
                BlockedList = new List<ProfileModel>()
            };

            await _mongoCollection.InsertOneAsync(newBlockeds);
        }

        public async Task<Blockeds> GetBlockeds(string id)
        {
            var blockedsModel = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (blockedsModel is null)
                return null!;

            var blockeds = _mapper.Map<Blockeds>(blockedsModel);

            return blockeds;
        }

        public async Task Remove(string id, string friendId)
        {
            var blockeds = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (blockeds is null)
                return;

            var profileModel = blockeds.BlockedList.FirstOrDefault(x => x.Id == friendId);

            if (profileModel is null)
                return;

            blockeds.BlockedList.Remove(profileModel);

            await _mongoCollection.ReplaceOneAsync(x => x.Id == id, blockeds);
        }
    }
}
