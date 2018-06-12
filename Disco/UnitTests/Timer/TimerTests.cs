using System.Collections.Generic;
using System.Linq;
using Moq;
using UnitTests.Services;
using WebApplication.Controllers;
using WebApplication.Controllers.Timer;
using WebApplication.Models.Timer;
using Xunit;

namespace UnitTests.Timer
{
    public class TimerTests
    {
        [Fact]
        public void Index_Contains_All_Activities()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IRepository<TimerActivity>>();
            mock.Setup(m => m.GetAll()).Returns(new[]
            {
                new TimerActivity {Name = "Apple"},
                new TimerActivity {Name = "Orange"},
                new TimerActivity {Name = "Banana"}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object, null);

            // Action
            var result
                = MVCHelper.GetViewModel<IEnumerable<TimerActivity>>(target.Index())?.ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            Assert.Equal("Apple", result[0].Name);
            Assert.Equal("Orange", result[1].Name);
            Assert.Equal("Banana", result[2].Name);
        }
    }
}
