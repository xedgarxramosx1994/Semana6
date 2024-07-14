using Npgsql;

namespace eramos_Semana6;

public partial class MainPage : ContentPage
{
    private readonly EstudianteService _estudianteService;
    public MainPage()
    {
        InitializeComponent();
        _estudianteService = new EstudianteService("Host=localhost;Port=5432;Database=uisrael;Username=postgres;Password=Uisrael");

    }
    private void CreateDatabaseTable()
    {
        using (var conn = new NpgsqlConnection(_estudianteService.ConnectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS estudiante (
                                            id SERIAL PRIMARY KEY,
                                            name VARCHAR(100),
                                            surname VARCHAR(100),
                                            age INT)";
                cmd.ExecuteNonQuery();
            }
        }
    }
}