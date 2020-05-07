using SquatCounter.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace SquatCounter.ViewModels
{
    public class GuidePageViewModel : ViewModelBase
    {
        public string ImagePath { get; set; }
        public ICommand GoToSquatCounterPageCommand { get; set; }

        public GuidePageViewModel()
        {
            GoToSquatCounterPageCommand = new Command(ExecuteGoToSquatCounterPageCommand);
        }

        private void ExecuteGoToSquatCounterPageCommand()
        {
            PageNavigationService.Instance.GoToSquatCounterPage();
        }
    }
}
