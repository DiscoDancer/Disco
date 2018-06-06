using System.Collections.Generic;
using System.Linq;
using Moq;
using UnitTests.Services;
using WebApplication.Controllers;
using WebApplication.Models.Timer;
using Xunit;

namespace UnitTests.Timer
{
    public class TimerSoundTests
    {
        [Fact]
        public void EditSounds_Contains_All_Sounds()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IRepository<TimerSound>>();
            mock.Setup(m => m.GetAll()).Returns(new[]
            {
                new TimerSound {Name = "Apple", Data = new byte[] {0x01}},
                new TimerSound {Name = "Orange", Data = new byte[] {0x02}},
                new TimerSound {Name = "Banana", Data = new byte[] {0x03}}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(null, mock.Object);

            // Action
            var result
                = MVCHelper.GetViewModel<IEnumerable<TimerSound>>(target.EditSounds())?.ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            Assert.Equal("Apple", result[0].Name);
            Assert.Equal("Orange", result[1].Name);
            Assert.Equal("Banana", result[2].Name);
        }
    }
}
