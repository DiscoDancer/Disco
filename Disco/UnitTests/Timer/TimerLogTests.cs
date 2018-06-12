using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using UnitTests.Services;
using WebApplication.Controllers.Timer;
using WebApplication.Models.Timer;
using Xunit;

namespace UnitTests.Timer
{
    public class TimerLogTests
    {
        [Fact]
        public void History_Contains_All_Logs()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IRepository<TimerLog>>();
            mock.Setup(m => m.GetAll()).Returns(new[]
            {
                new TimerLog {Activity = new TimerActivity(), Date = DateTime.Now, ID = 1},
                new TimerLog {Activity = new TimerActivity(), Date = DateTime.Now, ID = 2},
                new TimerLog {Activity = new TimerActivity(), Date = DateTime.Now, ID = 3}
            }.AsQueryable);

            // Arrange - create a controller
            var target = new TimerController(null, null, mock.Object);

            // Action
            var result
                = MVCHelper.GetViewModel<IEnumerable<TimerLog>>(target.History())?.ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            Assert.Equal(1, result[0].ID);
            Assert.Equal(2, result[1].ID);
            Assert.Equal(3, result[2].ID);
        }

        [Fact]
        public void Can_Delete_Existing_Log()
        {
            // Arrange - creating a sound
            var log = new TimerLog { Activity = new TimerActivity(), Date = DateTime.Now, ID = 2 };

            // Arrange - create the mock repository
            var mockRepository = new Mock<IRepository<TimerLog>>();
            mockRepository.Setup(m => m.GetAll()).Returns(new[]
            {
                new TimerLog {Activity = new TimerActivity(), Date = DateTime.Now, ID = 1},
                log,
                new TimerLog {Activity = new TimerActivity(), Date = DateTime.Now, ID = 3}
            }.AsQueryable);

            // Arrange - create a controller
            var target = new TimerController(null, null, mockRepository.Object);

            // Act - delete the product
            var result = target.DeleteLog(log.ID);

            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mockRepository.Verify(m => m.Delete(log.ID));
        }

        [Fact]
        public void Can_Delete_Nonexisting_Log()
        {
            // Arrange - creating a sound
            var log = new TimerLog { Activity = new TimerActivity(), Date = DateTime.Now, ID = 2 };

            // Arrange - create the mock repository
            var mockRepository = new Mock<IRepository<TimerLog>>();
            mockRepository.Setup(m => m.GetAll()).Returns(new[]
            {
                new TimerLog {Activity = new TimerActivity(), Date = DateTime.Now, ID = 1},
                new TimerLog {Activity = new TimerActivity(), Date = DateTime.Now, ID = 3}
            }.AsQueryable);

            // Arrange - create a controller
            var target = new TimerController(null, null, mockRepository.Object);

            // Act - delete the product
            var result = target.DeleteLog(log.ID);

            // Assert - ensure that the repository delete method was
            // called with the correct Log
            mockRepository.Verify(m => m.Delete(log.ID));
            Assert.Equal(2, mockRepository.Object.GetAll().Count());
        }
    }
}
