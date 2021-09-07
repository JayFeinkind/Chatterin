using Chatterin.ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.DataAccess.TableDefinitions
{
    public class RefreshTokenTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens", "Accounts");

            builder.HasKey(e => e.Token);

            builder.HasIndex(e => e.UserId).HasName("IX_RefreshToken_UserId");

            builder.Property(e => e.Token).IsRequired();

            builder.Property(e => e.TokenExpiredUtc).IsRequired();

            builder.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__UserI__5EBF139D");
        }
    }
}