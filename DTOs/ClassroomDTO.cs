
using System.Text.Json.Serialization;

namespace School.DTOs;
public record ClassroomDTO
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

     [JsonPropertyName("students")]

    public List<StudentDTO> Student { get; internal set; }


}


public record ClassroomCreateDTO
{
   

    [JsonPropertyName("name")]
    public string Name { get; set; }

}

     