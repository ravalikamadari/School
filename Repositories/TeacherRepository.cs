

using Dapper;
using School.DTOs;
using School.Models;
using School.Repositories;
using School.Utilities;

namespace School.Repositories;


public interface ITeacherRepository
{
    Task<Teacher> Create(Teacher Item);
    Task<bool> Update(Teacher Item);
    Task<bool> Delete(long Id);
    Task<Teacher> GetById(long Id);
    Task<List<Teacher>> GetList();
    Task<List<Teacher>> GetAllForStudent(long StudentId);
    Task<List<Teacher>> GetAllForSubject(long SubjectId);
    //Task<List<TeacherDTO>> GetAllForTeacher(long id);
}


public class TeacherRepository : BaseRepository, ITeacherRepository
{
    public TeacherRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Teacher> Create(Teacher Item)
    {
        var query = $@"INSERT INTO ""{TableNames.teacher}"" 
        (first_name, last_name, mobile, gender,subject_id) 
        VALUES (@FirstName, @LastName, @Mobile, @Gender, @SubjectId) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Teacher>(query, Item);
            return res;
        }
    }

    

    public async Task<bool> Delete(long Id)
    {
        var query = $@"DELETE FROM ""{TableNames.teacher}"" 
        WHERE id = @Id";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { Id });
            return res > 0;
        }
    }

    public async Task<List<Teacher>> GetAllForStudent(long StudentId)
    {
         var query = $@"SELECT t.*, s.name AS subject_name FROM {TableNames.student_teacher} st
         LEFT JOIN {TableNames.teacher} t ON t.id = st.teacher_id
         LEFT JOIN {TableNames.subject} s ON s.id = t.subject_id
        WHERE st.student_id = @StudentId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Teacher>(query, new {StudentId})).AsList();
    }

    public async Task<List<Teacher>> GetAllForSubject(long SubjectId)
    {
       var query = $@"SELECT t.*, s.name AS subject_name FROM {TableNames.teacher} t
       LEFT JOIN {TableNames.subject} s ON s.id = t.subject_id
        WHERE subject_id = @SubjectId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Teacher>(query, new {SubjectId})).AsList();
    }

    public async Task<Teacher> GetById(long Id)
    {
        var query = $@"SELECT t.*,s.name AS subject_name FROM {TableNames.teacher} t
        LEFT JOIN {TableNames.subject} s ON s.id = t.subject_id
        WHERE t.id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Teacher>(query, new { Id });
    }

    public async Task<List<Teacher>> GetList()
    {
        // Query
        var query = $@"SELECT t.*, s.name AS subject_name FROM {TableNames.teacher} t
        LEFT JOIN {TableNames.subject} s ON s.id = t.subject_id ORDER BY t.id";

        //List<Teacher> res;
        using (var con = NewConnection) // Open connection
            return (await con.QueryAsync<Teacher>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        //return res;
    }

    
    

     public async Task<bool> Update(Teacher Item)
     {
         var query = $@"UPDATE ""{TableNames.teacher}"" SET 
         last_name = @LastName, contact = @Mobile  WHERE id = @Id";
         

         using (var con = NewConnection)
         {
             var rowCount = await con.ExecuteAsync(query, Item);
             return rowCount == 1;
         }
     }
}