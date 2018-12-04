using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.System;

namespace StorageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void OnClicked(object sender, EventArgs args)
        {
            var storages = StorageManager.Storages;

            foreach (Storage storage in storages)
            {
                if (storage.StorageType != StorageArea.Internal)
                    continue;
                this.Navigation.PushAsync(new StoragePage(storage.Id, storage.RootDirectory, storage.TotalSpace, storage.AvailableSpace, storage.State));
            }
        }
    }
}