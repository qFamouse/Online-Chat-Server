using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace Application.Services.Abstractions
{
    public interface IBlobService
    {
        Task<BlobDownloadInfo> GetBlobAsync(string containerName, string fileName, CancellationToken cancellationToken = default);
        //Task<IEnumerable<string>> ListBlobsAsync();
        Task<BlobProperties> UploadFileBlobAsync(string containerName, Stream fileStream, string blobFileName,
            string contentType, CancellationToken cancellationToken = default);
        //Task UploadContentBlobAsync(string content, string fileName);
        //Task DeleteBlobAsync(string name);
    }
}
