using Microsoft.AspNetCore.Http;
using System.Text;

namespace UnitTests.TestCases;

public static class CQTestCases
{
    public const string DoctorsFirstName = "John";
    public const string DoctorsNewFirstName = "Tom";
    public const string PatientsFirstName = "Mickael";
    public const string PatientsNewFirstName = "James";
    public const string ReceptionistsFirstName = "Mickael";
    public const string ReceptionistsNewFirstName = "James";
    public const string DoctorsOldPhotoUrl = "old-doctor-photo.jpg";
    public const string DoctorsNewPhotoUrl = "new-doctor-photo.jpg";
    public const string PatientsOldPhotoUrl = "old-patient-photo.jpg";
    public const string PatientsNewPhotoUrl = "new-patient-photo.jpg";
    public const string ReceptionistsOldPhotoUrl = "old-receptionist-photo.jpg";
    public const string ReceptionistsNewPhotoUrl = "new-receptionist-photo.jpg";

    public static IFormFile GetTestFormFile()
    {
        var content = "Fake image content";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        return new FormFile(stream, 0, stream.Length, "photo", "test.jpg")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };
    }
}