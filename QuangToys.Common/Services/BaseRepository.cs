using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuangToys.Common.Interfaces;
using QuangToys.Common.Models;

namespace QuangToys.Common.Services
{
    public class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IOptions<CosmosSettings> _cosmosSettings;
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger<BaseRepository<T>> _logger;
        private readonly string _containerName;
        public BaseRepository(IOptions<CosmosSettings> cosmosSettings, ILogger<BaseRepository<T>> logger, string containerName)
        {
            _cosmosSettings = cosmosSettings;
            ManagedIdentityCredential miCredential;
            string account = _cosmosSettings.Value.Account;
            string key = _cosmosSettings.Value.Key;
            if (string.IsNullOrEmpty(key))
            {
                miCredential = new ManagedIdentityCredential();
                _cosmosClient = new CosmosClient(account, miCredential);
            }
            else
            {
                _cosmosClient = new CosmosClient(account, key);
            }
            _logger = logger;
            _containerName = containerName;
            // we should not do this
            _cosmosClient
                    .GetDatabase(_cosmosSettings.Value.DatabaseName)
                    .CreateContainerIfNotExistsAsync(_containerName, "/id", 400).Wait();
        }
        public async Task<bool> Add(T entity)
        {
            try
            {
                
                await _cosmosClient
                .GetContainer(_cosmosSettings.Value.DatabaseName, _containerName)
                .CreateItemAsync<T>(entity, new PartitionKey(entity.Id.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await _cosmosClient
                .GetContainer(_cosmosSettings.Value.DatabaseName, _containerName)
                .DeleteItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<T> Get(Guid id)
        {
            var itemResponse = await _cosmosClient
                .GetContainer(_cosmosSettings.Value.DatabaseName, _containerName)
                .ReadItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
            return itemResponse.Resource;
        }

        public async Task<List<T>> GetAll()
        {
            var queryable = _cosmosClient
                .GetContainer(_cosmosSettings.Value.DatabaseName, _containerName)
                .GetItemLinqQueryable<T>();
            var iterator = queryable.ToFeedIterator();
            var feedResponse = await iterator.ReadNextAsync();
            return feedResponse.Resource.ToList();
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                await _cosmosClient
                .GetContainer(_cosmosSettings.Value.DatabaseName, _containerName)
                .ReplaceItemAsync<T>(entity, entity.Id.ToString());
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
