using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TimeKeeper.Application.Services;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Entities;
using TimeKeeper.Core.Interface.Repositories;
using Xunit;

namespace Application.Tests
{
    public class AttendanceServiceTests
    {
        private readonly Mock<IAttendanceRepository> _attendanceRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AttendanceService _attendanceService;

        public AttendanceServiceTests()
        {
            _attendanceRepositoryMock = new Mock<IAttendanceRepository>();
            _mapperMock = new Mock<IMapper>();
            _attendanceService = new AttendanceService(_attendanceRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAttendanceAsync_ShouldAddEntry_WhenNoExistingEntry()
        {
            // Arrange
            int employeeId = 1;
            _attendanceRepositoryMock
                .Setup(repo => repo.GetEntryByDateAndEmployeeIdAsync(It.IsAny<DateTime>(), employeeId))
                .ReturnsAsync((AttendanceEntry?)null);

            // Act
            await _attendanceService.CreateAttendanceAsync(employeeId);

            // Assert
            _attendanceRepositoryMock.Verify(repo => repo.AddEntryAsync(It.IsAny<AttendanceEntry>()), Times.Once);
        }

        [Fact]
        public async Task CreateAttendanceAsync_ShouldThrowException_WhenTimeInAlreadyExists()
        {
            // Arrange
            int employeeId = 1;
            var existingEntry = new AttendanceEntry { TimeIn = DateTime.Now };
            _attendanceRepositoryMock
                .Setup(repo => repo.GetEntryByDateAndEmployeeIdAsync(It.IsAny<DateTime>(), employeeId))
                .ReturnsAsync(existingEntry);

            await Assert.ThrowsAsync<Exception>(() => _attendanceService.CreateAttendanceAsync(employeeId));
        }

        [Fact]
        public async Task UpdateAttendanceAsync_ShouldUpdateEntry_WhenEntryExists()
        {
            // Arrange
            int id = 1;
            var existingAttendance = new AttendanceEntry { Id = id, TimeIn = DateTime.Now, TimeOut = DateTime.Now };
            var attendanceDto = new AttendanceEntryDto { TimeInString = "10:00", TimeOutString = "18:00" };

            _attendanceRepositoryMock.Setup(repo => repo.GetEntryByIdAsync(id)).ReturnsAsync(existingAttendance);

            // Act
            await _attendanceService.UpdateAttendanceAsync(id, attendanceDto);

            // Assert
            _attendanceRepositoryMock.Verify(repo => repo.UpdateEntryAsync(existingAttendance), Times.Once);
        }

        [Fact]
        public async Task UpdateAttendanceAsync_ShouldNotUpdateEntry_WhenEntryDoesNotExist()
        {
            // Arrange
            int id = 1;
            var attendanceDto = new AttendanceEntryDto { TimeInString = "10:00", TimeOutString = "18:00" };

            _attendanceRepositoryMock.Setup(repo => repo.GetEntryByIdAsync(id)).ReturnsAsync((AttendanceEntry?)null);

            // Act
            await _attendanceService.UpdateAttendanceAsync(id, attendanceDto);

            // Assert
            _attendanceRepositoryMock.Verify(repo => repo.UpdateEntryAsync(It.IsAny<AttendanceEntry>()), Times.Never);
        }

        [Fact]
        public async Task TimeoutAsync_ShouldUpdateTimeOut_WhenTimeInExists()
        {
            // Arrange
            int employeeId = 1;
            _attendanceRepositoryMock.Setup(repo => repo.HasTimeInEntryAsync(employeeId)).ReturnsAsync(true);

            // Act
            await _attendanceService.TimeoutAsync(employeeId);

            // Assert
            _attendanceRepositoryMock.Verify(repo => repo.UpdateTimeOutAsync(employeeId, It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task TimeoutAsync_ShouldThrowException_WhenNoTimeInEntryExists()
        {
            // Arrange
            int employeeId = 1;
            _attendanceRepositoryMock.Setup(repo => repo.HasTimeInEntryAsync(employeeId)).ReturnsAsync(false);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _attendanceService.TimeoutAsync(employeeId));
        }
    }
}
