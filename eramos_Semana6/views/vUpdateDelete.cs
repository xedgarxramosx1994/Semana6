using CloudKit;

namespace MauiApp1;

public partial class vUpdateDelete : ContentPage
{
	private readonly EstudianteService estudianteService;
	public vUpdateDelete()
	{
		InitializeComponent();
		estudianteService = new EstudianteService("Host=localhost;Username=postgres;Password=Uisrael;Database=uisrael");
	}
	private int userId;
	public vUpdateDelete(int id, string name, string surname, int age){
		InitializeComponent();
		estudianteService = new EstudianteService("Host=localhost;Username=postgres;Password=Uisrael;Database=uisrael");
		userId = id;
		txtNombre.Text = name;
		txtApellido.Text = surname;
		txtEdad.Text = age.ToString();
	}

	public async void btnUpdate_Clicked(object sender, EventArgs e){
		if(txtApellido.Text != null && txtNombre.Text != null && txtEdad.Text != null){
			if(txtApellido.Text.Trim() != string.Empty && txtEdad.Text.Trim() != string.Empty && txtNombre.Text.Trim() != string.Empty){
				await estudianteService.UpdateEstudianteAsync(userId, txtNombre.Text, txtApellido.Text, Convert.ToInt32(txtEdad.Text));
				await Navigation.PushAsync(new vHome());
			}
			else
				await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
		}
		else
			await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
	}

	public async void btnDelete_Clicked(object sender, EventArgs e){
		bool isConfirmed = await DisplayAlert("Warning", "¿Esta seguro de eliminar el registro?", "Sí", "No");
		if(isConfirmed){
			await estudianteService.DeleteEstudianteAsync(userId);
			await Navigation.PushAsync(new vHome());
		}
	}
}