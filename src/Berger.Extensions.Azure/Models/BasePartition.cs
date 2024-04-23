using Azure;
using Azure.Data.Tables;
using Berger.Extensions.Abstractions;

namespace Berger.Extensions.Azure
{
    public class BasePartition : BaseEntity, ITableEntity
    {
        #region Properties
        public string PartitionKey { get; set; } = string.Empty;
        public string RowKey { get; set; } = string.Empty;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        #endregion

        #region Methods
        public void SetKeys(string code, Guid deviceId)
        {
            PartitionKey = code;
            RowKey = deviceId.ToString();
        }
        public void SetDates()
        {
            CreatedOn = DateTime.UtcNow;
            Timestamp = DateTimeOffset.UtcNow;
        }
        #endregion
    }
}