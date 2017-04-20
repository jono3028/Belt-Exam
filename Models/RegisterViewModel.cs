using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
  public class RegisterViewModel : BaseEntity
  {
    [Required]
    [MinLength(1)]
    public string UserName {get; set;}
    [Required]
    [MinLength(1)]
    [EmailAddress]
    public string Email {get; set;}
    [Required]
    [MinLengthAttribute(8)]
    public string Password {get; set;}
    [Compare(nameof(Password))]
    public string PasswordConf {get; set;}
  }
}