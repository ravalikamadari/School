


using Microsoft.AspNetCore.Mvc;
using School.DTOs;
using School.Models;
using School.Repositories;


namespace School.Controllers;


[ApiController]
[Route("api/student")]


public class StudentController : ControllerBase 
{

    private readonly ILogger<StudentController> _logger;
    private readonly IStudentRepository _student;
     private readonly ITeacherRepository _teacher;
    private readonly ISubjectRepository _subject;

    public StudentController(ILogger<StudentController> logger,
    IStudentRepository student ,ITeacherRepository teacher, ISubjectRepository subject)
    {
        _logger = logger;
        _student = student;
        _teacher  = teacher;
        _subject = subject;
    }
    


[HttpGet]

 public async Task<ActionResult<List<StudentDTO>>> GetAllStudent()
{
        var StudentsList = await _student.GetList();

        // Student -> StudentDTO
        var dtoList = StudentsList.Select(x => x.asDto);

        return Ok(dtoList);
}



    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> GetStudentById([FromRoute] long id)
    {
        var Student = await _student.GetById(id);

        if (Student is null)
            return NotFound("No Student found with given id");

        var dto = Student.asDto;
        dto.Teacher = (await _teacher.GetAllForStudent(id)).Select(x => x.asDto).ToList();
        dto.Subject = (await _subject.GetAllForStudent(id)).Select(x => x.asDto).ToList();


        return Ok(dto);
    }
[HttpPost]
    public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] StudentCreateDTO Data)
    {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
            return BadRequest("Gender value is not recognized");

    //    / var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
    //     if (subtractDate.TotalDays / 365 < 18.0)
    //         return BadRequest("Student must be at least 18 years old");

        var toCreateStudent = new Student
        {
           
            FirstName = Data.FirstName.Trim(),
            LastName = Data.LastName.Trim(),
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Gender = Data.Gender.Trim(),
            Mobile = Data.Mobile,
            DateOfJoin = Data.DateOfJoin.UtcDateTime,
            ClassroomId = Data.ClassroomId,
        };

        var createdStudent = await _student.Create(toCreateStudent);

        return StatusCode(StatusCodes.Status201Created, createdStudent.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStudent([FromRoute] long id,
    [FromBody] StudentUpdateDTO Data)
    {
        var existing = await _student.GetById(id);
        if (existing is null)
            return NotFound("No Student found with given id");

        var toUpdateStudent = existing with
        {
           
            LastName = Data.LastName?.Trim() ?? existing.LastName,
            Mobile = Data.Mobile ?? existing.Mobile,
            
        };

        var didUpdate = await _student.Update(toUpdateStudent);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Student");

        return NoContent();
    }



 [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudent([FromRoute] long id)
    {
        var existing = await _student.GetById(id);
        if (existing is null)
            return NotFound("No Student found with given Student name");

        var didDelete = await _student.Delete(id);

        return NoContent();
    }
}
