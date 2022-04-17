using ELearning.Models;

namespace ELearning.Interfaces.Repositories;
public interface IStudentRepository
{
    public Task<IEnumerable<Student>> SelectAllStudentsAsync(CancellationToken cancellationToken);
    public Task<IEnumerable<Student>> SelectStudentsPagingAsync(int skip, int take, CancellationToken cancellationToken);
    public Task<Student?> SelectStudentByIdAsync(long? id, CancellationToken cancellationToken);
    public Task<Student?> InsertStudentAsync(Student student,CancellationToken cancellationToken);
    public Task<Student?> UpdateStudentAsync(long? id,Student student, CancellationToken cancellationToken);
    public Task<Student?> DeleteStudentByIdAsync(long? id, CancellationToken cancellationToken);
}

