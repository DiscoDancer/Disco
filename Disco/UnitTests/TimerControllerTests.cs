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
        public void Cannot_Edit_Nonexistent_Activity()
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

        [Fact]
        public void Cannot_Save_Invalid_Activity()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IActivityRepository>();

            // Arrange - create a controller
            var target = new TimerController(mock.Object);

            var activity = new TimerActivity { Name = "Potato" };

            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the product
            var result = target.EditActivity(activity);

            // Assert - check that the repository was not called
            mock.Verify(m => m.Save(It.IsAny<TimerActivity>()), Times.Never());
            // Assert - check the method result type
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Activity()
        {
            // Arrange - creating an activity
            var activity = new TimerActivity { Name = "Potato", ID = 2};

            // Arrange - create the mock repository
            var mock = new Mock<IActivityRepository>();
            mock.Setup(m => m.TimerActivities).Returns(new [] {
                new TimerActivity {ID = 1, Name = "P1"},
                activity,
                new TimerActivity {ID = 3, Name = "P3"},
            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object);

            // Act - delete the product
            target.DeleteActivity(activity.ID);

            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mock.Verify(m => m.Delete(activity.ID));
        }

        [Fact]
        public void Cannot_Delete_Nonexistent_Activity()
        {
            // Arrange - creating an activity
            var activity = new TimerActivity { Name = "Potato", ID = 2 };

            // Arrange - create the mock repository
            var mock = new Mock<IActivityRepository>();
            mock.Setup(m => m.TimerActivities).Returns(new[] {
                new TimerActivity {ID = 1, Name = "P1"},
                new TimerActivity {ID = 3, Name = "P3"},
            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object);

            // Act - delete the product
            target.DeleteActivity(activity.ID);

            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mock.Verify(m => m.Delete(activity.ID));
            Assert.Equal(2, mock.Object.TimerActivities.Count());
        }

        private static T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}