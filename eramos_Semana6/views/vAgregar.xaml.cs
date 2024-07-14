using System.Net;

namespace MauiApp1;

public partial class vAgregar : ContentPage
{
	private readonly EstudianteService estudianteService;
	public vAgregar()
	{
		InitializeComponent();
		estudianteService = new EstudianteService("Host=localhost;Username=postgres;Password=Uisrael;Database=uisrael");
	}

	public async void btnAgregar_Clicked(object sender, EventArgs e){
		try
		{
			if(txtNombre.Text != null && txtApellido.Text != null && txtEdad.Text != null){
				await estudianteService.AddEstudianteAsync(txtNombre.Text.Trim(), txtApellido.Text.Trim(), Convert.ToInt32(txtEdad.Text.Trim()));
				await Navigation.PushAsync(new vHome());
			}
			else{
				await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Alerta", ex.Message, "Cerrar");
		}
	}
}