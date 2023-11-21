using ContractManagementValue.Common;
using ContractManagementValue.Data;
using ContractManagementValue.Interfaces;
using ContractManagementValue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace ContractManagementValue.Controllers
{
    
    public class SiteInstructionController : Controller
    {
        private IGenericRepo<SiteInstruction> _repo;
        private IFileUpload _fileRepo;
        private readonly ApplicationDbContext _context;
        private string FolderPath = "wwwroot/Files/SiteInstruction";
        public SiteInstructionController(IGenericRepo<SiteInstruction> repo, IFileUpload fileRepo,ApplicationDbContext context)
        {
            _repo = repo;
            _fileRepo = fileRepo;
            _context = context;
        }
        public async Task<IActionResult> Index(PaginatedRequest request)
        {
            var getSite = await _repo.GetPaginated(request.PageNumber, PaginatedRequest.ITEMS_PER_PAGE);
            return View(getSite);
        }
        public IActionResult Create()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            var UserId = int.TryParse(userIdClaim?.Value, out int userId) ? userId : 0;
            return View();
        }
        [HttpPost]
        public IActionResult Create(SiteInstruction siteInstruction)
        {
            if (siteInstruction != null)
            {
                _repo.Insert(siteInstruction);
                _repo.Save();
                if (siteInstruction.Files != null)
                {
                     _fileRepo.InsertSiteFile(siteInstruction);
                }

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var getSite=_repo.GetId(id);
            if(getSite ==null)
            {
                return View(getSite);
            }
   
            getSite.FileUpload = await _fileRepo.GetFileSiteId(id);
            return View(getSite);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var getSite = _repo.GetId(id);
            if (getSite == null)
            {
                return View(getSite);
            }

            getSite.FileUpload = await _fileRepo.GetFileSiteId(id);
            return View(getSite);
        }

        [HttpPost]
        public IActionResult Update(SiteInstruction siteInstruction) 
        {
            if (siteInstruction!=null)
            {
                if(siteInstruction.Files!=null)
                {
                    InsertFile(siteInstruction);
                }
                _repo.Update(siteInstruction);
                _repo.Save();
                
            }    
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var getFile = _fileRepo.GetFileSiteId(id);
            if (getFile.Result!=null) 
            {
                foreach(var fileUpload in getFile.Result)
                {
                    _fileRepo.DeleteFiles(fileUpload);
                }
               //var file= getFile.Result;
                
                _fileRepo.SaveFile();
            }
            
            _repo.Delete(id);
            _repo.Save();


            return RedirectToAction("Index");
        }
        private void InsertFile(SiteInstruction siteInstruction)
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
                                string path = Path.Combine(Directory.GetCurrentDirectory(), FolderPath);

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
                                _fileRepo.InsertFile(fileUpload);
                                _fileRepo.SaveFile();

                            }

                        }
                    }
                }

            }
        }
    }
}

