using Chatterin.ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.DataAccess.TableDefinitions
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "Accounts");

            builder.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
