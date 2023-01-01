using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebProgramlama.Areas.Identity.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.firstName).HasMaxLength(50);
            builder.Property(u => u.lastName).HasMaxLength(50);
            builder.Property(u => u.PhoneNumber).HasMaxLength(13);
        }
    }
}
