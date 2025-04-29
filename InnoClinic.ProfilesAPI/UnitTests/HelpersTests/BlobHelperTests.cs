using Infrastructure.Helpers;

namespace UnitTests.HelpersTests;

public class BlobHelperTests
{
    [Theory]
    [InlineData("not-a-valid-url", typeof(UriFormatException))]
    public void ParseBlobUrlInvalidUrlShouldThrow(string invalidUrl, Type expectedException)
    {
        Assert.Throws(expectedException, () => BlobHelper.ParseBlobUrl(invalidUrl));
    }

    [Fact]
    public void GenerateBlobNameShouldGenerateUniqueNames()
    {
        var fileName = "test.jpg";
        var names = new HashSet<string>();

        for (int i = 0; i < 5; i++)
        {
            names.Add(BlobHelper.GenerateBlobName(fileName));
        }

        Assert.Equal(5, names.Count);
    }
}