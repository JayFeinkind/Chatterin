using Chatterin.ClassLibrary;
using Chatterin.DataAccess.TableDefinitions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.DataAccess
{
 
    public partial class ChatterinContext : DbContext
    {
        public ChatterinContext(DbContextOptions<ChatterinContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<UserConversation> UserConversations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQL2019;Database=Chatterin;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MembershipTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConversationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConversationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MessageTypeConfiguration());
        }
    }
}
