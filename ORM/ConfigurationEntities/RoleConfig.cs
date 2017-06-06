using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ConfigurationEntities
{
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            HasKey(p => p.Id);
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
