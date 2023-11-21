using ContractManagementValue.Data;
using ContractManagementValue.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractManagementValue.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserApp> GetUserDetails(string username, string password)
        {
            //return await _dbContext.UserApps.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
            return await _dbContext.UserApps.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
        
    }
}
