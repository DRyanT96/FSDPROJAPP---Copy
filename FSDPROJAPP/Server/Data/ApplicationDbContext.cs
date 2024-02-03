using Duende.IdentityServer.EntityFramework.Options;
using FSDPROJAPP.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FSDPROJAPP.Shared.Domain;
using FSDPROJAPP.Server.Configurations.Entities;

namespace FSDPROJAPP.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        //public DbSet<Preference> Preferences { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Hobby> Hobbys { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new HobbySeedConfiguration());

            builder.ApplyConfiguration(new DetailSeedConfiguration());

            builder.ApplyConfiguration(new LikeSeedConfiguration());

            builder.ApplyConfiguration(new DislikeSeedConfiguration());

            builder.ApplyConfiguration(new RoleSeedConfiguration());

            builder.ApplyConfiguration(new UserSeedConfiguration());

            builder.ApplyConfiguration(new UserRoleSeedConfiguration());
        }

    }
}
