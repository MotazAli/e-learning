using ELearning.Interfaces.Repositories;
using ELearning.Interfaces.Services;
using ELearning.Models;
using ELearning.Utilities;

namespace ELearning.Services;
public class GradeService : IGradeService
{
    private readonly IUnitOfWork _unitOfWork;

    public GradeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Grade?> CreateGradeAsync(Grade grade, CancellationToken cancellationToken)
    {
        var createResult = await _unitOfWork.GradeRepository.InsertGradeAsync(grade, cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result <= 0)
            throw new ServiceUnavailableException("Error in the database");

        return createResult;
    }

    public async Task<Grade?> DeleteGradeByIdAsync(long? id, CancellationToken cancellationToken)
    {
        var oldGrade = await _unitOfWork.GradeRepository.DeleteGradeByIdAsync(id, cancellationToken);
        var result = await _unitOfWork.Save(cancellationToken);
        if (result <= 0)
            throw new ServiceUnavailableException("Error in the database");


        return oldGrade;
    }

    public async Task<Grade?> GetGradeByIdAsync(long? id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GradeRepository.SelectGradeByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Grade>> GetGradesAsync(int? page, int? numberOfRows, CancellationToken cancellationToken)
    {
        if (page != null && numberOfRows != null)
        {
            var skip = (numberOfRows * (page - 1));
            var take = numberOfRows;
            return await _unitOfWork.GradeRepository.SelectGradesPagingAsync((int)skip, (int)take, cancellationToken);
        }
        return await _unitOfWork.GradeRepository.SelectAllGradesAsync(cancellationToken);
    }

    public async Task<Grade?> UpdateGradeAsync(long? id, Grade grade, CancellationToken cancellationToken)
    {
        var updateResult = await _unitOfWork.GradeRepository.UpdateGradeAsync(id, grade, cancellationToken);

        var result = await _unitOfWork.Save(cancellationToken);
        if (result <= 0)
            throw new ServiceUnavailableException("Error in the database");

        return updateResult;
    }
}

