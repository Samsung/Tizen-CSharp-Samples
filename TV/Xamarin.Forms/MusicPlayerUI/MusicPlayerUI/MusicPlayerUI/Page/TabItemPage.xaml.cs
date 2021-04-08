using System.Collections;
using System.Linq;
using MusicPlayerUI.View;
using Xamarin.Forms;

namespace MusicPlayerUI.Page
{
    /// <summary>
    /// Represents a tab item page.
    /// </summary>
    public partial class TabItemPage : ContentPage
    {
        public static readonly BindableProperty MusicSourceProperty = BindableProperty.Create("MusicSource", typeof(IEnumerable), typeof(TabItemPage), null);

        public static MusicBar MusicBar = new MusicBar();

        /// <summary>
        /// Gets or sets the music sources
        /// </summary>
        public IEnumerable MusicSource
        {
            get { return (IEnumerable)GetValue(MusicSourceProperty); }
            set { SetValue(MusicSourceProperty, value); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TabItemPage()
        {
            InitializeComponent();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MusicBar.SongTitle = e.Item.ToString();
            Grid root = this.Content.FindByName<Grid>("root");
            MusicBar.Focus();
        }
    }
}