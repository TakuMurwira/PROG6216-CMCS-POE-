using System;
using System.Linq;
using Moq;
using Xunit;
using PROG6216_CMCS_POE_.Controllers;
using PROG6216_CMCS_POE_.Models.DBEntities;
using PROG6216_CMCS_POE_.Data;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PROG6216_CMCS_POE_Tests
{
    public class ClaimControllerTests
    {
        private Mock<ClaimDatabaseContext> CreateMockContext()
        {
            var mockSet = new Mock<DbSet<Claim>>();
            var mockContext = new Mock<ClaimDatabaseContext>();

            mockContext.Setup(c => c.Claims).Returns(mockSet.Object);

            return mockContext;
        }

        [Fact]
        public void SubmitClaim_ValidClaim_ReturnsRedirectToAction()
        {
            // Arrange
            var mockContext = CreateMockContext();
            var controller = new ClaimController(mockContext.Object);
            var claim = new Claim
            {
                LecturerID = 1,
                SubmissionDate = DateTime.Now,
                HoursWorked = 10,
                HourlyRate = 20,
                TotalClaimAmount = 200,
                ClaimStatus = "Pending",
                Document = new byte[1024] // Simulating a file
            };

            // Act
            var result = controller.SubmitClaim(claim) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.RouteValues["action"]);

            // Check if Add was called once
            mockContext.Verify(c => c.Claims.Add(It.IsAny<Claim>()), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void SubmitClaim_FileSizeExceedsLimit_ReturnsError()
        {
            // Arrange
            var mockContext = CreateMockContext();
            var controller = new ClaimController(mockContext.Object);
            var claim = new Claim
            {
                LecturerID = 1,
                SubmissionDate = DateTime.Now,
                HoursWorked = 10,
                HourlyRate = 20,
                TotalClaimAmount = 200,
                ClaimStatus = "Pending",
                Document = new byte[3 * 1024 * 1024] // Simulating a file larger than 2MB
            };

            // Act
            var result = controller.SubmitClaim(claim) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(controller.ModelState.ErrorCount > 0);
            Assert.Contains("File size must be less than 2MB.", controller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        }

        [Fact]
        public void SubmitClaim_InvalidFileExtension_ReturnsError()
        {
            // Arrange
            var mockContext = CreateMockContext();
            var controller = new ClaimController(mockContext.Object);
            var claim = new Claim
            {
                LecturerID = 1,
                SubmissionDate = DateTime.Now,
                HoursWorked = 10,
                HourlyRate = 20,
                TotalClaimAmount = 200,
                ClaimStatus = "Pending",
                DocumentNames = "invalidfile.txt" // Invalid file extension
            };

            // Act
            var result = controller.SubmitClaim(claim) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(controller.ModelState.ErrorCount > 0);
            Assert.Contains("Only .docx, .pdf, or .xlsx files are allowed.", controller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        }

        [Theory]
        [InlineData(-5, 20, 0, "HoursWorked must be greater than zero.")]
        [InlineData(10, -20, 0, "HourlyRate must be greater than zero.")]
        [InlineData(10, 20, -200, "TotalClaimAmount must be greater than zero.")]
        public void SubmitClaim_NegativeValues_ReturnsError(int hoursWorked, double hourlyRate, double totalClaimAmount, string expectedErrorMessage)
        {
            // Arrange
            var mockContext = CreateMockContext();
            var controller = new ClaimController(mockContext.Object);
            var claim = new Claim
            {
                LecturerID = 1,
                SubmissionDate = DateTime.Now,
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                TotalClaimAmount = totalClaimAmount,
                ClaimStatus = "Pending",
                Document = new byte[1024] // Simulating a file
            };

            // Act
            var result = controller.SubmitClaim(claim) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(controller.ModelState.ErrorCount > 0);
            Assert.Contains(expectedErrorMessage, controller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        }
    }
}
