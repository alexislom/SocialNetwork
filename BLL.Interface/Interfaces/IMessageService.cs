using BLL.Interface.Entities;
using System.Collections.Generic;

namespace BLL.Interface.Interfaces
{
    public interface IMessageService : IService<BllMessage>
    {
        IEnumerable<BllMessage> GetAllChatsWith(int userId);
        void DeleteAllUserMessagesById(int id);
    }
}
