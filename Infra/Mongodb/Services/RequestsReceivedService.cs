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
    public class RequestsReceivedService : IRequestsReceivedService
    {
        private readonly IMongoCollection<RequestsReceivedModel> _mongoCollection;
        private readonly IMapper _mapper;
        private const string collectionName = "RequestsReceiveds";
        public RequestsReceivedService(IOptions<MongoDbConfig> mongoDbConfig, IMapper mapper)
        {
            var mongoClient = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbConfig.Value.DatabaseName);
            _mongoCollection = mongoDatabase.GetCollection<RequestsReceivedModel>(collectionName);
            _mapper = mapper;
        }

        public async Task Add(string id, Domain.Entities.Profile profile)
        {
            var requestReceiveds = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if(requestReceiveds is null)
            {
                await Create(id);
                requestReceiveds = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }

            if (requestReceiveds is null)
                throw new Exception("Lista de solicitações recebidas não localizada");

            var profileModel = _mapper.Map<ProfileModel>(profile);

            requestReceiveds.RequestReceivedList.Add(profileModel);
            await _mongoCollection.ReplaceOneAsync(x => x.Id == id, requestReceiveds);
        }

        public async Task Create(string id)
        {
            var newRequestsReceiveds = new RequestsReceivedModel
            {
                Id = id,
                RequestReceivedList = new List<ProfileModel>()
            };

            await _mongoCollection.InsertOneAsync(newRequestsReceiveds);
        }

        public async Task<RequestsReceiveds> Get(string id)
        {
            var requestReceivedModel = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (requestReceivedModel is null)
                return null!;

            var requestsReceived = _mapper.Map<RequestsReceiveds>(requestReceivedModel);

            return requestsReceived;
        }

        public async Task Delete(string id, string friendId)
        {
            var requestsReceived = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (requestsReceived is null)
                return;

            var requestReceived = requestsReceived.RequestReceivedList.FirstOrDefault(x => x.Id == friendId);

            if (requestReceived is null)
                return;

            requestsReceived.RequestReceivedList.Remove(requestReceived);

            await _mongoCollection.ReplaceOneAsync(x => x.Id == id, requestsReceived);
        }
    }
}
