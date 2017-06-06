using DAL.Interfaces.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class FriendRequestMapper
    {
        public static DalFriendRequest ToDalFriendRequest(this FriendRequest ormFriendRequest)
        {
            if (ormFriendRequest == null)
                return null;
            var dalFriendRequest = new DalFriendRequest()
            {
                Id = ormFriendRequest.Id,
                RequestDate = ormFriendRequest.RequestDate,
                UserFromId = ormFriendRequest.UserFromId,
                UserToId = ormFriendRequest.UserToId,
                IsConfirmed = ormFriendRequest.IsConfirmed,
            };
            return dalFriendRequest;
        }
        public static FriendRequest ToOrmFriendRequest(this DalFriendRequest dalFriendRequest)
        {
            if (dalFriendRequest == null)
                return null;
            var ormFriendRequest = new FriendRequest()
            {
                Id = dalFriendRequest.Id,
                RequestDate = dalFriendRequest.RequestDate,
                UserToId = dalFriendRequest.UserToId,
                UserFromId = dalFriendRequest.UserFromId,
                IsConfirmed = dalFriendRequest.IsConfirmed,
            };
            return ormFriendRequest;
        }
    }
}
