using App.Presentations.Models;
using App.Presentations.Views;
using App.Services;
using App.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace App.Presentations.ViewModels
{
    public partial class ClientListViewModel : ObservableObject
    {
        public static IAlertService AlertService;

        public ObservableCollection<Client> Clients { get; set; }

        public ClientListViewModel(IAlertService alertService)
        {
            AlertService = alertService;

            Clients = new ObservableCollection<Client>();
        }

        [RelayCommand]
        public async Task AddClient()
        {
            await Shell.Current.GoToAsync(nameof(ClientAddView));
        }

        [RelayCommand]
        public async Task EditClient(int id)
        {
            await Shell.Current.GoToAsync($"{nameof(ClientEditView)}?Id={id}");
        }

        public async Task ClientsLoad()
        {
            Clients.Clear();

            foreach (var client in ClientService.Instance().Clients)
            {
                Clients.Add(client);
            }
        }

        [RelayCommand]
        public async Task DeleteClient(int id)
        {
            AlertService.ShowConfirmation("Delete", "The Client will be deleted, are you sure?", (async result =>
            {
                if (result)
                {
                    var index = ClientService.Instance().Clients.BinarySearch(new Client() { Id = id });

                    ClientService.Instance().Clients.RemoveAt(index);

                    await ClientsLoad();

                    AlertService.ShowAlert("Delete", "The Client is Deleted");
                }
            }));
        }
    }
}
