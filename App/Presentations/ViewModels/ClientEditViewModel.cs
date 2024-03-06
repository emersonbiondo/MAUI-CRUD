using App.Presentations.Models;
using App.Services;
using App.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace App.Presentations.ViewModels
{
    public partial class ClientEditViewModel : ObservableObject
    {
        public static IAlertService AlertService;

        private Client client;

        public Client Client
        {
            get => client;
            set
            {
                SetProperty(ref client, value);
            }
        }

        public string AgeString
        {
            get
            {
                return Client.age.ReturnIntToStringWithEmpty();
            }
            set
            {
                Client.Age = value.ReturnValidNumber();
                OnPropertyChanged("AgeString");
            }
        }

        public bool IsNameValid { get; set; }

        public bool IsLastNameValid { get; set; }

        public bool IsAgeValid { get; set; }

        public bool IsAddressValid { get; set; }

        public ClientEditViewModel(IAlertService alertService)
        {
            AlertService = alertService;

            Client = new Client();
        }

        [RelayCommand]
        public async Task Update()
        {
            if (ClientValidate())
            {
                var index = ClientService.Instance().Clients.BinarySearch(client);

                ClientService.Instance().Clients[index] = Client;

                AlertService.ShowAlert("Success", "The Client is Update");
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async void ClientLoad(int Id)
        {
            var index = ClientService.Instance().Clients.BinarySearch(new Client() { Id = Id });
            Client = ClientService.Instance().Clients[index];
            OnPropertyChanged("AgeString");
        }

        public bool ClientValidate()
        {
            var errors = new List<string>();

            if (!IsNameValid)
            {
                errors.Add("Name required and longer than 3 letters");
            }

            if (!IsLastNameValid)
            {
                errors.Add("Last Name required and longer than 3 letters");
            }

            if (!IsAgeValid)
            {
                errors.Add("Age required and over 12 years old");
            }

            if (!IsAddressValid)
            {
                errors.Add("Address required and longer than 5 letters");
            }

            if (errors.Count > 0)
            {
                AlertService.ShowAlert("Data Validation", string.Join(Environment.NewLine, errors));
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
