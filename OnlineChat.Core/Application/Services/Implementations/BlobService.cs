using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Abstractions;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.StaticFiles;
using Services.Interfaces;

namespace Application.Services.Implementations
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
        }

        public Task DeleteBlobAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<BlobDownloadInfo> GetBlobAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("direct");
            var blobClient = containerClient.GetBlobClient(name);
            return await blobClient.DownloadAsync();
        }

        public Task<IEnumerable<string>> ListBlobsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UploadContentBlobAsync(string content, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task UploadFileBlobAsync(string containerName, string clientFilePath, string blobFileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(blobFileName);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(blobFileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            await blobClient.UploadAsync(clientFilePath, new BlobHttpHeaders{ContentType = contentType});
        }
    }
}
