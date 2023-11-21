using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementValue.Data
{
    public class EOTClaim
    {
        [Key]
        public int EOTId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Required]
        [Display(Name = "PMI Number")]
        public string PMINumber { get; set; }
        [Required]
        [Display(Name = "VO Number")]
        public string VONumber { get; set; }
        public string Remarks { get; set; }
        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Open";
        [Required]
        [Display(Name = "PO Number")]
        public string PONumber { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]

        //[NotMapped]
        public UserApp? UserApps { get; set; }




        [NotMapped]
        public IList<IFormFile> Files { get; set; }
    }
}
