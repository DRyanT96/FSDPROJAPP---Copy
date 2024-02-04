using FSDPROJAPP.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Drawing;

namespace FSDPROJAPP.Server.Configurations.Entities
{
    public class UsernameSeedConfiguration : IEntityTypeConfiguration<Username>
    {
        public void Configure(EntityTypeBuilder<Username> builder)
        {
            builder.HasData(
                new Username
                {
                    Id = 1,
                    Name = "Bryan",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                });
               
        }
    }
}
