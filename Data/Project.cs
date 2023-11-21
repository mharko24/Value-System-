using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementValue.Data
{
    public class Project
    {
        [Key]
        
        public int ProjId { get; set; }
        public string ProjectName { get; set; }
        public int? ContractValue { get; set; }
        [NotMapped]
        public UserApp? UserApps { get; set; }

        
    }
}
