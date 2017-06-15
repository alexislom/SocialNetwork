using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    public interface IFriendRequestService : IService<BllFriendRequest>
    {
        bool IsFriend(int curUser, int otherUser);
        void AddFriend(int userId, int otherUserId);
        bool IsRequested(int currentUser, int otherUser);
        void DeleteAllUserRelationById(int id);
    }
}
