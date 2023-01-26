using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AppHistory.ViewModel
{
    class MainPageViewModel
    {
        public ObservableCollection<MainPageListIteam> Source { get; private set; } = new ObservableCollection<MainPageListIteam>();
        public MainPageViewModel()
        {
            Source.Add(new MainPageListIteam("Top 5 recently used applications", "during the last 5 hours"));
            Source.Add(new MainPageListIteam("Top 10 frequently used applications", "during the last 3 days"));
            Source.Add(new MainPageListIteam("Top 10 battery consuming applications", "since the last time when the device has fully charged"));
        }
    }

    public class MainPageListIteam : INotifyPropertyChanged
    {
        private string firstLine;
        private string secondLine;

        public string FirstLine
        {
            get => firstLine;
        }
        public string SecondLine
        {
            get => secondLine;
        }

        public MainPageListIteam(string _firstLine, string _secondLine)
        {
            firstLine = _firstLine;
            secondLine = _secondLine;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
