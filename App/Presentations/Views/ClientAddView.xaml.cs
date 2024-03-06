using App.Presentations.ViewModels;

namespace App.Presentations.Views;

public partial class ClientAddView : ContentPage
{
    public ClientAddView(ClientAddViewModel clientAddViewModel)
	{
		InitializeComponent();

        BindingContext = clientAddViewModel;
    }
}