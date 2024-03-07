using App.Presentations.Models;
using App.Repositories;
using App.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace App.Presentations.ViewModels
{
    public partial class ClientAddViewModel : ObservableObject
    {
        private static IAlertService AlertService;

        private IClientRepository ClientRepository;

        private Client client;

        public Client Client {
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
                if (Client.Age > 0)
                {
                    return Client.Age.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                string result = Regex.Replace(value, @"[^0-9]+", string.Empty);
                if (Regex.Match(result, @"^[0-9]+$").Success)
                {
                    Client.Age = int.Parse(result);
                }
                else
                {
                    Client.Age = 0;
                }
                OnPropertyChanged("AgeString");
            }
        }

        public bool IsNameValid { get; set; }

        public bool IsLastNameValid { get; set; }

        public bool IsAgeValid { get; set; }

        public bool IsAddressValid { get; set; }

        public ClientAddViewModel(IAlertService alertService, IClientRepository clientRepository) 
        {
            AlertService = alertService;

            ClientRepository = clientRepository;

            Client = new Client();
        }

        [RelayCommand]
        public async Task Save()
        {
            if (ClientValidate())
            {
                await ClientRepository.Add(client);

                AlertService.ShowAlert("Success", "The Client is Save");

                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
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
                errors.Add("Address required and longer than 3 letters");
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
