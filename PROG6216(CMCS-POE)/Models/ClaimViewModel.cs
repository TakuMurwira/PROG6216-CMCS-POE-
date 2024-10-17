using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PROG6216_CMCS_POE_.Models
{
    public class ClaimViewModel
    {
        [DisplayName("Claim ID")]
        public int ClaimID { get; set; }

        [DisplayName("Lecturer ID")]
        public int LecturerID { get; set; }

        [DisplayName("Date")]
        public DateTime SubmissionDate { get; set; }

        [DisplayName("Hours worked")]
        public int HoursWorked { get; set; }

        [DisplayName("Hourly rate")]
        public double HourlyRate { get; set; }

        [DisplayName("Total Claim Amount")]
        public double TotalClaimAmount { get; set; }

        [DisplayName("Additional notes")]
        public string? AddNotes { get; set; }

        [DisplayName("Supporting Documents")]
        public byte[]? Document { get; set; }

        [DisplayName("Supporting Document names")]
        public string? DocumentNames { get; set; }
        
        [Required]
        [DefaultValue("Pending")]  // Add this line to set default value
        public string ClaimStatus { get; set; } = "Pending"; // Initialize with "Pending"


    }
}
