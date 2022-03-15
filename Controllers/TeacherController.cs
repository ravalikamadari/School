
using Microsoft.AspNetCore.Mvc;
using School.Models;
using School.Repositories;
//using School.Repositories;

namespace Socialmedia.Controllers;


[ApiController]
[Route("api/teacher")]


public class TeacherController : ControllerBase 
{

    private readonly ILogger<TeacherController> _logger;
    private readonly ITeacherRepository _teacher;
    private readonly IStudentRepository _student;

    public TeacherController(ILogger<TeacherController> logger,
    ITeacherRepository teacher,IStudentRepository student)
    {
        _logger = logger;
        _teacher = teacher;
        _student = student;
    }
    


[HttpGet]

 public async Task<ActionResult<List<TeacherDTO>>> GetAllTeacher()
{
        var TeachersList = await _teacher.GetList();

        // Teacher -> TeacherDTO
        var dtoList = TeachersList.Select(x => x.asDto);

        return Ok(dtoList);
}



    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherDTO>> GetTeacherById([FromRoute] long id)
    {
        var Teacher = await _teacher.GetById(id);

        if (Teacher is null)
            return NotFound("No Teacher found with given id");

        var dto = Teacher.asDto;
        dto.Student = (await _student.GetAllForTeacher(id)).Select(x => x.asDto).ToList();


        return Ok(dto);
    }
[HttpPost]
    public async Task<ActionResult<TeacherDTO>> CreateTeacher([FromBody] TeacherCreateDTO Data)
    {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
            return BadRequest("Gender value is not recognized");

    //    / var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
    //     if (subtractDate.TotalDays / 365 < 18.0)
    //         return BadRequest("Teacher must be at least 18 years old");

        var toCreateTeacher = new Teacher
        {
            
            FirstName = Data.FirstName.Trim(),
            LastName = Data.LastName.Trim(),
            Gender = Data.Gender.Trim(),
            Mobile = Data.Mobile,
            SubjectId = Data.SubjectId,
           
        };

        var createdTeacher = await _teacher.Create(toCreateTeacher);

        return StatusCode(StatusCodes.Status201Created, createdTeacher.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeacher([FromRoute] long id,
    [FromBody] TeacherUpdateDTO Data)
    {
        var existing = await _teacher.GetById(id);
        if (existing is null)
            return NotFound("No Teacher found with given id");

        var toUpdateTeacher = existing with
        {
           
            LastName = Data.LastName?.Trim() ?? existing.LastName,
            Mobile = Data.Mobile ?? existing.Mobile,
            
        };

        var didUpdate = await _teacher.Update(toUpdateTeacher);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Teacher");

        return NoContent();
    }



 [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeacher([FromRoute] long id)
    {
        var existing = await _teacher.GetById(id);
        if (existing is null)
            return NotFound("No Teacher found with given Teacher name");

        var didDelete = await _teacher.Delete(id);

        return NoContent();
    }
}