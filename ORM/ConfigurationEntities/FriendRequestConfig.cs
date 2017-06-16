using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ORM.ConfigurationEntities
{
    public class FriendRequestConfig : EntityTypeConfiguration<FriendRequest>
    {
        public FriendRequestConfig()
        {
            HasKey(p => p.Id);
        }
    }
}
