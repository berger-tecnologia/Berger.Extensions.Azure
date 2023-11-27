using Azure.Data.Tables;

namespace Berger.Extensions.Azure
{
    public class TableStorageService
    {
        private readonly TableClient _tableClient;

        public TableStorageService(TableStorageSettings settings)
        {
            _tableClient = new TableClient(settings.ConnectionString, settings.TableName);

            _tableClient.CreateIfNotExists();
        }

        public async Task AddEntityAsync<T>(T entity) where T : class, ITableEntity, new()
        {
            try
            {
                await _tableClient.AddEntityAsync(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}