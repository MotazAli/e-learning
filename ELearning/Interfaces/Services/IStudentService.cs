using ELearning.Models;

namespace ELearning.Interfaces.Services;
public interface IStudentService
{
    //public Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken cancellationToken);
    public Task<IEnumerable<Student>> GetStudentsAsync(int? page, int? numberOfRows, CancellationToken cancellationToken);
    public Task<Student?> GetStudentByIdAsync(long? id, CancellationToken cancellationToken);
    public Task<Student?> CreateStudentAsync(Student student, CancellationToken cancellationToken);
    public Task<Student?> UpdateStudentAsync(long? id, Student student, CancellationToken cancellationToken);
    public Task<Student?> DeleteStudentByIdAsync(long? id, CancellationToken cancellationToken);
}

