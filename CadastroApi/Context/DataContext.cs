using CadastroApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroApi.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

    }
}
