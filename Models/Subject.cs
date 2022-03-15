using School.DTOs;

namespace School.Models;

public record Subject
{
    public int Id { get; set; }
    public string Name { get; set; }



     public SubjectDTO asDto => new SubjectDTO
    {
        Id = Id,
        Name = Name,

    };   
       
}