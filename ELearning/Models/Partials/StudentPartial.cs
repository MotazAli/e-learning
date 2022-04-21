using System.ComponentModel.DataAnnotations;

namespace ELearning.Models;


public partial class Student : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.StudentNameEn == null || this.StudentNameEn == string.Empty)
        {
            yield return new ValidationResult(
                "StudentNameEn is required",
                new[] { nameof(this.StudentNameEn) });
        }


        if (this.StudentNameAr == null ||  this.StudentNameAr == string.Empty)
        {
            yield return new ValidationResult(
                "StudentNameAr is required",
                new[] { nameof(this.StudentNameAr) });
        }


        if (this.Birthdate == null)
        {
            yield return new ValidationResult(
                "Birthdate is required",
                new[] { nameof(this.Birthdate) });
        }


        if (this.GradeId == null || this.GradeId <= 0)
        {
            yield return new ValidationResult(
                "GradeId is required",
                new[] { nameof(this.GradeId) });
        }

        if (this.StudentNameEn?.Length > 200)
        {
            yield return new ValidationResult(
                "StudentNameEn can't be greater than 200 characters",
                new[] { nameof(this.StudentNameEn) });
        }

        if (this.StudentNameAr?.Length > 200)
        {
            yield return new ValidationResult(
                "StudentNameAr can't be greater than 200 characters",
                new[] { nameof(this.StudentNameAr) });
        }

    }
}


