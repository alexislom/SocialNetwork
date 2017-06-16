using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Interfaces
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetUserByEmail(string email);
    }
}
