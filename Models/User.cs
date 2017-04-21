using System.ComponentModel.DataAnnotations;
using BeltExam.Models;

namespace BeltExam
{
  public class User : BaseEntity
  {
    [Key]
    public int UserId {get; set;}
    public string UserName {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
  }
}