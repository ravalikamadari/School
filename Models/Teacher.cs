namespace School.Models;

public record Teacher
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender  { get; set; }
    public long Mobile { get;set;}
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }



      public TeacherDTO asDto => new TeacherDTO
    {
        Id = Id,
        FirstName = FirstName,
        LastName  = LastName,
        Gender = Gender,
        Mobile = Mobile,
        SubjectId = SubjectId,
        SubjectName = SubjectName,
       
    };

}