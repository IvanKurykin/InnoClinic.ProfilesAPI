using Application.Exceptions;
using Azure;
using Azure.Storage.Blobs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests;

public class BlobStorageServiceTests
{
    private readonly Mock<BlobServiceClient> _blobServiceClientMock;
    private readonly BlobStorageService _blobStorageService;

    public BlobStorageServiceTests()
    {
        _blobServiceClientMock = new Mock<BlobServiceClient>();
        _blobStorageService = new BlobStorageService(_blobServiceClientMock.Object);
    }

    [Fact]
    public async Task UploadPhotoAsync_ValidFile_ReturnsBlobUrl()
    {
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.FileName).Returns("test.jpg");
        fileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());

        var containerClientMock = new Mock<BlobContainerClient>();
        var blobClientMock = new Mock<BlobClient>();

        _blobServiceClientMock.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(containerClientMock.Object);

        containerClientMock.Setup(x => x.GetBlobClient(It.IsAny<string>())).Returns(blobClientMock.Object);

        blobClientMock.Setup(x => x.Uri).Returns(new Uri("https://test.blob.core.windows.net/container/test.jpg"));

        var result = await _blobStorageService.UploadPhotoAsync(fileMock.Object);

        Assert.NotNull(result);
        Assert.Contains("test.jpg", result);
    }

    [Fact]
    public async Task UploadPhotoAsyncNullFileThrowsFileIsNullException()
    {
        await Assert.ThrowsAsync<FileIsNullException>(() => _blobStorageService.UploadPhotoAsync(null!));
    }


    [Fact]
    public async Task DeletePhotoAsyncNullBlobUrlThrowsBlobUrlIsNullException()
    {
        await Assert.ThrowsAsync<BlobUrlIsNullException>(() => _blobStorageService.DeletePhotoAsync(null!));
    }

    [Fact]
    public async Task GetPhotoAsync_BlobDoesNotExist_ReturnsNull()
    {
        var blobUrl = "https://test.blob.core.windows.net/container/nonexistent.jpg";
        var containerClientMock = new Mock<BlobContainerClient>();
        var blobClientMock = new Mock<BlobClient>();

        _blobServiceClientMock.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(containerClientMock.Object);

        containerClientMock.Setup(x => x.GetBlobClient(It.IsAny<string>())).Returns(blobClientMock.Object);

        blobClientMock.Setup(x => x.ExistsAsync(default)).ReturnsAsync(Response.FromValue(false, Mock.Of<Response>()));

        var result = await _blobStorageService.GetPhotoAsync(blobUrl);

        Assert.Null(result);
    }
}