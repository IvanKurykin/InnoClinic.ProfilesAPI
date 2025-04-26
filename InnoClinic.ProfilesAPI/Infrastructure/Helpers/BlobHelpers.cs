namespace Infrastructure.Helpers;

public static class BlobHelpers
{
    public static (string containerName, string blobName) ParseBlobUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        return (containerName: uri.Segments[1].Trim('/'), blobName: string.Join("", uri.Segments.Skip(2)));
    }

    public static string GenerateBlobName(string originalFileName)
        => $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
}