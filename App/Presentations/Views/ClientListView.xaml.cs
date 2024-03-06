using App.Presentations.ViewModels;

namespace App.Presentations.Views;

public partial class ClientListView : ContentPage
{
	private ClientListViewModel clientListViewModel;

    public ClientListView(ClientListViewModel clientListViewModel)
	{
		InitializeComponent();

		this.clientListViewModel = clientListViewModel;

        BindingContext = clientListViewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await clientListViewModel.ClientsLoad();
    }
}