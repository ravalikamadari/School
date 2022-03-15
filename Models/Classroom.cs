using School.DTOs;

namespace School.Models;

public record Classroom
{
    public int Id { get; set; }
    public string Name { get; set; }



     public ClassroomDTO asDto => new ClassroomDTO
    {
        Id = Id,
        Name = Name,

    };   
       
}