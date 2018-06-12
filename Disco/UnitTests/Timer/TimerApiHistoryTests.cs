using Moq;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.Timer;
using WebApplication.Models.Timer;
using Xunit;

namespace UnitTests.Timer
{
    public class TimerApiHistoryTests
    {
        [Fact]
        public void Can_Add_Log_With_Existing_Activity()
        {
            // Arrange - create the mock repositories
            var mockRepositoryLog = new Mock<IRepository<TimerLog>>();
            var mockRepositoryActivity = new Mock<IRepository<TimerActivity>>();

            var activity = new TimerActivity {ID = 1, Name = "Sport"};

            mockRepositoryActivity.Setup(m => m.GetAll()).Returns(new[]
            {
                activity
            }.AsQueryable);

            // Arrange - create a controller
            var target = new TimerController(mockRepositoryActivity.Object, null, mockRepositoryLog.Object);

            // action
            var result = target.AddLog(activity.ID);

            mockRepositoryLog.Verify(m => m.Save(It.IsAny<TimerLog>()));

            // Assert - check the method result type
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Cannot_Add_Log_With_Nonexisting_Activity()
        {
            // Arrange - create the mock repositories
            var mockRepositoryLog = new Mock<IRepository<TimerLog>>();
            var mockRepositoryActivity = new Mock<IRepository<TimerActivity>>();

            var activity = new TimerActivity { ID = 1, Name = "Sport" };

            mockRepositoryActivity.Setup(m => m.GetAll()).Returns(new TimerActivity[]
            {
            }.AsQueryable);

            // Arrange - create a controller
            var target = new TimerController(mockRepositoryActivity.Object, null, mockRepositoryLog.Object);

            // action
            var result = target.AddLog(activity.ID);

            // Assert
            mockRepositoryLog.Verify(m => m.Save(It.IsAny<TimerLog>()), Times.Never);

            // Assert - check the method result type
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
