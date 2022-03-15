

using System.Text.Json.Serialization;

namespace School.DTOs;
public record StudentDTO
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("date_of_join")]
    public DateTimeOffset DateOfJoin { get; set; }

    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

     [JsonPropertyName("classroom_id")]
     public int ClassroomId { get; set; }
    
    [JsonPropertyName("teachers_assigned")]
    public List<TeacherDTO> Teacher { get; internal set; }

     [JsonPropertyName("subject_enrolled")]
    public List<SubjectDTO> Subject { get; internal set; }
}


public record StudentCreateDTO
{
     [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("date_of_join")]
    public DateTimeOffset DateOfJoin { get; set; }

    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

     [JsonPropertyName("classroom_id")]
     public int ClassroomId { get; set; }
}


public record StudentUpdateDTO
{
     

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("mobile")]
     public long? Mobile { get; set; }
    
}