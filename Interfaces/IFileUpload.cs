using ContractManagementValue.Data;

namespace ContractManagementValue.Interfaces
{
    public interface IFileUpload
    {
        IEnumerable<FileUpload> GetAllFile();
        IEnumerable<FileUpload> GetAllSiteFileById(int id);
        FileUpload GetFileId(object id);
         Task<List<FileUpload>> GetFilePotId(int id);
         Task<List<FileUpload>> GetFileSiteId(int id);
         Task<List<FileUpload>> GetFileEOTId(int id);
        void InsertFile(FileUpload fileUpload);

        void InsertSiteFile(SiteInstruction siteInstruction);
        void InsertPotFile(PotentialClaim potentialClaim);
        void InsertEOTFile(EOTClaim eotClaim);

        void UpdateFile(FileUpload fileUpload);
        void DeleteFile(int id);
        void DeleteFiles(FileUpload fileUpload);
        void SaveFile();
        object GetAllSiteFiles(int id);
    }
}
