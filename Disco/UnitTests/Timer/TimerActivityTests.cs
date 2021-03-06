﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTests.Services;
using WebApplication.Controllers.Timer;
using WebApplication.Models.Timer;
using WebApplication.Models.ViewModels.Timer;
using Xunit;

namespace UnitTests.Timer
{
    public class TimerActivityTests
    {
        [Fact]
        public void EditActivities_Contains_All_Activities()
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
            var target = new TimerController(mock.Object, null, null);

            // Action
            var result
                = MVCHelper.GetViewModel<IEnumerable<TimerActivity>>(target.EditActivities())?.ToArray();

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
            var mock = new Mock<IRepository<TimerActivity>>();
            mock.Setup(m => m.GetAll()).Returns(new[]
            {
                new TimerActivity {Name = "Apple", ID = 1},
                new TimerActivity {Name = "Orange", ID = 2},
                new TimerActivity {Name = "Banana", ID = 3}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object, null, null);

            // Act
            var p1 = MVCHelper.GetViewModel<CaptionActivity>(target.EditActivity(1));
            var p2 = MVCHelper.GetViewModel<CaptionActivity>(target.EditActivity(2));
            var p3 = MVCHelper.GetViewModel<CaptionActivity>(target.EditActivity(3));

            // Assert
            Assert.Equal(1, p1.Activity.ID);
            Assert.Equal(2, p2.Activity.ID);
            Assert.Equal(3, p3.Activity.ID);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Activity()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IRepository<TimerActivity>>();
            mock.Setup(m => m.GetAll()).Returns(new[]
            {
                new TimerActivity {Name = "Apple", ID = 1},
                new TimerActivity {Name = "Orange", ID = 2},
                new TimerActivity {Name = "Banana", ID = 3}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object, null, null);


            // Act
            var result = MVCHelper.GetViewModel<TimerActivity>(target.EditActivity(4));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Activity()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IRepository<TimerActivity>>();

            // Arrange - create a controller
            var target = new TimerController(mock.Object, null, null);

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
            var mock = new Mock<IRepository<TimerActivity>>();

            // Arrange - create a controller
            var target = new TimerController(mock.Object, null, null);

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
            var mock = new Mock<IRepository<TimerActivity>>();
            mock.Setup(m => m.GetAll()).Returns(new [] {
                new TimerActivity {ID = 1, Name = "P1"},
                activity,
                new TimerActivity {ID = 3, Name = "P3"},
            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object, null, null);

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
            var mock = new Mock<IRepository<TimerActivity>>();
            mock.Setup(m => m.GetAll()).Returns(new[] {
                new TimerActivity {ID = 1, Name = "P1"},
                new TimerActivity {ID = 3, Name = "P3"},
            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(mock.Object, null, null);

            // Act - delete the product
            target.DeleteActivity(activity.ID);

            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mock.Verify(m => m.Delete(activity.ID));
            Assert.Equal(2, mock.Object.GetAll().Count());
        }
    }
}