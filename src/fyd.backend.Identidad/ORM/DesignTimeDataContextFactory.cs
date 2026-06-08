using fyd.backend.Identidad.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace fyd.backend.Identity.ORM
{
    public class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<ContextoSeguridad>
    {
        public ContextoSeguridad CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<ContextoSeguridad>().UseSqlServer("Data Source=ANDRES01\\SQLEXPRESS01;Initial Catalog=ByM-Identidad;Persist Security Info=True;Trusted_Connection=True; TrustServerCertificate=True;");
            return new ContextoSeguridad(optionsBuilder.Options);
        }
    }
}
