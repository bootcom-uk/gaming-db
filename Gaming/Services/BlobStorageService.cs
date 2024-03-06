using Azure.Storage.Blobs;

namespace Services
{
    public class BlobStorageService
    {

        internal string _blobConnectionString;

        public BlobStorageService(string blobConnectionString) {
            _blobConnectionString = blobConnectionString;
        }

        public async Task<Uri?> SaveFile(string baseDirectory, Guid userId, string fileName, Stream fileContents)
        {
            var container = new BlobContainerClient(_blobConnectionString, "gaming");

            await container.CreateIfNotExistsAsync();

            BlobClient blob = container.GetBlobClient($"{baseDirectory}/{userId.ToString().ToLower()}/{fileName}");

            // If the blob already exits then delete it.
            await blob.DeleteIfExistsAsync();

            // Save this file to blob storage
            await blob.UploadAsync(fileContents);
                        
            return blob.Uri;            
        }

        public async Task<Uri?> SaveFile(string baseDirectory, Guid userId, string fileName, string url)
        {
            var httpClient = new HttpClient();
            return await SaveFile(baseDirectory, userId, fileName, await httpClient.GetStreamAsync(url));
        }

    }
}
