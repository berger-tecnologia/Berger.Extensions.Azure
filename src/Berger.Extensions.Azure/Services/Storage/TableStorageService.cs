using Azure.Data.Tables;

namespace Berger.Extensions.Azure.Storage
{
    public class TableStorageService
    {
        private readonly string _connectionString;

        public TableStorageService(TableStorageSettings settings, string connection)
        {
            _connectionString = connection;
        }

        private TableClient GetTableClient(string tableName)
        {
            var table = new TableClient(_connectionString, tableName);

            table.CreateIfNotExists();

            return table;
        }

        public async Task AddEntityAsync<T>(T entity, string tableName) where T : class, ITableEntity, new()
        {
            try
            {
                var table = GetTableClient(tableName);

                await table.AddEntityAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}