using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Interfaces
{
    public interface IFriendRequestRepository : IRepository<DalFriendRequest>
    {
        void DeleteAllUserRelationById(int id);
    }
}
