using System.ComponentModel.DataAnnotations;

namespace ELearning.Models;

public partial class Grade : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.GradeNameEn == null || this.GradeNameEn == string.Empty)
        {
            yield return new ValidationResult(
                "GradeNameEn is required",
                new[] { nameof(this.GradeNameEn) });
        }


        if (this.GradeNameAr == null || this.GradeNameAr == string.Empty)
        {
            yield return new ValidationResult(
                "GradeNameAr is required",
                new[] { nameof(this.GradeNameAr) });
        }


        if (this.GradeNameEn?.Length > 200)
        {
            yield return new ValidationResult(
                "GradeNameEn can't be greater than 200 characters",
                new[] { nameof(this.GradeNameEn) });
        }

        if (this.GradeNameAr?.Length > 200)
        {
            yield return new ValidationResult(
                "GradeNameAr can't be greater than 200 characters",
                new[] { nameof(this.GradeNameAr) });
        }

    }
}



