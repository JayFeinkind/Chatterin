using Chatterin.ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.DataAccess.TableDefinitions
{
    public class MembershipTypeConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("Membership", "Accounts");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(e => e.Salt)
                .IsRequired()
                .HasMaxLength(8);

            builder.HasOne(d => d.User)
                .WithOne(p => p.Membership)
                .HasForeignKey<Membership>(d => d.Id)
                .HasConstraintName("FK_Membership_Users");
        }
    }
}
