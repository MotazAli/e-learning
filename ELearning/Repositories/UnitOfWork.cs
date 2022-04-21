using ELearning.Interfaces.Repositories;
using ELearning.Models;

namespace ELearning.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AspireContext _context;
    private IStudentRepository _StudentRepository;
    private IGradeRepository _GradeRepository;

    public UnitOfWork(AspireContext context)
    {
        _context = context;
    }


    public IStudentRepository StudentRepository => _StudentRepository ??= new StudentRepository(_context);
    public IGradeRepository GradeRepository => _GradeRepository ??= new GradeRepository(_context);



    public async Task<int> Save(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
