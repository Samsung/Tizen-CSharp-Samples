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
    /// <summary>
    /// Main page of Storage application
    /// User can see and choose storage which device have
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        /// <summary>
        /// The constructor of MainPage class
        /// Storages which this device have are listed
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method for clicked event of storage item
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="args">Event argument</param>
        public void OnClicked(object sender, EventArgs args)
        {
            var storages = StorageManager.Storages;

            foreach (Storage storage in storages)
            {
                if (storage.StorageType != StorageArea.Internal)
                {
                    continue;
                }

                this.Navigation.PushAsync(new StoragePage(storage.Id, storage.RootDirectory, storage.TotalSpace, storage.AvailableSpace, storage.State));
            }
        }
    }
}
