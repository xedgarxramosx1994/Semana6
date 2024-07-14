// EstudianteService.cs
using eramos_Semana6;
using Npgsql;

public class EstudianteService
{
    public string ConnectionString { get; }

    public EstudianteService(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public async Task<List<Estudiante>> GetEstudiantesAsync()
    {
        var estudiantes = new List<Estudiante>();
        using (var conn = new NpgsqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("SELECT * FROM estudiante", conn))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    estudiantes.Add(new Estudiante
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Surname = reader.GetString(2),
                        Age = reader.GetInt32(3)
                    });
                }
            }
        }
        return estudiantes;
    }

    public async Task AddEstudianteAsync(string name, string lastname, int age)
    {
        using (var conn = new NpgsqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO estudiante (name, surname, age) VALUES (@name, @lastname, @age)", conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("lastname", lastname);
                cmd.Parameters.AddWithValue("age", age);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateEstudianteAsync(int id, string name, string lastname, int age)
    {
        using (var conn = new NpgsqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("UPDATE estudiante SET name=@name, surname=@lastname, age=@age WHERE id=@id", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("lastname", lastname);
                cmd.Parameters.AddWithValue("age", age);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteEstudianteAsync(int id)
    {
        using (var conn = new NpgsqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("DELETE FROM estudiante WHERE id=@id", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}