using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementValue.Data
{
    public class UserProject
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProjId")]
        [Column("ProjId")]
        public int ProjId { get; set; }
        [ForeignKey("UserId")]
        [Column("UserId")]
        public int UserId { get; set; }
        [NotMapped]
        public UserApp? UserApp { get; set; }
        [NotMapped]
        public Project? Project { get; set; }
    }
}
