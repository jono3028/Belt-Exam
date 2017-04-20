using Microsoft.EntityFrameworkCore;

namespace BeltExam.Models
{
  public class BeltExamContext : DbContext
  {
    public BeltExamContext(DbContextOptions<BeltExamContext> options) : base(options) {}
    public DbSet<User> Users {get; set;}
  }
}