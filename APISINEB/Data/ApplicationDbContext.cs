using APISINEB.Models;
using Microsoft.EntityFrameworkCore;

namespace APISINEB.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        //aqui se agregan todos los Modelos (entidades) que se esten creando
        public DbSet<Users> Users { get; set; }
    }
}
