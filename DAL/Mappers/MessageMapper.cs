using DAL.Interfaces.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class MessageMapper
    {
        public static Message ToOrmMessage(this DalMessage dalMessage)
        {
            if (dalMessage == null)
                return null;

            return new Message
            {
                Id = dalMessage.Id,
                Date = dalMessage.Date,
                TextMessage = dalMessage.TextMessage,
                FromUserId = dalMessage.FromUserId,
                ToUserId = dalMessage.ToUserId
            };
        }
        public static DalMessage ToDalMessage(this Message ormMessage)
        {
            if (ormMessage == null)
                return null;

            return new DalMessage()
            {
                Id = ormMessage.Id,
                Date = ormMessage.Date,
                TextMessage = ormMessage.TextMessage,
                FromUserId = ormMessage.FromUserId,
                ToUserId = ormMessage.ToUserId,
                FromUser = ormMessage.FromUser.ToDalUserProfile(),
                ToUser = ormMessage.ToUser.ToDalUserProfile()
            };
        }
    }
}
