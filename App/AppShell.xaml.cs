using App.Presentations.Views;

namespace App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ClientListView), typeof(ClientListView));
            Routing.RegisterRoute(nameof(ClientAddView), typeof(ClientAddView));
            Routing.RegisterRoute(nameof(ClientEditView), typeof(ClientEditView));
        }
    }
}
