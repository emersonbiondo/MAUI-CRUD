using App.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace App.Presentations.Models
{
    public class Client : ObservableObject
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string name;

        [Required]
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }

        public string lastName;

        [Required]
        public string LastName
        {
            get => lastName;
            set
            {
                SetProperty(ref lastName, value);
            }
        }

        public int age;

        [Required]
        public int Age
        {
            get => age;
            set
            {
                SetProperty(ref age, value);
            }
        }

        public string address;

        [Required]
        public string Address
        {
            get => address;
            set
            {
                SetProperty(ref address, value);
            }
        }

        public Client()
        {
            Id = 0;
            Name = string.Empty;
            LastName = string.Empty;
            Age = DefaultValues.CLIENT_AGE;
            Address = string.Empty;
        }
    }
}
