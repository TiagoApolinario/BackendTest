using Microsoft.EntityFrameworkCore.Design;

namespace BackendTest.Command.EFInfraStructure
{
    public class MigrationFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BackendTest;Integrated Security=True";
            return new AppDbContext(connectionString);
        }
    }
}