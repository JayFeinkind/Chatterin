using Chatterin.ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.DataAccess.TableDefinitions
{
    public class UserConversationTypeConfiguration : IEntityTypeConfiguration<UserConversation>
    {
        public void Configure(EntityTypeBuilder<UserConversation> builder)
        {
            builder.ToTable("UserConversations", "Messages");

            builder.HasOne(d => d.Conversation)
                .WithMany(p => p.UserConversations)
                .HasForeignKey(d => d.ConversationId)
                .HasConstraintName("FK__UserConve__Conve__6383C8BA");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserConversations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserConve__UserI__6477ECF3");
        }
    }
}
