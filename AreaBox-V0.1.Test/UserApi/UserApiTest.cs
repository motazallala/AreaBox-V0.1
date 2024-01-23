using AreaBox_V0._1.Areas.Admin.Controllers;
using AreaBox_V0._1.Areas.User.Controllers;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaBox_V0._1.Test.UserApi
{
    public class UserApiTest
    {
        private UserApiController userApiController;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<IImageService> imageService;
        private Mock<ILocationService> location;
        private Mock<UserManager<ApplicationUser>> userManager;
        private Mock<SignInManager<ApplicationUser>> signInManager;

        public UserApiTest()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            userManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null);
            imageService = new Mock<IImageService>();
            location = new Mock<ILocationService>();

            signInManager = new Mock<SignInManager<ApplicationUser>>(
                userManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null, null);

            userApiController = new UserApiController(
                mockUnitOfWork.Object,
                userManager.Object,
                imageService.Object,
                location.Object,
                signInManager.Object);
        }

        [Fact]
        public void CheckRegex_Validate_ReturnsTrueIfMatch()
        {
            // Arrange
            string input = "abcdABCD";

            // Act
            bool result = userApiController.ContainsOnlyLetters(input);

            // Assert
            Assert.True(result);
        }
    }
}
