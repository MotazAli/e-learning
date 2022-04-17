using ELearning.Interfaces.Repositories;
using ELearning.Interfaces.Services;
using ELearning.Models;
using ELearning.Utilities;

namespace ELearning.Services;
public class StudentService : IStudentService
{


    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Student?> DeleteStudentByIdAsync(long? id, CancellationToken cancellationToken)
    {
        var oldStudent =  await _unitOfWork.StudentRepository.DeleteStudentByIdAsync(id, cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result <= 0)
            throw new ServiceUnavailableException("Error in the database");


        return oldStudent;
    }

   

    public async Task<Student?> GetStudentByIdAsync(long? id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.StudentRepository.SelectStudentByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync(int? page, int? numberOfRows, CancellationToken cancellationToken)
    {

        if (page != null && numberOfRows != null)
        {
            var skip = (numberOfRows * (page - 1));
            var take = numberOfRows;
            return await _unitOfWork.StudentRepository.SelectStudentsPagingAsync((int)skip, (int)take, cancellationToken);
        }
        return await _unitOfWork.StudentRepository.SelectAllStudentsAsync(cancellationToken);

    }

    public async Task<Student?> CreateStudentAsync(Student student, CancellationToken cancellationToken)
    {
        var createResult = await _unitOfWork.StudentRepository.InsertStudentAsync(student, cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result <= 0)
            throw new ServiceUnavailableException("Error in the database");

        return createResult;
    }

    public async Task<Student?> UpdateStudentAsync(long? id, Student student, CancellationToken cancellationToken)
    {
        var updateResult = await _unitOfWork.StudentRepository.UpdateStudentAsync(id,student, cancellationToken);

        var result = await _unitOfWork.Save(cancellationToken);
        if (result <= 0)
            throw new ServiceUnavailableException("Error in the database");

        return updateResult;
    }
}

