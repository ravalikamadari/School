
using System.Text.Json.Serialization;
using School.DTOs;

public record TeacherDTO
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

     [JsonPropertyName("subject_id")]
     public int SubjectId { get; set; }
     
     [JsonPropertyName("subject_name")]
     public string SubjectName { get; set; }

       
     [JsonPropertyName("students_assigned")]

    public List<StudentDTO> Student { get; internal set; }

}




public record TeacherCreateDTO
{


    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

     [JsonPropertyName("subject_id")]
     public int SubjectId { get; set; }
}

public record TeacherUpdateDTO
{



    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("mobile")]
    public long? Mobile { get; set; }
}





