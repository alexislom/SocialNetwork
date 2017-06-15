using DAL.Interfaces.DTO;
using BLL.Interface.Entities;

namespace BLL.Mappers
{
    public static class FriendRequestMapper
    {
        public static DalFriendRequest ToDalFriendRequest(this BllFriendRequest bllFriendship)
        {
            if (bllFriendship == null)
                return null;

            return new DalFriendRequest
            {
                Id = bllFriendship.Id,
                RequestDate = bllFriendship.RequestDate,
                UserToId = bllFriendship.UserToId,
                UserFromId = bllFriendship.UserFromId,
                IsConfirmed = bllFriendship.IsConfirmed      
            };
        }
        public static BllFriendRequest ToBllFriendRequest(this DalFriendRequest dalFriendship)
        {
            if (dalFriendship == null)
                return null;

            return new BllFriendRequest
            {
                Id = dalFriendship.Id,
                RequestDate = dalFriendship.RequestDate,
                UserToId = dalFriendship.UserToId,
                UserFromId = dalFriendship.UserFromId,
                IsConfirmed = dalFriendship.IsConfirmed   
            };
        }
    }
}
