using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IBlobStorageService
{
    Task<string?> UploadPhotoAsync(IFormFile file);
    Task DeletePhotoAsync(string blobUrl);
    Task<Stream?> GetPhotoAsync(string blobUrl);
}