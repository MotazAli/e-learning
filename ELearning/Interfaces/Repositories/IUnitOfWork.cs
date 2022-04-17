namespace ELearning.Interfaces.Repositories;

public interface IUnitOfWork 
{
    public IStudentRepository StudentRepository { get;}
    Task<int> Save(CancellationToken cancellationToken = default);
}

