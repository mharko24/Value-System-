using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementValue.Data
{
    public class PotentialClaim
    {
        [Key]
        public int PotId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Required]
        [Display(Name = "CVI Number")]
        public string CVINumber { get; set; }
        [Required]
        [Display(Name = "Asec-PMI Number")]
        public string AsecPMINumber { get; set; }
        public string Remarks { get; set; }
        //[Required]
        //[Display(Name = "Status")]
        public string Status { get; set; } = "Open";

        public int? Amount { get; set; }

        public string? PONumber { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]

        //[NotMapped]
        public UserApp? UserApps { get; set; }







        [NotMapped]
        public IList<IFormFile>? Files { get; set; }
    }
}
