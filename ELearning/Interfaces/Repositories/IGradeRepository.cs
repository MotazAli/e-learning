using ELearning.Models;

namespace ELearning.Interfaces.Repositories;
public interface IGradeRepository 
{
    public Task<IEnumerable<Grade>> SelectAllGradesAsync(CancellationToken cancellationToken);
    public Task<IEnumerable<Grade>> SelectGradesPagingAsync(int skip, int take, CancellationToken cancellationToken);
    public Task<Grade?> SelectGradeByIdAsync(long? id, CancellationToken cancellationToken);
    public Task<Grade?> InsertGradeAsync(Grade grade, CancellationToken cancellationToken);
    public Task<Grade?> UpdateGradeAsync(long? id, Grade grade, CancellationToken cancellationToken);
    public Task<Grade?> DeleteGradeByIdAsync(long? id, CancellationToken cancellationToken);
}

