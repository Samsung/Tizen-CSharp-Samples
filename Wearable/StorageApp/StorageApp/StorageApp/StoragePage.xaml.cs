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
    /// Storage page of Storage application
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StoragePage : CirclePage
	{
        /// <summary>
        /// Constructor of StoragePage
        /// </summary>
        /// <param name="id">Id of storage</param>
        /// <param name="rootDirectory">Root directory of storage</param>
        /// <param name="totalSpace">Total space of storage</param>
        /// <param name="availableSpace">Available space of storage</param>
        /// <param name="state">State of storage</param>
        public StoragePage(int id, string rootDirectory, ulong totalSpace, ulong availableSpace, StorageState state)
		{
			InitializeComponent();
            CreateListView(id, rootDirectory, totalSpace, availableSpace, state);
		}

        /// <summary>
        /// Method to create list view
        /// </summary>
        /// <param name="id">Id of storage</param>
        /// <param name="rootDirectory">Root directory of storage</param>
        /// <param name="totalSpace">Total space of storage</param>
        /// <param name="availableSpace">Available space of storage</param>
        /// <param name="state">State of storage</param>
        public void CreateListView(int id, string rootDirectory, ulong totalSpace, ulong availableSpace, StorageState state)
        {
            string str_state;

            if (state == StorageState.Mounted || state == StorageState.MountedReadOnly)
            {
                str_state = "Mounted";

            }
            else
            {
                str_state = "Unmounted";
            }

            List<StoragePrint> storages = new List<StoragePrint>
            {
                new StoragePrint("Id: " + id),
                new StoragePrint("Type: Internal Storage"),
                new StoragePrint("Root directory: " + rootDirectory),
                new StoragePrint("Total space: " + totalSpace),
                new StoragePrint("Available space: " + availableSpace),
                new StoragePrint("State: " + str_state),
            };
            StorageDetail.ItemsSource = storages;
        }
	}
}
