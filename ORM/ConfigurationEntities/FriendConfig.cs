using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
