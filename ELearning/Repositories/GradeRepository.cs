using ELearning.Interfaces.Repositories;
using ELearning.Models;
using ELearning.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly AspireContext _context;

    public GradeRepository(AspireContext context)
    {
        _context = context;
    }
    public async Task<Grade?> DeleteGradeByIdAsync(long? id, CancellationToken cancellationToken)
    {
        var oldGrade = await SelectGradeByIdAsync(id, cancellationToken);
        if (oldGrade == null) throw new NotFoundException($"Grade with id {id} not found"); ;

        _context.Grades.Remove(oldGrade);
        return oldGrade;
    }

    public async Task<Grade?> InsertGradeAsync(Grade grade, CancellationToken cancellationToken)
    {
        var insertedResult = await _context.Grades.AddAsync(grade, cancellationToken);
        return insertedResult.Entity;
    }

    public async Task<IEnumerable<Grade>> SelectAllGradesAsync(CancellationToken cancellationToken)
    {
        return await _context.Grades
            .ToListAsync(cancellationToken);
    }

    public async Task<Grade?> SelectGradeByIdAsync(long? id, CancellationToken cancellationToken)
    {
        var grade = await _context.Grades.Include(g => g.Students).FirstOrDefaultAsync(g => g.GradeId == id, cancellationToken);
        if (grade == null) throw new NotFoundException($"Grades with id {id} not found");

        return grade;
    }

    public async Task<IEnumerable<Grade>> SelectGradesPagingAsync(int skip, int take, CancellationToken cancellationToken)
    {
        return await _context.Grades
           .Skip(skip)
           .Take(take)
           .ToListAsync(cancellationToken);
    }

    public async Task<Grade?> UpdateGradeAsync(long? id, Grade grade, CancellationToken cancellationToken)
    {
        var oldGradet = await SelectGradeByIdAsync(id, cancellationToken);
        if (oldGradet == null) throw new NotFoundException($"Student with id {id} not found");

        oldGradet.GradeNameEn = (grade.GradeNameEn == string.Empty) ? oldGradet.GradeNameEn : grade.GradeNameEn;
        oldGradet.GradeNameAr = (grade.GradeNameAr == string.Empty) ? oldGradet.GradeNameAr : grade.GradeNameAr;
        _context.Entry<Grade>(oldGradet).State = EntityState.Modified;
        return oldGradet;
    }
}

