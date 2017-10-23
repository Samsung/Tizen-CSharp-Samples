using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AccountManager.Models;

namespace SampleAccount.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountSignIn : ContentPage
	{
        private static IAccountManagerAPIs accountAPIs;
        string userId;
        string userPass;
        public AccountSignIn ()
		{
			InitializeComponent ();

            var entryId = new Entry();
            var entryPass = new Entry();

            entryId.TextChanged += ID_TextChanged;
            entryPass.TextChanged += Pass_TextChanged;

            accountAPIs = DependencyService.Get<IAccountManagerAPIs>();
        }
        void ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            userId = e.NewTextValue;
        }
        void Pass_TextChanged(object sender, TextChangedEventArgs e)
        {
            userPass = e.NewTextValue;
        }

        async void CreateClicked(object sender, EventArgs args)
        {
            int id;
            Button button = (Button)sender;

            AccountItem account = new AccountItem();
            account.UserId = userId;
            account.UserPassword = userPass;

            if (String.IsNullOrEmpty(account.UserId) || String.IsNullOrEmpty(account.UserPassword)) { 
                await DisplayAlert("Error!",
                "Please Input ID / Password Correctly",
                "OK");
            } else {
                id = accountAPIs.AccountAdd(account);
                AccountSignOut accountSignOut = new AccountSignOut(id, userId);
                await Navigation.PushAsync(accountSignOut);
            }
        }
    }
}