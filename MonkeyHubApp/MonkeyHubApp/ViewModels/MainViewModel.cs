using System.Diagnostics;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetProperty(ref _searchTerm, value); }
        }

        public Command SearchCommand { get; }

        public MainViewModel()
        {
            SearchCommand = new Command(ExecuteSearchCommand);
        }

        void ExecuteSearchCommand()
        {
            Debug.WriteLine();
        }
    }
}
