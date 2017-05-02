using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeltExam.Models;

namespace BeltExam
{
  public class Like : BaseEntity
  {
    [Key]
    public int LikeId {get; set;}

    public int UserId {get; set;}
    public User User {get; set;}

    public int IdeaId {get; set;}
    public Idea Idea {get; set;}
  }
}