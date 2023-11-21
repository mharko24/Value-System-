using ContractManagementValue.Data;
using ContractManagementValue.Interfaces;
using ContractManagementValue.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractManagementValue.Repositories
{
    public class FileUploadRepo : IFileUpload
    {
        private IGenericRepo<FileUpload> _repo;
        private readonly ApplicationDbContext _context; 
        public FileUploadRepo(IGenericRepo<FileUpload> repo, ApplicationDbContext context) 
        {
            _repo = repo;   
            _context = context;
        }
        public IEnumerable<FileUpload> GetAllFile()
        {
           return _repo.GetAll();
        }

        public FileUpload GetFileId(object id)
        {
            return _repo.GetId(id);
        }
        public async Task<List<FileUpload>> GetFileSiteId(int id)
        {
            var getFileBySiteId = await _context.FileUploads.Where(f => f.CMSiteId == id).ToListAsync();
            
            return getFileBySiteId;
        }
        public  List<FileUpload> GetAllSiteFiles(int id)
        {
            var getFileBySiteId = _context.FileUploads.Where(f => f.CMSiteId == id).ToList();

            return getFileBySiteId;

        }

        public void InsertFile(FileUpload fileUpload)
        {
            _repo.Insert(fileUpload);
        }

        public void SaveFile()
        {
            _repo.Save();
        }

        public void UpdateFile(FileUpload fileUpload)
        {
            _repo.Update(fileUpload);
        }
        public void DeleteFile(int id)
        {
            _context.Remove(id);
        }
        public void DeleteFiles(FileUpload fileUpload)
        {
            _context.Remove(fileUpload);
        }

        public async Task<List<FileUpload>> GetFilePotId(int id)
        {
            var getFileByPotId =  _context.FileUploads.Where(f => f.PotId == id).ToList();

            return getFileByPotId;
        }

        public async Task<List<FileUpload>> GetFileEOTId(int id)
        {
            var getFileByEOTId = _context.FileUploads.Where(f => f.EOTId == id).ToList();

            return  getFileByEOTId;
        }

        public void InsertSiteFile(SiteInstruction siteInstruction)
        {
            if (siteInstruction != null)
            {
                if (siteInstruction.Files != null)
                {
                    if (siteInstruction.Files.Count > 0)
                    {
                        foreach (var file in siteInstruction.Files)
                        {
                            if (file.Length > 0)
                            {
                                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/SiteInstruction");

                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);

                                string fileNameWithPath = Path.Combine(path, file.FileName);

                                // Save the file to disk
                                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                                {
                                    file.CopyToAsync(stream);
                                }
                                FileUpload fileUpload = new FileUpload
                                {
                                    FileName = file.FileName,
                                    FileSize = file.Length,
                                    FilePath = path,
                                    CMSiteId = siteInstruction.CMSiteId,
                                };
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.CopyToAsync(memoryStream);
                                    fileUpload.FileContent = memoryStream.ToArray();
                                }
                                //_dbContext.FileUploads.Add(fileUpload);
                                InsertFile(fileUpload);
                                SaveFile();

                            }

                        }
                    }
                }

            }
        }

        public void InsertPotFile(PotentialClaim potentialClaim)
        {
            throw new NotImplementedException();
        }

        public void InsertEOTFile(EOTClaim eotClaim)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileUpload> GetAllSiteFileById(int id)
        {
            return  _context.FileUploads.Where(f => f.CMSiteId == id).ToList();
        }

        object IFileUpload.GetAllSiteFiles(int id)
        {
            throw new NotImplementedException();
        }
    }
}
