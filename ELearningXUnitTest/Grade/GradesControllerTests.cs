using ELearning.Controllers;
using ELearning.Interfaces.Repositories;
using ELearning.Interfaces.Services;
using ELearning.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ELearningXUnitTest
{
    public class GradesControllerTests
    {

        private Mock<IUnitOfWork> unityOfWorkStub;
        private Mock<IGradeService> gradeServiceStub;

        public GradesControllerTests()
        {
            unityOfWorkStub = new Mock<IUnitOfWork>();
            gradeServiceStub = new Mock<IGradeService>();
        }


        [Fact]
        public async Task GetAllGrades_WithExistingGrades_ShouldReturnsAllGrades()
        {
            // Arrange
            var gradesStub = getFakeGrades(skip:null,take:null);

            unityOfWorkStub.Setup(repo => repo.GradeRepository.SelectAllGradesAsync(CancellationToken.None))
                .ReturnsAsync(gradesStub);

            gradeServiceStub.Setup(x => x.GetGradesAsync(null, null, CancellationToken.None))
                .ReturnsAsync(gradesStub);

            var gradesController = new GradesController(gradeServiceStub.Object);
            //End -Arrange

            // Act
            var response = await gradesController.GetAllAsync(null, null, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as IEnumerable<Grade>);
            result.Should().BeEquivalentTo(
               gradesStub,
               options => options.ComparingByMembers<Grade>()
               );
        }


        [Fact]
        public async Task GetGradesPaging_WithExistingGradesFristTwo_ShouldFristTwoGrades()
        {
            // Arrange
            var gradesStub = getFakeGrades(0,2);

            unityOfWorkStub.Setup(repo => repo.GradeRepository.SelectGradesPagingAsync(1,2,CancellationToken.None))
                .ReturnsAsync(gradesStub);

            gradeServiceStub.Setup(x => x.GetGradesAsync(1, 2, CancellationToken.None))
                .ReturnsAsync(gradesStub);

            var gradesController = new GradesController(gradeServiceStub.Object);
            //End -Arrange

            // Act
            var response = await gradesController.GetAllAsync(1, 2, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as IEnumerable<Grade>);
            result.Should().BeEquivalentTo(
               gradesStub,
               options => options.ComparingByMembers<Grade>()
               );
        }




        [Fact]
        public async Task GetGradeById_WithExistingGrades_ShouldReturnGradeById()
        {
            // Arrange
            var gradesStub = getFakeGrades(skip:null,take:null);
            var gradeStub = gradesStub.FirstOrDefault();

            unityOfWorkStub.Setup(repo => repo.GradeRepository.SelectGradeByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(gradeStub);

            gradeServiceStub.Setup(x => x.GetGradeByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(gradeStub);

            var gradesController = new GradesController(gradeServiceStub.Object);
            //End -Arrange

            // Act
            var response = await gradesController.GetByIdAsync(1, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as Grade);
            result.Should().BeEquivalentTo(
               gradeStub,
               options => options.ComparingByMembers<Grade>()
               );
        }


        [Fact]
        public async Task DeleteGradeById_WithExistingGrades_ShouldDeleteAndReturnDeletedGrade()
        {
            // Arrange
            var gradesStub = getFakeGrades(skip: null, take: null);
            var gradeStub = gradesStub.FirstOrDefault();

            unityOfWorkStub.Setup(repo => repo.GradeRepository.DeleteGradeByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(gradeStub);

            gradeServiceStub.Setup(x => x.DeleteGradeByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(gradeStub);

            var gradesController = new GradesController(gradeServiceStub.Object);
            //End -Arrange

            // Act
            var response = await gradesController.DeleteAsync(1, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as Grade);
            result.Should().BeEquivalentTo(
               gradeStub,
               options => options.ComparingByMembers<Grade>()
               );
        }


        [Fact]
        public async Task CreateGrade_ShouldCreateAndReturnInsertedGrade()
        {
            // Arrange
            var gradesStub = getFakeGrades(skip: null, take: null);
            var gradeStub = gradesStub.FirstOrDefault();

            unityOfWorkStub.Setup(repo => repo.GradeRepository.InsertGradeAsync(gradeStub, CancellationToken.None))
                .ReturnsAsync(gradeStub);

            gradeServiceStub.Setup(x => x.CreateGradeAsync(gradeStub, CancellationToken.None))
                .ReturnsAsync(gradeStub);

            var gradesController = new GradesController(gradeServiceStub.Object);
            //End -Arrange

            // Act
            var response = await gradesController.CreateAsync(gradeStub, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as Grade);
            result.Should().BeEquivalentTo(
               gradeStub,
               options => options.ComparingByMembers<Grade>()
               );
        }


        [Fact]
        public async Task UpdateGrade__WithExistingGrade_ShouldupdateAndReturnUpdateGrade()
        {
            // Arrange
            var gradesStub = getFakeGrades(skip: null, take: null);
            var gradeStub = gradesStub.FirstOrDefault();

            unityOfWorkStub.Setup(repo => repo.GradeRepository.UpdateGradeAsync(1,gradeStub, CancellationToken.None))
                .ReturnsAsync(gradeStub);

            gradeServiceStub.Setup(x => x.UpdateGradeAsync(1,gradeStub, CancellationToken.None))
                .ReturnsAsync(gradeStub);

            var gradesController = new GradesController(gradeServiceStub.Object);
            //End -Arrange

            // Act
            var response = await gradesController.UpdateAsync(1,gradeStub, CancellationToken.None);
            //End -Act

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var value = (response as OkObjectResult).Value;
            var result = (value as Grade);
            result.Should().BeEquivalentTo(
               gradeStub,
               options => options.ComparingByMembers<Grade>()
               );
        }

        private IEnumerable<Grade> getFakeGrades(int? skip,int? take)
        {

            if (skip != null && take != null)
            {
                return new List<Grade> {
                new Grade(){ GradeNameEn = "Grade 1" , GradeNameAr="الصف الاول" , GradeId = 1 },
                new Grade(){ GradeNameEn = "Grade 2" , GradeNameAr="الصف الثاني" , GradeId = 2 },
                new Grade(){ GradeNameEn = "Grade 3" , GradeNameAr="الصف الثالث" , GradeId = 2 },
                }.GetRange((int)skip, (int)take);
            }

            return new List<Grade> {
                new Grade(){ GradeNameEn = "Grade 1" , GradeNameAr="الصف الاول" , GradeId = 1 },
                new Grade(){ GradeNameEn = "Grade 2" , GradeNameAr="الصف الثاني" , GradeId = 2 },
                new Grade(){ GradeNameEn = "Grade 3" , GradeNameAr="الصف الثالث" , GradeId = 2 },
            };


        }

    }
}