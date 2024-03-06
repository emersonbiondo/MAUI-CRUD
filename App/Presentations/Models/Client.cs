using App.Utils;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App.Presentations.Models
{
    public class Client: ObservableObject, IComparable<Client>
    {
        public int Id { get; set; }

        public string name;

        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }

        public string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                SetProperty(ref lastName, value);
            }
        }

        public int age;

        public int Age
        {
            get => age;
            set
            {
                SetProperty(ref age, value);
            }
        }

        public string address;

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

        public int CompareTo(Client? other)
        {
            if (other == null) return 1;

            return Id.CompareTo(other.Id);  
        }
    }
}
