using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ConfigurationEntities
{
    public class MessageConfig : EntityTypeConfiguration<Message>
    {
        public MessageConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.TextMessage)
                .IsRequired()
                .HasMaxLength(500);

            Property(p => p.Date)
                .IsRequired()
                .HasColumnType("datetime");
        }
    }
}
