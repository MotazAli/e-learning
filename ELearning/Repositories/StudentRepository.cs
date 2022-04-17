using ELearning.Database;
using ELearning.Interfaces.Repositories;
using ELearning.Models;
using ELearning.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Repositories;
public class StudentRepository : IStudentRepository
{


    private readonly ELearningDbContext _context;

    public StudentRepository(ELearningDbContext context)
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
        student.CreationDate = student.UpdatedDate = DateTime.UtcNow;
        var insertedResult = await _context.Students.AddAsync(student, cancellationToken);
        return insertedResult.Entity;
    }

    public async Task<IEnumerable<Student>> SelectAllStudentsAsync(CancellationToken cancellationToken)
    {
        return await _context.Students
            .OrderByDescending(s => s.CreationDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<Student?> SelectStudentByIdAsync(long? id, CancellationToken cancellationToken)
    {
        var student =  await _context.Students.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (student == null) throw new NotFoundException($"Student with id {id} not found");

        return student;
    }

    public async Task<IEnumerable<Student>> SelectStudentsPagingAsync(int skip, int take, CancellationToken cancellationToken)
    {
        return await _context.Students
           .OrderByDescending(s => s.CreationDate)
           .Skip(skip)
           .Take(take)
           .ToListAsync(cancellationToken);
    }

    public async Task<Student?> UpdateStudentAsync(long? id, Student student, CancellationToken cancellationToken)
    {
        var oldStudent = await SelectStudentByIdAsync(id, cancellationToken);
        if (oldStudent == null) throw new NotFoundException($"Student with id {id} not found");

        oldStudent.FristName = (student.FristName == string.Empty )? oldStudent.FristName : student.FristName;
        oldStudent.LastName = (student.LastName == string.Empty) ? oldStudent.LastName : student.LastName;
        oldStudent.Age = (student.Age <= 0) ? oldStudent.Age : student.Age;
        oldStudent.UpdatedDate = DateTime.UtcNow;
        _context.Entry<Student>(oldStudent).State = EntityState.Modified;
        return oldStudent;
    }
}
