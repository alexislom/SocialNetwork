using DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Interfaces
{
    public interface IFriendRequestRepository : IRepository<DalFriendRequest>
    {
        void DeleteAllUserRelationById(int id);
    }
}
