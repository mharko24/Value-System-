using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ContractManagementValue.Data
{
    public class UserApp
    {
        [Required]
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? ProjectName { get; set; }
        [NotMapped]
        public int[]? ProjId { get; set; }
        [NotMapped]
        public ICollection<UserProject> userProjects { get; set; }
        [NotMapped]
        public virtual ICollection<Project>[]? ProjectList { get; set; }
        [NotMapped]
        public Project? Projects { get; set; }
        
        [NotMapped]
        public string? txtProjectName { get; set; }
        [NotMapped]
        public string? ConfirmPassword { get; set; }
        [NotMapped]
        public bool checkProjectname { get; set; }


    }
}
