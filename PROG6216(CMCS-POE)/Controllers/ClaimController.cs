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
            return View();
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        
        }

        
    }
}
