using DAL.Interfaces.DTO;
using System.Collections.Generic;

namespace DAL.Interfaces.Interfaces
{
    public interface IMessageRepository : IRepository<DalMessage>
    {
        List<DalMessage> GetMessages(int FromUser, int ToUser);
        void DeleteAllUserMessagesById(int id);
    }
}
