using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTests.Services;
using WebApplication.Controllers.Timer;
using WebApplication.Models.Timer;
using WebApplication.Models.ViewModels.Timer;
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
            var target = new TimerController(null, mock.Object, null);

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

        [Fact]
        public void Can_Edit_Sound()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IRepository<TimerSound>>();
            mock.Setup(m => m.GetAll()).Returns(new[]
            {
                new TimerSound {Name = "Apple", Data = new byte[] {0x01}, ID = 1},
                new TimerSound {Name = "Orange", Data = new byte[] {0x02}, ID = 2},
                new TimerSound {Name = "Banana", Data = new byte[] {0x03}, ID = 3}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(null, mock.Object, null);

            // Act
            var p1 = MVCHelper.GetViewModel<EditSoundViewModel>(target.EditSound(1));
            var p2 = MVCHelper.GetViewModel<EditSoundViewModel>(target.EditSound(2));
            var p3 = MVCHelper.GetViewModel<EditSoundViewModel>(target.EditSound(3));

            // Assert
            Assert.Equal(1, p1.ID);
            Assert.Equal(2, p2.ID);
            Assert.Equal(3, p3.ID);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Activity()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IRepository<TimerSound>>();
            mock.Setup(m => m.GetAll()).Returns(new[]
            {
                new TimerSound {Name = "Apple", Data = new byte[] {0x01}, ID = 1},
                new TimerSound {Name = "Orange", Data = new byte[] {0x02}, ID = 2},
                new TimerSound {Name = "Banana", Data = new byte[] {0x03}, ID = 3}

            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(null, mock.Object, null);


            // Act
            var result = MVCHelper.GetViewModel<EditSoundViewModel>(target.EditSound(4));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Can_Save_Valid_Sound()
        {
            // Arrange - create the mock repository
            var mockRepository = new Mock<IRepository<TimerSound>>();

            // Arrange - create a controller
            var targetController = new TimerController(null, mockRepository.Object, null);

            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(x => x.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var model = new EditSoundViewModel
            {
                Name = "Potato",
                File = mockFile.Object
            };

            // Act - try to save the sound
            var result = await targetController.EditSound(model);

            // Assert - check that the repository was called
            mockRepository.Verify(m => m.Save(It.IsAny<TimerSound>()));

            // Assert - check the result type is a redirection
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("EditSounds", (result as RedirectToActionResult)?.ActionName);
        }

        [Fact]
        public async Task Cannot_Save_Invalid_Sound()
        {
            // Arrange - create the mock repository
            var mockRepository = new Mock<IRepository<TimerSound>>();

            // Arrange - create a controller
            var targetController = new TimerController(null, mockRepository.Object, null);

            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(x => x.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var model = new EditSoundViewModel
            {
                Name = "Potato",
                File = mockFile.Object
            };

            // Arrange - add an error to the model state
            targetController.ModelState.AddModelError("error", "error");

            // Act - try to save the product
            var result = await targetController.EditSound(model);

            // Assert - check that the repository was not called
            mockRepository.Verify(m => m.Save(It.IsAny<TimerSound>()), Times.Never());
            // Assert - check the method result type
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Existing_Sound()
        {
            // Arrange - creating a sound
            var sound = new TimerSound { Name = "Apple", Data = new byte[] { 0x01 }, ID = 1 };

            // Arrange - create the mock repository
            var mockRepository = new Mock<IRepository<TimerSound>>();
            mockRepository.Setup(m => m.GetAll()).Returns(new[] {
                new TimerSound { Name = "Orange", Data = new byte[] { 0x04 }, ID = 2 },
                sound,
                new TimerSound { Name = "Cabbage", Data = new byte[] { 0x07 }, ID = 3 },
            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(null, mockRepository.Object, null);

            // Act - delete the product
            var result = target.DeleteSound(sound.ID);

            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mockRepository.Verify(m => m.Delete(sound.ID));
        }

        [Fact]
        public void Cannot_Delete_Nonexistent_Sound()
        {
            // Arrange - creating a sound
            var sound = new TimerSound { Name = "Apple", Data = new byte[] { 0x01 }, ID = 1 };

            // Arrange - create the mock repository
            var mockRepository = new Mock<IRepository<TimerSound>>();
            mockRepository.Setup(m => m.GetAll()).Returns(new[] {
                new TimerSound { Name = "Orange", Data = new byte[] { 0x04 }, ID = 2 },
                new TimerSound { Name = "Cabbage", Data = new byte[] { 0x07 }, ID = 3 },
            }.AsQueryable());

            // Arrange - create a controller
            var target = new TimerController(null, mockRepository.Object, null);

            // Act - delete the product
            var result = target.DeleteSound(sound.ID);

            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mockRepository.Verify(m => m.Delete(sound.ID));
            Assert.Equal(2, mockRepository.Object.GetAll().Count());
        }

        [Fact]
        public void Can_Get_Sound_By_Valid_Id()
        {
            // Arrange - creating a sound
            var sound = new TimerSound { Name = "Apple", Data = new byte[] { 0x01 }, ID = 2 };

            // Arrange - create the mock repository
            var mockRepository = new Mock<IRepository<TimerSound>>();
            mockRepository.Setup(m => m.GetAll()).Returns(new[] {
                new TimerSound { Name = "Orange", Data = new byte[] { 0x04 }, ID = 1 },
                sound,
                new TimerSound { Name = "Cabbage", Data = new byte[] { 0x07 }, ID = 3 },
            }.AsQueryable());

            // Arrange - create a controller
            var targetController = new TimerController(null, mockRepository.Object, null);

            // Act - get the sound
            var result = targetController.GetSoundById(sound.ID);

            // Assert - check the method result type
            Assert.IsType<FileStreamResult>(result);
            Assert.Equal("application/octet-stream", (result as FileStreamResult)?.ContentType);
        }

        [Fact]
        public void Cannot_Get_Sound_By_Invalid_Id()
        {
            // Arrange - creating a sound
            var sound = new TimerSound { Name = "Apple", Data = new byte[] { 0x01 }, ID = 2 };

            // Arrange - create the mock repository
            var mockRepository = new Mock<IRepository<TimerSound>>();
            mockRepository.Setup(m => m.GetAll()).Returns(new[] {
                new TimerSound { Name = "Orange", Data = new byte[] { 0x04 }, ID = 1 },
                new TimerSound { Name = "Cabbage", Data = new byte[] { 0x07 }, ID = 3 },
            }.AsQueryable());

            // Arrange - create a controller
            var targetController = new TimerController(null, mockRepository.Object, null);

            // Act - get the sound
            var result = targetController.GetSoundById(sound.ID);

            // Assert - check the method result type
            Assert.Null(result);
        }
    }
}
