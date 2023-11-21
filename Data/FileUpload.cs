using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementValue.Data
{
    public class FileUpload
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public byte[] FileContent { get; set; }
        public long FileSize { get; set; }
        [Column("CMSiteId")]
        public int? CMSiteId { get; set; }
        [ForeignKey("CMSiteId")]
        [NotMapped]
        public SiteInstruction? SiteInstruction { get; set; }
        [Column("PotId")]
        public int? PotId { get; set; }
        [ForeignKey("PotId")]
        [NotMapped]
        public PotentialClaim? PotentialClaim { get; set; }

        [ForeignKey("EOTId")]
        public int? EOTId { get; set; }
        [ForeignKey("EOTId")]
       [NotMapped]
        public EOTClaim? EOTClaim { get; set; }


    }
}
