using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Biblioteca.Emprestimo
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<Emprestimo> Emprestimo{ get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Exemplo>().HasKey(p => p.Id);
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
