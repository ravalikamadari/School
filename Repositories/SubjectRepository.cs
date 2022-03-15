
using Dapper;
using School.DTOs;
using School.Models;
using School.Repositories;
using School.Utilities;

public interface ISubjectRepository
{
    Task<Subject> Create(Subject Item);
    Task<Subject> GetById(long Id);
    Task<List<Subject>> GetList();
    Task<List<Subject>> GetAllForTeacher(long TeacherId);
    Task<List<Subject>> GetAllForStudent(long StudentId);
}

public class SubjectRepository : BaseRepository, ISubjectRepository
{
    public SubjectRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Subject> Create(Subject Item)
    {
        var query = $@"INSERT INTO ""{TableNames.subject}"" 
        (name)  VALUES (@Name) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Subject>(query, Item);
            return res;
        }

  
    }

    public async Task<Subject> GetById(long Id)
    {
         var query = $@"SELECT * FROM ""{TableNames.subject}"" 
        WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Subject>(query, new { Id });
    }

    public async Task<List<Subject>> GetList()
    {
          var query = $@"SELECT * FROM ""{TableNames.subject}""";

        List<Subject> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Subject>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }

    public async Task<List<Subject>> GetAllForTeacher(long TeacherId)
    {
          var query = $@"SELECT * FROM {TableNames.teacher} 
        WHERE id = @TeacherId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Subject>(query, new {TeacherId})).AsList();
    
    }

    public async Task<List<Subject>> GetAllForStudent(long StudentId)
    {
        var query = $@"SELECT * FROM {TableNames.student_subject} ss
        LEFT JOIN {TableNames.subject} s ON s.id = ss.subject_id 
        WHERE ss.student_id = @StudentId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Subject>(query, new {StudentId})).AsList();
    }
}
