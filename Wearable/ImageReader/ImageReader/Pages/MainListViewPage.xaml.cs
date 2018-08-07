using System.Collections.ObjectModel;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImageReader.Pages
{
    /// <summary>
    /// Class MainListViewPage
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListViewPage : CirclePage
    {
        public const string LOCAL_IMAGE = "Local images";
        public const string EMBEDDED_IMAGE = "Embedded images";
        public const string DOWNLOADED_IMAGE = "Downloaded images";

        public ObservableCollection<string> Items { get; set; }

        public MainListViewPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                LOCAL_IMAGE,
                EMBEDDED_IMAGE,
                DOWNLOADED_IMAGE,
            };
			
			MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var tappedItem = e.Item.ToString();

            if (tappedItem.Equals(LOCAL_IMAGE))
            {
                await Navigation.PushAsync(new LocalImages());
            }
            else if (tappedItem.Equals(EMBEDDED_IMAGE))
            {
                await Navigation.PushAsync(new EmbeddedImages());
            }
            else if (tappedItem.Equals(DOWNLOADED_IMAGE))
            {
                await Navigation.PushAsync(new DownloadImages());
            }
            else
            {
                await DisplayAlert("Item Tapped", "Something wrong.", "OK");
            }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
