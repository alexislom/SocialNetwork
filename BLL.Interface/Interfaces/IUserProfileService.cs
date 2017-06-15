using BLL.Interface.Entities;
using System.Collections.Generic;

namespace BLL.Interface.Interfaces
{
    public interface IUserProfileService : IService<BllUserProfile>
    {
        IEnumerable<BllUserProfile> Search(BllUserProfile profile);
    }
}