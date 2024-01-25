using AreaBox_V0._1.Areas.Admin.Controllers;
using AreaBox_V0._1.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using AreaBox_V0._1.Data.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;

namespace AreaBox_V0._1.Test.AdminApi
{
	public class AdminApiTest
	{
		private Mock<IUnitOfWork> mockUnitOfWork;
		private AdminApiController adminApiController;

		public AdminApiTest()
		{
			mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(uow => uow.MediaPosts).Returns(Mock.Of<IMediaPostRepository>());
			mockUnitOfWork.Setup(uow => uow.Users).Returns(Mock.Of<IUserManagementRepository>());
			adminApiController = new AdminApiController(mockUnitOfWork.Object);
		}

		[Fact]
		public async Task DisableMediaPost_ValidData_ReturnsOk()
		{
			// Arrange
			string postId = "29e36807-e910-40ac-9370-c6ab8a5894a0";
			string newState = "true";

			// Act
			var result = await adminApiController.DisableMediaPost(postId, newState) as ObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(200, result.StatusCode);

			string expectedMessage = newState == "true" ? "MediaPost has been successfully Published" : "MediaPost has been successfully Suspended";
			Assert.Equal(expectedMessage, result.Value);

			mockUnitOfWork.Verify(uow => uow.MediaPosts.Disable(postId, true), Times.Once);
			mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
		}


		[Fact]
		public async Task DisableUser_ValidData_ReturnsOk()
		{
			// Arrange
			string userId = "31b511c6-8bee-438e-a227-bd60a11266ea";
			string newState = "true";

			// Act
			var result = await adminApiController.DisableUser(userId, newState) as ObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(200, result.StatusCode);

			string expectedMessage = newState == "true" ? "User has been successfully Activated" : "User has been successfully Suspended";
			Assert.Equal(expectedMessage, result.Value);

			mockUnitOfWork.Verify(uow => uow.Users.Disable(userId, true), Times.Once);
			mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
		}
	}
}
