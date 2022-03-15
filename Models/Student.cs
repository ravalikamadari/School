using School.DTOs;

namespace School.Models;

public record Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender  { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long Mobile { get;set;}
    public DateTimeOffset DateOfJoin { get;set;}
     public int ClassroomId { get; set; }




      public StudentDTO asDto => new StudentDTO
    {
        Id = Id,
        FirstName = FirstName,
        LastName  = LastName,
        Gender = Gender,
        Mobile = Mobile,
        DateOfBirth = DateOfBirth,
        DateOfJoin = DateOfJoin,
        ClassroomId = ClassroomId,
    };

}
