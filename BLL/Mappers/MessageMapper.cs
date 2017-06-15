using DAL.Interfaces.DTO;
using BLL.Interface.Entities;

namespace BLL.Mappers
{
    public static class MessageMapper
    {
        public static BllMessage ToBllMessage(this DalMessage dalMessage)
        {
            if (dalMessage == null)
                return null;

            return new BllMessage
            {
                Id = dalMessage.Id,
                Date = dalMessage.Date,
                TextMessage = dalMessage.TextMessage,
                FromUserId = dalMessage.FromUserId,
                ToUserId = dalMessage.ToUserId,
                UserFrom = dalMessage.FromUser.ToBllUserProfile(),
                UserTo =dalMessage.ToUser.ToBllUserProfile()
            };
        }
        public static DalMessage ToDalMessage(this BllMessage ormMessage)
        {
            if (ormMessage == null)
                return null;

            return new DalMessage
            {
                Id = ormMessage.Id,
                Date = ormMessage.Date,
                TextMessage = ormMessage.TextMessage,
                FromUserId = ormMessage.FromUserId,
                ToUserId = ormMessage.ToUserId
            };
        }
    }
}
