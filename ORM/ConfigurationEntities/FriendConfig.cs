using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ORM.ConfigurationEntities
{
    public class FriendConfig : EntityTypeConfiguration<Friend>
    {
        public FriendConfig()
        {
            HasKey(p => p.Id);
        }
    }
}
