using Azure.Storage.Blobs.Models;
using ELearning.Dto;

namespace ELearning.Interfaces.Services;
public interface IStorageService 
{
    public Task<FileResponse> GetFileAsync(string fileName, string containerName, CancellationToken cancellationToken);
    public Task<IEnumerable<string>> GetAllFilesAsync(string containerName, CancellationToken cancellationToken);
    public Task<bool> UploadFileAsync(IFormFile file, string containerName, CancellationToken cancellationToken);
    public Task<bool> DeleteFileAsync(string fileName, string containerName, CancellationToken cancellationToken);

}

