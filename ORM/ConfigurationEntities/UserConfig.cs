using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ORM.ConfigurationEntities
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(30);

            Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(30);

            Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(70);
        }
    }
}
