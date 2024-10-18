using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PROG6216_CMCS_POE_.Controllers;
using PROG6216_CMCS_POE_.DataAccesLayer;
using PROG6216_CMCS_POE_.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PROG6216_CMCS_POE_.Tests
{
    public class ClaimControllerTests
    {
        private ClaimController _controller;
        private ClaimsDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ClaimsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Create a unique database name for each test
                .Options;

            _context = new ClaimsDbContext(options);
            _controller = new ClaimController(_context);

            // Clear the database before each test
            _context.Claims.RemoveRange(_context.Claims);
            _context.SaveChanges();
        }

        [Test]
        public void SubmitClaim_NegativeHoursWorked_ReturnsViewWithErrorMessage()
        {
            var claimData = new ClaimViewModel
            {
                HoursWorked = -1,
                HourlyRate = 100,
                SubmissionDate = DateTime.Today
            };
            var files = new List<IFormFile>();

            var result = _controller.SubmitClaim(claimData, files) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Hours worked and hourly rate cannot be negative.", _controller.TempData["errorMessage"]);
        }

        [Test]
        public void SubmitClaim_NegativeHourlyRate_ReturnsViewWithErrorMessage()
        {
            var claimData = new ClaimViewModel
            {
                HoursWorked = 10,
                HourlyRate = -100,
                SubmissionDate = DateTime.Today
            };
            var files = new List<IFormFile>();

            var result = _controller.SubmitClaim(claimData, files) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Hours worked and hourly rate cannot be negative.", _controller.TempData["errorMessage"]);
        }

        [Test]
        public void SubmitClaim_FutureSubmissionDate_ReturnsViewWithErrorMessage()
        {
            var claimData = new ClaimViewModel
            {
                HoursWorked = 10,
                HourlyRate = 100,
                SubmissionDate = DateTime.Today.AddDays(1) // Future date
            };
            var files = new List<IFormFile>();

            var result = _controller.SubmitClaim(claimData, files) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Submission date cannot be in the Future.", _controller.TempData["errorMessage"]);
        }

        [Test]
        public void SubmitClaim_InvalidFileType_ReturnsViewWithErrorMessage()
        {
            var claimData = new ClaimViewModel
            {
                HoursWorked = 10,
                HourlyRate = 100,
                SubmissionDate = DateTime.Today
            };
            var files = new List<IFormFile>
            {
                new FormFile(new MemoryStream(), 0, 1, "file", "invalid_file.txt")
            };

            var result = _controller.SubmitClaim(claimData, files) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Only .docx, .pdf, and .xlsx files are allowed.", _controller.TempData["errorMessage"]);
        }

        [Test]
        public void SubmitClaim_FileTooLarge_ReturnsViewWithErrorMessage()
        {
            var claimData = new ClaimViewModel
            {
                HoursWorked = 10,
                HourlyRate = 100,
                SubmissionDate = DateTime.Today
            };
            var files = new List<IFormFile>
            {
                new FormFile(new MemoryStream(new byte[3 * 1024 * 1024]), 0, 3 * 1024 * 1024, "file", "large_file.pdf") // 3MB file
            };

            var result = _controller.SubmitClaim(claimData, files) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("File size must be less than 2MB.", _controller.TempData["errorMessage"]);
        }

        [Test]
        public void SubmitClaim_ValidData_RedirectsToIndex()
        {
            var claimData = new ClaimViewModel
            {
                HoursWorked = 10,
                HourlyRate = 100,
                SubmissionDate = DateTime.Today
            };
            var files = new List<IFormFile>
            {
                new FormFile(new MemoryStream(), 0, 1, "file", "valid_file.docx") // Valid file
            };

            var result = _controller.SubmitClaim(claimData, files) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual(1, _context.Claims.Count()); // Check that a claim was added
        }
    }
}
