namespace ELearning.Interfaces.Repositories;

public interface IUnitOfWork 
{
    public IStudentRepository StudentRepository { get;}
    public IGradeRepository GradeRepository { get; }
    Task<int> Save(CancellationToken cancellationToken = default);
}

