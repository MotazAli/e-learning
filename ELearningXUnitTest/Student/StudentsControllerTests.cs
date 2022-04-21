using ELearning.Controllers;
using ELearning.Interfaces.Repositories;
using ELearning.Interfaces.Services;
using ELearning.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ELearningXUnitTest
{
    public class StudentsControllerTests
    {

        private Mock<IUnitOfWork> unityOfWorkStub;
        private Mock<IStudentService> studentServiceStub;

        public StudentsControllerTests()
        {
            unityOfWorkStub = new Mock<IUnitOfWork>();
            studentServiceStub = new Mock<IStudentService>();
        }


        [Fact]
        public async Task GetAllStudents_WithExistingStudents_ShouldReturnsAllStudents()
        {
            // Arrange
            var studentsStub = getFakeStudents(skip: null, take: null);

            unityOfWorkStub.Setup(repo => repo.StudentRepository.SelectAllStudentsAsync(CancellationToken.None))
                .ReturnsAsync(studentsStub);

            studentServiceStub.Setup(x => x.GetStudentsAsync(null, null, CancellationToken.None))
                .ReturnsAsync(studentsStub);

            var studentsController = new StudentsController(studentServiceStub.Object);
            //End -Arrange

            // Act
            var response = await studentsController.GetAllAsync(null, null, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as IEnumerable<Student>);
            result.Should().BeEquivalentTo(
               studentsStub,
               options => options.ComparingByMembers<Student>()
               );
        }


        [Fact]
        public async Task GetStudentsPaging_WithExistingStudentsFristTwo_ShouldFristTwoStudents()
        {
            // Arrange
            var studentsStub = getFakeStudents(0, 2);

            unityOfWorkStub.Setup(repo => repo.StudentRepository.SelectStudentsPagingAsync(1, 2, CancellationToken.None))
                .ReturnsAsync(studentsStub);

            studentServiceStub.Setup(x => x.GetStudentsAsync(1, 2, CancellationToken.None))
                .ReturnsAsync(studentsStub);

            var studentsController = new StudentsController(studentServiceStub.Object);
            //End -Arrange

            // Act
            var response = await studentsController.GetAllAsync(1, 2, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as IEnumerable<Student>);
            result.Should().BeEquivalentTo(
               studentsStub,
               options => options.ComparingByMembers<Student>()
               );
        }




        [Fact]
        public async Task GetStudentById_WithExistingStudents_ShouldReturnStudentById()
        {
            // Arrange
            var studentsStub = getFakeStudents(skip: null, take: null);
            var studentStub = studentsStub.FirstOrDefault();

            unityOfWorkStub.Setup(repo => repo.StudentRepository.SelectStudentByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(studentStub);

            studentServiceStub.Setup(x => x.GetStudentByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(studentStub);

            var studentsController = new StudentsController(studentServiceStub.Object);
            //End -Arrange

            // Act
            var response = await studentsController.GetByIdAsync(1, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as Student);
            result.Should().BeEquivalentTo(
               studentStub,
               options => options.ComparingByMembers<Student>()
               );
        }


        [Fact]
        public async Task DeleteStudentById_WithExistingStudents_ShouldDeleteAndReturnDeletedStudent()
        {
            // Arrange
            var studentsStub = getFakeStudents(skip: null, take: null);
            var studentStub = studentsStub.FirstOrDefault();

            unityOfWorkStub.Setup(repo => repo.StudentRepository.DeleteStudentByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(studentStub);

            studentServiceStub.Setup(x => x.DeleteStudentByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(studentStub);

            var studentsController = new StudentsController(studentServiceStub.Object);
            //End -Arrange

            // Act
            var response = await studentsController.DeleteAsync(1, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as Student);
            result.Should().BeEquivalentTo(
               studentStub,
               options => options.ComparingByMembers<Student>()
               );
        }


        [Fact]
        public async Task CreateStudent_ShouldCreateAndReturnInsertedStudent()
        {
            // Arrange
            var studentsStub = getFakeStudents(skip: null, take: null);
            var studentStub = studentsStub.FirstOrDefault();

            unityOfWorkStub.Setup(repo => repo.StudentRepository.InsertStudentAsync(studentStub, CancellationToken.None))
                .ReturnsAsync(studentStub);

            studentServiceStub.Setup(x => x.CreateStudentAsync(studentStub, CancellationToken.None))
                .ReturnsAsync(studentStub);

            var studentsController = new StudentsController(studentServiceStub.Object);
            //End -Arrange

            // Act
            var response = await studentsController.CreateAsync(studentStub, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as Student);
            result.Should().BeEquivalentTo(
               studentStub,
               options => options.ComparingByMembers<Student>()
               );
        }


        [Fact]
        public async Task UpdateStudent__WithExistingStudent_ShouldupdateAndReturnUpdateStudent()
        {
            // Arrange
            var studentsStub = getFakeStudents(skip: null, take: null);
            var studentStub = studentsStub.FirstOrDefault();

            unityOfWorkStub.Setup(repo => repo.StudentRepository.UpdateStudentAsync(1, studentStub, CancellationToken.None))
                .ReturnsAsync(studentStub);

            studentServiceStub.Setup(x => x.UpdateStudentAsync(1, studentStub, CancellationToken.None))
                .ReturnsAsync(studentStub);

            var studentsController = new StudentsController(studentServiceStub.Object);
            //End -Arrange

            // Act
            var response = await studentsController.UpdateAsync(1, studentStub, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as Student);
            result.Should().BeEquivalentTo(
               studentStub,
               options => options.ComparingByMembers<Student>()
               );
        }

        private IEnumerable<Student> getFakeStudents(int? skip, int? take)
        {

            if (skip != null && take != null)
            {
                return new List<Student> {
                new Student(){ StudentId = 1, StudentNameEn = "Motaz Ali" , StudentNameAr="معتز على" , GradeId = 1,Birthdate = DateTime.Now },
                new Student(){ StudentId = 2, StudentNameEn = "Mohamed Ali" , StudentNameAr="محمد على" , GradeId = 2,Birthdate = DateTime.Now },
                new Student(){ StudentId = 3, StudentNameEn = "Ahmed Ali" , StudentNameAr="احمد على" , GradeId = 2,Birthdate = DateTime.Now },
                }.GetRange((int)skip, (int)take);
            }

            return new List<Student> {
                new Student(){ StudentId = 1, StudentNameEn = "Motaz Ali" , StudentNameAr="معتز على" , GradeId = 1,Birthdate = DateTime.Now },
                new Student(){ StudentId = 2, StudentNameEn = "Mohamed Ali" , StudentNameAr="محمد على" , GradeId = 2,Birthdate = DateTime.Now },
                new Student(){ StudentId = 3, StudentNameEn = "Ahmed Ali" , StudentNameAr="احمد على" , GradeId = 2,Birthdate = DateTime.Now },
                };


        }

    }
}