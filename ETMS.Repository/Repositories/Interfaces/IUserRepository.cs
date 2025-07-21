using ETMS.Domain.Entities;

namespace ETMS.Repository.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetByEmail(string email, CancellationToken cancellationToken);

}
