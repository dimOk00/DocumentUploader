using DocumentUploader.Data;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

using System.Configuration;

namespace DocumentUploader.Services
{


    public class CosmosService
    {
        private const string dbname = "documentsdb";
        private const string containerName = "documentscontainer";
        
        private readonly CosmosClient _client;
        private Database _db;
        private Container _container;
        
        private Task<DatabaseResponse> _dbCreateTask;


        public CosmosService(IConfiguration configuration)
        {
            _client = new CosmosClient(
                connectionString: configuration.GetSection("DB").GetValue<string>("ConnectionString"));

           _dbCreateTask = _client.CreateDatabaseIfNotExistsAsync(dbname);
        }

        public async Task<IEnumerable<FileMetadata>> GetFilesMetadata()
        {
            await Initialize();

            var result = new List<FileMetadata>();

            var iterator = _container.GetItemLinqQueryable<FileMetadata>().ToFeedIterator();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                result.AddRange(response);
            }

            return result;
        }

        public async Task AddFileMetadata(FileMetadata file)
        {
            await Initialize();

            await _container.CreateItemAsync(file, new PartitionKey(file.Id));

        }


        public async Task UpdateFileMetadata(FileMetadata file)
        {
            await Initialize();

            await _container.UpsertItemAsync(file);

         }

        public async Task DeleteFileMetadata(FileMetadata file)
        {
            await Initialize();

            await _container.DeleteItemAsync<FileMetadata>(file.Id.ToString(), new PartitionKey(file.Id.ToString()));
        }

        private async Task Initialize()
        {
            _db = (await _dbCreateTask).Database;
            _container = (await _db.CreateContainerIfNotExistsAsync(new ContainerProperties(containerName, "/id"))).Container;
        }
    }
}
