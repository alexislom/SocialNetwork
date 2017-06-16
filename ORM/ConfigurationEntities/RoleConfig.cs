using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

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
