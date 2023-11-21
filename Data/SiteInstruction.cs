using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementValue.Data
{
    public class SiteInstruction
    {
        [Key]
        public int CMSiteId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Required]
        [Display(Name = "CM-PMI Number")]
        public string CMPMINumber { get; set; }
        [Required]
        [Display(Name = "Asec-PMI Number")]
        public string AsecPMINumber { get; set; }
        [Required]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        public string Status { get; set; } = "Open";

        public int? Amount { get; set; }

        public string? PONumber { get; set; }
        [Column("UserId")]
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
      
        //[NotMapped]
        public UserApp? UserApps { get; set; }

        [NotMapped]
        public List<FileUpload>? FileUpload { get; set; }
        [NotMapped]
        public IList<IFormFile> Files { get; set; }



    }
}
