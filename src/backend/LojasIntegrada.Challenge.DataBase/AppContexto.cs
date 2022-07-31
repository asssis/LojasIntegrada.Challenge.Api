using LojasIntegrada.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LojasIntegrada.Challenge.DataBase
{
    public class AppContexto : DbContext
    {
        public AppContexto()
        {
        }

        public AppContexto(DbContextOptions<AppContexto> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<ItensPurchaseOrder> ItensPurchaseOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer(@"Data Source='localhost,1433';User Id=sa;Password=aaabbbccc1234;Initial Catalog=LojasIntegrada;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(b => b.UserName).IsUnique(); 
        }
    }
}
