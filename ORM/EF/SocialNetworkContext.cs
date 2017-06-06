using ORM.ConfigurationEntities;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.EF
{
    public class SocialNetworkContext : DbContext
    {
        #region Constructors

        public SocialNetworkContext() : base("SocialNetworkDatabase") { }

        public SocialNetworkContext(string connectionString) : base(connectionString) { }

        #endregion

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new UserProfileConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new FriendConfig());
            modelBuilder.Configurations.Add(new FriendRequestConfig());
            modelBuilder.Configurations.Add(new MessageConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
