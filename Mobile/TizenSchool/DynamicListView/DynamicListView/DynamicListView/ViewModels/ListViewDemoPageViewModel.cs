using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace DynamicListView.ViewModels
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
    }

    public class ListViewDemoPageViewModel : ViewModelBase
    {
        public Command OnSortButtonClick { get; set; }
        private List<Person> _initialItmes;

        private List<Person> _persons;
        public List<Person> Persons 
        { 
            get => _persons;
            set => SetProperty(ref _persons, value); 
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterItems();
            }
        }

        private void FilterItems()
        {
            List<Person> tempList = new List<Person>();

            if(SearchText.Length == 0)
            {
                Persons = new List<Person>(_initialItmes);
            }
            else
            {
                _initialItmes.Where(
                    t => t.Name.ToLower().Contains(SearchText.ToLower())
                    ).ToList().ForEach(
                        t => tempList.Add(t)
                    );

                Persons = new List<Person>(tempList);                
            }
        }

        private void SortPersons(object sortBy)
        {
            Persons = new List<Person>(Persons)
                .OrderBy(
                    i => i.GetType().GetProperty(sortBy as string).GetValue(i, null)
                ).ToList();
        }

        public ListViewDemoPageViewModel()
        {
            Persons = new List<Person>
            {
                new Person { Name = "Kenny Harris Franklyn", Age = 34, Location = "Narthwich" },
                new Person { Name = "Mick Malcom Radclyffe", Age = 24, Location = "Penketh" },
                new Person { Name = "Nicky Benjamin Bloxam", Age = 31, Location = "Aberdeen" },
                new Person { Name = "Gerrard Fraser Martins", Age = 34, Location = "London" },
                new Person { Name = "Lincoln Benny Willard", Age = 28, Location = "Silverkeep " },
                new Person { Name = "Dustin Johnny Alfredson", Age = 29, Location = "Perlshaw" },
                new Person { Name = "Marty Cullen Lyndon", Age = 30, Location = "Everton" },
                new Person { Name = "Ern Ormond Hardwick", Age = 28, Location = "Paentmarwy" }
            };

            _initialItmes = new List<Person>(Persons);
            OnSortButtonClick = new Command(SortPersons);
        }
    }
}