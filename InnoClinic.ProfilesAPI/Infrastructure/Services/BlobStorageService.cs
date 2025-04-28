using Application.Exceptions;
using Application.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Infrastructure.Constants;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        this.blobServiceClient = blobServiceClient;
    }
    public async Task DeletePhotoAsync(string blobUrl)
    {
        if (blobUrl is null) throw new BlobUrlIsNullException();

        var (containerName, blobName) = BlobHelper.ParseBlobUrl(blobUrl);
        var blobClient = GetBlobClient(containerName, blobName);

        await blobClient.DeleteIfExistsAsync();
    }

    public async Task<string?> UploadPhotoAsync(IFormFile file)
    { 
        if (file  is null) throw new FileIsNullException();

        var containerClient = blobServiceClient.GetBlobContainerClient(BlobConstants.ContainerName);

        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var blobName = BlobHelper.GenerateBlobName(file.FileName);

        var blobClient = containerClient.GetBlobClient(blobName);

        await UploadStreamAsync(blobClient, file.OpenReadStream());

        return blobClient.Uri.ToString();
    }
    public async Task<Stream?> GetPhotoAsync(string blobUrl)
    {
        if (blobUrl is null) throw new BlobUrlIsNullException();

        var (containerName, blobName) = BlobHelper.ParseBlobUrl(blobUrl);
        var blobClient = GetBlobClient(containerName, blobName);

        var blobExists = await blobClient.ExistsAsync();
        if (!blobExists)
        {
            return null;
        }

        var downloadInfo = await blobClient.DownloadAsync();
        return downloadInfo.Value.Content;
    }

    private BlobClient GetBlobClient(string containerName, string blobName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        return containerClient.GetBlobClient(blobName);
    }

    private async Task UploadStreamAsync(BlobClient blobClient, Stream stream)
    {
        await using (stream)
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }
    }
}