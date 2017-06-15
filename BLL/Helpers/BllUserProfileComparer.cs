using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Helpers
{
    public class BllUserProfileComparer : IEqualityComparer<BllUserProfile>
    {
        public bool Equals(BllUserProfile x, BllUserProfile y)
        {
            if (x.Id == y.Id)
                return true;
            return false;
        }

        public int GetHashCode(BllUserProfile obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
