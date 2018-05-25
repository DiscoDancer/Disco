using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication.Controllers;
using WebApplication.Models.Timer;
using Xunit;

namespace UnitTests
{
    public class TimerControllerTests
    {
        [Fact]
        public void Index_Contains_All_Activities()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IActivityRepository>();
            mock.Setup(m => m.TimerActivities).Returns(new[]
            {
                new TimerActivity {Name = "Apple"},
                new TimerActivity {Name = "Orange"},
                new TimerActivity {Name = "Banana"}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object);

            // Action
            var result
                = GetViewModel<IEnumerable<TimerActivity>>(target.Index())?.ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            Assert.Equal("Apple", result[0].Name);
            Assert.Equal("Orange", result[1].Name);
            Assert.Equal("Banana", result[2].Name);
        }

        [Fact]
        public void EditActivities_Contains_All_Activities()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IActivityRepository>();
            mock.Setup(m => m.TimerActivities).Returns(new[]
            {
                new TimerActivity {Name = "Apple"},
                new TimerActivity {Name = "Orange"},
                new TimerActivity {Name = "Banana"}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object);

            // Action
            var result
                = GetViewModel<IEnumerable<TimerActivity>>(target.EditActivities())?.ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            Assert.Equal("Apple", result[0].Name);
            Assert.Equal("Orange", result[1].Name);
            Assert.Equal("Banana", result[2].Name);
        }

        [Fact]
        public void Can_Edit_Activity()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IActivityRepository>();
            mock.Setup(m => m.TimerActivities).Returns(new[]
            {
                new TimerActivity {Name = "Apple", ID = 1},
                new TimerActivity {Name = "Orange", ID = 2},
                new TimerActivity {Name = "Banana", ID = 3}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object);

            // Act
            var p1 = GetViewModel<TimerActivity>(target.EditActivity(1));
            var p2 = GetViewModel<TimerActivity>(target.EditActivity(2));
            var p3 = GetViewModel<TimerActivity>(target.EditActivity(3));
        }

        [Fact]
        public void Cannot_Edit_Activity()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IActivityRepository>();
            mock.Setup(m => m.TimerActivities).Returns(new[]
            {
                new TimerActivity {Name = "Apple", ID = 1},
                new TimerActivity {Name = "Orange", ID = 2},
                new TimerActivity {Name = "Banana", ID = 3}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object);


            // Act
            var result = GetViewModel<TimerActivity>(target.EditActivity(4));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Activity()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IActivityRepository>();

            // Arrange - create a controller
            var target = new TimerController(mock.Object);

            var activity = new TimerActivity { Name = "Potato" };

            // Act - try to save the product
            var result = target.EditActivity(activity);

            // Assert - check that the repository was called
            mock.Verify(m => m.Save(activity));
            // Assert - check the result type is a redirection
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("EditActivities", (result as RedirectToActionResult)?.ActionName);
        }

        private static T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}