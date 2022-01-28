using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Tizen.Location.Geofence;

namespace Geofence.ViewModels
{
    class SelectIDPageViewModel
    {
        public ObservableCollection<ListIteam> Source { get; private set; } = new ObservableCollection<ListIteam>();

        public SelectIDPageViewModel(VirtualPerimeter perimeter)
        {
            // Get the information for all the places
            foreach (PlaceData data in perimeter.GetPlaceDataList())
            {
                // Add the place id to the list
                Source.Add(new ListIteam(data.PlaceId.ToString() + " " +data.Name));
            }
        }
    }

    public class ListIteam : INotifyPropertyChanged
    {
        private string item;
        public string Item
        {
            get => item;
            set
            {
                if(item != value)
                {
                    item = value;
                    OnPropertyChanged("Item");
                }
            }
        }

        public ListIteam(string item)
        {
            Item = item;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }
    }
}
