using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeltExam.Models;

namespace BeltExam
{
  public class Idea : BaseEntity
  {
    [Key]
    public int IdeaId {get; set;}
    //Foriegn key: Owner of the idea
    public int UserId {get; set;}
    public string UserIdea {get; set;}
    public User Owner {get; set;}
    public List<Like> LikedBy {get; set;}
    public Idea()
    {
      LikedBy = new List<Like>();
    }
  }
}