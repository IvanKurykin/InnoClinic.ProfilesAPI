namespace Infrastructure.Helpers;

public static class BlobHelper
{
    public static (string containerName, string blobName) ParseBlobUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        var segments = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var containerName = segments[1];
        var blobName = string.Join('/', segments.Skip(2)); 

        return (containerName, blobName);
    }

    public static string GenerateBlobName(string originalFileName)
        => $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
}