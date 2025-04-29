using Application.Commands.DoctorCommands;
using Application.Commands.PatientCommands;
using Application.DTO.Doctor;
using Application.DTO.Patient;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.PatientTests;

public class UpdatePatientCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithNewPhoto_UpdatesPatientPhoto()
    {
        var mockRepo = new Mock<IPatientRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockBlob = new Mock<IBlobStorageService>();

        var patientId = Guid.NewGuid();
        var formFile = CQTestCases.GetTestFormFile();
        var dto = new RequestPatientDto { Photo = formFile };
        var existing = new Patient { Id = patientId, PhotoUrl = CQTestCases.PatientsOldPhotoUrl, DateOfBirth = DateTime.MinValue };
        var updated = new Patient { Id = patientId, PhotoUrl = CQTestCases.PatientsNewPhotoUrl, DateOfBirth = DateTime.MinValue };
        var response = new ResponsePatientDto { PhotoUrl = CQTestCases.PatientsNewPhotoUrl };

        mockRepo.Setup(r => r.GetByIdAsync(patientId, It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        mockBlob.Setup(b => b.UploadPhotoAsync(formFile)).ReturnsAsync(CQTestCases.PatientsNewPhotoUrl);
        mockRepo.Setup(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>())).ReturnsAsync(updated);
        mockMapper.Setup(m => m.Map<ResponsePatientDto>(updated)).Returns(response);

        var handler = new UpdatePatientCommandHandler(mockRepo.Object, mockMapper.Object, mockBlob.Object);

        var result = await handler.Handle(new UpdatePatientCommand(patientId, dto), default);

        mockBlob.Verify(b => b.UploadPhotoAsync(formFile), Times.Once);
        mockBlob.Verify(b => b.DeletePhotoAsync(CQTestCases.PatientsOldPhotoUrl), Times.Once);
        result.PhotoUrl.Should().Be(CQTestCases.PatientsNewPhotoUrl);
    }
}