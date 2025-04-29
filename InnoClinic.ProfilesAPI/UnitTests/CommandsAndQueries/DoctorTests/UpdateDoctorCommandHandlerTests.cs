using Application.Commands.DoctorCommands;
using Application.DTO.Doctor;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.DoctorTests;

public class UpdateDoctorCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithNewPhoto_UpdatesDoctorPhoto()
    {
        var mockRepo = new Mock<IDoctorRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockBlob = new Mock<IBlobStorageService>();

        var doctorId = Guid.NewGuid();
        var formFile = CQTestCases.GetTestFormFile();
        var dto = new RequestDoctorDto { Photo = formFile };
        var existing = new Doctor { Id = doctorId, PhotoUrl = CQTestCases.DoctorsOldPhotoUrl, DateOfBirth = DateTime.MinValue };
        var updated = new Doctor { Id = doctorId, PhotoUrl = CQTestCases.DoctorsNewPhotoUrl, DateOfBirth = DateTime.MinValue };
        var response = new ResponseDoctorDto { PhotoUrl = CQTestCases.DoctorsNewPhotoUrl };

        mockRepo.Setup(r => r.GetByIdAsync(doctorId, It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        mockBlob.Setup(b => b.UploadPhotoAsync(formFile)).ReturnsAsync(CQTestCases.DoctorsNewPhotoUrl);
        mockRepo.Setup(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>())).ReturnsAsync(updated);
        mockMapper.Setup(m => m.Map<ResponseDoctorDto>(updated)).Returns(response);

        var handler = new UpdateDoctorCommandHandler(mockRepo.Object, mockMapper.Object, mockBlob.Object);

        var result = await handler.Handle(new UpdateDoctorCommand(doctorId, dto), default);

        mockBlob.Verify(b => b.UploadPhotoAsync(formFile), Times.Once);
        mockBlob.Verify(b => b.DeletePhotoAsync(CQTestCases.DoctorsOldPhotoUrl), Times.Once);
        result.PhotoUrl.Should().Be(CQTestCases.DoctorsNewPhotoUrl);
    }
}