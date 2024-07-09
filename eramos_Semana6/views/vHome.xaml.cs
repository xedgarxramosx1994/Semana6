using System.Collections.ObjectModel;
using eramos_Semana6;
using Newtonsoft.Json;

namespace MauiApp1;

public partial class vHome : ContentPage
{
	private const string url = "http://10.2.13.170/moviles/post.php";
	private readonly HttpClient client = new HttpClient();
	private ObservableCollection<estudiante> student = new ObservableCollection<estudiante>();
	public vHome()
	{
		InitializeComponent(); 
		Obtener();
	}
	public async void Obtener()
	{
		try
		{
			var content = await client.GetStringAsync(url);
			List<estudiante> studentList = JsonConvert.DeserializeObject<List<estudiante>>(content)!;
			student = new ObservableCollection<estudiante>(studentList);
			listaEstudiantes.ItemsSource = student;
		}
		catch (HttpRequestException httpEx)
		{
			// Manejar excepciones relacionadas con la solicitud HTTP
			await DisplayAlert("Error", $"Request error: {httpEx.Message}", "OK");
		}
		catch (Exception ex)
		{
			// Manejar cualquier otra excepción
			await DisplayAlert("Error", $"Unexpected error: {ex.Message}", "OK");
		}
	}
}