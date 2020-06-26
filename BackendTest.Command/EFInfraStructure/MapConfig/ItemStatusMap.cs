using BackendTest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendTest.Command.EFInfraStructure.MapConfig
{
    internal class ItemStatusMap : IEntityTypeConfiguration<ItemStatus>
    {
        public void Configure(EntityTypeBuilder<ItemStatus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Status);
        }
    }
}