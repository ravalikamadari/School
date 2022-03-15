using Dapper;
using School.DTOs;
using School.Models;
using School.Repositories;
using School.Utilities;

namespace School.Repositories;

public interface IStudentRepository
{
    Task<Student> Create(Student Item);
    Task<bool> Update(Student Item);
    Task<bool> Delete(long Id);
    Task<Student> GetById(long Id);
    Task<List<Student>> GetList();
    Task<List<Student>> GetAllForClassroom(long ClassroomId);
    Task<List<Student>> GetAllForTeacher(long TeacherId);
    //Task<List<StudentDTO>> GetAllForSubject(long SubjectId);
}
public class StudentRepository : BaseRepository, IStudentRepository
{
    public StudentRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Student> Create(Student Item)
    {
        var query = $@"INSERT INTO ""{TableNames.student}"" 
        (first_name, last_name, date_of_birth, mobile, gender,date_of_join,classroom_id) 
        VALUES (@FirstName, @LastName, @DateOfBirth, @Mobile, @Gender,@DateOfJoin,@ClassroomId) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Student>(query, Item);
            return res;
        }
    }

    

    public async Task<bool> Delete(long Id)
    {
        var query = $@"DELETE FROM ""{TableNames.student}"" 
        WHERE id = @Id";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { Id });
            return res > 0;
        }
    }

    public async Task<List<Student>> GetAllForClassroom(long ClassroomId)
    {
            var query = $@"SELECT * FROM {TableNames.student} 
        WHERE classroom_id = @ClassroomId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Student>(query, new {ClassroomId})).AsList();
    }

   /* public async Task<List<StudentDTO>> GetAllForSubject(long SubjectId)
    {
         var query = $@"SELECT * FROM {TableNames.subject} 
        WHERE id = @SubjectId";

        using (var con = NewConnection)
            return (await con.QueryAsync<StudentDTO>(query, new {SubjectId})).AsList();
    
    }*/

    public async Task<List<Student>> GetAllForTeacher(long TeacherId)
    {
          var query = $@"SELECT * FROM {TableNames.student_teacher} st
          LEFT JOIN {TableNames.student} s ON s.id = st.student_id 
          WHERE st.teacher_id = @TeacherId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Student>(query, new {TeacherId})).AsList();
    
    }

    public async Task<Student> GetById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.student}"" 
        WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Student>(query, new { Id });
    }

    public async Task<List<Student>> GetList()
    {
        // Query
        var query = $@"SELECT * FROM ""{TableNames.student}""";

        List<Student> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Student>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }

    
    

     public async Task<bool> Update(Student Item)
     {
         var query = $@"UPDATE ""{TableNames.student}"" SET  
         last_name = @LastName, mobile = @Mobile WHERE id = @Id";
         

         using (var con = NewConnection)
         {
             var rowCount = await con.ExecuteAsync(query, Item);
             return rowCount == 1;
         }
     }
}