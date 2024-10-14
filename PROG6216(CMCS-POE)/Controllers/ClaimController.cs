using Microsoft.AspNetCore.Mvc;
using PROG6216_CMCS_POE_.DataAccesLayer;
using PROG6216_CMCS_POE_.Models;
using PROG6216_CMCS_POE_.Models.DBEntities;

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
            var claims = _context.Claims.ToList();

            List<ClaimViewModel> claimList = new List<ClaimViewModel>();
            if(claims != null)
            {
                foreach(var claim in claims)
                {
                    var ClaimViewModel = new ClaimViewModel()
                    {
                        ClaimID = claim.ClaimID,
                        LecturerID = claim.LecturerID,
                        SubmissionDate = claim.SubmissionDate,
                        HoursWorked = claim.HoursWorked,
                        HourlyRate = claim.HourlyRate,
                        TotalClaimAmount = claim.TotalClaimAmount,
                        AddNotes = claim.AddNotes,
                        Document = claim.Document,
                    };
                    claimList.Add(ClaimViewModel);
                }
                return View(claimList);
            }
            return View(claimList);
        }

        [HttpGet]
        public IActionResult SubmitClaim() 
        {
            return View();
        
        }

        [HttpPost]
        public IActionResult SubmitClaim(ClaimViewModel claimData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var claim = new Claim()
                    {
                        LecturerID = claimData.LecturerID,
                        SubmissionDate = claimData.SubmissionDate,
                        HoursWorked = claimData.HoursWorked,
                        HourlyRate = claimData.HourlyRate,
                        TotalClaimAmount = claimData.TotalClaimAmount,
                        AddNotes = claimData.AddNotes,
                        Document = claimData.Document,
                    };

                    _context.Claims.Add(claim);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Claim captured successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Claim entries not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
                throw;
            }
        }

        
    }
}
