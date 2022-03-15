
using System.Text.Json.Serialization;

namespace School.DTOs;
public record SubjectDTO
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

     [JsonPropertyName("who_teaches")]

    public List<TeacherDTO> Teacher { get; internal set; }


}