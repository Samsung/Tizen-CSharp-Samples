using Clock.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Clock.Pages
{
    public partial class AlarmRecordSample : ContentPage
    {
        public AlarmRecordSample()
        {
            InitializeComponent();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = SQLiteDBAccessor.Instance.GetItems();
        }
    }
}
