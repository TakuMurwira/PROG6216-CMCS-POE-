using Microsoft.AspNetCore.Mvc;
using PROG6216_CMCS_POE_.DataAccesLayer;
using PROG6216_CMCS_POE_.Models;
using PROG6216_CMCS_POE_.Models.DBEntities;
using System.Collections.Generic;
using System.Linq;

namespace PROG6216_CMCS_POE_.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ClaimsDbContext _context;

        public ClaimController(ClaimsDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Fetch the updated claims list from the database
            var claims = _context.Claims
                .OrderBy(c => c.ClaimStatus == "Pending" ? 0 : 1) // Pending claims first
                .ThenByDescending(c => c.SubmissionDate)          // Then by submission date
                .ToList();

            // Map to the ViewModel if necessary
            List<ClaimViewModel> claimList = claims.Select(claim => new ClaimViewModel()
            {
                ClaimID = claim.ClaimID,
                LecturerID = claim.LecturerID,
                SubmissionDate = claim.SubmissionDate,
                HoursWorked = claim.HoursWorked,
                HourlyRate = claim.HourlyRate,
                TotalClaimAmount = claim.TotalClaimAmount,
                AddNotes = claim.AddNotes,
                DocumentNames = claim.DocumentNames,
                ClaimStatus = claim.ClaimStatus,
            }).ToList();

            return View(claimList);
        }

        [HttpGet]
        public IActionResult LecturerClaims()
        {
            // Fetch all claims without filtering
            var claims = _context.Claims
                .OrderBy(c => c.ClaimStatus == "Pending" ? 0 : 1) // Pending claims first
                .ThenByDescending(c => c.SubmissionDate)          // Then by submission date
                .ToList();

            // Map to the ViewModel
            List<ClaimViewModel> claimList = claims.Select(claim => new ClaimViewModel()
            {
                ClaimID = claim.ClaimID,
                LecturerID = claim.LecturerID,
                SubmissionDate = claim.SubmissionDate,
                HoursWorked = claim.HoursWorked,
                HourlyRate = claim.HourlyRate,
                TotalClaimAmount = claim.TotalClaimAmount,
                AddNotes = claim.AddNotes,
                DocumentNames = claim.DocumentNames,
                ClaimStatus = claim.ClaimStatus,
            }).ToList();

            return View(claimList); // Return to the LecturerClaims view
        }

        [HttpGet]
        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim(ClaimViewModel claimData, List<IFormFile> files)
        {
            try
            {
                // Validate for negative values
                if (claimData.HoursWorked < 0 || claimData.HourlyRate < 0)
                {
                    TempData["errorMessage"] = "Hours worked and hourly rate cannot be negative.";
                    return View(claimData);
                }

                // Validate for past dates
                if (claimData.SubmissionDate > DateTime.Today)
                {
                    TempData["errorMessage"] = "Submission date cannot be in the Future.";
                    return View(claimData);
                }

                if (ModelState.IsValid)
                {
                    var claim = new Claim
                    {
                        LecturerID = claimData.LecturerID,
                        SubmissionDate = claimData.SubmissionDate,
                        HoursWorked = claimData.HoursWorked,
                        HourlyRate = claimData.HourlyRate,
                        TotalClaimAmount = claimData.TotalClaimAmount,
                        AddNotes = claimData.AddNotes,
                        ClaimStatus = "Pending"
                    };

                    var documentNames = new List<string>();
                    foreach (var file in files)
                    {
                        if (file.Length > 0 && file.Length <= 2 * 1024 * 1024)
                        {
                            var extension = Path.GetExtension(file.FileName).ToLower();
                            if (extension == ".docx" || extension == ".pdf" || extension == ".xlsx")
                            {
                                documentNames.Add(file.FileName);
                            }
                            else
                            {
                                TempData["errorMessage"] = "Only .docx, .pdf, and .xlsx files are allowed.";
                                return View(claimData);
                            }
                        }
                        else
                        {
                            TempData["errorMessage"] = "File size must be less than 2MB.";
                            return View(claimData);
                        }
                    }

                    claim.DocumentNames = string.Join(", ", documentNames);
                    _context.Claims.Add(claim);
                    _context.SaveChanges();

                    TempData["successMessage"] = "Claim captured successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Claim entries are not valid";
                    return View(claimData);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(claimData);
            }
        }


        public IActionResult Approve(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == id);
            if (claim != null)
            {
                claim.ClaimStatus = "Approved";
                _context.SaveChanges();
                TempData["successMessage"] = "Claim approved successfully.";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == id);
            if (claim != null)
            {
                claim.ClaimStatus = "Rejected";
                _context.SaveChanges();
                TempData["errorMessage"] = "Claim rejected.";
            }
            return RedirectToAction("Index");
        }
    }
}
