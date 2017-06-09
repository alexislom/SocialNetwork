using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ConfigurationEntities
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasKey(p => p.Id);

            //HasMany(p => p.Friends)
            //    .WithRequired(r => r.User)
            //    .HasForeignKey(k => k.UserId);

            //HasMany(p => p.Messages)
            //    .WithRequired(r => r.FromUser)
            //    .HasForeignKey(k => k.FromUserId);

            Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(30);

            //Property(p => p.CreationDate)
            //    .IsRequired();

            Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(30);

            Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(70);
        }
    }
}
