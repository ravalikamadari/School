

using Microsoft.AspNetCore.Mvc;
using School.Repositories;
using School.Models;
using School.DTOs;



namespace School.Controllers;


[ApiController]
[Route("api/subject")]


public class SubjectController : ControllerBase 
{

    private readonly ILogger<SubjectController> _logger;
    private readonly ISubjectRepository _subject;
     private readonly ITeacherRepository _teacher;
    

    public SubjectController(ILogger<SubjectController> logger,
    ISubjectRepository student ,ITeacherRepository teacher)
    {
        _logger = logger;
        _subject = student;
        _teacher  = teacher;
        
    }

    [HttpGet]

 public async Task<ActionResult<List<SubjectDTO>>> GetAllSubject()
{
        var SubjectList = await _subject.GetList();

        // Student -> StudentDTO
        var dtoList = SubjectList.Select(x => x.asDto);

        return Ok(dtoList);
}

 [HttpGet("{id}")]
    public async Task<ActionResult<SubjectDTO>> GetSubjectById([FromRoute] long id)
    {
        var Subject = await _subject.GetById(id);

        if (Subject is null)
            return NotFound("No Subject found with given id");

        var dto = Subject.asDto;
        dto.Teacher = (await _teacher.GetAllForSubject(id)).Select(x => x.asDto).ToList();
        //dto.Subject = await _subject.GetAllForStudent(Student.Id);


        return Ok(dto);
    } 
    
}




    