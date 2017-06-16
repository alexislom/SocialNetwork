using DAL.Interfaces.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class FriendRequestMapper
    {
        public static DalFriendRequest ToDalFriendRequest(this FriendRequest ormFriendRequest)
        {
            if (ormFriendRequest == null)
                return null;

            return new DalFriendRequest
            {
                Id = ormFriendRequest.Id,
                RequestDate = ormFriendRequest.RequestDate,
                UserFromId = ormFriendRequest.UserFromId,
                UserToId = ormFriendRequest.UserToId,
                IsConfirmed = ormFriendRequest.IsConfirmed,
            };
        }
        public static FriendRequest ToOrmFriendRequest(this DalFriendRequest dalFriendRequest)
        {
            if (dalFriendRequest == null)
                return null;

            return new FriendRequest()
            {
                Id = dalFriendRequest.Id,
                RequestDate = dalFriendRequest.RequestDate,
                UserToId = dalFriendRequest.UserToId,
                UserFromId = dalFriendRequest.UserFromId,
                IsConfirmed = dalFriendRequest.IsConfirmed,
            };
        }
    }
}
