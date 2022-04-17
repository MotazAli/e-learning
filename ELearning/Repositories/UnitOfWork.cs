using ELearning.Database;
using ELearning.Interfaces.Repositories;

namespace ELearning.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ELearningDbContext _context;
    private IStudentRepository _StudentRepository;

    public UnitOfWork(ELearningDbContext context)
    {
        _context = context;
    }


    public IStudentRepository StudentRepository => _StudentRepository ??= new StudentRepository(_context);



    public async Task<int> Save(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
