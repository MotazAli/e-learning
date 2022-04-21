using ELearning.Models;

namespace ELearning.Interfaces.Services;
public interface IGradeService 
{
    public Task<IEnumerable<Grade>> GetGradesAsync(int? page, int? numberOfRows, CancellationToken cancellationToken);
    public Task<Grade?> GetGradeByIdAsync(long? id, CancellationToken cancellationToken);
    public Task<Grade?> CreateGradeAsync(Grade grade, CancellationToken cancellationToken);
    public Task<Grade?> UpdateGradeAsync(long? id, Grade grade, CancellationToken cancellationToken);
    public Task<Grade?> DeleteGradeByIdAsync(long? id, CancellationToken cancellationToken);
}

