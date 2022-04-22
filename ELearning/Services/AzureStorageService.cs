using Azure.Storage.Blobs;
using ELearning.Dto;
using ELearning.Interfaces.Services;

namespace ELearning.Services;
public class AzureStorageService : IStorageService
{

    private readonly BlobServiceClient _blobServiceClient;
    public AzureStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }


    public async Task<bool> DeleteFileAsync(string fileName, string containerName, CancellationToken cancellationToken)
    {
        try
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var result = await blobClient.DeleteIfExistsAsync();
            return result.Value;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<IEnumerable<string>> GetAllFilesAsync(string containerName, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var items = new List<string>();
        await foreach(var blobItem in containerClient.GetBlobsAsync())
            items.Add(blobItem.Name);

        return items;

    }

    public async Task<FileResponse> GetFileAsync(string fileName, string containerName, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        var blobDownloadInfo = await blobClient.DownloadContentAsync(cancellationToken);
        return new FileResponse(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.Details.ContentType);
    }

    public async Task<bool> UploadFileAsync(IFormFile file, string containerName, CancellationToken cancellationToken)
    {

        try
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = await containerClient.UploadBlobAsync(file.FileName, file.OpenReadStream(),cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

        


    }
}

