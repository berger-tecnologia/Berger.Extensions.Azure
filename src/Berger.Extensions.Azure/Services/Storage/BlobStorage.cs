using System.Text;
using Azure.Storage.Blobs;

namespace Berger.Extensions.Azure.Storage
{
    public class BlobStorage
    {
        public async Task<bool> DirectoryExistsAsync(BlobContainerClient container, string directory)
        {
            var blobItems = container.GetBlobsAsync(prefix: directory);

            await foreach (var blobItem in blobItems)
            {
                return true;
            }
            return false;
        }
        public async Task CreateDirectoryAsync(BlobContainerClient container, string directory)
        {
            var blobClient = container.GetBlobClient(directory + "/");

            await blobClient.UploadAsync(new MemoryStream(), overwrite: true);
        }
        public async Task WriteMediaAsync(string file, Stream contentStream, string connection, string container)
        {
            var blobClient = new BlobClient(connection, container, file);

            await blobClient.UploadAsync(contentStream, overwrite: true);
        }
        public async Task WriteAsync(string file, string content, string connection, string name)
        {
            var containerName = name;

            BlobServiceClient blobServiceClient = new BlobServiceClient(connection);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient(file);

            var output = new StringBuilder();

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                await blobClient.UploadAsync(ms, true);
            }
        }
    }
}