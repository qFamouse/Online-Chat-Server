using Application.Services.Abstractions;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Application.Services.Implementations;

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

    public async Task<BlobProperties> UploadFileBlobAsync(string containerName, Stream fileStream, string blobFileName, string contentType, CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        var blobClient = containerClient.GetBlobClient(blobFileName);

        await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, null, cancellationToken); // TODO: Whats snapshots?

        await blobClient.UploadAsync(fileStream, new BlobHttpHeaders{ ContentType = contentType }, cancellationToken: cancellationToken);

        var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
        return properties.Value;
    }
}