using System.Collections.ObjectModel;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImageReader.Pages
{
    /// <summary>
    /// Class MainListViewPage
    /// The main page of this application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListViewPage : CirclePage
    {
        public const string LOCAL_IMAGE = "Local images";
        public const string EMBEDDED_IMAGE = "Embedded images";
        public const string DOWNLOADED_IMAGE = "Downloaded images";

        public ObservableCollection<string> Items { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainListViewPage()
        {
            // Make this page have no navigationbar
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            // the items to display
            Items = new ObservableCollection<string>
            {
                LOCAL_IMAGE,
                EMBEDDED_IMAGE,
                DOWNLOADED_IMAGE,
            };

            // Set a collection of strings
            MyListView.ItemsSource = Items;
        }

        /// <summary>
        /// Called when an item of ListView is tapped
        /// The new page related to tapped item will be shown
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ItemTappedEventArgs</param>
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            // Get which item is tapped
            var tappedItem = e.Item.ToString();

	    //  Asynchronously add a new page to the top of the navigation stack
	    //  The page is related to the tapped item of ListView
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

            // Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
