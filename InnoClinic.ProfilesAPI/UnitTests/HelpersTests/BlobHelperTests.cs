using FluentAssertions;
using Infrastructure.Helpers;

namespace UnitTests.HelpersTests;

public class BlobHelperTests
{
    [Fact]
    public void ParseBlobUrlValidUrlReturnsContainerAndBlobName()
    {
        var url = "https://mystorageaccount.blob.core.windows.net/account/container-name/folder/image.jpg";

        var (containerName, blobName) = BlobHelper.ParseBlobUrl(url);

        containerName.Should().Be("container-name");
        blobName.Should().Be("folder/image.jpg");
    }

    [Fact]
    public void ParseBlobUrlUrlWithOnlyContainerAndBlobNameReturnsCorrectParts()
    {
        var url = "https://mystorageaccount.blob.core.windows.net/account/photos/image.jpg";

        var (containerName, blobName) = BlobHelper.ParseBlobUrl(url);

        containerName.Should().Be("photos");
        blobName.Should().Be("image.jpg");
    }

    [Fact]
    public void ParseBlobUrlUrlWithDeepBlobPathReturnsCorrectBlobName()
    {
        var url = "https://account.blob.core.windows.net/root/files/folder1/folder2/file.png";

        var (containerName, blobName) = BlobHelper.ParseBlobUrl(url);

        containerName.Should().Be("files");
        blobName.Should().Be("folder1/folder2/file.png");
    }

}