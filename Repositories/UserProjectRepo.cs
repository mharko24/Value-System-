using ContractManagementValue.Data;
using ContractManagementValue.Models;
using Microsoft.CodeAnalysis;

namespace ContractManagementValue.Repositories
{
    public abstract class UserProjectRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public UserProjectRepo(ApplicationDbContext dbContext) 
        {
            _dbContext= dbContext;
        }
        public void InsertUserProjects(UserApp userApp)
        {
            if(userApp.ProjId !=null)
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
