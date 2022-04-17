using System.ComponentModel.DataAnnotations;

namespace ELearning.Models;
public class Student
{
    
    public long Id { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FristName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Age is required")]
    [Range(1, 90)]
    public int Age { get; set; } = 0;
    public DateTime CreationDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

