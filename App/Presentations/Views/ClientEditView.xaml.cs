using App.Presentations.ViewModels;

namespace App.Presentations.Views;

[QueryProperty(nameof(ClientId), "Id")]
public partial class ClientEditView : ContentPage
{
	private readonly ClientEditViewModel clientEditViewModel;

	public ClientEditView(ClientEditViewModel clientEditViewModel)
	{
		InitializeComponent();

		this.clientEditViewModel = clientEditViewModel;

        BindingContext = clientEditViewModel;
    }

	public string ClientId {
		set
		{
            clientEditViewModel.ClientLoad(int.Parse(value));
        }	
	}
}