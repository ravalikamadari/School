

using Dapper;
using School.Models;
using School.Utilities;

namespace School.Repositories;


public interface IClassroomRepository
{
    Task<Classroom> Create(Classroom Item);
    Task<Classroom> GetById(long Id);
    Task<List<Classroom>> GetList();

}

public class ClassroomRepository : BaseRepository, IClassroomRepository
{
    public ClassroomRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Classroom> Create(Classroom Item)
    {
        var query = $@"INSERT INTO ""{TableNames.classroom}"" 
        (name)  VALUES (@Name) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Classroom>(query, Item);
            return res;
        }

  
    }

    public async Task<Classroom> GetById(long Id)
    {
              var query = $@"SELECT * FROM ""{TableNames.classroom}"" 
        WHERE id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Classroom>(query, new { Id });
        
    }

    public async Task<List<Classroom>> GetList()
    {

         // Query
        var query = $@"SELECT * FROM ""{TableNames.classroom}""";

        List<Classroom> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Classroom>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
       
    }
}

