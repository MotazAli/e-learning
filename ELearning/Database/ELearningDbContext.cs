using ELearning.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Database;
public class ELearningDbContext : DbContext 
{

    public ELearningDbContext(DbContextOptions<ELearningDbContext> options) 
        : base(options) 
    { 
    }

    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ELearningDbContext).Assembly);
        //base.OnModelCreating(modelBuilder);
    }


    
}

