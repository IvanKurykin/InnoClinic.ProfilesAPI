using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IBlobStorageService
{
    Task<string?> UploadPhotoAsync(IFormFile file);
    Task DeletePhotoAsync(string blobUrl);
}