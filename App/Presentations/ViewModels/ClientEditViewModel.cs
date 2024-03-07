using App.Presentations.Models;
using App.Repositories;
using App.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.Presentations.ViewModels
{
    public partial class ClientEditViewModel : ObservableObject
    {
        private static IAlertService AlertService;

        private IClientRepository ClientRepository;

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

        public ClientEditViewModel(IAlertService alertService, IClientRepository clientRepository)
        {
            AlertService = alertService;

            ClientRepository = clientRepository;

            Client = new Client();
        }

        [RelayCommand]
        public async Task Update()
        {
            if (ClientValidate())
            {
                await ClientRepository.Update(Client);

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
            Client = await ClientRepository.Get(Id);
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
