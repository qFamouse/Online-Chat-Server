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
        Task<BlobDownloadInfo> GetBlobAsync(string name);
        Task<IEnumerable<string>> ListBlobsAsync();
        Task UploadFileBlobAsync(string containerName, string filePath, string fileName);
        Task UploadContentBlobAsync(string content, string fileName);
        Task DeleteBlobAsync(string name);
    }
}
