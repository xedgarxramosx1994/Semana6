using eramos_Semana6;

namespace MauiApp1;

public partial class vHome : ContentPage
{
	private readonly EstudianteService estudianteService;
	public vHome()
	{
		InitializeComponent();
		estudianteService = new EstudianteService("Host=localhost;Username=postgres;Password=Uisrael;Database=uisrael");
		LoadStudents();
	}
	public async void LoadStudents()
	{
		var estudiantes = await estudianteService.GetEstudiantesAsync();
		// Verificar la cantidad de estudiantes recuperados
		Console.WriteLine($"Cantidad de estudiantes recuperados: {estudiantes.Count}");

		foreach (var estudiante in estudiantes)
		{
			// Imprimir los detalles de cada estudiante
			Console.WriteLine($"{estudiante.Name} {estudiante.Surname}");
		}

		listaEstudiantes.ItemsSource = estudiantes;
	}

	public void btnAgregar_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new vAgregar());
	}
	private void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e){
		var estudiante = (Estudiante)e.SelectedItem;
		Navigation.PushAsync(new vUpdateDelete(estudiante.Id, estudiante.Name, estudiante.Surname, estudiante.Age));
	}
}