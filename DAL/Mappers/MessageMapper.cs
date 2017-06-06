using DAL.Interfaces.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class MessageMapper
    {
        public static Message ToOrmMessage(this DalMessage dalMessage)
        {
            if (dalMessage == null)
                return null;
            var ormMessage = new Message()
            {
                Id = dalMessage.Id,
                Date = dalMessage.Date,
                TextMessage = dalMessage.TextMessage,
                FromUserId = dalMessage.FromUserId,
                ToUserId = dalMessage.ToUserId
            };
            return ormMessage;
        }
        public static DalMessage ToDalMessage(this Message ormMessage)
        {
            if (ormMessage == null)
                return null;
            var dalMessage = new DalMessage()
            {
                Id = ormMessage.Id,
                Date = ormMessage.Date,
                TextMessage = ormMessage.TextMessage,
                FromUserId = ormMessage.FromUserId,
                ToUserId = ormMessage.ToUserId,
                FromUser = ormMessage.FromUser.ToDalUserProfile(),
                ToUser = ormMessage.ToUser.ToDalUserProfile()
            };
            return dalMessage;
        }
    }
}
