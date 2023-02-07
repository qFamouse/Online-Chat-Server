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

        public async Task<BlobDownloadInfo> GetBlobAsync(string containerName, string fileName, CancellationToken cancellationToken = default)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            return await blobClient.DownloadAsync(cancellationToken);
        }

        public async Task<BlobProperties> UploadFileBlobAsync(string containerName, string clientFilePath, string blobFileName, CancellationToken cancellationToken = default)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(blobFileName);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(blobFileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            await blobClient.UploadAsync(clientFilePath, new BlobHttpHeaders{ContentType = contentType}, cancellationToken: cancellationToken);

            // TODO: GetPropertiesAsync has BlobRequestConditions. May for performance we need use it
            var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
            return properties.Value;
        }
    }
}
