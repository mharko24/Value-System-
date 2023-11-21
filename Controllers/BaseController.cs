using ContractManagementValue.Data;
using ContractManagementValue.Interfaces;
using ContractManagementValue.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagementValue.Controllers
{
    public class BaseController : Controller
    {
        private IGenericRepo<FileUpload> _Filerepo;
        public BaseController(IGenericRepo<FileUpload> Filerepo) 
        {
            _Filerepo = Filerepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        private async Task InserFile(SiteInstruction siteInstruction)
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
                                    await file.CopyToAsync(stream);
                                }
                                FileUpload fileUpload = new FileUpload
                                {
                                    FileName = file.FileName,
                                    FileSize = file.Length,
                                    FilePath = path,
                                    //CMSiteId = siteInstruction.CMSiteId,
                                };
                                using (var memoryStream = new MemoryStream())
                                {
                                    await file.CopyToAsync(memoryStream);
                                    fileUpload.FileContent = memoryStream.ToArray();
                                }
                                //_dbContext.FileUploads.Add(fileUpload);
                                _Filerepo.Insert(fileUpload);

                            }

                        }
                    }
                }

            }
        }
    }
}
