using ContractManagementValue.Data;
using ContractManagementValue.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ContractManagementValue.Repositories;
using ContractManagementValue.Models.ViewModel;
using ContractManagementValue.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContractManagementValue.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserRepository _userRepo;
        private readonly ProjectRepo _projRepo;
        private readonly IGenericRepo<UserApp> _repo;

        private bool _disconnect;
        public AccountController(ApplicationDbContext dbContext, UserRepository userRepo,ProjectRepo projRepo, IGenericRepo<UserApp> repo )
        {
            _userRepo = userRepo;
            _dbContext = dbContext;
            _projRepo = projRepo;
            _repo = repo;
             
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var userFromDb = await _userRepo.GetUserDetails(username, password);

            if (userFromDb == null)
            {
                ViewBag.login = "Invalid Credentials";
                return View();
            }

            var department = userFromDb.Department;
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, userFromDb.Username),
        new Claim(ClaimTypes.Email, userFromDb.Email),
        new Claim("UserId", userFromDb.UserId.ToString())
    };

            if (IsRoleValid(department))
            {
                claims.Add(new Claim(ClaimTypes.Role, department));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                _disconnect = true;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        private bool IsRoleValid(string department)
        {
            string[] validRoles = { "Admin", "Cost", "Office", "Superindent/Manager", "Mamplasan", "Planning", "Engineering", "Audit", "LYE", "MTYE", "Management" };
            return validRoles.Contains(department);
        }
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear(); // Clear session data
            HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
       
        public IActionResult CreateAccount()
        {
            List<Project> ProjectList = new List<Project>();
            ProjectList = (from project in _dbContext.Projects
                           select project).ToList();
         
           // ProjectList.Insert(0, new Project { ProjId = 0, ProjectName = "Select Project Name" });
            ViewBag.ProjectList = ProjectList;
            //var user = new UserApp();
            //user.ProjectList = _dbContext.Projects.ToList();
            //ViewBag.ProjectList = _dbContext.Projects.Select(p => new SelectListItem
            //{
            //    Value = p.ProjId.ToString(),  
            //    Text = p.ProjectName  // Use the appropriate property of Project as the text
            //})
            //.ToList();


            return View();
        }
        public async Task<IActionResult> Register(UserApp userApp)
        {
            if (userApp == null)
            {
                ViewBag.register = string.Format("Invalid Credentials");
                return View("CreateAccount");
            }
            if(userApp.Password !=userApp.ConfirmPassword)
            {
                ViewBag.register = string.Format("Password Not Match!");
                return View("CreateAccount");
            }
            if (userApp != null)
            {
                if (!string.IsNullOrEmpty(userApp.Username))
                {
                    var checkName =  _dbContext.UserApps
                        .Where(l => l.Username == userApp.Username)
                        .ToList();

                    if (checkName.Count > 0)
                    {
                        ViewBag.register = "Username already taken, Try another one";
                        return View("CreateAccount", userApp);
                    }
                }
                _repo.Insert(userApp);
                _repo.Save();
                ViewBag.register = ("new user successfully registered!");
                InsertUserProjects(userApp);

                return View("CreateAccount");
            }
           // userApp.ProjectList = _dbContext.Projects.ToList();
            if (!string.IsNullOrEmpty(userApp.ProjectName))
            {
               
            }
            if (userApp.userProjects !=null)
            {

                var getIdByName = _dbContext.Projects
                   .Where(l => l.ProjectName == userApp.ProjectName)
                   .Select(c => c.ProjId)
                   .FirstOrDefault();

                var user = new UserProject();
                user.ProjId = getIdByName;
                user.UserId = userApp.UserId;
                _dbContext.Add(user);
                _dbContext.SaveChanges();
            }
            return View("Home");
        }
        public void InsertUserProjects(UserApp userApp)
        {
            if (userApp.ProjId != null)
            {

                foreach (var projectid in userApp.ProjId)
                {

                    var userProjects = new UserProject()
                    {
                        ProjId = projectid,
                        UserId = userApp.UserId,
                    };
                    _dbContext.Add(userProjects);
                    _dbContext.SaveChanges();
                }
            }



        }
    }
}
