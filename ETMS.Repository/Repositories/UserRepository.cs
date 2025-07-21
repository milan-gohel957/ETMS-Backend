using ETMS.Domain.Entities;
using ETMS.Repository.Context;

namespace ETMS.Repository.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context)
{
    
}