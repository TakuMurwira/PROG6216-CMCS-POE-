using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG6216_CMCS_POE_.Models.DBEntities
{
    public class Claim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaimID {  get; set; }

        [Required(ErrorMessage ="Please enter a Lecturer ID")]
        public int LecturerID { get; set; }

        [Required(ErrorMessage = "Description is required.")] //Use this for error messages when you try validate data
        [DataType(DataType.DateTime)]//Set a datatype for complex types: anything more complex than a number or string
        public DateTime SubmissionDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")] //Set acceptable min and max values for numbers
        public int HoursWorked { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double HourlyRate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double TotalClaimAmount { get; set; }
        public string? AddNotes { get; set; }

        public byte[]? Document { get; set; }
    }
}
