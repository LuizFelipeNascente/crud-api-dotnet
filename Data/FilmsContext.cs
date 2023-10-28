using Films.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Films.Api.Data

{
    public class FilmsContext : DbContext
    {
        public FilmsContext(DbContextOptions<FilmsContext> options )
            :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("ServerConnection"));
        }

         public DbSet<Film> Films { get; set; }

         public DbSet<User> Users { get; set; }
       

    }

 }