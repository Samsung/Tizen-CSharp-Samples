using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Clock.ViewModels
{
    public class AlarmViewModelObservableCollection<T> : ObservableCollection<T>
    {
        public AlarmViewModelObservableCollection()
        {
        }

        public AlarmViewModelObservableCollection(IEnumerable<T> items) : this()
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public void ReportItemChange(T item)
        {
            System.Diagnostics.Debug.WriteLine("$$$$$$$$$$$$$$$$$$ REPORT CHANGE $$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            NotifyCollectionChangedEventArgs args =
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Replace,
                    item,
                    item,
                    IndexOf(item));
            OnCollectionChanged(args);
        }
    }
}
