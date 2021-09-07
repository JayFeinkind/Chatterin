using Chatterin.ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.DataAccess.TableDefinitions
{
    public class MessageTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages", "Messages");

            builder.Property(e => e.MessageTxt)
                .IsRequired()
                .HasColumnName("Message")
                .HasMaxLength(500);

            builder.HasOne(d => d.UserConversation)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserConversationId)
                .HasConstraintName("FK__Messages__UserCo__6754599E");
        }
    }
}
