using ELearning.Interfaces.Repositories;
using ELearning.Models;
using ELearning.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Repositories;
public class StudentRepository : IStudentRepository
{


    private readonly AspireContext _context;

    public StudentRepository(AspireContext context)
    {
        _context = context;
    }

    public async Task<Student?> DeleteStudentByIdAsync(long? id, CancellationToken cancellationToken)
    {
        var oldStudent = await SelectStudentByIdAsync(id, cancellationToken);
        if (oldStudent == null) throw new NotFoundException($"Student with id {id} not found"); ;

        _context.Students.Remove(oldStudent);
        return oldStudent;
    }

    public async Task<Student?> InsertStudentAsync(Student student, CancellationToken cancellationToken)
    {
        //student.CreationDate = student.UpdatedDate = DateTime.UtcNow;
        var insertedResult = await _context.Students.AddAsync(student, cancellationToken);

        return await SelectStudentByIdAsync(insertedResult.Entity.StudentId,cancellationToken);
    }

    public async Task<IEnumerable<Student>> SelectAllStudentsAsync(CancellationToken cancellationToken)
    {
        return await _context.Students
            //.OrderByDescending(s => s.CreationDate)
            .Include(s => s.Grade)
            .ToListAsync(cancellationToken);
    }

    public async Task<Student?> SelectStudentByIdAsync(long? id, CancellationToken cancellationToken)
    {
        var student =  await _context.Students.Include(s => s.Grade).FirstOrDefaultAsync(s => s.StudentId == id, cancellationToken);
        if (student == null) throw new NotFoundException($"Student with id {id} not found");

        return student;
    }

    public async Task<IEnumerable<Student>> SelectStudentsPagingAsync(int skip, int take, CancellationToken cancellationToken)
    {
        return await _context.Students
           //.OrderByDescending(s => s.CreationDate)
           .Skip(skip)
           .Take(take)
           .Include(s => s.Grade)
           .ToListAsync(cancellationToken);
    }

    public async Task<Student?> UpdateStudentAsync(long? id, Student student, CancellationToken cancellationToken)
    {
        var oldStudent = await SelectStudentByIdAsync(id, cancellationToken);
        if (oldStudent == null) throw new NotFoundException($"Student with id {id} not found");

        oldStudent.StudentNameEn = (student.StudentNameEn == string.Empty )? oldStudent.StudentNameEn : student.StudentNameEn;
        oldStudent.StudentNameAr = (student.StudentNameAr == string.Empty) ? oldStudent.StudentNameAr : student.StudentNameAr;
        oldStudent.Birthdate = (student.Birthdate == null || !student.Birthdate.HasValue) ? oldStudent.Birthdate : student.Birthdate;
        oldStudent.GradeId = (student.GradeId == null || student.GradeId <= 0) ? oldStudent.GradeId : student.GradeId;
        _context.Entry<Student>(oldStudent).State = EntityState.Modified;
        return await SelectStudentByIdAsync(oldStudent.StudentId, cancellationToken); ;
    }
}
