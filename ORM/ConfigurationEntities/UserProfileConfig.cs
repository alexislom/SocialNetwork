﻿using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ConfigurationEntities
{
    public class UserProfileConfig : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfig()
        {
            HasKey(p => p.Id)
                .HasRequired(p => p.User)
                .WithRequiredPrincipal(p => p.UserProfile);

            Property(p => p.FirstName)
                .HasMaxLength(25);

            Property(p => p.LastName)
                .HasMaxLength(25);

            Property(p => p.DateOfBirth)
                .HasColumnType("date");

            //Property(p => p.Gender)
            //    .HasMaxLength(8);

            Property(p => p.MobilePhoneNumber)
                .HasMaxLength(20);

            Property(p => p.Country)
                .HasMaxLength(20);

            Property(p => p.City)
                .HasMaxLength(20);

            Property(p => p.Street)
                .HasMaxLength(30);

            Property(p => p.CompanyOfWork)
                .HasMaxLength(30);

            Ignore(p => p.Age);
        }
    }
}
