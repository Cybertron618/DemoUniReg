using System.ComponentModel.DataAnnotations;

namespace DemoUniReg.Api.Models
{
    public class StudentMIS
    {
        public int SchoolID { get; set; }
        [StringLength(5, MinimumLength = 5, ErrorMessage = "limit is 5 DIGITS")]
        public string? Pin { get; set; }
        
    }
}
