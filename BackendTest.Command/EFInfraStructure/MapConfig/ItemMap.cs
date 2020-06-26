using BackendTest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendTest.Command.EFInfraStructure.MapConfig
{
    internal class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(512).IsRequired();
            builder.HasMany(x => x.StatusHistory).WithOne(x => x.Item).IsRequired();
        }
    }
}