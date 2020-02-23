using Andreys.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Andreys.Data
{
    using Microsoft.EntityFrameworkCore;

    public class AndreysDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-DFIV1S7\SQLEXPRESS;Database=AndreysDatabase;Integrated Security=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
