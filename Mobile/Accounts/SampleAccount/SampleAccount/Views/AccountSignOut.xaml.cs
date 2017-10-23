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
	public partial class AccountSignOut : ContentPage
	{
        private static IAccountManagerAPIs accountAPIs;
        AccountItem Account;
        public AccountSignOut (int accountId, string userId)
		{
			InitializeComponent ();
            AccountLabel.Text = userId;

            Account = new AccountItem
            {
                AccountId = accountId,
                UserId = userId
            };

            accountAPIs = DependencyService.Get<IAccountManagerAPIs>();
        }

        async void SignOutClicked(object sender, EventArgs args)
        {
            accountAPIs.AccountDelete(Account);

            AccountSignIn accountSignIn = new AccountSignIn();
            await DisplayAlert("Account Deleted","ID : " +  Account.UserId, "OK");
            await Navigation.PushAsync(accountSignIn);
        }
    }
}