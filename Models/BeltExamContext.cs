using Microsoft.EntityFrameworkCore;

namespace BeltExam.Models
{
  public class BeltExamContext : DbContext
  {
    public BeltExamContext(DbContextOptions<BeltExamContext> options) : base(options) {}
    public DbSet<User> Users {get; set;}
    public DbSet<Idea> Ideas {get; set;}
    public DbSet<Like> Likes {get; set;}
  }
}