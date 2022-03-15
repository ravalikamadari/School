



using Microsoft.AspNetCore.Mvc;
using School.DTOs;
using School.Models;
using School.Repositories;
//using School.Repositories;

[ApiController]
[Route("api/classroom")]


public class ClassroomController : ControllerBase 
{

    private readonly ILogger<ClassroomController> _logger;
    private readonly IClassroomRepository _classroom;
     private readonly IStudentRepository _student;

     public ClassroomController(ILogger<ClassroomController> logger,
    IClassroomRepository classroom ,IStudentRepository student)
    {
        _logger = logger;
        _classroom = classroom;
        _student = student;
      
    }

    [HttpGet]

 public async Task<ActionResult<List<ClassroomDTO>>> GetAllClassroom()
{
        var ClassroomList = await _classroom.GetList();

        // Student -> StudentDTO
        var dtoList = ClassroomList.Select(x => x.asDto);

        return Ok(dtoList);
}

 [HttpGet("{id}")]
    public async Task<ActionResult<ClassroomDTO>> GetClassroomById([FromRoute] long id)
    {
        var Classroom = await _classroom.GetById(id);

        if (Classroom is null)
            return NotFound("No Class found with given id");

        var dto = Classroom.asDto;
        dto.Student = (await _student.GetAllForClassroom(id)).Select(x => x.asDto).ToList();
        


        return Ok(dto);
    }
    
[HttpPost]
    public async Task<ActionResult<ClassroomDTO>> CreateClassroom([FromBody] ClassroomCreateDTO Data)
    {
       
        var toCreateClassroom = new Classroom
        {
           
            Name = Data.Name.Trim(),
          
        };

        var createdClassroom = await _classroom.Create(toCreateClassroom);

        return StatusCode(StatusCodes.Status201Created, createdClassroom.asDto);
    }
}




    