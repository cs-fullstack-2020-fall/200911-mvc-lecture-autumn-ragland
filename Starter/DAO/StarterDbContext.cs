using Microsoft.EntityFrameworkCore;
using Starter.Models;
namespace Starter.DAO
{
    public class StarterDbContext : DbContext
    {
        public StarterDbContext(DbContextOptions<StarterDbContext> options) : base (options)
        {
        }
        // add db set(s)
        public DbSet<ProfessorModel> professors{get;set;}
        public DbSet<CourseModel> courses{get;set;}
    }
}