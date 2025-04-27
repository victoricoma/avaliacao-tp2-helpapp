using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using HelpApp.Infra.Data.Context; // Namespace do seu ApplicationDbContext

namespace HelpApp.Infra.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(@"Server=WILLIANSC\SQLEXPRESS;Database=HelpAppBd;Trusted_Connection=True;TrustServerCertificate=True;");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}