using FSDPROJAPP.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Drawing;

namespace FSDPROJAPP.Server.Configurations.Entities
{
    public class DislikeSeedConfiguration : IEntityTypeConfiguration<Dislike>
    {
        public void Configure(EntityTypeBuilder<Dislike> builder)
        {
            builder.HasData(
                new Dislike
                {
                    Id = 1,
                    Name = "Matcha",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Dislike
                {
                    Id = 2,
                    Name = "Shopping",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
                );
        }
    }
}
