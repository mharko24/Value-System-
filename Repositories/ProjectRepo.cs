using ContractManagementValue.Data;
using ContractManagementValue.Models;

namespace ContractManagementValue.Repositories
{
    public class ProjectRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public ProjectRepo(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext; 
        }
       public IEnumerable<Project> GetProjects() 
        {
            return _dbContext.Projects;
        }
        
       
    }
}
